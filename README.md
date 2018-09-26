# Acrolinx Sidebar .NET SDK

This is a library for integrating the [Acrolinx](http://www.acrolinx.com/) Sidebar into a .NET application.

You can use the [Acrolinx Sidebar .NET SDK NuGet package](https://www.nuget.org/packages/Acrolinx.Sidebar/) to integrate Acrolinx in your .NET application.

## Live Demo

[Acrolinx Sidebar Web Live Demo](https://acrolinx.github.io/acrolinx-sidebar-demo/samples/index.html)

## Examples

[Acrolinx Sidebar .NET Demo](https://github.com/acrolinx/acrolinx-sidebar-demo-dotnet)

## The Acrolinx Sidebar

The Acrolinx Sidebar is designed to show up beside the window where you edit your content.
You use it for checking, reviewing, and correcting your content.
To get an impression what the Sidebar looks like in existing integrations, have a look at
[Get Started With the Sidebar](https://support.acrolinx.com/hc/en-us/articles/205697451-Get-Started-With-the-Sidebar).

## Prerequisites

Please contact [Acrolinx SDK support](https://github.com/acrolinx/acrolinx-coding-guidance/blob/master/topics/sdk-support.md)
for consulting and getting your integration certified.
This sample works with a test license on an internal Acrolinx URL.
This license is only meant for demonstration and developing purposes.
Once you finished your integration, you'll have to get a license for your integration from Acrolinx.
  
Acrolinx offers different other SDKs, and examples for developing integrations.

Before you start developing your own integration, you might benefit from looking into:

* [Getting Started with Custom Integrations](https://support.acrolinx.com/hc/en-us/articles/205687652-Getting-Started-with-Custom-Integrations),
* the [Guidance for the Development of Acrolinx Integrations](https://github.com/acrolinx/acrolinx-coding-guidance),
* the [Acrolinx SDKs](https://github.com/acrolinx?q=sdk), and
* the [Acrolinx Demo Projects](https://github.com/acrolinx?q=demo).

## Getting Started

## Build Locally

1. Make sure that you have installed Microsoft Visual Studio with C# support version 2015 or later.
2. Since the Acrolinx Sidebar performs static code analysis to improve quality, you also have to install [Code Contracts for .NET](https://visualstudiogallery.msdn.microsoft.com/1ec7db13-3363-46c9-851f-1ce455f66970).
3. Open the solution file [`Acrolinx.Sidebar.Net.sln`](Acrolinx.Sidebar.Net.sln) with Visual Studio.
4. Build the solution.

Visual Studio downloads the required dependencies and compiles the Acrolinx .NET Sidebar Demo solution using [NuGet](https://www.nuget.org/).

*Note: The dependency download may fail on the first build. This problem can usually be solved by building a second time.*

## Using the SDK

Have a look at the sample source code provided in the [Acrolinx Sidebar .NET Demo](https://github.com/acrolinx/acrolinx-sidebar-demo-dotnet). Use the [Acrolinx Sidebar .NET SDK NuGet package](https://www.nuget.org/packages/Acrolinx.Sidebar/).


## SDK Features

1. **Document Model** - Provides [lookup](https://github.com/acrolinx/acrolinx-coding-guidance/blob/master/topics/text-lookup.md "Lookup") functionality.
2. **Start page** - Provides interactive way to sign in to Acrolinx with built-in error handling.
3. **Logger** - Provides [logging](https://github.com/acrolinx/sidebar-sdk-dotnet/blob/master/Acrolinx.Sidebar/Util/Logging/Logger.cs) using Log4net.
4. **Acrolinx Storage**: Applications using the IE web browser control may be denied to access LocalStorage.
   The SDK uses its own [storage](https://github.com/acrolinx/sidebar-sdk-dotnet/blob/master/Acrolinx.Sidebar/Storage/RegistryAcrolinxStorage.cs) mechanism using Windows registry.
   **Registry path**: `HKCU\Software\Acrolinx\Plugins\Storage\[KEY]`
   **Fallback path**: `HKLM\Software\Acrolinx\Plugins\Storage\[KEY]`

## References

* The [Sidebar DEMO .NET](https://github.com/acrolinx/acrolinx-sidebar-demo-dotnet) is built based on this SDK.
* The Sidebar SDKs are based on the [Acrolinx Sidebar Interface](https://acrolinx.github.io/sidebar-sdk-js/).

## License

Copyright 2016-present Acrolinx GmbH

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at:

[http://www.apache.org/licenses/LICENSE-2.0](http://www.apache.org/licenses/LICENSE-2.0)

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

For more information visit: [http://www.acrolinx.com](http://www.acrolinx.com)