# DbJson

This is a demo project I have created while learning C#.

The project is splitted in two parts :
 * **ArticleDbService**: this Windows Service serves news articles over HTTP using hte Json format. News articles are retrieved by this service from a MySQL/MariaDb database.
 * **ArticleViewer**: this is the client application to display the news articles obtained from ArticleDbService

## Requirements

 * The MySQL Connector need to be installed to build the solution (http://dev.mysql.com/downloads/connector/net/).
 * You need to have a MySQL/MariaDb server running, populated with the *sample_database.sql* script and create the *csharp* user with password *testC#* and SELECT permission on the *jdsystem_pciflux* database.

## Usage

 * Build the solution using Visual Studio
 * Open Developer Command Prompt for Visual Studio in Administrator mode
 * Navigate to the folder containg the EXE for ArticleDbService
 * Run `installutil ArticleDbService.exe`
 * From Visual Studio or Windows Explorer, launch the ArticleViewer application
