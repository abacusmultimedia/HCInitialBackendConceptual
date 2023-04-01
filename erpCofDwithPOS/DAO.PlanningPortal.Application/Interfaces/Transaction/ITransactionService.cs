using zero.Shared.Models.Dashboard;
using zero.Shared.Models.Finance;
using zero.Shared.Models.GDPRAccess;
using zero.Shared.Models.Reporting;
using zero.Shared.Models.TransactionDTo;
using zero.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Application.Interfaces.Transaction
{
    public interface ITransactionService
    {
        Task<Result<long>> AddEdit(TransactionDto parameter);
        Task<Result<long>> AddContract(ContractDTO parameter);
        Task<Result<long>> AddRent(RentReceiptDTO parameter);
        Task<Result<List<TransactionDto>>> GetList();
        Task<Result<bool>> Delete(long Id);
        Task<Result<List<ContractDTO>>> GetListofContracts();
        Task<Result<List<RentReceiptDTO>>> GetListofCollection();
        Task<Result<List<WidgetDto>>> GetTransactionsWidgets();
        Task<Result<List<ChildTransactionDto>>> GetListByLedgerId(long Id);
        Task<Result<List<long>>> MakeRentPayableforThisMonth();
        Task<Result<List<OutstandingRentDto>>> GetOutstandingRents();
        Task<Result<long>> RentCollectionByContract(RentReceiptByContractDTO parameter);
        Task<Result<List<TransactionDto>>> GetDayBook();
        Task<Result<string>> GetTransactionAttachment(long Id);
        Task<Result<List<TransactionDto>>> GetAllmyCollections();
        Task<Result<bool>> UpdatechildAmount(UpDateChildAmountOnly request);
        Task<Result<bool>> ValidateContractLedger(RemoteValidationItemTransaction validation);
        Task<Result<bool>> ValidateContractItem(RemoteValidationItemTransaction validation);
        Task<Result<List<WidgetDto>>> GetContractWidgets();
        Task<Result<long>> UpdateContractStatus(ContractStatusDto parameter);
    }
}
