To run the project:

In debug mode:

Open project in Visual Studio as Admin

Open multiple project startup with all the wanted projects, except Host and Service, and the services run automatically in debug mode.

In deploy mode:

Move App.config and Database.sql from TTService to TTServiceHost

Remove commented lines in TTServiceHost with host.open() and host2.open()

Run Binaries