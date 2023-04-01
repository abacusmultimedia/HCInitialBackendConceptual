using System;
using Amazon.S3;
using System.IO;
using UploadToAWS;
using System.Linq;
using Amazon.Runtime;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DAO.PlanningPortal.Domain.Enums;
using DAO.PlanningPortal.Common.Sessions;
using DAO.PlanningPortal.Domain.Entities;
using DAO.PlanningPortal.Domain.Entities.Finance;
using zero.Shared.Response;
using zero.Shared.Repositories;
using zero.Shared.Models.Finance;
using DAO.PlanningPortal.Application.Interfaces.Transaction;
using zero.Shared.Models.Dashboard;
using zero.Shared.Models.Reporting;
using zero.Shared.Models.TransactionDTo;
using DAO.PlanningPortal.Application.Services.thirdPartyIntigrations.Communicaiton;

namespace DAO.PlanningPortal.Application.Services.TransactionService
{
    public class TransactionService : ITransactionService
    {
        private readonly IUserSession _userSession;
        IGenericRepositoryAsync<LedgerSMS> _ledgerSMSRepo { get; }
        private readonly IGenericRepositoryAsync<Ledger> _ledgerRepo;
        private readonly IGenericRepositoryAsync<Contract> _contractRepo;
        private readonly IGenericRepositoryAsync<ItemBatch> _batchesRepo;
        private readonly IGenericRepositoryAsync<ApplicationUser> _userRepo;
        private readonly IGenericRepositoryAsync<ApplicationRole> _roleRepository;
        private readonly IGenericRepositoryAsync<Transaction> _dataAccessRepositoryAsync;
        private readonly IGenericRepositoryAsync<ApplicationUserRole> _applicationUserRole;
        private readonly IGenericRepositoryAsync<ParentTransaction> _parentTransactionRepository;
        public TransactionService(
             IUserSession userSession,
             IGenericRepositoryAsync<Ledger> ledgerRepo,
             IGenericRepositoryAsync<Contract> contractRepo,
             IGenericRepositoryAsync<ItemBatch> batchesRepo,
             IGenericRepositoryAsync<LedgerSMS> ledgerSMSRepo,
             IGenericRepositoryAsync<ApplicationUser> userRepo,
             IGenericRepositoryAsync<ApplicationRole> roleRepository,
             IGenericRepositoryAsync<Transaction> dataAccessRepositoryAsync,
             IGenericRepositoryAsync<ApplicationUserRole> applicationUserRole,
             IGenericRepositoryAsync<ParentTransaction> parentTransactionRepository
        )
        {
            _userRepo = userRepo;
            _ledgerRepo = ledgerRepo;
            _batchesRepo = batchesRepo;
            _userSession = userSession;
            _contractRepo = contractRepo;
            _ledgerSMSRepo = ledgerSMSRepo;
            _roleRepository = roleRepository;
            _applicationUserRole = applicationUserRole;
            _dataAccessRepositoryAsync = dataAccessRepositoryAsync;
            _parentTransactionRepository = parentTransactionRepository;
        }






        public async Task<Result<long>> AddContract(ContractDTO parameter)
        {
            //assigns year, month, day
            var rawnextDueDate = new DateTime(parameter.StartDate.Year, parameter.StartDate.Month, parameter.DueDate);
            var entity = new Contract
            {
                isActive = true,
                CostCenterId = 1,
                IsDeleted = false,
                EndDate = parameter.EndDate,
                DueDate = parameter.DueDate,
                LedgerId = parameter.LedgerId,
                Frequancy = parameter.Frequancy,
                StartDate = parameter.StartDate,
                RentAmount = parameter.RentAmount,
                ItemBatchId = parameter.ItemBatchId,
                Description = parameter.Description,
                SecurityAmount = parameter.SecurityAmount,
                NextDueDate = rawnextDueDate.AddMonths(parameter.Frequancy - 1),
            };
            var response = await _contractRepo.AddAsync(entity);
            return Result<long>.Success(response.Id);

        }

        public async Task<Result<List<TransactionDto>>> GetAllmyCollections()
        {


            var userRoles =
                (from userRole in _applicationUserRole.GetAllQueryable()
                 join role in _roleRepository.GetAllQueryable() on userRole.RoleId equals role.Id
                 where (userRole.UserId == _userSession.UserId && (role.Name.ToLower() == "admin"
                 || role.Name.ToLower() == "superadmin"
                 ))
                 select new
                 {
                     role.Name
                 }).ToList();

            var users = _userRepo.GetAllQueryable().ToList();
            var resp = _parentTransactionRepository
                .GetAllQueryable()
                .Include(x => x.CreatedByUser)
                .Where(x => x.CreatedBy == _userSession.UserId || userRoles.Any())
                .Select(x => new TransactionDto
                {
                    Id = x.Id,
                    Date = x.Date,
                    Description = x.Description,
                    CreatedByID = (x.CreatedBy == null ? "--" : x.CreatedByUser.FullName),
                    Transaction = x.Transactions.Select(t =>
                     new ChildTransactionDto
                     {
                         Id = t.Id,
                         Qty = t.Qty,
                         Rate = t.Rate,
                         isDr = t.IsDr,
                         Amount = t.Amount,
                         LedgerId = t.LedgerId,
                         Description = t.Description,
                         ItemBatchId = t.ItemBatchId,
                         LedgerTitle = t.Ledger.Title,
                         CostCenterId = t.CostCenterId,
                         CreatedByID = (x.CreatedBy == null ? "--" : x.CreatedByUser.FullName),
                         CostCenterTitle = t.CostCenter.Title,
                         BatchTitle = t.ItemBatch.Parent.Title,
                     }
                     ).ToList()
                }).ToList();
            return Result<List<TransactionDto>>.Success(resp);
        }

        public async Task<Result<long>> UpdateContractStatus(ContractStatusDto parameter)
        {
            var entitity = _contractRepo.GetAllQueryable().FirstOrDefault(x => x.Id == parameter.DocId);
            entitity.isActive = parameter.Status;
            await _contractRepo.UpdateAsync(entitity);
            return Result<long>.Success(entitity.Id);

        }


        public async Task<Result<bool>> UpdatechildAmount(UpDateChildAmountOnly request)
        {
            var parent = _parentTransactionRepository.GetAllQueryable()
                .Include(x => x.Transactions)
                .FirstOrDefault(x => x.Id == request.ParentID);

            foreach (var item in parent.Transactions)
            {
                item.Amount = request.Amount;
                await _dataAccessRepositoryAsync.UpdateAsync(item);

            }
            return Result<bool>.Success(true);

        }

        public async Task<Result<List<long>>> MakeRentPayableforThisMonth()
        {
            var regCustomers = _ledgerSMSRepo.GetAllQueryable()
                .Select(x => new
                {
                    x.LedgerId
                })
                .ToList();
            var dayofMonth = DateTime.Today;
            var CurrentMonth = DateTime.Now.Month;
            var RentalLedger = _ledgerRepo.GetAllQueryable().Where(x => x.Title == "Rent").FirstOrDefault();
            //var contracts = _contractRepo.GetAllQueryable().Where(x => !x.IsDeleted).ToList();
            var ItemsWithoutPaidRent = _batchesRepo.GetAllQueryable()
                .Include(x => x.Contracts)
                .Where(x => x.Contracts.Any(

                    //c=>(c.Frequancy == 1  && c.StartDate ) ||
                    //(c.Frequancy == 1 && c.StartDate)||
                    //(c.Frequancy == 1 && c.StartDate)||
                    ) && x.Contracts.FirstOrDefault(f => f.isActive).NextDueDate <= dayofMonth

                         && !(x.Transactions.Where(e => e.ParentTransaction.Date.Month == CurrentMonth
                         && e.ParentTransaction.TransactionType == (int)TransactionTypeEnum.Sales).Any())
                         )
                .ToList();
            var transactionsList = new List<ParentTransaction>();
            var listUpdate = new List<long>();
            var listPosts = new List<long>();
            foreach (var item in ItemsWithoutPaidRent)
            {
                foreach (var Contracts in item.Contracts)
                {
                    var transaction = new ParentTransaction
                    {
                        TransactionType = (int)TransactionTypeEnum.Sales,
                        Date = DateTime.Now,
                        Description = "Rent Payable Credited to Customers Account",
                        Transactions = new List<Transaction> {
                             new Transaction {
                                 Amount = Contracts.RentAmount,
                                 CostCenterId = 1,
                                 IsDeleted =false,
                                 IsDr = true,
                                 ItemBatchId = Contracts.ItemBatchId,
                                 Qty=0,
                                 Rate = 0,
                                 LedgerId = Contracts.LedgerId
                             },
                             new Transaction {
                                 Amount = Contracts.RentAmount,
                                 CostCenterId = 1,
                                 IsDeleted =false,
                                 IsDr = false,
                                 ItemBatchId = null,
                                 Qty=0,
                                 Rate = 0,
                                 LedgerId = RentalLedger.Id
                             }
                        }

                    };
                    transactionsList.Add(transaction);

                    sendSMS(Contracts);
                    if (regCustomers.Any(r => r.LedgerId == Contracts.LedgerId))
                    {
                        listUpdate.Add(Contracts.LedgerId);
                    }
                    else
                    {
                        listPosts.Add(Contracts.LedgerId);
                    }
                }

            }
            foreach (var item in listPosts)
            {
                postNewEnteries(item);

            }
            foreach (var item in listUpdate)
            {
                updateEnteries(item);
            }



            var response = await _parentTransactionRepository.AddRangeAsync(transactionsList);
            return Result<List<long>>.Success(response.Select(x => x.Id).ToList());

        }

        private void sendSMS(Contract contract)
        {
            string smsText = "Dear Customr \n Amount of Rs:" + contract.RentAmount + ".00 is due" +
                ", please pay you dues as soon as posible.";
            //string receiver = "+92" + contract.Ledger.PersonalInfo.Cell;
            //SMSByVeevotech.MapadnSend(receiver, smsText); /// this code is comment out to save cost of SMS // when ever you purchase the API uncoment it 

        }
        private async void postNewEnteries(long ledgerid)
        {
            await _ledgerSMSRepo.AddAsync(new LedgerSMS()
            {
                IsSent = true,
                LedgerId = ledgerid
            });
        }
        private async void updateEnteries(long ledgerid)
        {
            var entity = _ledgerSMSRepo.GetAllQueryable()
                 .FirstOrDefault(x => x.LedgerId == ledgerid);
            entity.IsSent = true;
            await _ledgerSMSRepo.UpdateAsync(entity);

        }
        public async Task<Result<long>> RentCollectionByContract(RentReceiptByContractDTO parameter)
        {

            //var filePath = Path.GetTempFileName();

            //using (var stream = System.IO.File.Create(filePath))
            //{ 
            //    await parameter.File.CopyToAsync(stream); 
            //}
            //var c = filePath; 
            //var config = new AmazonS3Config { ServiceURL = "https://s3.wasabisys.com" }; 
            //var credentials = new StoredProfileAWSCredentials("wasabi"); 
            //_s3Client = new AmazonS3Client(credentials, config); 
            string Objectname = $"{DateTime.Now:yyyyMMddhhmmss}{parameter.File.FileName}";
            //,  DateTime.Now.ToString(("ddddddMMMMyyyyHHmmss")) + ".png";
            await UploadObject.UploadObjectFromFileAsync(BucketName, Objectname, parameter.File);



            var contract = _contractRepo.GetAllQueryable().Include(x => x.Ledger).ThenInclude(x => x.PersonalInfo)
                .FirstOrDefault(x => x.Id == parameter.ContractID);
            var modePayment = _ledgerRepo.GetAllQueryable().FirstOrDefault(x => x.Title == "Cash");
            TransactionDto transactionDto = new TransactionDto
            {
                Description = parameter.Description,
                Date = DateTime.Now,
                TransactionType = (int)TransactionTypeEnum.Receipt,
                FileList = new List<string>(),
                Transaction = new List<ChildTransactionDto> {
                     new ChildTransactionDto
                    {
                        Amount = parameter.Amount,
                        ItemBatchId =  null,
                        LedgerId = modePayment.Id,
                        Qty = 0, Rate =0,
                        Description =  "Rent Collection",
                        isDr = true,CostCenterId = 1,
                    },
                    new ChildTransactionDto
                    {
                        Amount = parameter.Amount,
                        ItemBatchId =  contract.ItemBatchId,
                        LedgerId = contract.LedgerId,
                        Qty = 0, Rate =0,
                        Description = "Rent Collection",
                        isDr = false,CostCenterId = 1,
                    }
                }
            };
            transactionDto.FileList.Add(Objectname);
            var messageText = "Dear Customer, \n Your payment of Rs:" + parameter.Amount + "/- has been collected";
            var ph = contract.Ledger.PersonalInfo.Cell;
            if (ph.Trim() != "")
            {
                SMSByVeevotech.MapadnSend("92" + ph.Trim(), messageText);
            }
            //ShortMessageService.MapAndSend("923099025332");
            return await post(transactionDto);
        }

        public async Task<Result<long>> AddRent(RentReceiptDTO parameter)
        {
            TransactionDto transactionDto = new TransactionDto
            {
                Description = parameter.Description,
                Date = DateTime.Now,
                TransactionType = (int)TransactionTypeEnum.Receipt,
                Transaction = new List<ChildTransactionDto> {
                    new ChildTransactionDto
                    {
                        Amount = parameter.Amount,
                        ItemBatchId = parameter.ItemBatchId,
                        LedgerId = parameter.ledgerIdDR,
                        Qty = 0, Rate =0,
                        Description = parameter.DescriptionDR,
                        isDr = true,CostCenterId = 1,

                    },

                    new ChildTransactionDto
                    {
                        Amount = parameter.Amount,
                        ItemBatchId = parameter.ItemBatchId,
                        LedgerId = parameter.ledgerIdCR,
                        Qty = 0, Rate =0,
                        Description = parameter.DescriptionCR,
                        isDr = false,CostCenterId = 1,
                    }

                }
            };
            return await post(transactionDto);
        }


        public Task<Result<long>> AddEdit(TransactionDto parameter)
        {

            throw new NotImplementedException();
        }

        public Task<Result<bool>> Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<TransactionDto>>> GetList()
        {


            throw new NotImplementedException();
        }
        public async Task<Result<List<WidgetDto>>> GetTransactionsWidgets()
        {
            var response = new List<WidgetDto>();
            var qry = _parentTransactionRepository.GetAllQueryable()
                 .Select(x => new { x.TransactionType }).ToList();

            var totalCount = qry.Count();


            var transactionWidgets = qry.GroupBy(x => x.TransactionType).ToList();
            foreach (var item in transactionWidgets)
            {
                var c = item.Key;
                var k = item.Count();
                var temp = new WidgetDto
                {
                    Description = "Transactions",
                    Size = "",
                    StylingClass = "",
                    Title = Enum.GetName(typeof(TransactionTypeEnum), item.Key),
                    Type = "",
                    Value = (item.Count() * 100 / totalCount).ToString(),
                };
                response.Add(temp);
            }
            return Result<List<WidgetDto>>.Success(response);
        }


        public async Task<Result<List<WidgetDto>>> GetContractWidgets()
        {
            var response = new List<WidgetDto>();
            var qry = _contractRepo.GetAllQueryable()
                 .Select(x => new { x.isActive }).ToList();

            var totalCount = qry.Count();


            var transactionWidgets = qry.GroupBy(x => x.isActive).ToList();
            foreach (var item in transactionWidgets)
            {
                var c = item.Key;
                var k = item.Count();
                var temp = new WidgetDto
                {
                    Description = "Contract",
                    Size = "",
                    StylingClass = item.Key ? "success" : "danger",
                    Title = item.Key ? "Active Contracts" : "In Active Contracts",
                    Type = "",
                    Value = (item.Count() * 100 / totalCount).ToString(),
                };
                response.Add(temp);
            }
            return Result<List<WidgetDto>>.Success(response);
        }

        public async Task<Result<List<ContractDTO>>> GetListofContracts()
        {
            await MakeRentPayableforThisMonth();
            var response = _contractRepo.GetAllQueryable()
                .Select(x => new ContractDTO
                {
                    Id = x.Id,
                    DueDate = x.DueDate,
                    EndDate = x.EndDate,
                    IsActive = x.isActive,
                    StartDate = x.StartDate,
                    Frequancy = x.Frequancy,
                    RentAmount = x.RentAmount,
                    Description = x.Description,
                    ItemTitle = x.ItemBatch.Title,
                    CustomerTitle = x.Ledger.Title,
                    SecurityAmount = x.SecurityAmount,

                })
                .OrderByDescending(e => e.StartDate)
                .ToList();
            return Result<List<ContractDTO>>.Success(response);
        }

        public async Task<Result<List<RentReceiptDTO>>> GetListofCollection()
        {
            var response = _parentTransactionRepository.GetAllQueryable()
                .Include(x => x.Transactions)
                .Where(x => x.TransactionType == (int)TransactionTypeEnum.Receipt)
                .Select(x => new RentReceiptDTO
                {
                    Date = x.Date,
                    Description = x.Description,
                    Amount = x.Transactions.FirstOrDefault(x => x.IsDr).Amount,
                    VenderTitle = x.Transactions.FirstOrDefault(x => x.IsDr).Ledger.Title,
                    CustomerTitle = x.Transactions.FirstOrDefault(x => !x.IsDr).Ledger.Title,
                    ItemTitle = x.Transactions.FirstOrDefault(x => !x.IsDr).ItemBatch.Title,
                })
                .OrderByDescending(e => e.Date)
                .ToList();
            return Result<List<RentReceiptDTO>>.Success(response);
        }

        public async Task<Result<List<ChildTransactionDto>>> GetListByLedgerId(long Id)
        {
            var resp = _dataAccessRepositoryAsync
                .GetAllQueryable().Where(x => x.LedgerId == Id)
                .Select(x => new ChildTransactionDto
                {
                    Id = x.Id,
                    Qty = x.Qty,
                    Rate = x.Rate,
                    isDr = x.IsDr,
                    Amount = x.Amount,
                    LedgerId = x.LedgerId,
                    Description = x.Description,
                    ItemBatchId = x.ItemBatchId,
                    CostCenterId = x.CostCenterId,
                    CostCenterTitle = x.CostCenter.Title,
                    BatchTitle = x.ItemBatch.Parent.Title,
                    CreatedOn = x.CreatedOn.ToString("dd-MM-yyyy"),
                    LedgerTitle = x.ParentTransaction.Transactions.
                    FirstOrDefault(x => x.LedgerId != Id).Ledger.Title,
                })
                .ToList();
            return Result<List<ChildTransactionDto>>.Success(resp);
        }

        public async Task<Result<string>> GetTransactionAttachment(long Id)
        {


            var transaction = _parentTransactionRepository.GetAllQueryable()
                .Include(x => x.TransactionsAttachments)
                .FirstOrDefault(x => x.Id == Id);
            if (transaction != null && transaction.TransactionsAttachments.Any())
            {
                string file = await UploadObject.read(transaction.TransactionsAttachments.FirstOrDefault().Key, BucketName);
                return Result<string>.Success(file);

            }
            else
            {
                return Result<string>.Success("noimagefound");

            }
        }

        public async Task<Result<List<TransactionDto>>> GetDayBook()
        {
            var resp = _parentTransactionRepository
                .GetAllQueryable()
                .Where(x => x.Date.Date == DateTime.Now.Date)
                .Select(x => new TransactionDto
                {
                    Id = x.Id,
                    Date = x.Date,
                    Description = x.Description,
                    Transaction = x.Transactions.Select(t =>
                     new ChildTransactionDto
                     {
                         Id = t.Id,
                         Qty = t.Qty,
                         Rate = t.Rate,
                         Amount = t.Amount,
                         LedgerId = t.LedgerId,
                         Description = t.Description,
                         ItemBatchId = t.ItemBatchId,
                         LedgerTitle = t.Ledger.Title,
                         CostCenterId = t.CostCenterId,
                         CostCenterTitle = t.CostCenter.Title,
                         BatchTitle = t.ItemBatch.Parent.Title,
                         isDr = t.IsDr
                     }
                     ).ToList()
                }).ToList();
            return Result<List<TransactionDto>>.Success(resp);
        }




        #region PostTransactions  
        private async Task<Result<long>> postSales(TransactionDto parameter)
        {
            if (parameter.TransactionType == (int)TransactionTypeEnum.Sales)
            {

            }
            else if (parameter.TransactionType == (int)TransactionTypeEnum.Purcchase)
            {

            }
            else if (parameter.TransactionType == (int)TransactionTypeEnum.Payment)
            {

            }
            else if (parameter.TransactionType == (int)TransactionTypeEnum.Receipt)
            {

            }
            else if (parameter.TransactionType == (int)TransactionTypeEnum.Contract)
            {
                await post(parameter);
            }

            return null;
        }

        private async Task<Result<long>> post(TransactionDto parameter)
        {

            var entity = new ParentTransaction
            {
                Transactions = mapTransaction(parameter),
                Date = DateTime.Now,
                Description = parameter.Description,
                TransactionType = parameter.TransactionType,
                TransactionsAttachments = mapAttachements(parameter.FileList)
            };
            try
            {
                var response = await _parentTransactionRepository.AddAsync(entity);
                return Result<long>.Success(response.Id);

            }
            catch (Exception ex)
            {

            }
            return null;
        }
        private List<Transaction> mapTransaction(TransactionDto parameter)
        {
            var response = new List<Transaction>();

            foreach (var item in parameter.Transaction)
            {
                var temp = new Transaction
                {
                    IsDr = item.isDr,
                    IsDeleted = false,
                    ItemBatchId = item.ItemBatchId,
                    Description = item.Description,
                    Amount = item.Amount,
                    CostCenterId = item.CostCenterId,
                    Qty = item.Qty,
                    Rate = item.Rate,
                    LedgerId = item.LedgerId
                };
                response.Add(temp);
            }
            return response;
        }

        private List<TransactionsAttachments> mapAttachements(List<string> fileList)
        {
            var mappedList = new List<TransactionsAttachments>();

            foreach (var item in fileList)
            {
                var file = new TransactionsAttachments
                {
                    Bucket = "",
                    Key = item
                };
                mappedList.Add(file);

            }
            return mappedList;
        }

        #endregion


        #region Reports 


        private static IAmazonS3 _s3Client;
        private const string BucketName = "acntrenterpproofstorage";

        private const string ObjectName1 = "new-test-file.txt";

        // updated it to take any object from desktop, just adjust the file name above
        private static readonly string PathToDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public async Task<Result<List<OutstandingRentDto>>> GetOutstandingRents()
        {

            var dayofMonth = DateTime.Today.Day;
            var CurrentMonth = DateTime.Now.Month;
            var ItemsWithoutPaidRent = _batchesRepo.GetAllQueryable()
                .Include(x => x.Contracts).Include(x => x.Agent)
                .Where(x => x.Contracts.Any(f => f.isActive) && (x.UserId == _userSession.UserId))
                .Select(x => new OutstandingRentDto
                {
                    ItemName = x.Parent.Title,
                    batchId = x.Id,
                    BatchName = x.Title,
                    CustomerName = x.Contracts.FirstOrDefault().Ledger.Title,
                    CrAmount = x.Transactions.Where(r => !r.IsDr).Sum(e => e.Amount),
                    DrAmount = x.Transactions.Where(r => r.IsDr).Sum(e => e.Amount),
                    ContractID = x.Contracts.FirstOrDefault().Id
                })
                 .Where(x => x.DrAmount != x.CrAmount)
                .ToList();
            return Result<List<OutstandingRentDto>>.Success(ItemsWithoutPaidRent);
        }

        public async Task<Result<bool>> ValidateContractItem(RemoteValidationItemTransaction validation)
        {
            bool resp = _contractRepo.GetAllQueryable().Any(x =>
            (x.Id == validation.DocumentId && x.ItemBatchId == validation.ItemId)
             || (validation.DocumentId == 0 && validation.ItemId == x.ItemBatchId));
            return Result<bool>.Success(resp);
        }
        public async Task<Result<bool>> ValidateContractLedger(RemoteValidationItemTransaction validation)
        {
            bool resp = _contractRepo.GetAllQueryable().Any(
                 x => (x.Id == validation.DocumentId && x.LedgerId == validation.ItemId) ||
                   (validation.DocumentId == 0 && validation.ItemId == x.LedgerId));
            return Result<bool>.Success(resp);
        }


        #endregion
    }
}
