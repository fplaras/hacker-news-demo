# Demo Application using Hacker News API

## Development Branch
This branch is used for active development and will change frequently. 

## Getting Started
1. Install Dotnetcore 5.0 https://dotnet.microsoft.com/download
2. For IDE I used Visual Studio 2019 Community Edition but I think VS code might work aslo
3. Start API
4. Verify localhost port numbers for the API and update the url on the client appsettings.json file
5. Start both API and Client projects

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
Client is currently developed as a server side app using ASP.Net Core. There are many JS libraries to handle paging, sorting, filtering and searching of data on the client but instead I implemented an API enpoint to search through records that were not originally retrieved.  

## Findings
I am new to the Hacker News API but I found that the updates to a Title in an item do not reflect in the endpoint that the Hacker News API has for updates so I believe the "updates" endpoint might be also be cached or the frequency of udpates is not realtime. The application code originally used that endpoint to simply update those records at a 30 second interval but considering it is not working as expected  all records for "newstories" are currently updated every 30 seconds. It is working that way for now. 

### Issues
Feel free to report issues and observations for potential improvement or if there is an alternate approach to the task and I will explore it.

## What's next?
Continue exploring backend strategies for caching and performance, creating different clients using JS libraries or Blazor, improve use experience with better exception handling and communication of application performance.

