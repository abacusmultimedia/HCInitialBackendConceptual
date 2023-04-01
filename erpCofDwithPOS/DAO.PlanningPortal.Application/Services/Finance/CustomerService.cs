using DAO.PlanningPortal.Application.Interfaces.Finance;
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
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepositoryAsync<Ledger> _dataAccessRepositoryAsync;
        private readonly IGenericRepositoryAsync<LedgerGroup> _ledgerGroupRepository;
        private readonly IGenericRepositoryAsync<PersonalInfo> _personalInformation;

        public CustomerService(IGenericRepositoryAsync<Ledger> dataAccessRepositoryAsync,
            IGenericRepositoryAsync<LedgerGroup> ledgerGroupRepository,
            IGenericRepositoryAsync<PersonalInfo> personalInformation
        )
        {
            _dataAccessRepositoryAsync = dataAccessRepositoryAsync;
            _ledgerGroupRepository = ledgerGroupRepository;
            _personalInformation = personalInformation;
        }


        public async Task<Result<long>> AddEdit(CustomerDTO parameter)
        {
            if (parameter.Id == 0)
            {
                var ledgertype = _ledgerGroupRepository.GetAllQueryable().FirstOrDefault(x => x.Title.ToUpper() == "CUSTOMERS");
                var entity = new Ledger
                {
                    IsDeleted = false,
                    ParentId = ledgertype.Id,
                    Description = parameter.Description,
                    Title = parameter.BusinessName,
                    PersonalInfo = new PersonalInfo
                    {
                        NTN = parameter.NTN,
                        Cell = parameter.Cell,
                        Email = parameter.Email,
                        Phone = parameter.Phone,
                        Address = parameter.Address,
                        LatName = parameter.LastName,
                        FirstName = parameter.FirstName,
                    }
                };
                var resp = await _dataAccessRepositoryAsync.AddAsync(entity);
                return Result<long>.Success(resp.Id);
            }
            else
            {
                var entity = _dataAccessRepositoryAsync.GetAllQueryable().
                    Include(x => x.PersonalInfo).
                    FirstOrDefault(x => x.Id == parameter.Id);
                entity.Title = parameter.BusinessName;
                entity.Description = parameter.Description;
                await _dataAccessRepositoryAsync.UpdateAsync(entity);
                
                var PersonalInfo = _personalInformation.GetAllQueryable().FirstOrDefault(x => x.Id == entity.PersonalInfoID);
               
                PersonalInfo.NTN = parameter.NTN;
                PersonalInfo.Cell = parameter.Cell;
                PersonalInfo.Phone = parameter.Phone;
                PersonalInfo.Email = parameter.Email;
                PersonalInfo.LatName = parameter.LastName;
                PersonalInfo.FirstName = parameter.FirstName;
                //entity.PersonalInfo.Address = parameter.Address;
                await _personalInformation.UpdateAsync(PersonalInfo);

                return Result<long>.Success(parameter.Id);
            }

        }

        public async Task<Result<bool>> Delete(long Id)
        {
            var entity = _dataAccessRepositoryAsync.GetAllQueryable().
                  FirstOrDefault(x => x.Id == Id);
            entity.IsDeleted = !entity.IsDeleted;
            await _dataAccessRepositoryAsync.UpdateAsync(entity);

            return Result<bool>.Success(true);
        }

        public async Task<Result<List<CustomerDTO>>> GetList()
        {
            var resp = _dataAccessRepositoryAsync.GetAllQueryable()
                .Where(x => x.LedgerGroup.Title.ToUpper() == "CUSTOMERS")
                .Select(x => new CustomerDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    IsDeleted = x.IsDeleted,
                    Cell = x.PersonalInfo.Cell,
                    Phone = x.PersonalInfo.Phone,
                    Address = x.PersonalInfo.Address,
                    LastName = x.PersonalInfo.LatName,
                    FirstName = x.PersonalInfo.FirstName,
                }).ToList();
            return Result<List<CustomerDTO>>.Success(resp);
        }

        public async Task<Result<CustomerDTO>> GetById(long Id)
        {
            var resp = _dataAccessRepositoryAsync.GetAllQueryable()
                .Where(x => x.Id == Id)
                .Select(x => new CustomerDTO
                {
                    Title = x.Title,
                    BusinessName = x.Title,
                    Cell = x.PersonalInfo.Cell,
                    Phone = x.PersonalInfo.Phone,
                    Email = x.PersonalInfo.Email,
                    LastName = x.PersonalInfo.LatName,
                    FirstName = x.PersonalInfo.FirstName,
                    NTN = x.PersonalInfo.NTN
                    //Description = x.Description,
                    //Address = x.PersonalInfo.Address,

                })
                .FirstOrDefault();
            return Result<CustomerDTO>.Success(resp);
        }

        public Task<Result<List<LookupDto>>> GetLookups()
        {
            throw new NotImplementedException();
        }

    }
}
