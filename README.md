## Technical Test for CompuTrabajo
### Prerequisites:

- Docker instaled.
- .Net Core v3.1 instaled.
- SQL Server Management Studio instaled.
- Visual Studio 2019 or higher instaled.


    
## Run Locally
### To start the application in development environment do the following steps:
- Execute the follow docker commands in your terminal.

```bash
  docker pull mcr.microsoft.com/mssql/server:2022-latest
```
```bash
  docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=m4st3rk3yRedarbor" -p 1433:1433 --name SQLServer --hostname SQLServer -d mcr.microsoft.com/mssql/server:2022-latest
```
```bash
  docker ps -a
```
Start the docker container
```bash
  docker start SQLServer
```
- Open SQL Server Management Studio and access to SQL Server with the follow credentials:
    - Servername: localhost
    - Authentication: Sql Server Authentication
    - Login: sa
    - Password: m4st3rk3yRedarbor
- Run the sql script ```dbScripts/InitialdbSQLExpress.sql``` on a query window
- Clone the project

```bash
  git clone https://github.com/Hinovska/TechnicalTest-Computrabajo.git
```
- Open ```Redarbor.sln``` file with Visual Studio.
- Set as startup project to ```WebApp```.
- Run the proyect with IIS Express. 


## API Reference

#### Check API status

```http
  GET /api/v1/healthcheck
```

#### Get a Json Web Token

```http
  POST /api/v1/healthcheck/auth
```
| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `body` | `json` | **Required**. Example: {"userName":"ApiService","password":"g+%5Hxk9M&F@wwC-"}|

#### Get all employees (**Token Header Required**)

```http
  GET /api/v1/redarbor
```

#### Get employees filtered (**Token Header Required**)

```http
  GET /api/v1/redarbor/filter
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `body` | `json` | **Required**. Example: {"Username":"test12","Email":"","StatusId":2} |

#### Get a employee by id (**Token Header Required**)

```http
  GET /api/v1/redarbor/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of employee to fetch |

####  Add a employee (**Token Header Required**)

```http
  POST /api/v1/redarbor
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `body` | `json` | **Required**. Employee to add |

####  Modify a employee (**Token Header Required**)

```http
  PUT /api/v1/redarbor/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of employee to modify |
| `body` | `json` | **Required**. Employee to modify |


####  Delete a employee (**Token Header Required**)

```http
  DEL /api/v1/redarbor/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of employee to delete |


####  Validate credentials of a employee (**Token Header Required**)

```http
  POST /api/v1/redarbor/auth
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `body`      | `json` | **Required**. Example: {"Login":"test11","Password":"test11"} |

If you prefer you can use Postman importing the collection ```Redarbor Collection.postman_collection.json```


## Feedback

If you have any feedback, please reach out to me at andresgomez456@gmail.com


## Authors

- [@hinovska](https://www.github.com/hinovska)


## 🚀 About Me
I'm a full stack developer...


## 🔗 Links
[![linkedin](https://img.shields.io/badge/linkedin-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/andr%C3%A9s-g%C3%B3mez-1563b7130/)

