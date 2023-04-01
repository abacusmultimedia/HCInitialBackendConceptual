using DAO.PlanningPortal.Application.Interfaces.Finance;
using zero.Shared.Models.Dashboard;
using zero.Shared.Models.Finance;
using zero.Shared.Models.GDPRAccess;
using zero.Shared.Repositories;
using zero.Shared.Response;
using DAO.PlanningPortal.Domain.Entities.Finance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace DAO.PlanningPortal.Application.Services.Finance
{
    public class LedgerService : ILedgerService
    {
        private readonly IGenericRepositoryAsync<Ledger> _dataAccessRepositoryAsync;
        private readonly IGenericRepositoryAsync<LedgerGroup> _ledgerGroupRepository;

        public LedgerService(IGenericRepositoryAsync<Ledger> dataAccessRepositoryAsync,
            IGenericRepositoryAsync<LedgerGroup> ledgerGroupRepository
        )
        {
            _dataAccessRepositoryAsync = dataAccessRepositoryAsync;
            _ledgerGroupRepository = ledgerGroupRepository;
        }

        public async Task<Result<List<WidgetDto>>> GetWidgets()
        {
            var response = new List<WidgetDto>();
            var qry = _dataAccessRepositoryAsync.GetAllQueryable()
                 .Select(x => new { x.LedgerGroup.Title }).ToList();

            var totalCount = qry.Count();


            var transactionWidgets = qry.GroupBy(x => x.Title).ToList();
            foreach (var item in transactionWidgets)
            {
                var c = item.Key;
                var k = item.Count();
                var temp = new WidgetDto
                {
                    Description = "Accounts ",
                    Size = "",
                    StylingClass = "",
                    Title = item.Key,
                    Type = "",
                    Value = (item.Count() * 100 / totalCount).ToString(),
                };
                response.Add(temp);
            }
            return Result<List<WidgetDto>>.Success(response);
        }

        public async Task<Result<long>> AddEdit(AddLedgerDTO parameter)
        {
            var venderID = _ledgerGroupRepository.GetAllQueryable().FirstOrDefault(x => x.Title.ToUpper() == "CUSTOMERS");
            var entity = new Ledger
            {
                IsDeleted = false,
                ParentId = venderID.Id,
                Title = parameter.Title,
                Description = parameter.Description,


            };
            var resp = await _dataAccessRepositoryAsync.AddAsync(entity);
            return Result<long>.Success(resp.Id);

        }

        public Task<Result<bool>> Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<AddLedgerDTO>>> GetList()
        {
            var resp = _dataAccessRepositoryAsync.GetAllQueryable().Include(e => e.LedgerGroup)
                //.Where(x => x.LedgerGroup.Title.ToUpper() == "CUSTOMERS")
                .Select(x => new AddLedgerDTO
                {
                    Title = x.Title,
                    Description = x.Description,
                    ParentName = x.LedgerGroup.Title,
                }).ToList();
            return Result<List<AddLedgerDTO>>.Success(resp);
        }

        public async Task<Result<List<LookupDto>>> GetLookups(string name)
        {
            var resp = _dataAccessRepositoryAsync.GetAllQueryable()
                .Where(x => (x.LedgerGroup.Title.Trim().ToUpper() == name.Trim().ToUpper() || name == ""))
                 .Select(x => new LookupDto
                 {
                     Key = x.Id.ToString(),
                     Title = x.Title,

                 }).ToList();
            return Result<List<LookupDto>>.Success(resp);
        }

    }
}
