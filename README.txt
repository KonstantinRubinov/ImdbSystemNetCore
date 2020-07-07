This is your favorite movie database.
Connected to imdb library, and sql database to save your favorite movies.

* .NET Core 3 server
* MsSql/MySql/MongoDb database
* Angular 9 front.
* Inter-server communication (this server communiacts with imdb api).
* Token bearer authentication.

Works with 6 types of queries!!!
- Entity Framework for MsSql
- Query for MsSql
- Query for MsSql stored procedure
- Query for MySql
- Query for MySql stored procedure
- Query for MongoDb

the options are in 002-BusinessLogicLayer/GloablData/GlobalVariable

Some bugs!
* First name starts with capital letter - front dosn't alert this
* Last name starts with capital letter - front dosn't alert this