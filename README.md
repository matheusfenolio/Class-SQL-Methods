# SQLMethods Class

This is a class with static functions to make easy the use with databases in C# projects.

### ExecQuery

It will execute your query and return a int with the number of changed rows.

### ExecScalar

It will execute your query and return a object.

### ExecMutiple

It will execute a list of querys with a transaction, if one has problem it will rollback.

### GetField

It will return to you a string with the value of the column. 
Obs: Only the last line of the query, make sure you query return just one line. 

### GetDT

It will return to you a DataTable resultant of the query.

### Advice

Use the String.Format to pass your parameters to the query.

```
String.Format(@"select * from Table where name = '{0}' and age = {1}", Name, Age);
```


## Future

* GetField return a object.
* ExecMultiple return a list of objects.
* Integration with other servers (Oracle, Mysql, Postgre).

## Did you like it?

Do want to make it better? Be comfortable to make changes!
If you want to make a sugestion email-me mtfprado@outlook.com.

## Did it help you?

Won't forget to give me credits, it will help me too.