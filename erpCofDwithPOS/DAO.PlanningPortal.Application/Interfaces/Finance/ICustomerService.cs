using zero.Shared.Models.Finance;
using zero.Shared.Models.GDPRAccess;
using zero.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Application.Interfaces.Finance
{
    public interface ICustomerService
    {
        Task<Result<long>> AddEdit(CustomerDTO parameter);
        Task<Result<List<CustomerDTO>>> GetList();
        Task<Result<List<LookupDto>>> GetLookups();
        Task<Result<bool>> Delete(long Id);
        Task<Result<CustomerDTO>> GetById(long Id);

    }
}
