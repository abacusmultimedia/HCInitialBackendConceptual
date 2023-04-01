using DAO.PlanningPortal.Application.Interfaces.Finance;
using zero.Shared.Models.Finance;
using zero.Shared.Models.GDPRAccess;
using zero.Shared.Repositories;
using zero.Shared.Response;
using DAO.PlanningPortal.Domain.Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Application.Services.Finance
{
    public class VenderService : IVenderService
    {
        private readonly IGenericRepositoryAsync<Ledger> _dataAccessRepositoryAsync;
        private readonly IGenericRepositoryAsync<LedgerGroup> _ledgerGroupRepository;

        public VenderService(IGenericRepositoryAsync<Ledger> dataAccessRepositoryAsync,
            IGenericRepositoryAsync<LedgerGroup> ledgerGroupRepository
        )
        {
            _dataAccessRepositoryAsync = dataAccessRepositoryAsync;
            _ledgerGroupRepository = ledgerGroupRepository;
        }


        public async Task<Result<long>> AddEdit(VenderDTO parameter)
        {
            var venderID = _ledgerGroupRepository.GetAllQueryable().FirstOrDefault(x => x.Title.ToUpper() == "VENDERS");
            var entity = new Ledger
            {
                IsDeleted = false,
                ParentId = venderID.Id,
                Description = parameter.Description,
                Title = parameter.LastName + ", " + parameter.FirstName,
                PersonalInfo = new PersonalInfo
                {
                    Email = parameter.Email,
                    Address = parameter.Address,
                    Phone = parameter.Phone,
                    LatName = parameter.LastName,
                    FirstName = parameter.FirstName,
                    Cell = parameter.Cell,
                }


            };
            var resp = await _dataAccessRepositoryAsync.AddAsync(entity);
            return Result<long>.Success(resp.Id);

        }

        public Task<Result<bool>> Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<VenderDTO>>> GetList()
        {
            var resp = _dataAccessRepositoryAsync.GetAllQueryable()
                 .Where(x => x.LedgerGroup.Title.ToUpper() == "VENDERS")
                .Select(x => new VenderDTO
                {
                    Address = x.PersonalInfo.Address,
                    Title = x.Title,
                    Cell = x.PersonalInfo.Cell,
                    Phone = x.PersonalInfo.Phone,
                    FirstName = x.PersonalInfo.FirstName,
                    LastName = x.PersonalInfo.LatName,

                }).ToList();
            return Result<List<VenderDTO>>.Success(resp);

        }

        public Task<Result<List<LookupDto>>> GetLookups()
        {
            throw new NotImplementedException();
        }

    }
}
