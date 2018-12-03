using System;
using System.Linq.Expressions;
using DBConsoleNF.DataAccess.Models;
using DBConsoleNF.Projections.Interfaces;

namespace DBConsoleNF.Projections
{
    public class DocumentToViewModelProjectionMapper : IProjectionMapper<DocumentInfo, DocumentInfoViewModel>
    {
        public Expression<Func<DocumentInfo, DocumentInfoViewModel>> Map()
        {
            return x => new DocumentInfoViewModel
            {
                Id = x.Id,
                Name = x.Type,
                DocumentStatus = x.Status,
                Date = x.CreateDate,
                UserName = x.Owner.Name,
                IsDeleted = x.IsDeleted,
                Price = x.Price,
                Pages = x.Pages
            };
        }
    }
}