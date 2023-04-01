using DAO.PlanningPortal.Application.Interfaces.POS;
using zero.Shared.Models.POS;
using zero.Shared.Repositories;
using zero.Shared.Response;
using DAO.PlanningPortal.Common.Sessions;
using POSERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zero.Application.Shared.Models.POS;
using Microsoft.EntityFrameworkCore;
using DAO.PlanningPortal.Domain.Entities;
using DAO.PlanningPortal.Domain.Entities.Finance;

namespace DAO.PlanningPortal.Application.Services.POS
{
    public class POSService : IPOS
    {
        private readonly IUserSession _userSession;
        private readonly IGenericRepositoryAsync<ApplicationUser> _userRepo;
        private readonly IGenericRepositoryAsync<SD_POS_Invoice> _posInvoiceRepo;
        private readonly IGenericRepositoryAsync<SD_POS_Invoice_Template> _posInvoiceRepo_template;
        private readonly IGenericRepositoryAsync<SD_POS_InvoiceDetail_Template> _posInvoiceDetail_template;
        private readonly IGenericRepositoryAsync<INV_MSD_SubItemCode> _subItemsRepo;
        private readonly IGenericRepositoryAsync<SD_POS_InvoiceDetail> _posDetailRepo;

        public POSService(
             IUserSession userSession,
             IGenericRepositoryAsync<ApplicationUser> userRepo,
             IGenericRepositoryAsync<SD_POS_Invoice> posInvoiceRepo,
             IGenericRepositoryAsync<INV_MSD_SubItemCode> subItemsRepo,
             IGenericRepositoryAsync<SD_POS_InvoiceDetail> posDetailRepo,
             IGenericRepositoryAsync<SD_POS_Invoice_Template> posInvoiceRepo_template,
             IGenericRepositoryAsync<SD_POS_InvoiceDetail_Template> posInvoiceDetail_template
        )
        {
            _userRepo = userRepo;
            _userSession = userSession;
            _subItemsRepo = subItemsRepo;
            _posDetailRepo = posDetailRepo;
            _posInvoiceRepo = posInvoiceRepo;
            _posInvoiceRepo_template = posInvoiceRepo_template;
            _posInvoiceDetail_template = posInvoiceDetail_template;
        }

        public async Task<Result<List<ItemPOSDto>>> ItemsLookup(ItemPOSLookupSearchReq req)
        {

            var response = _subItemsRepo.GetAllQueryable().Where(x =>
            x.ShortEng.ToUpper().Contains(req.ShortEng.ToUpper().Trim())
            || String.IsNullOrEmpty(req.ShortEng)
            ).Take(req.ShortEng == "" ? 20 : 100)
               .Select(x => new ItemPOSDto
               {
                   ShortEng = x.ShortEng,
                   SubItemCode = x.SubItemCode,
                   Selling_Price = x.Selling_Price,
               }).ToList();
            return Result<List<ItemPOSDto>>.Success(response);
        }
        public async Task<Result<POSGrid>> ItemSelectionByID(string subItemCode)
        {
            var startasPreshable = subItemCode.Trim().Substring(0, 2);
            if (startasPreshable == "99" && subItemCode.Length > 6)
            {
                string perishableCode = subItemCode.Trim().Substring(0, 7);
                var res = _subItemsRepo.GetAllQueryable()
                    .Include(x => x.Item)
                    .FirstOrDefault(x => x.SubItemCode == perishableCode);
                if (res == null)
                {
                    res = _subItemsRepo.GetAllQueryable()
                .Include(x => x.Item)
                .FirstOrDefault(x => x.SubItemCode == subItemCode);
                }
                if (subItemCode.Length == 13)
                {
                    try
                    {
                        string priceBeforeDecml = subItemCode.Trim().Substring(7, 3);
                        string priceAfterDecml = subItemCode.Trim().Substring(10, 2);
                        return await MapResponse(res, false, convetDesimalFromTwoString(priceBeforeDecml, priceAfterDecml));

                    }
                    catch (Exception ex) { }
                }
                return await MapResponse(res, true, 0);
            }
            else
            {
                var res = _subItemsRepo.GetAllQueryable()
                .Include(x => x.Item)
                .FirstOrDefault(x => x.SubItemCode == subItemCode);
                return await MapResponse(res, true, 0);

            }


        }

        private decimal convetDesimalFromTwoString(string beforeDm, string afterDm)
        {
            decimal n = 0;
            decimal n2 = 0;
            try
            {
                n = Convert.ToDecimal(beforeDm);
                n2 = Convert.ToDecimal("0." + afterDm);
            }
            catch (Exception ex)
            {

            }

            return n + n2;
        }
        private async Task<Result<POSGrid>> MapResponse(INV_MSD_SubItemCode res, bool isUpdatePriceReq, decimal? price)
        {

            if (res != null)
            {
                var response = new POSGrid
                {
                    Qty = 1,
                    Id = res.Id,
                    Seq = res.Seq,
                    Tax = res.Tax,
                    UnitID = res.UnitID,
                    IsActive = res.IsActive,
                    ShortEng = res.ShortEng,
                    AvgPrice = res.AvgPrice,
                    CostPrice = res.CostPrice,
                    EstProfit = res.EstProfit,
                    SubItemCode = res.SubItemCode,
                    Selling_Price = price > 0 ? price : res.Selling_Price,
                    IsPerishable = (bool)res.Item?.IsPerishable,
                    isUpdatePariceRequired = isUpdatePriceReq,
                    ArabicName = res.ShortArb,
                };

                return Result<POSGrid>.Success(response);

            }
            else
            {
                var resp = Result<POSGrid>.Success(new POSGrid()
                {
                    Qty = 0,
                    Id = 0,
                    Seq = 0,
                    Tax = 0,
                    UnitID = 0,
                    AvgPrice = 0,
                    CostPrice = 0,
                    EstProfit = 0,
                    ShortEng = "",
                    SubItemCode = "",
                    IsActive = false,
                    Selling_Price = 0,
                });
                resp.Succeeded = false; resp.Errors = new string[] { "Not found" };
                return resp;

            }

        }


        public async Task<Result<POSInvoiceDocReqDto>> GetBillbyNo(string request)
        {
            POSInvoiceDocReqDto response = _posInvoiceRepo.GetAllQueryable()
                .Where(x => x.DocumentNo == long.Parse(request))
                .Select(x => new POSInvoiceDocReqDto()
                {
                    Void = x.Void,
                    Refund = x.Refund,
                    Discount = x.Discount,
                    Round = x.Round,
                    Net = x.Net,
                    Taxable = x.Taxable,
                    Tax = x.Tax,
                    Paid = x.Paid,
                    Cash = x.Cash,
                    Card = x.Card,
                    CardTypeID = x.CardTypeID,
                    CounterNo = x.CounterNo,
                    StatusID = x.StatusID,
                    ApproveID = x.ApproveID,
                    ActionUserID = x.ActionUserID,
                    ActionDate = x.ActionDate,
                    ActionTypeId = x.ActionTypeId,
                    ExpTypeID = x.ExpTypeID,
                    EmpId = x.EmpId,
                    DocucmentNo = x.DocumentNo,
                    DocumentDate = x.DocumentDate,
                    POS_InvoiceDetail = x.SD_POS_InvoiceDetail.Select(r => new POSGrid
                    {
                        Id = r.ItemID,
                        Seq = r.Seq,
                        SubItemCode = r.SubitemCode,
                        ShortEng = r.SubItemCode.ShortEng,
                        Qty = r.Qty,
                        CostPrice = r.Price,
                        AvgPrice = r.AvgCost,
                        Tax = r.Tax,
                        Selling_Price = r.Price,
                        //UnitID =  r. ,
                        //EstProfit =r.p   ,
                        //IsPerishable = r  ,
                        //IsActive = r.isac  ,
                        //isUpdatePariceRequired =   ,
                        ArabicName = r.SubItemCode.ShortArb,

                    }).ToList()
                })
                .FirstOrDefault();
            return Result<POSInvoiceDocReqDto>.Success(response);

        }
        public async Task<Result<POSInvoiceDocReqDto>> RecallbyNo(string request)
        {
            POSInvoiceDocReqDto response = _posInvoiceRepo_template.GetAllQueryable()
                .Where(x => x.DocumentNo == long.Parse(request))
                .Select(x => new POSInvoiceDocReqDto()
                {
                    Void = x.Void,
                    Refund = x.Refund,
                    Discount = x.Discount,
                    Round = x.Round,
                    Net = x.Net,
                    Taxable = x.Taxable,
                    Tax = x.Tax,
                    Paid = x.Paid,
                    Cash = x.Cash,
                    Card = x.Card,
                    CardTypeID = x.CardTypeID,
                    CounterNo = x.CounterNo,
                    StatusID = x.StatusID,
                    ApproveID = x.ApproveID,
                    ActionUserID = x.ActionUserID,
                    ActionDate = x.ActionDate,
                    ActionTypeId = x.ActionTypeId,
                    ExpTypeID = x.ExpTypeID,
                    EmpId = x.EmpId,
                    RefDocucmentNo = x.RefDocumentNo,
                    DocucmentNo = x.DocumentNo,
                    POS_InvoiceDetail = x.SD_POS_InvoiceDetail.Select(r => new POSGrid
                    {
                        Id = r.ItemID,
                        Seq = r.Seq,
                        SubItemCode = r.SubitemCode,
                        ShortEng = r.SubItemCode.ShortEng,
                        Qty = r.Qty,
                        CostPrice = r.Price,
                        AvgPrice = r.AvgCost,
                        Tax = r.Tax,
                        Selling_Price = r.Price,
                        //UnitID =  r. ,
                        //EstProfit =r.p   ,
                        //IsPerishable = r  ,
                        //IsActive = r.isac  ,
                        //isUpdatePariceRequired =   ,
                        ArabicName = r.SubItemCode.ShortArb,

                    }).ToList()
                })
                .FirstOrDefault();
            return Result<POSInvoiceDocReqDto>.Success(response);

        }

        public async Task<Result<POSSalesResonseDTO>> PostPOS(POSInvoiceDocReqDto request)
        {
            if (isDocumentExistAlready(request))
            {
                return Result<POSSalesResonseDTO>.Failure();
            }
            var largetstDoc = 1;
            //_posInvoiceRepo.GetAllQueryable().Max(x => x.DocumentNo) == null ? 0:
            //_posInvoiceRepo.GetAllQueryable().Max(x => x.DocumentNo);

            List<SD_POS_InvoiceDetail> posDetailList = new List<SD_POS_InvoiceDetail>();
            request.POS_InvoiceDetail.ForEach(x =>
            {
                posDetailList.Add(new SD_POS_InvoiceDetail
                {

                    Seq = 1,
                    Qty = x.Qty,
                    Taxable = 0,
                    Tax = x.Tax,
                    Status = "",
                    BranchID = 1,
                    CompanyID = 1,
                    ItemID = x.Id,
                    AvgCost = x.AvgPrice,
                    Price = x.Selling_Price,
                    SubitemCode = x.SubItemCode,
                    Amount = x.Selling_Price * x.Qty,
                    DocumentNo = request.DocucmentNo
                });
            });
            SD_POS_Invoice pOS_Invoice = new SD_POS_Invoice
            {

                Net = request.Net,
                Tax = request.Tax,
                InvoiceTypeID = "",
                Paid = request.Paid,
                Cash = request.Cash,
                Card = request.Card,
                Void = request.Void,
                EmpId = _userSession.UserId,
                Round = request.Round,
                Refund = request.Refund,
                //DocumentNo = largetstDoc + 1,
                Taxable = request.Taxable,
                ActionDate = DateTime.Now,
                InvoiceDate = DateTime.Now,
                StatusID = request.StatusID,
                Discount = request.Discount,
                DocumentDate = DateTime.Now,
                CounterNo = request.CounterNo,
                InvoiceNo = DateTime.Now.ToString(),
                ModeOfPayment = request.ModeOfPayment,
                SD_POS_InvoiceDetail = posDetailList,

                ///Auto Generation 

                Qty = 0,
                Total = 0,
                BranchID = 1,
                CompanyID = 1,
                AccountNo = "",
                ApproveID = 0,
                ExpTypeID = 0,
                CardTypeID = 0,
                ActionTypeId = 1,
                ActionUserID = 1,
                DocumentTypeID = 1,



            };

            try
            {
                var enity = await _posInvoiceRepo.AddAsync(pOS_Invoice);
                var entityRecall = _posInvoiceRepo_template.GetAllQueryable()
                    .Include(x => x.SD_POS_InvoiceDetail)
                    .FirstOrDefault(e => e.DocumentNo == request.RefDocucmentNo);
                if (entityRecall != null)
                {
                    foreach (var item in entityRecall.SD_POS_InvoiceDetail)
                    {
                        await _posInvoiceDetail_template.DeleteAsync(item);

                    }
                    await _posInvoiceRepo_template.DeleteAsync(entityRecall);
                }
                return Result<POSSalesResonseDTO>.Success(new POSSalesResonseDTO
                {
                    DocNumber = enity.DocumentNo.ToString(),
                    QRString =

                    GetBasestring(
                    (decimal)(request.Tax + request.Taxable)
                    //(decimal)(request.Cash > request.Card ? request.Cash : request.Card)
                    , (decimal)request.Tax)
                }
                    );
            }
            catch (Exception ex)
            {
                return Result<POSSalesResonseDTO>.Failure();
            }
            return Result<POSSalesResonseDTO>.Failure();


        }

        public async Task<Result<bool>> RemoveRecallById(long RefDocucmentNo)
        {
            try
            {

                var entityRecall = _posInvoiceRepo_template.GetAllQueryable()
                .Include(x => x.SD_POS_InvoiceDetail)
                .FirstOrDefault(e => e.DocumentNo == RefDocucmentNo);
                if (entityRecall != null)
                {
                    foreach (var item in entityRecall.SD_POS_InvoiceDetail)
                    {
                        await _posInvoiceDetail_template.DeleteAsync(item);

                    }
                    await _posInvoiceRepo_template.DeleteAsync(entityRecall);
                }
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure();
            }
        }
        public async Task<Result<string>> HoldPOS(POSInvoiceDocReqDto request)
        {
            var largetstDoc = 1;

            List<SD_POS_InvoiceDetail_Template> posDetailList = new List<SD_POS_InvoiceDetail_Template>();
            request.POS_InvoiceDetail.ForEach(x =>
            {
                posDetailList.Add(new SD_POS_InvoiceDetail_Template
                {

                    Seq = 1,
                    Qty = x.Qty,
                    Taxable = 0,
                    Tax = x.Tax,
                    Status = "",
                    BranchID = 1,
                    CompanyID = 1,
                    ItemID = x.Id,
                    AvgCost = x.AvgPrice,
                    Price = x.Selling_Price,
                    SubitemCode = x.SubItemCode,
                    Amount = x.Selling_Price * x.Qty,
                });
            });
            SD_POS_Invoice_Template pOS_Invoice = new SD_POS_Invoice_Template
            {

                Net = request.Net,
                Tax = request.Tax,
                InvoiceTypeID = "",
                Paid = request.Paid,
                Cash = request.Cash,
                Card = request.Card,
                Void = request.Void,
                EmpId = _userSession.UserId,
                Round = request.Round,
                Refund = request.Refund,
                //DocumentNo = largetstDoc + 1,
                Taxable = request.Taxable,
                ActionDate = DateTime.Now,
                InvoiceDate = DateTime.Now,
                StatusID = request.StatusID,
                Discount = request.Discount,
                DocumentDate = DateTime.Now,
                CounterNo = request.CounterNo,
                InvoiceNo = DateTime.Now.ToString(),
                SD_POS_InvoiceDetail = posDetailList,
                RefDocumentNo = request.DocucmentNo,
                ModeOfPayment = request.ModeOfPayment,
                ///Auto Generation 

                Qty = 0,
                Total = 0,
                BranchID = 1,
                CompanyID = 1,
                AccountNo = "",
                ApproveID = 0,
                ExpTypeID = 0,
                CardTypeID = 0,
                ActionTypeId = 1,
                ActionUserID = 1,
                DocumentTypeID = 1


            };
            try
            {
                var enity = await _posInvoiceRepo_template.AddAsync(pOS_Invoice);
                return Result<string>.Success(GetBasestring(
                    (decimal)(request.Tax + request.Taxable)
                    , (decimal)request.Tax));
            }
            catch (Exception ex)
            {
                return Result<string>.Failure();
            }
            return Result<string>.Failure();

        }

        private PosPrinting MapAndGetRequiredData(SD_POS_Invoice request)
        {
            var posPrintingData = new PosPrinting();
            var DetailList = new List<ItemBilldetails>();

            request.SD_POS_InvoiceDetail.ToList().ForEach(x =>
            {
                DetailList.Add(
                    new ItemBilldetails()
                    {
                        ItemId = x.ItemID,
                        //Name = ,
                        SubItemcode = x.SubitemCode,
                        Qty = x.Qty,
                        Price = x.Price,
                        Tax = x.Tax,
                        Status = x.Status,
                        Id = x.Id,
                        TaxAMount = x.Tax,
                        ItemName = x.SubitemCode,
                        //Stock    =x.Stock ,
                        //TaxAmount=x.TaxAmount, 
                        //Discount =x.Discount ,
                        //Unit     =x.Unit  ,
                        //EpAmount =x.EpAmount ,
                        //ImageUrl =x.ImageUrl,

                    }
                    );
            });
            var PrintBilldetails = new PrintBilldetails()
            {
                Net = request.Net,
                Tax = request.Tax,
                Paid = request.Paid,
                Cash = request.Cash,
                Card = request.Card,
                Void = request.Void,
                Round = request.Round,
                Refund = request.Refund,
                Taxable = request.Taxable,
                DocumentDate = DateTime.Now,
                Counter = request.CounterNo,
                LoginName = "",
                DocummentTime = request.DocumentDate.ToString(),
                TotalItems = request.SD_POS_InvoiceDetail.Count(),
                TotalQty = request.SD_POS_InvoiceDetail.Sum(x => x.Qty),
                Total = request.Total,
                //PaymentType = request.PaymentType,
                //SubTotal  
                //Base64string
                //CardTypeId

            };


            return posPrintingData;
        }

        public Result<string> lastBill()
        {
            long c = _posInvoiceRepo.GetAll().Max(x => x.DocumentNo) + 1;
            return Result<string>.Success(c.ToString());


        }

        public Result<string> GetQRCodeString(decimal Amount, decimal VatAmount)
        {
            return Result<string>.Success(GetBasestring(Amount, VatAmount));
        }


        private string GetBasestring(decimal Amount, decimal VatAmount)
        {

            //var sellername = _configuration["QRValues:SellerName"];
            var vatregistration = "";
            try
            {
                var sellername = @"محمد حمد علي النعمي";              //var sellername = "testing seller name";
                                                                      //// var sellername = "Salah Hospital";
                                                                      ////var vatregistration = "310122393500003";
                                                                      //var timestamp = System.DateTime.Now;
                                                                      //var invoiceamount = Amount;
                                                                      //var vatamoun = VatAmount;


                //   var sellername = "testing seller name";

                vatregistration = "311460059600003";
                //var timestamp = "2021-11-17 08:30:00";
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var invoiceamount = Amount;
                var vatamoun = VatAmount;

                var basestring = getBase64(sellername, vatregistration, timestamp, Convert.ToString(invoiceamount), Convert.ToString(vatamoun));

                return basestring;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private String getBase64(String sellername, String vatregistration, String timestamp, String invoiceamount,
                                     String vatamoun)
        {
            string ltr = ((char)0x200E).ToString();
            var seller = getTlvVAlue("1", sellername);
            var vatno = getTlvVAlue("2", vatregistration);
            var time = getTlvVAlue("3", timestamp);
            var invamt = getTlvVAlue("4", invoiceamount);
            var vatamt = getTlvVAlue("5", vatamoun);
            var result = seller.Concat(vatno).Concat(time).Concat(invamt).Concat(vatamt).ToArray();
            //Console.WriteLine(result);

            //Console.WriteLine(result.ToString());

            var output = Convert.ToBase64String(result);
            Console.WriteLine(output);
            return output;
        }

        private byte[] getTlvVAlue(String tagnums, String tagvalue)
        {
            string[] tagnums_array = { tagnums };
            var tagvalue1 = tagvalue;

            var tagnum = tagnums_array.Select(s => Byte.Parse(s)).ToArray();



            var tagvalueb = Encoding.UTF8.GetBytes(tagvalue1);
            string[] taglengths = { tagvalueb.Length.ToString() };
            var tagvaluelengths = taglengths.Select(s => Byte.Parse(s)).ToArray();
            var tlvVAlue = tagnum.Concat(tagvaluelengths).Concat(tagvalueb).ToArray();


            return tlvVAlue;
        }

        public Result<List<RecallList>> GetRecallList()
        {
            var currentUSer = _userSession.UserId;
            var respon = _posInvoiceRepo_template.GetAllQueryable()
                .Where(e => e.EmpId == currentUSer)
                .Select(x => new RecallList
                {
                    DateTime = x.DocumentDate,
                    DocNumber = x.DocumentNo,
                    RefDocNumber = x.RefDocumentNo,
                    Amount = x.Tax + x.Taxable,
                }).OrderByDescending(x => x.DateTime).ToList();
            return Result<List<RecallList>>.Success(respon);
        }

        #region Reports 

        #region UserRepot 

        public async Task<Result<List<POSUserReport>>> GetUserReport(ReportFilters filters)
        {

            var c = filters.EndDate.ToLocalTime();
            var userlist = _userRepo.GetAllQueryable()
            //    .Select(x => new { 
            //    x.UserName,
            //    x.Id,
            //})
                .ToList();

            var response = _posInvoiceRepo.GetAllQueryable()
                 .Where(x => x.DocumentDate.Value.Date >= filters.StartDate.ToLocalTime().Date
                     && x.DocumentDate.Value.Date <= filters.EndDate.ToLocalTime().Date
                )
                .GroupBy(x => x.EmpId)
                .Select(x => new POSUserReport
                {
                    UserName = "-",
                    UserId = x.Key,
                    Cash_Sale = (double)(x.Sum(e => (e.ModeOfPayment == "A" ? (e.Tax + e.Taxable) : 0))),
                    Cash_SDiscount = (double)(x.Sum(e => (e.ModeOfPayment == "A" ? (e.Discount) : 0))),
                    Cash_SNet = (double)(x.Sum(e => (e.ModeOfPayment == "A" ? (e.Net) : 0))),


                    Card_Sale = (double)(x.Sum(e => (e.ModeOfPayment == "B" ? (e.Tax + e.Taxable) : 0))),
                    Card_Discount = (double)(x.Sum(e => (e.ModeOfPayment == "B" ? (e.Discount) : 0))),
                    Card_Net = (double)(x.Sum(e => (e.ModeOfPayment == "B" ? (e.Net) : 0))),


                    CashNCard_Sale = (double)(x.Sum(e => (e.ModeOfPayment == "C" ? (e.Tax + e.Taxable) : 0))),
                    CashNCard_Discount = (double)(x.Sum(e => (e.ModeOfPayment == "C" ? (e.Discount) : 0))),
                    CashNCard_Net = (double)(x.Sum(e => (e.ModeOfPayment == "C" ? ((e.Tax + e.Taxable) - (e.Discount)) : 0))),

                    Total = 0,
                    InvoicNo = x.Count().ToString(),
                    Bkt = 0,
                    Void = (double)x.Sum(x => x.Void),
                    Hold = 0,
                })
                .ToList();

            var totalRow = new POSUserReport();
            foreach (var item in response)
            {
                item.UserName = userlist.FirstOrDefault(e => e.Id == item.UserId) == null ? "--" : userlist.FirstOrDefault(e => e.Id == item.UserId).FullName;
                item.Card_Net = item.Card_Sale - item.Card_Discount;
                item.Cash_SNet = item.Cash_Sale - item.Cash_SDiscount;


                /// total row // 
                totalRow.UserName = "Total";
                totalRow.Card_Net = totalRow.Card_Net + item.Card_Net;
                totalRow.Card_Sale = totalRow.Card_Sale + item.Card_Sale;
                totalRow.Card_Discount = totalRow.Card_Discount + item.Card_Discount;
                totalRow.Cash_SDiscount = totalRow.Cash_SDiscount + item.Cash_SDiscount;
                totalRow.Cash_Sale = totalRow.Cash_Sale + item.Cash_Sale;
                totalRow.Cash_SNet = totalRow.Cash_SNet + item.Cash_SNet;
                totalRow.Bkt = totalRow.Bkt + item.Bkt;
                totalRow.Hold = totalRow.Hold + item.Hold;
                totalRow.Void = totalRow.Void + item.Void;


                totalRow.CashNCard_Sale = totalRow.CashNCard_Sale + item.CashNCard_Sale;
                totalRow.CashNCard_Discount = totalRow.CashNCard_Discount + item.CashNCard_Discount;
                totalRow.CashNCard_Net = totalRow.CashNCard_Net + item.CashNCard_Net;




            }
            totalRow.IsTotal = true;
            response.Add(totalRow);
            var resp = Result<List<POSUserReport>>.Success(response);
            return resp;
        }
        #endregion

        #region categoryRepot 

        public async Task<Result<POSCategoryReportWithTotal>> GetCategoryWiseReport(ReportFilters filters)
        {
            var d = filters.StartDate.ToLocalTime().Date;

            var response = _posInvoiceRepo.GetAllQueryable().Include(e => e.SD_POS_InvoiceDetail).Where(x =>
                     x.DocumentDate.Value.Date >= filters.StartDate.ToLocalTime().Date
                     && x.DocumentDate.Value.Date <= filters.EndDate.ToLocalTime().Date
                ).Select(x => new CategoryGroup
                {
                    Date = x.DocumentDate.Value.Date,
                    DocNO = x.DocumentNo,
                    Items = x.SD_POS_InvoiceDetail.Where(i => i.SubItemCode.CatId == filters.CategoryId)
                    .Select(i => new POSCategoryReport
                    {

                        ItemCode = i.SubItemCode.SubItemCode,
                        ItemName = i.SubItemCode.Item.NameEng,
                        Qty = i.Qty,
                        Price = i.Price,
                        Amount = i.Qty * i.Price,
                        Cash = x.ModeOfPayment == "A" ? i.Amount : 0,
                        Return = 0,
                        Credit = x.ModeOfPayment == "B" ? i.Amount : 0,
                        CashNCredit = x.ModeOfPayment == "C" ? i.Amount : 0,
                        Vat = i.Qty * i.Price / 100 * 15,
                        CateName = i.SubItemCode.Category.NameAra,
                    }).ToList(),
                    Amount = x.SD_POS_InvoiceDetail.Where(i => i.SubItemCode.CatId == filters.CategoryId).Sum(e => (e.Qty * e.Price)),
                }).Where(e => e.Items.Any()).ToList();

            //var response = _posDetailRepo.GetAllQueryable()
            //    .Where(x => x.SubItemCode.CatId == filters.CategoryId
            //        && x.SD_POS_Invoice.DocumentDate.Value.Date >= filters.StartDate.ToLocalTime().Date
            //         && x.SD_POS_Invoice.DocumentDate.Value.Date <= filters.EndDate.ToLocalTime().Date
            //    )
            //    .Select(x => new POSCategoryReport
            //    {
            //        Date = x.SD_POS_Invoice.DocumentDate,
            //        DocNO = x.SD_POS_Invoice.DocumentNo,
            //        ItemCode = x.SubItemCode.SubItemCode,
            //        ItemName = x.SubItemCode.Item.NameEng,
            //        Qty = x.Qty,
            //        Price = x.Price,
            //        Amount = x.Qty * x.Price,
            //        Cash = x.SD_POS_Invoice.ModeOfPayment == "A" ? x.Amount : 0,
            //        Return = 0,
            //        Credit = x.SD_POS_Invoice.ModeOfPayment == "B" ? x.Amount : 0,
            //        CashNCredit = x.SD_POS_Invoice.ModeOfPayment == "C" ? x.Amount : 0,
            //        Vat = x.Qty * x.Price / 100 * 15,
            //        CateName = x.SubItemCode.Category.NameAra

            //    })
            //    .ToList();

            var total = new POSCategoryReport
            {
                Qty = response.Sum(x => x.Items.Sum(e => e.Qty)),
                Price = response.Sum(x => x.Items.Sum(e => e.Price)),
                Amount = response.Sum(x => x.Items.Sum(e => e.Amount)),
                Cash = response.Sum(x => x.Items.Sum(e => e.Cash)),
                Return = 0,
                Credit = response.Sum(x => x.Items.Sum(e => e.Credit)),
                CashNCredit = response.Sum(x => x.Items.Sum(e => e.CashNCredit)),
                Vat = response.Sum(x => x.Items.Sum(e => e.Vat))
            };

            var responsWithTotal = new POSCategoryReportWithTotal()
            {
                GroupItems = response,
                Total = total,
                Title = ""
            };


            var resp = Result<POSCategoryReportWithTotal>.Success(responsWithTotal);
            return resp;
        }

        #endregion

        #region Total Report

        public async Task<Result<TotalSalesReportDTO>> GetTotalSalesReport(ReportFilters filters)
        {
            var d = filters.StartDate.ToLocalTime().Date;


            var response = _posInvoiceRepo.GetAllQueryable()
                .Where(x => x.DocumentDate.Value.Date >= filters.StartDate.ToLocalTime().Date
                     && x.DocumentDate.Value.Date <= filters.EndDate.ToLocalTime().Date
                )
                .Select(x => new POSSalesReportDTO
                {
                    Date = x.DocumentDate,
                    DocNO = x.DocumentNo,
                    CustomerName = x.ModeOfPayment == "A" ? "Cash" : (x.ModeOfPayment == "B" ? "Card" : "Cash + Card "),
                    Cash_Amount = x.ModeOfPayment == "A" ? (x.Tax + x.Taxable) : 0,
                    Cash_Discount = x.Discount,
                    //Cash_Discount = x.ModeOfPayment == "A" ? (x.Discount) : 0,
                    Cash_Net = x.ModeOfPayment == "A" ? ((x.Tax + x.Taxable) - x.Discount) : 0,

                    Card_Amount = x.ModeOfPayment == "B" ? (x.Tax + x.Taxable) : 0,
                    Card_Discount = x.ModeOfPayment == "B" ? (x.Discount) : 0,
                    Card_Net = x.ModeOfPayment == "B" ? ((x.Tax + x.Taxable) - x.Discount) : 0,

                    CashNCard_Amount = x.ModeOfPayment == "C" ? (x.Tax + x.Taxable) : 0,
                    CashNCard_Discount = x.ModeOfPayment == "C" ? (x.Discount) : 0,
                    CashNCard_Net = x.ModeOfPayment == "C" ? ((x.Tax + x.Taxable) - x.Discount) : 0,


                    Total = (x.Tax + x.Taxable)
                })
                .ToList();

            var total = new POSSalesReportDTO
            {
                CustomerName = "--",
                Cash_Amount = response.Sum(x => x.Cash_Amount),
                Cash_Discount = response.Sum(x => x.Cash_Discount),
                Cash_Net = response.Sum(x => x.Cash_Net),
                Card_Amount = response.Sum(x => x.Card_Amount),
                Card_Discount = response.Sum(x => x.Card_Discount),
                Card_Net = response.Sum(x => x.Card_Net),
                CashNCard_Amount = response.Sum(x => x.CashNCard_Amount),
                Total = response.Sum(x => x.Total),

            };

            var responsWithTotal = new TotalSalesReportDTO()
            {
                Items = response,
                Total = total,
                Title = "",
            };


            var resp = Result<TotalSalesReportDTO>.Success(responsWithTotal);
            return resp;
        }


        #endregion
        #endregion

        #region validation
        private bool isDocumentExistAlready(POSInvoiceDocReqDto request)
        {
            var maxdoc = _posInvoiceRepo.GetAllQueryable().Max(e => e.DocumentNo);
            
            var response = false;
            int marginalMinits = 10;
            var latest = _posInvoiceRepo.GetAllQueryable().Include(x => x.SD_POS_InvoiceDetail).Where(x =>
            ((maxdoc - x.DocumentNo) < marginalMinits)
            &&
            (EF.Functions.DateDiffMinute(DateTime.Now.TimeOfDay, x.DocumentDate.Value.TimeOfDay) < marginalMinits)
            && x.Paid == request.Paid && x.Discount == request.Discount
            //&& x.Taxable = request.Taxable
            && x.SD_POS_InvoiceDetail.Count() == request.POS_InvoiceDetail.Count
            ).ToList();

            if (!latest.Any())
            {
                return false;
            }
            foreach (var list in latest)
            {
                int couter = 0;
                int mtches = 0;
                foreach (var item in list.SD_POS_InvoiceDetail.ToList())
                {
                    if (request.POS_InvoiceDetail[couter].SubItemCode == item.SubitemCode)
                    {
                        mtches++;
                    }
                    couter++;
                }
                if (mtches == couter)
                {
                    return true;
                }

                if (response)
                {
                    return response;
                }
            }


            return response;
        }
        #endregion


        #region Purchase 

        public async Task<Result<string>> PostPOSPurchaseInvoice(PurchaseInvoiceDto request)
        {
            return Result<string>.Failure();
        }

        public async Task<Result<List<PurcahseListDto>>> GetPurchaseInvoiceList(PurchaseListFilter request)
        {
            var resp = new List<PurcahseListDto>
            {
                new PurcahseListDto
                {
                    Amount= 500,
                    InvoiceDate = DateTime.Now,
                    InvoiceNumber= "165114",
                    Id = 99
                },
                new PurcahseListDto
                {
                    Amount= 457,
                    InvoiceDate = DateTime.Now,
                    InvoiceNumber= "47871",
                    Id = 99
                },
                new PurcahseListDto
                {
                    Amount= 250,
                    InvoiceDate = DateTime.Now,
                    InvoiceNumber= "22541",
                    Id = 99
                },
                new PurcahseListDto
                {
                    Amount= 710,
                    InvoiceDate = DateTime.Now,
                    InvoiceNumber= "55104",
                    Id = 99
                }
            };
            return Result<List<PurcahseListDto>>.Success(resp);
        }

        #endregion Purchase voucher Ends here 

    }
}
