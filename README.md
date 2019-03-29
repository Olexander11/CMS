# CMS
ASP.Net Core 2.0 Web API REST service  -  customer message service, which provides to manage feeds collections

Solution contains three projects: Web API REST service, Unit test, Console app for testing WebAPI

Web API REST service provides next functions:

-  GET api/Collection  Get all  collections;
-  GET api/Collection/id  Get all news for a collection{id};
-  POST api/Collection  Create a new collection (returns collection Id);
-  PUT api/Collection/5  Add feed to a collection

Used to store information MS SQL server or local DB, configure  in appsettings.json

To logger used Serilog.Extensions.Logging.File (1.1.0)

CMS support only RSS and Atom feeds, but can be extensible to support adding new types of feed sources
