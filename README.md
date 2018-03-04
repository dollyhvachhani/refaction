Project: refactor-me (Modified by Dolly Vachhani)
_____________________________________________________________________________________________________________________
The following modifications have been made in this project:
•	Implemented a multi-tier architecture and used Generic Repository pattern and Unit of Work along with entity framework.
•	Implementation of Unity Container and Managed Extensibility Framework (MEF) to resolve dependencies.
•	Created custom URLs using Attribute Routing 
•	Implemented basic Authentication
•	Logging and Exception Handling using Action Filters, Exception Filters and nLog.
•	Unit Testing using nUnit.
_____________________________________________________________________________________________________________________
Setup database
SQL Server 2012 has been used as a database server. I have provided the sql scripts to create the database named RefactorDB in Sql Server (~\DBScripts\RefactorDBScript.sql). This database contains three tables - Product, ProductOption and User. 
•	Product – This table stores the details of all the products. (Columns -  Id(Guid, PK), Name, Description, Price, DeliveryPrice)
•	ProductOption – This table stores options for every product. (Columns – Id(Guid, PK), ProductID(Guid, FK), Name, Description)
•	User – This table stores user credentials for Authentication. (Columns – Id, Username, Password, Name)
_____________________________________________________________________________________________________________________
DataAccessLayer:
•	DatabaseModel – In this project WebApiDatabaseModel.edmx is created using database first approach of entity framework, which 	contains database context and database entities, to communicate with data source.
•	GenericRepository – This class serves a template based generic code for all the entities that will interact with the database.
•	UnitOfWork - This class implements IUnitOfWork interface. The main purpose of using this class is to manage transactions. 
	This class also implements IDisposable interface to free up connections and objects.
•	DependencyResolver – This class implements IComponent interface to register types.
_____________________________________________________________________________________________________________________
BusinessAccessLayer 
This layer acts as our business logic layer. It contains business entities (In this case, ProductEntity and ProductOptionsEntity) and Service(s) (ProductService)
•	BusinessEntities: Business Entities are used to communicate between business logic and Web API project. In this project, the 	business entities are -  ProductEntity and ProductOptionsEntity.
•	ProductService – This class implements business logic to communicate with data access layer. This class contains a private variable 	of UnitOfWork and a constructor to initialize that variable, as well as, an AutoMapper to map the db entities to business entities.
•	DependencyResolver - This class implements IComponent interface to register type of UnitOfWork.
_____________________________________________________________________________________________________________________
Refactor-meWebAPI – It contains 
•	ProductsController – Defined endpoints in products controller. They are:
	1. GET /products or GET /products/allproducts - gets all products.
	2. GET /products/productname/{name} - finds all products matching the specified name.
	3. GET /products/productid/{id} - gets the project that matches the specified ID.
	4. POST /products - creates a new product.
	5. PUT /products/{id} - updates a product.
	6. DELETE /products/{id} - deletes a product and its options.
	7. GET /products/{productid}/options - finds all options for a specified product.
	8. GET /products/{productid}/options/{optionId} - finds the specified product option for the specified product.
	9. POST /products/{productid}/options - adds a new product option to the specified product.
	10. PUT /products/{productid}/options/{optionId} - updates the specified product option.
	11. DELETE /products/{productid}/options/{optionId} - deletes the specified product option.
•	ActionFilters contains classes for logging and Exception handling.
•	ErrorHelpers contains custom classes for exceptions.
•	Filters contains Generic Authentication filter, basic Authentication filter and API Authentication filter.
•	Helpers contains NLogger class to log errors. All the errors are logged in APILog directory (~/APILog).
•	Bootstrapper – This class initializes unity container and registers types of refactor-meWebApi, businessAccessLayer and 	DataAccessLayer.
_____________________________________________________________________________________________________________________
Resolver
To design an architecture in which components are independent of each other in terms of object creation and instantiation. Hence, to create a loosely coupled system, I have used MEF(Managed Extensibility Framework) along with Unity Container and reflection.
Resolver is used to resolve dependencies from the solution as the controller was depended on services and the services was depended on data model. 
_____________________________________________________________________________________________________________________
Security – Basic authentication is implemented to make this Web API more secure. This authentication mode will authenticate user by taking user credentials (i.e username and password). 
UserService – This class implements IUserService interface in BusinessAccessLayer. Its Authenticate() method accepts user credentials in the form of parameters and authenticates user.
_____________________________________________________________________________________________________________________
Logging and Exceptional Handling – 
This application uses NLog to log requests and exceptions. There are custom classes mapped with the type of exception to centralize request logging and exception handling in WebAPI.
•	ActionFilter in WebAPI - Action filter is responsible for handling all the incoming requests to our APIs and log them using NLogger 	class. We have “OnActionExecuting” method that is implicitly called if we mark our controllers or global application to use that filter. So, each time any action of any controller will be hit, our “OnActionExecuting” method will execute to log the request.
_____________________________________________________________________________________________________________________
Unit Testing – I have used NUnit and Moq framework to write test cases for business logic layer and controller methods.
