using zero.Shared.Models.Dashboard;
using zero.Shared.Models.Finance;
using zero.Shared.Models.GDPRAccess;
using zero.Shared.Models.Inventory;
using zero.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Application.Interfaces.Inventory
{
    public interface IInventoryService
    {
        #region Item  
        Task<Result<long>> Item_AddEdit(ItemDto parameter);
        Task<Result<List<ItemDto>>> Item_GetList();
        Task<Result<ItemDto>> Item_GetBYID(long Id);
        Task<Result<List<LookupDto>>> Item_GetLookups();
        Task<Result<bool>> Item_Delete(long Id);

        #endregion

        #region Item Group
        Task<Result<long>> ItemGroup_AddEdit(ItemGroupDto parameter);
        Task<Result<List<ItemGroupDto>>> ItemGroup_GetList();
        Task<Result<ItemGroupDto>> ItemGroup_GetBYID(long Id);
        Task<Result<List<LookupDto>>> ItemGroup_GetLookups();
        Task<Result<List<LookupDto>>> ItemGroupsChildOnly_GetLookups();
        Task<Result<bool>> ItemGroup_Delete(long Id);

        #endregion
        #region Batch  
        Task<Result<long>> Batch_AddEdit(BatchDto parameter);
        Task<Result<List<BatchDto>>> Batch_GetList();
        Task<Result<List<BatchDto>>> GetAllAssignedtoMe();
        Task<Result<BatchDto>> Batch_GetBYID(long Id);
        Task<Result<List<LookupDto>>> Batch_GetLookups();
        Task<Result<bool>> Batch_Delete(long Id);
        Task<Result<long>> Batch_AssignUser(AssignBatchtoAgentDTO parameter);

        #endregion

        Task<Result<List<LookupDto>>> Item_BatchGetLookups();



        Task<Result<List<WidgetDto>>> GetWidgets();

    }
}





