# SQLMethods Class

It's a class with static functions to make easy the use with databases in C# projects.
This is the version 2. Now you have compatibility with SqlServer, MySql and Oracle.

### References

Please, note of you need to add 3 references to you project.

* Newtonsoft.Json
* MySql.Data
* Oracle.ManagedDataAccess

### Configuration file

```JSON
{
    "Host": "127.0.0.1",
    "Port": "1433", 
    "Instance": "master",
    "User": "sa", 
    "Password": "masterkey ",
    "Timeout": "60", 
    "Type": "sqlServer", 
    "ConnectionStringModel": "",
    "ConnectionString": "" 
}
```

* Host //*{0}
* Port //{1}
* Instance //{2}
* User //{3}
* Password //{4}
* Timeout //{5}
* Type //It can be sqlServer, MySql, Oracle.
* ConnectionStringModel //If you need to put your connection string model: Follow the in front of the fields. e.g. 
    ```
    Data Source={0},{1};Initial Catalog={2};User id={3};Password={4};Connection Timeout={5};
    ```
* ConnectionString //Here you're gonna put your ready connection string if you don't wanna to use the default in the class.

### ExecQuery

It will execute your query and return a int with the number of changed rows.

```C#
SQLMethods.ExecQuery(sql);
```

### ExecQuery

It will execute a list of querys with a transaction, if one has problem it will rollback.

```C#
List<string> sql = new List<string>();

sql.Add("insert into....");
sql.Add("insert into....");
sql.Add("insert into....");

SQLMethods.ExecQuery(sql);
```

### ExecScalar

It will execute your query and return a object.

```C#
SQLMethods.ExecScalar(sql);
```

### GetField

It will return to you a object with the value of the column. 
Obs: Only the last line of the query, make sure you query return just one line. 

```C#
SQLMethods.GetField(sql, "FieldName");
```

### GetDT

It will return to you a DataTable resultant of the query.

```C#
SQLMethods.GetDaTaTable(sql);
```

### Advice

Use the String.Format to pass your parameters to the query.

```C#
String.Format(@"select * from Table where name = '{0}' and age = {1}", Name, Age);
```


## Future

* ExecMultiple return a list of objects.


## Did you like it?

Do you wanna make it better? Be comfortable to make changes!
If you wanna make a sugestion email-me mtfprado@outlook.com.

## Did it help you?

Don't forget to give me credits, it will help me too.