using DAO.PlanningPortal.Application.Interfaces.Inventory;
using DAO.PlanningPortal.Common.Sessions;
using Microsoft.EntityFrameworkCore;
using POSERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zero.Application.Shared.Models.Inventory;
using zero.Shared.Models.GDPRAccess;
using zero.Shared.Models.POS;
using zero.Shared.Repositories;
using zero.Shared.Response;

namespace DAO.PlanningPortal.Application.Services.Inventory
{
    public class ItemMasterService : IItemMasterService
    {

        private readonly IUserSession _userSession;
        private readonly IGenericRepositoryAsync<INV_MSD_Brand> _brandReop;
        private readonly IGenericRepositoryAsync<INV_MSD_Color> _colorRepo;
        private readonly IGenericRepositoryAsync<INV_MSD_Model> _modelRepo;
        private readonly IGenericRepositoryAsync<INV_MSD_Item> _parentItemRepo;
        private readonly IGenericRepositoryAsync<INV_MSD_SubItemCode> _subItemsRepo;
        private readonly IGenericRepositoryAsync<INV_MSD_Department> _departmetnRep;
        private readonly IGenericRepositoryAsync<INV_MSD_Category> _categoryRep;
        private readonly IGenericRepositoryAsync<INV_MSD_PackingSize> _packingSize;



        public ItemMasterService(
            IUserSession userSession,
            IGenericRepositoryAsync<INV_MSD_Brand> brandReop,
            IGenericRepositoryAsync<INV_MSD_Color> colorRepo,
            IGenericRepositoryAsync<INV_MSD_Model> modelRepo,
            IGenericRepositoryAsync<INV_MSD_Item> parentItemRepo,
            IGenericRepositoryAsync<INV_MSD_Department> departmetnRepo,
            IGenericRepositoryAsync<INV_MSD_Category> categoryRep,
            IGenericRepositoryAsync<INV_MSD_PackingSize> packingSize,
        IGenericRepositoryAsync<INV_MSD_SubItemCode> subItemsRepo)
        {
            _colorRepo = colorRepo;
            _brandReop = brandReop;
            _modelRepo = modelRepo;
            _userSession = userSession;
            _subItemsRepo = subItemsRepo;
            _parentItemRepo = parentItemRepo;
            _departmetnRep = departmetnRepo;
            _categoryRep = categoryRep;
            _packingSize = packingSize;
        }
        public async Task<Result<List<ItemMasterDTO>>> GetAll(ItemMasterReqLazyLoad req)
        {
            //resetData();
            var response = _subItemsRepo.GetAllQueryable().Where(x =>
         (x.SubItemCode == req.ShortEng)
          //||         x.ShortEng.ToUpper().Contains(req.ShortEng.ToUpper().Trim())
          || String.IsNullOrEmpty(req.ShortEng)
         ).Include(x => x.Item).ThenInclude(x => x.Category)
         .OrderByDescending(x => x.ItemId)
             .Take(20)
            .Select(x => new ItemMasterDTO
            {
                ItemId = x.ItemId,
                ShortEng = x.ShortEng,
                SubItemCode = x.SubItemCode,
                Selling_Price = x.Selling_Price,
                Department = x.Category.NameEng,
                Rating = x.Item.Rating,
                IsActive = x.IsActive,
                IsAproved = x.Item.ApprvTypeId > 0,
                Cost_Price = x.CostPrice

            }).ToList();
            return Result<List<ItemMasterDTO>>.Success(response);
        }

        public async Task<Result<ItemMasterCreationDTO>> Post(ItemMasterCreationDTO req)
        {
            var SubItemCodeList = new List<INV_MSD_SubItemCode>();
            req.SubItems.ForEach(x =>
            {
                SubItemCodeList.Add(
                new INV_MSD_SubItemCode
                {
                    Id = x.Id,
                    Seq = x.Seq,
                    Qty = x.Qty,
                    Tax = x.Tax,
                    UnitID = x.UnitID,
                    ShortEng = x.ShortEng,
                    AvgPrice = x.AvgPrice,
                    IsActive = x.IsActive,
                    EstProfit = x.EstProfit,
                    CostPrice = x.CostPrice,
                    SubItemCode = x.SubItemCode,
                    Selling_Price = x.Selling_Price,
                    CatId = x.Category,
                    ShortArb = x.ShortArb,
                    Stock = x.Stock,
                    TaxAmount = x.TaxAmount,
                    Discount = x.Discount,
                    Unit = x.Unit,
                    EpAmount = x.EpAmount,
                    ImageUrl = x.ImageUrl,
                }
                        );
            });
            var entity = new INV_MSD_Item
            {
                Id = req.ItemId,
                ItemCode = req.ItemCode,
                NameEng = req.ItemName,
                ShortEng = req.ShortName,
                Rating = req.Rating,
                IsDeafult = req.Default,
                IsExpiration = req.Expiration,
                ReminderDays = req.ReminderDays.ToString(),
                PackiseSize = req.PackingSize.ToString(),
                DeptId = req.Department,
                CatId = req.Category,
                SubCatId = req.SubCategory,
                BrandId = req.Brand,
                ModelId = req.Model,
                ColorId = req.Color,
                ItemTypeID = req.ItemType,
                StockMethodID = req.StockMethod,
                TaxPer = req.Tax,
                PackingSizeID = req.PackingSize,
                EstProfitPer = req.EstimationProfit,
                IsPerishable = req.Perishable,
                IsConsignment = req.Consignment,
                //LastActived = req.LastActive,
                //CreatedOn = req.CreatedOn,
                //LastModified = req.LastModified,
                //LastDelted = req.LastDeleted,
                ApprvUserId = req.ApprovedBy,
                //ApprvDate = req.ApprovedOn,
                //RejectedOn = req.RejectedOn,
                //ApprovedType = req.ApprovedType,
                INV_MSD_SubItems = SubItemCodeList,
                                       
            };

            try
            {
                var res = await _parentItemRepo.AddAsync(entity);

            }
            catch (Exception ex)
            {


            }
            return Result<ItemMasterCreationDTO>.Success(req);
        }

        public async Task<Result<ItemMasterCreationDTO>> Put(ItemMasterCreationDTO req)
        {
            var subItmes = new List<INV_MSD_SubItemCode>();

            var toRemove = _subItemsRepo.GetAll().Where(x => x.ItemId == req.ItemId && !req.SubItems.Select(e => e.Id).Contains(x.Id)).ToList();
            foreach (var subItem in toRemove)
            {
                await _subItemsRepo.DeleteAsync(subItem);
            }
            foreach (var x in req.SubItems)
            {

                var suenitty = _subItemsRepo.GetAllQueryable().FirstOrDefault(e => e.Id == x.Id);
                if (suenitty != null)
                {
                    suenitty.CatId = x.Category;
                    suenitty.Seq = x.Seq;
                    suenitty.Qty = x.Qty;
                    suenitty.Tax = x.Tax;
                    suenitty.UnitID = x.UnitID;
                    suenitty.ShortEng = x.ShortEng;
                    suenitty.ShortArb = x.ShortArb;
                    suenitty.AvgPrice = x.AvgPrice;
                    suenitty.IsActive = x.IsActive;
                    suenitty.EstProfit = x.EstProfit;
                    suenitty.CostPrice = x.CostPrice;
                    suenitty.SubItemCode = x.SubItemCode;
                    suenitty.Selling_Price = x.Selling_Price;
                     
                    await _subItemsRepo.UpdateAsync(suenitty);

                }
                else
                {
                    var newEntity = new INV_MSD_SubItemCode
                    {

                        Seq = x.Seq,
                        Qty = x.Qty,
                        Tax = x.Tax,
                        UnitID = x.UnitID,
                        ShortEng = x.ShortEng,
                        AvgPrice = x.AvgPrice,
                        IsActive = x.IsActive,
                        EstProfit = x.EstProfit,
                        CostPrice = x.CostPrice,
                        SubItemCode = x.SubItemCode,
                        Selling_Price = x.Selling_Price,
                        ItemId = req.ItemId,
                        CatId = x.Category,
                        Stock = x.Stock,
                        TaxAmount = x.TaxAmount,
                        Discount = x.Discount,
                        Unit = x.Unit,
                        EpAmount = x.EpAmount,
                        ImageUrl = x.ImageUrl,

                    };
                    await _subItemsRepo.AddAsync(newEntity);
                }
            }

            var entity = _parentItemRepo.GetAllQueryable().FirstOrDefault(x => x.Id == req.ItemId);

            entity.ItemCode = req.ItemCode;
            entity.NameEng = req.ItemName;
            entity.ShortEng = req.ShortName;
            entity.Rating = req.Rating;
            entity.IsDeafult = req.Default;
            entity.IsExpiration = req.Expiration;
            entity.ReminderDays = req.ReminderDays.ToString();
            entity.PackiseSize = req.PackingSize.ToString();
            entity.DeptId = req.Department;
            entity.CatId = req.Category;
            entity.SubCatId = req.SubCategory;
            entity.BrandId = req.Brand;
            entity.ModelId = req.Model;
            entity.ColorId = req.Color;
            entity.ItemTypeID = req.ItemType;
            entity.StockMethodID = req.StockMethod;
            entity.TaxPer = req.Tax;
            entity.EstProfitPer = req.EstimationProfit;
            entity.IsPerishable = req.Perishable;
            entity.IsConsignment = req.Consignment;
            entity.PackingSizeID = req.PackingSize;
            //entity.LastActived = req.LastActive;
            //entity.CreatedOn = req.CreatedOn;
            //entity.LastModified = req.LastModified;
            //entity.LastDelted = req.LastDeleted;
            entity.ApprvUserId = req.ApprovedBy;
            //entity.ApprvDate = req.ApprovedOn;
            //RejectedOn = req.RejectedOn,
            //ApprovedType = req.ApprovedType, 

            //entity.INV_MSD_SubItems = subItmes;

            try
            {
                await _parentItemRepo.UpdateAsync(entity);

            }
            catch (Exception ex)
            {

            }
            return Result<ItemMasterCreationDTO>.Success(req);
        }

        public async Task<Result<ItemMasterCreationDTO>> GetById(long Id)
        {
            var response = _parentItemRepo.GetAllQueryable()
                .Include(x => x.INV_MSD_SubItems)
                .FirstOrDefault(x => x.Id == Id);
            return Result<ItemMasterCreationDTO>.Success(mapEntityTOModel(response));
        }
        public async Task<Result<bool>> ItemExists(ItemExistValidationRequestDto item)
        {
            var subItemexist = _subItemsRepo.GetAllQueryable().Any(e => e.Id != item.Id && e.SubItemCode == item.Barcode);
            //var  itemexist =    _parentItemRepo.GetAllQueryable().Any(e => e.Id != item.Id && e.su == item.Barcode);

            return Result<bool>.Success(subItemexist);


        }

        #region Lookup

        public async Task<Result<List<LookupDto>>> GetLookupDepartment()
        {
            var req =
                _departmetnRep.GetAll().Select(x => new LookupDto
                {
                    Key = x.Id.ToString(),
                    Title = x.NameEng
                }).ToList();
            return Result<List<LookupDto>>.Success(req);

        }


        public async Task<Result<List<LookupDto>>> GetLookupPaking()
        {
            var req = _packingSize.GetAllQueryable().Select(x => new LookupDto
            {
                Key = x.Id.ToString(),
                Title = x.NameEng,

            }).ToList();
            return Result<List<LookupDto>>.Success(req);

        }
        public async Task<Result<List<LookupDto>>> GetLookupCategory()
        {
            var req = new List<LookupDto>() {
               new LookupDto
               {
                   Key = "1", Title ="Cat 1"
               },  new LookupDto()
               {
                   Key = "2", Title ="Cat 2"
               }, new LookupDto()
               {
                     Key = "3", Title ="Catxxxxxxx"
               }

            };
            return Result<List<LookupDto>>.Success(req);
        }

        public async Task<Result<List<LookupDto>>> GetLookupSubCategory()
        {
            var req = _categoryRep.GetAllQueryable().Select(x => new LookupDto
            {
                Key = x.Id.ToString(),
                Title = x.NameEng,

            }).ToList();
            return Result<List<LookupDto>>.Success(req);
        }


        public async Task<Result<List<LookupDto>>> GetLookupBrand()
        {
            var req = _brandReop.GetAllQueryable().Select(x => new LookupDto
            {
                Key = x.Id.ToString(),
                Title = x.NameEng,

            }).ToList();
            //var req = new List<LookupDto>() {
            //   new LookupDto
            //   {
            //       Key = "1", Title ="Brand 1"
            //   },  new LookupDto()
            //   {
            //       Key = "2", Title ="Brand 2"
            //   }, new LookupDto()
            //   {
            //         Key = "3", Title ="Brand 3 "
            //   } };
            return Result<List<LookupDto>>.Success(req);
        }

        public async Task<Result<List<LookupDto>>> GetLookupPakingSize()
        {
            //var req = _brandReop.GetAllQueryable().Select(x => new LookupDto
            //{
            //    Key = x.Id.ToString(),
            //    Title = x.NameEng,

            //}).ToList();
            var req = new List<LookupDto>() {
               new LookupDto
               {
                   Key = "1", Title ="Dz"
               },  new LookupDto()
               {
                   Key = "2", Title ="Pak of 6"
               }, new LookupDto()
               {
                     Key = "3", Title ="Pak of 10"
               } };
            return Result<List<LookupDto>>.Success(req);
        }

        public async Task<Result<List<LookupDto>>> GetLookupModel()
        {
            var req = _modelRepo.GetAllQueryable().Select(x => new LookupDto
            {
                Key = x.Id.ToString(),
                Title = x.NameEng
            }).ToList();

            //var req = new List<LookupDto>() {
            //   new LookupDto
            //   {
            //       Key = "1", Title ="Model 1"
            //   },  new LookupDto()
            //   {
            //       Key = "2", Title ="Model 2"
            //   }, new LookupDto()
            //   {
            //         Key = "3", Title ="Model 3 "
            //   } };
            return Result<List<LookupDto>>.Success(req);
        }

        public async Task<Result<List<LookupDto>>> GetLookupColor()
        {
            var req = _colorRepo.GetAllQueryable().Select(x => new LookupDto
            {
                Key = x.Id.ToString(),
                Title = x.NameEng,

            }).ToList();

            //var req = new List<LookupDto>() {
            //   new LookupDto
            //   {
            //       Key = "1", Title ="Black"
            //   },  new LookupDto()
            //   {
            //       Key = "2", Title ="Red"
            //   }, new LookupDto()
            //   {
            //         Key = "3", Title ="Blue"
            //   },
            //      new LookupDto() {
            //         Key = "4", Title ="Green"
            //   }
            //};
            return Result<List<LookupDto>>.Success(req);
        }




        #endregion



        #region mappers 
        private ItemMasterCreationDTO mapEntityTOModel(INV_MSD_Item entity)
        {

            var subItem = new List<subItemDTO>();
            foreach (var item in entity.INV_MSD_SubItems)
            {
                try
                {
                    subItem.Add(
                        new subItemDTO
                        {
                            Id = item.Id,
                            Seq = item.Seq,
                            Qty = item.Qty,
                            Tax = item.Tax,
                            UnitID = item.UnitID,
                            IsActive = item.IsActive,
                            AvgPrice = item.AvgPrice,
                            ShortArb = item.ShortArb,
                            ShortEng = item.ShortEng,
                            CostPrice = item.CostPrice,
                            EstProfit = item.EstProfit,
                            Category = item.CatId != null ? (int)item.CatId : 0,
                            SubItemCode = item.SubItemCode,
                            Selling_Price = item.Selling_Price,
                            Discount = item.Discount,
                            Stock = item.Stock,

                        });

                }
                catch (Exception ex) { }
            }
            int temp = 0;
            try
            {
                var resp = new ItemMasterCreationDTO
                {

                    ItemId = entity.Id,
                    ItemCode = entity.ItemCode,
                    ItemName = entity.NameEng,
                    ShortName = entity.ShortEng,
                    Rating = entity.Rating,
                    Default = (bool)entity.IsDeafult,
                    Expiration = (bool)entity.IsExpiration,
                    PackingSize = (int)entity.PackingSizeID,
                    //!= null ? (Int32.TryParse(entity.PackingSizeID, out temp) ? temp: 0) : 0,



                    Department = (int)entity.DeptId,
                    Category = (int)entity.CatId,
                    SubCategory = (int)entity.SubCatId,
                    Brand = (int)entity.BrandId,
                    Model = (int)entity.ModelId,
                    Color = (int)entity.ColorId,
                    Perishable = (bool)entity.IsPerishable,
                    Consignment = (bool)entity.IsConsignment,



                    ItemType = (int)entity.ItemTypeID,
                    StockMethod = (int)entity.StockMethodID,
                    EstimationProfit = (decimal)entity.EstProfitPer,
                    Tax = (decimal)entity.TaxPer,
                    ReminderDays = entity.ReminderDays,
                    //ApprovedOn = (DateTime)entity.ApprvDate,

                    SubItems = subItem



                    // LastDeleted = (DateTime)entity.LastDelted,
                    //  ApprovedType = (int)entity.ApprvTypeId,
                    //ApprovedBy = entity.ApprovedBy,
                    //LastActive = (DateTime)entity.LastActived,
                    //CreatedOn = (DateTime)entity.CreatedOn,
                    // LastModified = (DateTime)entity.LastModified,
                };
                return resp;
            }
            catch (Exception ex)
            {

            }
            return new ItemMasterCreationDTO();

        }



        //private void resetData()
        //{
        //    var listofItems = _parentItemRepo.GetAll().Select(x => new
        //    {
        //        x.ShortAra,
        //        x.Id
        //    }).ToList();
        //    foreach (var item in listofItems)
        //    {
        //        var entitylist = _subItemsRepo.GetAll().Where(x => x.ItemId == item.Id).ToList();
        //        foreach (var ent in entitylist)
        //        {
        //            try
        //            {
        //            ent.ShortArb = item.ShortAra;
        //            _subItemsRepo.UpdateAsync(ent);

        //            }catch (Exception ex) {

        //            }
        //        }

        //    }
        //}


        #endregion

    }
}
