# What is disksrv?

disksrv is a suite of tools, to read & write hard/floppy disks on a DOS machine, over a TCP/IP based network.
Its intended use case is to manage storage media for these machines, without the need to purchase additional hardware to read/write these storage mediums. The `disksrv.exe` program is ~40KiB in size, at the time of writing, and is a good candidate for a rescue boot disk.

**Warning:** disksrv does not contain any form of authentication or connection security, and should be run over a Local Area Network only. Disk images are transferred over the network unencrypted - so can be read by a packet sniffer during transfer. If the server were to be exposed on a public IP address, anyone who connects will be able to read/write data to your drives.

## Getting started

This getting started guide will assume you already have a DOS machine, with a packet driver loaded, and a Windows based machine as a client.

1. Copy `disksrv.exe` to your DOS machine, and run it from DOS.
2. Launch `disksrv-client.exe` on your Windows machine.
3. Connect to the DOS machine by hostname or IP address.
4. Use the GUI to read a floppy disk, to validate the connection is working as intended. Please note, you must select the floppy disk geometry yourself, and it will fail if the disk has bad sectors. This may take several minutes on real hardware.

## Documentation

Full documentation can be found on [GitHub Pages](https://stereorocker.github.io/disksrv/).