Project description :
.net core api + ms sql

On start of application test data inserted in db in SeedData.Initialize method.

To run the project:
1.docker-compose build
2. docker-compose up

Test api:
1. Get all children nodes of a given node :
GET request:
http://localhost:8000/api/companystructure/{nodeName} // http://localhost:8000/api/companystructure/A

2. Change parent of the node :
PUT request:
http://localhost:8000/api/companystructure/{nodeName} // http://localhost:8000/api/companystructure/F
Headers:
[{"key":"Content-Type","name":"Content-Type","value":"application/json","description":"","type":"text"}]

 {
    "name":"F", // node name 
    "ParentName":"B" // new parent name 
  }