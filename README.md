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

## Example:
### Linq to Sql
```javascript
db.DocumentInfos.Select(projection.Map()).Where(x =>
                !x.Name.Contains("letin") && 
                x.Date > dt && 
                !x.IsDeleted && 
                x.Price > 5 && 
                x.Pages == 2)
                .OrderBy(x => x.Name)
                .ThenByDescending(x => x.Price)
                .ToList();
```
```
exec sp_executesql N'SELECT 
    [Project1].[Id] AS [Id], 
    [Project1].[Type] AS [Type], 
    [Project1].[Status] AS [Status], 
    [Project1].[CreateDate] AS [CreateDate], 
    [Project1].[Name] AS [Name], 
    [Project1].[IsDeleted] AS [IsDeleted], 
    [Project1].[Price] AS [Price], 
    [Project1].[Pages] AS [Pages]
    FROM ( SELECT 
        [Extent1].[Id] AS [Id], 
        [Extent1].[Type] AS [Type], 
        [Extent1].[Status] AS [Status], 
        [Extent1].[Price] AS [Price], 
        [Extent1].[Pages] AS [Pages], 
        [Extent1].[CreateDate] AS [CreateDate], 
        [Extent1].[IsDeleted] AS [IsDeleted], 
        [Extent2].[Name] AS [Name]
        FROM  [dbo].[DocumentInfoes] AS [Extent1]
        LEFT OUTER JOIN [dbo].[Users] AS [Extent2] ON [Extent1].[Owner_Id] = [Extent2].[Id]
        WHERE ( NOT ([Extent1].[Type] LIKE N''%letin%'')) AND ([Extent1].[CreateDate] > @p__linq__0) AND ([Extent1].[IsDeleted] <> 1) AND ([Extent1].[Price] > cast(5 as decimal(18))) AND (2 = [Extent1].[Pages])
    )  AS [Project1]
    ORDER BY [Project1].[Type] ASC, [Project1].[Price] DESC',N'@p__linq__0 datetime2(7)',@p__linq__0='2018-06-03 15:09:39.3064967'

```
____

### Filter and Sort Service

```javascript

new QueryRequest
            {
                Search = null,
                SortBy = new List<Sort>
                {
                    new Sort
                    {
                        Property = nameof(DocumentInfoViewModel.Name),
                        SortOrder = SortOperators.Ascendant
                    },
                    new Sort
                    {
                        Property = nameof(DocumentInfoViewModel.Price),
                        SortOrder = SortOperators.Descendant
                    }
                },
                Filters = new List<Filter>
                {
                  new Filter
                    {
                        Property =  nameof(DocumentInfoViewModel.Name),
                        Operator = FilterOperators.NotContains,
                        Value = "letin"
                    },
                    new Filter
                    {
                        Property =  nameof(DocumentInfoViewModel.Date),
                        Operator = FilterOperators.Greater,
                        Value = DateTime.UtcNow.AddMonths(-6).ToString()
                    },
                    new Filter
                    {
                        Property = nameof(DocumentInfoViewModel.IsDeleted),
                        Operator = FilterOperators.Equal,
                        Value = "false"
                    },
                    new Filter
                    {
                        Property = nameof(DocumentInfoViewModel.Price),
                        Operator = FilterOperators.Greater,
                        Value = "5"
                    },
                    new Filter
                    {
                        Property = nameof(DocumentInfoViewModel.Pages),
                        Operator = FilterOperators.Equal,
                        Value = "2"
                    }
                }
            };
```
```javascript
 Expression<Func<DocumentInfoViewModel, bool>> searchImplementation = searchService.Filter<DocumentInfoViewModel>(request.Filters);
 var whereQuery = db.DocumentInfos.Select(projection.Map()).AsExpandable().Where(searchImplementation);
 var result = sortService.Sort(whereQuery, request.SortBy).ToList();
```
```
SELECT 
    [Extent1].[Id] AS [Id], 
    [Extent1].[Type] AS [Type], 
    [Extent1].[Status] AS [Status], 
    [Extent1].[CreateDate] AS [CreateDate], 
    [Extent2].[Name] AS [Name], 
    [Extent1].[IsDeleted] AS [IsDeleted], 
    [Extent1].[Price] AS [Price], 
    [Extent1].[Pages] AS [Pages]
    FROM  [dbo].[DocumentInfoes] AS [Extent1]
    LEFT OUTER JOIN [dbo].[Users] AS [Extent2] ON [Extent1].[Owner_Id] = [Extent2].[Id]
    WHERE ( NOT ([Extent1].[Type] LIKE N'%letin%')) AND ([Extent1].[CreateDate] > convert(datetime2, '2018-06-03 15:09:39.0000000', 121)) AND (0 = [Extent1].[IsDeleted]) AND ([Extent1].[Price] > cast(5 as decimal(18))) AND (2 = [Extent1].[Pages])
    ORDER BY [Extent1].[Type] ASC, [Extent1].[Price] DESC
```

