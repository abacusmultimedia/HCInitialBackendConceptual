using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zero.Application.Shared.Models.Inventory;
using zero.Shared.Models.GDPRAccess;
using zero.Shared.Models.POS;
using zero.Shared.Response;

namespace DAO.PlanningPortal.Application.Interfaces.Inventory
{
    public interface IItemMasterService
    {
        Task<Result<bool>> ItemExists(ItemExistValidationRequestDto item);
        Task<Result<List<ItemMasterDTO>>> GetAll(ItemMasterReqLazyLoad req);
        Task<Result<ItemMasterCreationDTO>> Post(ItemMasterCreationDTO req);
        Task<Result<ItemMasterCreationDTO>> Put(ItemMasterCreationDTO req);
        Task<Result<ItemMasterCreationDTO>> GetById(long Id);
        Task<Result<List<LookupDto>>> GetLookupDepartment();
        Task<Result<List<LookupDto>>> GetLookupCategory();
        Task<Result<List<LookupDto>>> GetLookupSubCategory();
        Task<Result<List<LookupDto>>> GetLookupBrand();
        Task<Result<List<LookupDto>>> GetLookupModel();
        Task<Result<List<LookupDto>>> GetLookupColor();
        Task<Result<List<LookupDto>>> GetLookupPaking();
        Task<Result<List<LookupDto>>> GetLookupPakingSize();


    }
}
