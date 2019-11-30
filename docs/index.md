# Documentation for disksrv

disksrv is a suite of tools, to read & write hard/floppy disks on a DOS machine, over a TCP/IP based network.
Its intended use case is to manage storage media for these machines, without the need to purchase additional hardware to read/write these storage mediums.

Please see the [GitHub repository](https://github.com/StereoRocker/disksrv/) for source code and downloads.

## Getting started

This getting started guide will assume you already have a DOS machine, with a packet driver loaded, and a Windows based machine as a client.

1. Copy `disksrv.exe` to your DOS machine, and run it from DOS.
2. Launch `disksrv-client.exe` on your Windows machine.
3. Connect to the DOS machine by hostname or IP address.
4. Use the GUI to read a floppy disk, to validate the connection is working as intended. This may take several minutes on real hardware.

## Download

Please see the releases on [GitHub](https://github.com/StereoRocker/disksrv/releases).

## License

disksrv is licensed under the GNU Public License, version 3. You may read the full license [here](https://github.com/StereoRocker/disksrv/blob/master/LICENSE).