using zero.Shared.Models.POS;
using zero.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zero.Application.Shared.Models.POS;

namespace DAO.PlanningPortal.Application.Interfaces.POS
{
    public interface IPOS
    {
        Task<Result<List<ItemPOSDto>>> ItemsLookup(ItemPOSLookupSearchReq req);
        Task<Result<POSGrid>> ItemSelectionByID(string subItemCode);
        Task<Result<POSSalesResonseDTO>> PostPOS(POSInvoiceDocReqDto request);
        Result<string> GetQRCodeString(decimal Amount, decimal VatAmount);
        Result<string> lastBill();
        Task<Result<POSInvoiceDocReqDto>> GetBillbyNo(string request);
        Result<List<RecallList>> GetRecallList();

        #region Reprting 
        Task<Result<List<POSUserReport>>> GetUserReport(ReportFilters filters);
        Task<Result<POSCategoryReportWithTotal>> GetCategoryWiseReport(ReportFilters filters);
        Task<Result<TotalSalesReportDTO>> GetTotalSalesReport(ReportFilters filters);
        #endregion


        #region Hold and Recall 
        Task<Result<string>> HoldPOS(POSInvoiceDocReqDto request);
        Task<Result<POSInvoiceDocReqDto>> RecallbyNo(string request);
        Task<Result<bool>> RemoveRecallById(long RefDocucmentNo);

        #endregion Hold and Recall


        #region POS Purchase Invoice 

        Task<Result<string>> PostPOSPurchaseInvoice(PurchaseInvoiceDto request);
        Task<Result<List<PurcahseListDto>>> GetPurchaseInvoiceList(PurchaseListFilter request);

        #endregion  POS Purchase Invoice Ends 
    }
}
