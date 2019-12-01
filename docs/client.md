# disksrv - Client

## Requirements

* A .Net 4.0 environment, capable of running WinForms based programs. This should include recent installations of Mono.
* A TCP/IP network
* Network access to the disksrv host.

## Limitations

* The client currently only supports connecting to one server at a time.
* The client will fail to read/write images where the physical disk has bad sectors.

## Usage

Extract the `client` directory from the disksrv release, somewhere on your filesystem. Run the `disksrv-client.exe` file, and the GUI will launch.

First you must connect to your disksrv host, you can specify either an IP address or a hostname.

## Portability

`disksrv.client.exe` relies on only 1 file, `settings.xml`. If this file is not found, it will be created automatically upon launch, in the current working directory. (If launched from explorer, or a default shortcut made with explorer, the working directory will be the same directory the file is located in.)