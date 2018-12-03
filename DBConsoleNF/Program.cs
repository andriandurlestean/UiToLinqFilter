using System;
using System.Linq;
using System.Linq.Expressions;
using DBConsoleNF.DataAccess;
using DBConsoleNF.DataAccess.Models;
using DBConsoleNF.Projections;
using DBConsoleNF.Projections.Interfaces;
using DBConsoleNF.Services;
using DBConsoleNF.Services.Interfaces;
using LinqKit;

namespace DBConsoleNF
{
    class Program
    {
        static void Main(string[] args)
        {
            var helper = new Helper();
            var db = new MyContext();
            var request = helper.GenerateQueryRequest();
            ISearchService searchService = new SearchService();
            ISortService sortService = new SortService();
            IProjectionMapper<DocumentInfo, DocumentInfoViewModel> projection = new DocumentViewModelToDocumentEntity();
            var predicateBuilder = PredicateBuilder.New<DocumentInfoViewModel>();

            var dt = DateTime.UtcNow.AddMonths(-6);
            Expression<Func<DocumentInfoViewModel, bool>> linqImplementation = predicateBuilder.And(x =>
                !x.Name.Contains("letin") && 
                x.Date > dt && 
                !x.IsDeleted && 
                x.Price > 5 && 
                x.Pages == 2);

            Expression<Func<DocumentInfoViewModel, bool>> searchImplementation = searchService.Filter<DocumentInfoViewModel>(request.Filters);

            var oldWhereImp = db.DocumentInfos.Select(projection.Map()).AsExpandable().Where(linqImplementation);
            var oldSortImp = oldWhereImp.OrderBy(x => x.Name).ThenByDescending(x => x.Price);
            var oldImp = oldSortImp.ToList();

            var newWhereImp = db.DocumentInfos.Select(projection.Map()).AsExpandable().Where(searchImplementation);
            var newSortImp = sortService.Sort(newWhereImp, request.SortBy);
            var newImp = newSortImp.ToList();

            Console.ReadLine();
        }
    }
}

