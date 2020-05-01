# ServiceBusMonitor

This is a simple command line tool to monitor the current queue length of Azure ServiceBus.
Responses conform to the standard expected for monitoring tools such as Nagios.


ServiceBusMonitor is written in C# .Net Core and tested in both Windows and Linux.

# Compiling

# Installation

## Linux
Download the self-contained linux-x64 binary. This binary is large, but does not have external dependencies.

If you want a smaller binary and already have .Net Core 3.1 installed on the target computer, 
you can instead download the Portable version, which is a lot smaller.


#Usage

# Linux

./ServiceBusMonitor "Endpoint=sb://YOUR-SERVICEBUS-NAMESPACE.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=BASE64-SHARED-ACCESSKEY-COPIED-FROM-AZURE"

