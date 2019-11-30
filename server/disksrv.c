/* This file is part of disksrv.
 * 
 * Disksrv is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Disksrv is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with disksrv.  If not, see <https://www.gnu.org/licenses/>.
 */

#include <stdio.h>
#include <stdlib.h>
#include <tcp.h>
#include <bios.h>

// Global definitions/variables
#define SECSIZE 512
#define DEFAULT_SECBUF  32
unsigned char SECBUF = DEFAULT_SECBUF;
unsigned int  BUFSIZE;

#define LISTEN_PORT 6000

#define MAGIC_1 0x64
#define MAGIC_2 0x73
#define VER_MAJ 1
#define VER_MIN 0

typedef struct {
	unsigned char sectors;
	unsigned char heads;
	unsigned int cylinders;
} diskstats;

unsigned char getdiskcount()
{
	// Variable declaration
	diskstats retval;
	union REGS regs;
	
	// Set up registers to call BIOS for drive parameters
	regs.h.ah = 8;			// GET DRIVE PARAMETERS
	regs.h.dl = 0x80;		// 1st hard disk
	
	// Call the BIOS
	int86(0x13, &regs, &regs);
	
	// Return number of drives
	return regs.h.dl;
}

unsigned char getfloppycount()
{
	// Variable declaration
	diskstats retval;
	union REGS regs;
	
	// Set up registers to call BIOS for drive parameters
	regs.h.ah = 8;			// GET DRIVE PARAMETERS
	regs.h.dl = 0x00;		// 1st floppy disk
	
	// Call the BIOS
	int86(0x13, &regs, &regs);
	
	// Return number of drives
	return regs.h.dl;
}

// diskno is expected to be the disk number you would request from the BIOS
// 0x00 - 1st floppy disk drive
// 0x80 - 1st hard disk drive
void getdiskstats(int diskno, diskstats* ptr)
{
	// Variable declaration
	diskstats retval;
	union REGS regs;
	
	// Set up registers to call BIOS for drive parameters
	regs.h.ah = 8;			// GET DRIVE PARAMETERS
	regs.h.dl = diskno;		// Disk number
	
	// Call the BIOS
	int86(0x13, &regs, &regs);
	
	// Calculate and return drive stats
	// drives = regs.h.dl;
	ptr->cylinders = regs.h.ch + ((regs.h.cl & 0xC0) << 2) + 1;
	ptr->sectors = regs.h.cl & 0x3F;
	ptr->heads = regs.h.dh + 1;
}

#define sendfail() \
	packetbuf[0] = 0; /* Status byte */ \
	packetbuf[1] = 0; /* Data length - high byte */ \
	packetbuf[2] = 0; /* Data length - low byte */ \
	sock_write(s, packetbuf, 3);
	
#define sendsuccess(datalen) \
	packetbuf[0] = 1; /* Status byte */ \
	packetbuf[1] = (datalen >> 8) & 0xFF; /* Data length - high byte */ \
	packetbuf[2] = (datalen) & 0xFF; /* Data length - low byte */ \
	sock_write(s, packetbuf, 3);
	

void handle(tcp_Socket* s)
{
	// Variables
	unsigned char* diskbuf;
	unsigned char packetbuf[4];
	unsigned char* databuf;
	unsigned char* respbuf;
	
	int status;
	int readbytes;
	
	unsigned int datalen;
	unsigned char request;
	unsigned char running;
	unsigned char seccount;
	
	unsigned char result;
	unsigned char driveno;
	
	diskstats* ds;
	
	// Allocate the disk buffer
	diskbuf = (unsigned char*)malloc(BUFSIZE);
	if (diskbuf == 0)
	{
		printf("Failed to allocate disk buffer\n");
		return;
	}
	
	// Allocate diskstats
	ds = (diskstats*)malloc(sizeof(diskstats));
	
	// Send the initial connection packet
	// Magic number, followed by the major and minor version of the program
	packetbuf[0] = MAGIC_1;
	packetbuf[1] = MAGIC_2;
	packetbuf[2] = VER_MAJ;
	packetbuf[3] = VER_MIN;
	sock_write(s, packetbuf, 4);
	
	// Start handling anything inbound
	running = 1;
	while (running)
	{
		// Wait for a request to be received
		sock_wait_input(s, 0, NULL, &status);
		
		// Read the request
		readbytes = sock_read(s, packetbuf, 3);
		
		if (readbytes != 3)
		{
			printf("Error reading request - read %i bytes\nClosing connection\n\n", readbytes);
			sock_close(s);
			running = 0;
			break;
		}
		
		// Interpret the request
		request = packetbuf[0];
		datalen = (packetbuf[1] << 8) + packetbuf[2];
		
		// Allocate a buffer for the data, and read it, as long as the data is not > 512 bytes.
		// If the data is > 512 bytes, we will allow the switch cases to read the data, as
		// this is likely going to be read into the disk buffer instead.
		if ((datalen != 0) && (datalen < SECSIZE))
		{
			databuf = (unsigned char*)malloc(datalen);
			if (databuf == 0)
			{
				printf("Error reading request - could not allocate the data buffer. datalen: %u\nClosing connection\n\n", datalen);
				sock_close(s);
				running = 0;
				break;
			}
			
			sock_read(s, databuf, datalen);
		}
		
		// Handle the request
		switch (request)
		{
			case 0:		// REQ 0 - QUIT
				sock_close(s);
				running = 0;
				break;
				
			case 1:		// REQ 1 - GET DISK COUNT
				// Send success, with 2 bytes of data
				sendsuccess(2);
				
				// Send 2 bytes of data
				packetbuf[0] = getfloppycount();	// Floppy count
				packetbuf[1] = getdiskcount();		// HDD count
				sock_write(s, packetbuf, 2);
				break;
				
			case 2:		// REQ 2 - GET HARD DISK INFO
				// Confirm we were given a drive number
				if (datalen != 1)
				{
					printf("Invalid data length in request GET HARD DISK INFO\n");
					sendfail();
					break;
				}
				
				// Confirm the drive exists
				driveno = databuf[0];
				if (driveno >= getdiskcount())
				{
					printf("Invalid drive number in request GET HARD DISK INFO\n");
					sendfail();
					break;
				}
				
				// Get the information about the drive
				driveno += 0x80;			// BIOS numbers hard drives from 0x80
				getdiskstats(driveno, ds);	// Get out drive stats
				
				// Place the info into a new buffer
				respbuf = (unsigned char*)malloc(4);
				respbuf[0] = ds->sectors;
				respbuf[1] = ds->heads;
				respbuf[2] = (ds->cylinders >> 8) & 0xFF;
				respbuf[3] = (ds->cylinders)      & 0xFF;
				
				// Send the info
				sendsuccess(4);
				sock_write(s, respbuf, 4);
				
				// Free the response buffer
				free(respbuf);
				
				break;
				
			case 3:		// REQ 3 - READ DISK SECTOR
				// Confirm we have 5 bytes of data (driveno + CHS)
				if (datalen != 5)
				{
					sendfail();
					break;
				}
				
				// Get the drive number
				driveno = databuf[0];
				
				// Interpret CHS
				ds->sectors = databuf[1];
				ds->heads = databuf[2];
				ds->cylinders = (databuf[3] << 8) + databuf[4];
				
				// Read the data
				result = biosdisk(2, driveno, ds->heads, ds->cylinders, ds->sectors, 1, diskbuf);
				
				// If the BIOS returned a failure, send fail
				if (result)
				{
					printf("Failed READ DISK SECTOR, error %x\n", result);
					sendfail();
					break;
				}
				
				// Send success response and sector data
				sendsuccess(SECSIZE);
				sock_write(s, diskbuf, SECSIZE);
				break;
				
			case 4: 	// REQ 4 - WRITE DISK SECTOR
				// Confirm we have 517 bytes of data (driveno, CHS + 512b/1 sector)
				if (datalen != 517)
				{
					sendfail();
					break;
				}
				
				// Read the first 5 bytes of data
				databuf = (unsigned char*)malloc(5);
				sock_read(s, databuf, 5);
				
				// Get the drive number
				driveno = databuf[0];
				
				// Interpret CHS
				ds->sectors = databuf[1];
				ds->heads = databuf[2];
				ds->cylinders = (databuf[3] << 8) + databuf[4];
				
				// Read the sector into the disk buffer
				sock_read(s, diskbuf, SECSIZE);
				
				// Write the data
				result = biosdisk(3, driveno, ds->heads, ds->cylinders, ds->sectors, 1, diskbuf);
				
				// If the BIOS returned a failure, send fail
				if (result)
				{
					printf("Failed WRITE DISK SECTOR, error %x\n", result);
					sendfail();
					break;
				}
				
				// Send success response
				sendsuccess(0);
				break;
			
			case 5:		// REQ 5 - GET MAX DISK BUFFER SIZE
				sendsuccess(2);
				packetbuf[0] = (BUFSIZE >> 8) & 0xFF;
				packetbuf[1] = (BUFSIZE)      & 0xFF;
				sock_write(s, packetbuf, 2);
				break;
				
			case 6:		// REQ 6 - READ MULTIPLE DISK SECTORS
				// Validate the size of the data
				if (datalen != 6)
				{
					sendfail();
					break;
				}
				
				// Interpret request
				driveno = databuf[0];
				ds->sectors = databuf[1];
				ds->heads = databuf[2];
				ds->cylinders = (databuf[3] << 8) + databuf[4];
				seccount = databuf[5];
				
				// Validate seccount
				if ((seccount > SECBUF) || (seccount * SECSIZE > BUFSIZE))
				{
					sendfail();
					break;
				}
				
				// Read the sector into the disk buffer
				result = biosdisk(2, driveno, ds->heads, ds->cylinders, ds->sectors, seccount, diskbuf);
				
				// Validate success
				if (result)
				{
					printf("Failed READ MULTIPLE DISK SECTORS with BIOS code %x\n", result);
					printf("C: %i\tH:%i\tS:%i\nseccount: %i\n", ds->cylinders, ds->heads, ds->sectors, seccount);
					sendfail();
					break;
				}
				
				// Send success
				sendsuccess(seccount*SECSIZE);
				
				// Send the disk buffer
				sock_write(s, diskbuf, seccount*SECSIZE);
				break;
				
			case 7:		// REQ 7 - WRITE MULTIPLE DISK SECTORS
				// Validate the size of the data
				if ((datalen % SECSIZE != 6) || (datalen < SECSIZE + 6))
				{
					sendfail();
					break;
				}
				
				// Allocate 6 bytes for databuf, and read the first 6 bytes of data from the socket
				databuf = (unsigned char*)malloc(6);
				sock_read(s, databuf, 6);
				
				// Interpret request
				driveno = databuf[0];
				ds->sectors = databuf[1];
				ds->heads = databuf[2];
				ds->cylinders = (databuf[3] << 8) + databuf[4];
				seccount = databuf[5];
				
				// Validate seccount
				if ((seccount > SECBUF) || (seccount * SECSIZE > BUFSIZE) || (seccount < 1))
				{
					sendfail();
					break;
				}
				
				// Read the contents to write into the disk buffer
				readbytes = sock_read(s, diskbuf, seccount * SECSIZE);
				
				
				if (readbytes != seccount * SECSIZE)
				{
					printf("Failed WRITE MULTIPLE DISK SECTORS - read less bytes than expected at sock_read\n");
					printf("Exp: %i\tGot: %i\n", seccount * SECSIZE, result);
					printf("C: %i\tH:%i\tS:%i\nseccount: %i\n", ds->cylinders, ds->heads, ds->sectors, seccount);
					sendfail();
					break;
				}
				
				// Write the buffer to the disk
				result = biosdisk(3, driveno, ds->heads, ds->cylinders, ds->sectors, seccount, diskbuf);
				
				// Validate success
				if (result)
				{
					// Inform the console
					printf("Failed WRITE MULTIPLE DISK SECTORS\nC: %u\tS: %u\tH: %u\nseccount: %u",
						ds->cylinders, ds->sectors, ds->heads, seccount);
						
					// Send failure
					sendfail();
					break;
				}
				
				// Send success, with no bytes
				sendsuccess(0);
				break;
				
			default:
				printf("Invalid request sent\n");
				sendfail();
				break;
		}
		
		if (datalen != 0)
		{
			free(databuf);
		}
	}
quit:
	free(diskbuf);
	free(ds);
	return;
sock_err:
	printf("Something went wrong while handling an inbound connection\n\n");
	goto quit;
}

int main(void)
{
	// Variables
	tcp_Socket s;
	BUFSIZE = (SECSIZE * SECBUF);
	
	// Print copyright notice and description
	printf("disksrv   Copyright (c) 2019 Dominic Houghton\n");
	printf("This program comes with ABSOLUTELY NO WARRANTY; for details please see\n");
	printf("https://github.com/StereoRocker/disksrv/ and read \"LICENSE\"\n");
	printf("This is free software, and you are welcome to redistribute it\n");
	printf("under certain conditions; see the license for details.\n\n");
	
	
	// Show drive counts
	printf("FDDs: %i\tHDDs: %i\n", getfloppycount(), getdiskcount());
	
	// Initialize WATTCP
	sock_init();
	printf("WATTCP initialized\n");
	
	// Busy wait for an inbound connection
	while (1)
	{
		printf("Waiting for a connection\n");
		
		// Wait for a network connection
		tcp_listen(&s, LISTEN_PORT, 0, 0, NULL, 0);
		sock_mode(&s, TCP_MODE_BINARY);
		sock_wait_established(&s, 0, NULL, NULL);
		
		printf("Connected\n");
		handle(&s);
	}
	
	// This is actually unreachable. It's intended for the user to Ctrl+C.
	return 0;
	
	sock_err:
	printf("Something went wrong while listening\n");
	
	return 1;
}