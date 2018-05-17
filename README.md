# Trouble Ticket System


### How to develop (not needed once the client becomes a website - see prof's example)

* When updating the service (TTService project), open a terminal inside `TTSystem` and run `svcutil.exe /language:cs /out:generatedProxy.cs /config:app.config http://localhost:8000/TTSystem`

**Note:** Usually, `svcutil.exe` is located inside `C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6.1 Tools\`

**Note:** The service needs to be running in order for this to work.

### How to run

* Open `TTSystem\TTSystem.sln` in Visual Studio and build it.
* Open a terminal **as admin** inside `TTSystem\TTServiceHost\bin\Debug` and run `TTServHost.exe`
* Open a terminal **as admin** inside `TTSystem\TTSolver\bin\Debug` and run `TTSolver.exe`

**Note:** It's important that the terminals are run as admin, otherwise it won't work.