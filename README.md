Habit Logger
------------

My first time using ADO.NET to supplement for C# and SQLITE combination. 
I implemented the DRY principle in my Console based CRUD application.

Given Requirements
-----------------

- [x] When the application starts, it should create a sqlite database, if one isn’t present.
- [x] It should also create a table in the database, where the hours will be logged.
- [ ] You need to be able to insert, delete, update and view your logged hours.
- [ ] You should handle all possible errors so that the application never crashes
- [x] The application should only be terminated when the user inserts 0.
- [x] You can only interact with the database using raw SQL. You can’t use mappers such as Entity Framework
- [ ] Reporting Capabilities


Features
------------
SQLite database connection

The program uses a SQLite db connection to store and read information.
If no database exists, or the correct table does not exist they will be created on program start.
A console based UI where users can navigate by key presses

image
CRUD DB functions

From the main menu users can Create, Read, Update or Delete entries for whichever date they want, entered in mm-DD-yyyy format. Duplicate days will not be inputted.
Time and Dates inputted are checked to make sure they are in the correct and realistic format.

Learning
-----------
1. ADO.NET uses parameterized queries to execute SQL commands
   > This is a first line of defense against SQL injection attacks
   > User input is treated as value instead of a part of the SQL command
   > Prevent malicious users from injecting rogue SQL code to my query
2. DRY - Don't Repeat Yourself
   > Having a separate method to solve a specific task for simplification and repeatability.
3. Validation
   > Make sure user input fits the guidelines of what we want and prevent the program from crashing 

