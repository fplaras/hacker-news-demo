# Demo Application using Hacker News API

## Overview
This project is developed in Dotnetcore 5.0. The solution contains in addition to service project a client and an API application.

## Technologies used other than dotnetcore libraries
1. Automapper
2. Entity Framework inMemoryDB

## Implementation Strategy

### Background Service
When a request is made for any endpoint a background service is started and does not stop. This service runs every 30 seconds retrieving new stories and validating if they have been added or not to the inMemoryDB. 

### Retrieving Data
The API endpoints that retrieve a list of items first searches for the item in the inMemoryDB and if it is not found retrives it from the Hacker News API. 

### API Models
There are 3 API models implemented. One is used to map to the json retrieved from Hacker News API. The other 2 models include the model used as the domain object to map from the entities in the inMemoryDB and the other one is used to produce JSON from the API. This offers flexibility for what is stored and retrieved. 

### Unit Test
The current unit test demonstrates testing of some service methods and can be expanded to do more including doing controller layer testing.

### Client
Client is currently developed as a server side app using ASP.Net Core. There are many JS libraries to handle paging, sorting, filtering and searching but I implemented an API based search to provide records that were not original retrieved in the original newstories request.  

## Findings
I am new to the Hacker News API but I found that the updates to a Title in an item do not reflect in the endpoint in the API for updates so I believe the updates endpoint might be cached also. The code originally used that endpoint to simply update those records at a 30second interval but considering it is not reflecting updates as expected currently all records for a story type are updates every 30 seconds. Not what I would like but it works.

### Issues
Concurrency conflicts with db inserts in the retrieval of items while the background service is running. I found some strategies in EF to handle this but need further R&D.
Potentially I might be leading to a different strategy like inMemoryCaching or Redis.

## What's next?
Continue exploring backend strategies, creating different clients using JS libraries and Blazor. 

