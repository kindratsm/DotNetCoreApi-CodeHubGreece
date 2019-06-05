# .NET Core API Code.Hub Greece Training Project

### Usage
1. Download and unzip [release](https://github.com/kindratsm/DotNetCoreApi-CodeHubGreece/releases) for your platform
2. Start executable file `DotNetCoreApi-CodeHubGreece`
3. API will be now available on following URL: [http://localhost:5000](http://localhost:5000)

### API
API based on [OData](https://www.odata.org) protocol ([documentation](http://docs.oasis-open.org/odata/odata/v4.01/odata-v4.01-part1-protocol.html))\
Each data model is exposed via OData and can be accessed by following route: `/odata/[DataModel]`
### Supported HTTP methods:
#### Get
Get existing objects
```
GET http://localhost:5000/odata/[DataModel]
```
Get existing object by ID
```
GET http://localhost:5000/odata/[DataModel]/[ID]
```
#### Post
Insert object
```
POST http://localhost:5000/odata/[DataModel]

Request body:
{
    Id: 0 | null
}
```
Update object
```
POST http://localhost:5000/odata/[DataModel]

Request body:
{
    Id: ID > 0
}
```
#### Delete
Delete object
```
DELETE http://localhost:5000/odata/[DataModel]/[ID]
```
### Examples:
Get all countries:
```
GET http://localhost:5000/odata/Country
```
Get country with ID `87`:
```
GET http://localhost:5000/odata/Country/87
```
Get country with Code `GR`:
```
GET http://localhost:5000/odata/Country?$filter=Code eq 'GR'
```
Create new country object
```
POST http://localhost:5000/odata/Country

Request body:
{
    Id: null,
    Code: 'NC',
    Name: 'New Country'
}
```
Update country object
```
POST http://localhost:5000/odata/Country

Request body:
{
    Id: 87,
    Code: 'GR',
    Name: 'Greece (default)'
}
```
Delete country with ID `87`
```
DELETE http://localhost:5000/odata/Country/87
```
### Data Models
```js
class Country {
    Id,
    Code,
    Name
}

class User {
    Id,
    FirstName,
    LastName,
    Email
}

class Product {
    Id,
    Name
}

class Customer {
    Id,
    Name,
    Description,
    CountryId,
    Country
}

class Version {
    Id,
    ReleaseDate,
    ProductId,
    Product,
    VersionNumber,
    Description
}

class VersionNoteType {
    Id,
    Name
}

class VersionNote {
    Id,
    VersionId,
    Version,
    UserId,
    User,
    VersionNoteTypeId,
    VersionNoteType,
    Description
}

class Installation {
    Id,
    CustomerId,
    Customer,
    VersionId,
    Version,
    RefDate
}

class InstallationNote {
    Id,
    InstallationId,
    Installation,
    UserId,
    User,
    Name,
    Description
}
```
