# disksrv - Server

## Requirements

* IBM compatible BIOS
* DOS (only tested in MS-DOS 6.22)
* A packet driver
* A TCP/IP network

## Limitations

The disk reading/writing routines in `disksrv.exe` rely on the BIOS, specifically [INT 13h](https://en.wikipedia.org/wiki/INT_13H). It does not implement the INT 13h extensions, introduced by IBM/Microsoft in 1992.

## Usage

Just run `disksrv.exe` - it has no dependencies to other files.

## Configuration

There is none! Watch this space, modifying the disk buffer size is on the roadmap.

WATTCP is the library used to provide networking support for disksrv. See [this guide](http://wiki.freedos.org/wiki/index.php/Networking_FreeDOS_-_WATTCP) on how to configure WATTCP. If disksrv is unable to read a WATTCP configuration file, it will automatically attempt DHCP/BOOTP auto-configuration.