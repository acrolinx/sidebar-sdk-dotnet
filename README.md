# Acrolinx Sidebar .NET SDK

You can use the Acrolinx sidebar .NET SDK [NuGet package](https://www.nuget.org/packages/Acrolinx.Sidebar/) to integrate Acrolinx in your .NET application. You can take deeper look into the SDK code in this repository.

## Prerequisites

Please contact Acrolinx SDK support (sdk-support@acrolinx.com) for initial consulting. 
We like to schedule a kickoff meeting to answer any questions about your integration project. 
After the meeting, we provide you with test server credentials and configuration settings you would need to get started.

## How to Start

Make sure that you have installed
* Microsoft Visual Studio with C# support version 2015 or later.
* Node [Node.js](https://nodejs.org/en/download/)

Since the Acrolinx Sidebar performs static code analysis to improve quality, you also have to install Code Contracts for .NET:

https://visualstudiogallery.msdn.microsoft.com/1ec7db13-3363-46c9-851f-1ce455f66970

Open the solution file `Acrolinx.Sidebar.Net.sln` with Visual Studio.

Run the solution.
 
Visual Studio downloads the required dependencies and compiles the Acrolinx .NET Sidebar solution using [NuGet] (https://www.nuget.org/).
(Note: The dependency download may fail on the first build. This problem can usually be solved by building a second time).

## How to Use the Sidebar in Your Integration

Refer to [sidebar demo] (https://github.com/acrolinx/acrolinx-sidebar-demo-dotnet/blob/master/README.md)

## Server Dependency

To use the Acrolinx Sidebar, you need to connect to an Acrolinx server. If you've already received your Acrolinx server address, you’re good to go. If your company has installed an Acrolinx server, but you don't have an address yet, ask your server administrator first.

## License

Copyright 2016-2017 Acrolinx GmbH

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

For more information visit: http://www.acrolinx.com
