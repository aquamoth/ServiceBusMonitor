# ServiceBusMonitor

This is a simple command line tool to monitor the current queue length of Azure ServiceBus.
Responses conform to the standard expected for monitoring tools such as Nagios.


ServiceBusMonitor is written in C# .Net Core and tested in both Windows and Linux.

# Building
Just open in Visual Studio 2019 and build.
Or open a command line and run `dotnet build ServiceBusMonitor.sln`.

Publish as `win-x86 single file` or `linux-x64 single file` to build a 
self-contained executable that doesn't require .Net Core 3.1 on the target machine.


# Installation

## Linux

### Without dependencies
Download the self-contained linux-x64 binary. 
This binary is large, but does not have external dependencies.

### With .Net Core 3.1 pre-installed

If you want a smaller binary and already have .Net Core 3.1 installed on the target computer, 
you can download the Portable version, which is a lot smaller.

# Usage

Run ServiceBusMonitor.exe without parameters to see the help.

Example syntax to execute on Linux or Windows:

```bash
./ServiceBusMonitor --warning 20 --critical 30 "Endpoint=sb://YOUR-SERVICEBUS-NAMESPACE.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=BASE64-SHARED-ACCESSKEY-COPIED-FROM-AZURE"

ServiceBusMonitor.exe --warning 20 --critical 30 Endpoint=sb://YOUR-SERVICEBUS-NAMESPACE.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=BASE64-SHARED-ACCESSKEY-COPIED-FROM-AZURE
```

## Expected output

Successfully connecting to an Azure Servicebus should return a similar output:

```
OK Servicebus responded in 1193 ms|queues=1,messages=12
```

