# UiToLinqFilter

Its an example how I tried to solve popular issue having UI filter for Table and you want to pass it to DB, but each new column adds new complexity and "if", using on runtime Linq Expression to build Left and Right parameters and mapping with AND by LinqKit.

## Start point of example is **Program.cs** and don't forget in **App.config** to change **connectionStrings** and run **Update-Database**

Example we have List of Filter and Sort

```javascript
public class Filter
{
    public string Property { get; set; }

    public string Operator { get; set; }

    public string Value { get; set; }
}
```
```javascript
public class Sort
{
    public string Property { get; set; }

    public string SortOrder { get; set; }
}
```
## How it works:
Main functionality are in **SearchService** and **SortService**
1. Code is mapping Entity **DocumentInfo** to **DocumentInfoViewModel** using mapping projection **DocumentViewModelToDocumentEntity**  and passing to Select
2. From List of **Filter** it generates **Expression<Func<DocumentInfoViewModel, bool>>** that now can only parse string, int, decimal, DateTime and boolean.
3. From List of **Sort** it maps **IQuerable** with OrderBy/OrderByDescending/ThenBy/ThenByDescending

## Features Included:
* Cases for "AND" only
* String operators "==, !=, Contains, !Contains"
* Boolean operators "==, !="
* DateTime operators "==, !=, <=, <, >, >="
* Decimal operators "==, !=, <=, <, >, >="
* Integer operators "==, !=, <=, <, >, >="

* Sorting for OrderBy/OrderByDescending/ThenBy/ThenByDescending

It generates same query or even better like Linq To SQL (EF).
____
Its generic works for any Type, easy to extend. In future if will be popular or demand to make in nuget with additional features.
