using DAO.PlanningPortal.Application.Interfaces.Inventory;
using zero.Shared.Models.Dashboard;
using zero.Shared.Models.GDPRAccess;
using zero.Shared.Models.Inventory;
using zero.Shared.Repositories;
using zero.Shared.Response;
using DAO.PlanningPortal.Common.Sessions;
using DAO.PlanningPortal.Domain.Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Application.Services.Inventory
{
    public class Inventory : IInventoryService
    {
        private readonly IGenericRepositoryAsync<Item> _itemRepositoryAsync;
        private readonly IGenericRepositoryAsync<ItemBatch> _batchRepositoryAsync;
        private readonly IGenericRepositoryAsync<ItemGroup> _itemGroupRepositoryAsync;
        private readonly IUserSession _userSession;
        public Inventory(
            IUserSession userSession,
            IGenericRepositoryAsync<Item> itemRepositoryAsync,
            IGenericRepositoryAsync<ItemBatch> batchRepositoryAsync,
            IGenericRepositoryAsync<ItemGroup> itemGroupRepositoryAsync
        )
        {
            _userSession = userSession;
            _itemRepositoryAsync = itemRepositoryAsync;
            _batchRepositoryAsync = batchRepositoryAsync;
            _itemGroupRepositoryAsync = itemGroupRepositoryAsync;
        }



        #region

        public async Task<Result<long>> Batch_AddEdit(BatchDto parameter)
        {
            var entity = new ItemBatch
            {
                ParentID = parameter.ParentID,
                Title = parameter.Title
            };
            var response = await _batchRepositoryAsync.AddAsync(entity);
            return Result<long>.Success(response.Id);
        }
        public async Task<Result<long>> Batch_AssignUser(AssignBatchtoAgentDTO parameter)
        {
            var entity = _batchRepositoryAsync.GetAllQueryable().FirstOrDefault(x => x.Id == parameter.Id);

            entity.UserId = parameter.AssignedTo;

            await _batchRepositoryAsync.UpdateAsync(entity);
            return Result<long>.Success(parameter.Id);
        }

        public Task<Result<bool>> Batch_Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<BatchDto>> Batch_GetBYID(long Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<BatchDto>>> Batch_GetList()
        {
            var response = _batchRepositoryAsync.GetAllQueryable()
                 .Select(x => new BatchDto
                 {
                     Id = x.Id,
                     ParentName = x.Parent.Title,
                     Title = x.Title,
                     AssignedName = x.Agent == null ? "" : x.Agent.FullName

                 }).ToList();
            return Result<List<BatchDto>>.Success(response);
        }
        public async Task<Result<List<BatchDto>>> GetAllAssignedtoMe()
        {
            var response = _batchRepositoryAsync.GetAllQueryable()
                .Where(x => x.UserId == _userSession.UserId)
                 .Select(x => new BatchDto
                 {

                     Id = x.Id,
                     ParentName = x.Parent.Title,
                     Title = x.Title,
                     AssignedName = x.Agent == null ? "" : x.Agent.FullName,
                     ContractId = x.Contracts.Any(e => e.isActive) ? x.Contracts.FirstOrDefault(e => e.isActive).Id : 0

                 }).ToList();
            return Result<List<BatchDto>>.Success(response);
        }
        public Task<Result<List<LookupDto>>> Batch_GetLookups()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Item Group
        public async Task<Result<long>> ItemGroup_AddEdit(ItemGroupDto parameter)
        {

            var item = new ItemGroup
            {
                ParentID = parameter.ParentID,
                Title = parameter.Title,
            };
            var response = await _itemGroupRepositoryAsync.AddAsync(item);

            return Result<long>.Success(response.Id);
        }

        public Task<Result<bool>> ItemGroup_Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<ItemGroupDto>> ItemGroup_GetBYID(long Id)
        {
            var response = _itemGroupRepositoryAsync.GetAllQueryable().Select(x => new ItemGroupDto
            {
                Title = x.Title,
                ParentTitle = x.Parent.Title,
            }).FirstOrDefault();
            return Result<ItemGroupDto>.Success(response);
        }

        public async Task<Result<List<ItemGroupDto>>> ItemGroup_GetList()
        {
            var response = _itemGroupRepositoryAsync.GetAllQueryable().Select(x => new ItemGroupDto
            {
                Title = x.Title,
                ParentTitle = x.Parent.Title,

            }).ToList();

            return Result<List<ItemGroupDto>>.Success(response);
        }

        public async Task<Result<List<LookupDto>>> ItemGroup_GetLookups()
        {
            var response = _itemGroupRepositoryAsync.GetAllQueryable()
               .Select(x => new LookupDto
               {
                   Title = x.Title,
                   Key = x.Id.ToString()
               }).ToList();
            return Result<List<LookupDto>>.Success(response);
        }
        public async Task<Result<List<LookupDto>>> ItemGroupsChildOnly_GetLookups()
        {
            var response = _itemGroupRepositoryAsync.GetAllQueryable()
               .Where(x => x.ParentID != null)
               .Select(x => new LookupDto
               {
                   Title = x.Title,
                   Key = x.Id.ToString()
               }).ToList();
            return Result<List<LookupDto>>.Success(response);
        }
        #endregion

        #region Item CRUD 

        public async Task<Result<long>> Item_AddEdit(ItemDto parameter)
        {
            var entity = new Item
            {
                ParentID = parameter.ParentID,
                Title = parameter.Title,
                Descriptions = parameter.Descriptions,
                Batches = GetBatchesMapped(parameter.BatchList)
            };
            var k = await _itemRepositoryAsync.AddAsync(entity);
            return Result<long>.Success(k.Id);
        }

        public Task<Result<bool>> Item_Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<ItemDto>> Item_GetBYID(long id)
        {

            var reponse = _itemRepositoryAsync.GetAllQueryable()
                .Where(x => x.Id == id)
                .Select(x => new ItemDto
                {
                    ParentID = x.ParentID,
                    ParentName = x.Parent.Title,
                    Descriptions = x.Descriptions,
                    BatchList = x.Batches.Select(
                        p => new BatchDto
                        {
                            Title = p.Title,
                        }
                        ).ToList(),
                }).FirstOrDefault();
            return Result<ItemDto>.Success(reponse);

        }

        public async Task<Result<List<ItemDto>>> Item_GetList()
        {
            var reponse = _itemRepositoryAsync.GetAllQueryable()
                .Select(x => new ItemDto
                {
                    Title = x.Title,
                    ParentID = x.ParentID,
                    ParentName = x.Parent.Title,
                    Descriptions = x.Descriptions,
                    BatchList = x.Batches.Select(
                        p => new BatchDto
                        {
                            Title = p.Title,
                        }
                        ).ToList(),
                }).ToList();
            return Result<List<ItemDto>>.Success(reponse);
        }

        public async Task<Result<List<LookupDto>>> Item_GetLookups()
        {
            var response = _itemRepositoryAsync.GetAllQueryable()
             .Select(x => new LookupDto
             {
                 Title = x.Title,
                 Key = x.Id.ToString()
             }).ToList();
            return Result<List<LookupDto>>.Success(response);
        }

        public async Task<Result<List<LookupDto>>> Item_BatchGetLookups()
        {
            var response = _batchRepositoryAsync.GetAllQueryable()
             .Select(x => new LookupDto
             {
                 Title = x.Title + "(" + x.Parent.Title + ")",
                 Key = x.Id.ToString()
             }).ToList();

            return Result<List<LookupDto>>.Success(response);
        }

        #endregion

        #region privateItem Methods 
        private List<ItemBatch> GetBatchesMapped(List<BatchDto> batches)
        {
            var response = new List<ItemBatch>();
            batches.ForEach(batch =>
            {
                var batchDto = new ItemBatch
                {
                    Title = batch.Title
                };
                response.Add(batchDto);
            });

            return response;
        }
        #endregion



        #region  dashboard 
        public async Task<Result<List<WidgetDto>>> GetWidgets()
        {
            var response = new List<WidgetDto>();
            var qry = _itemRepositoryAsync.GetAllQueryable()
                 .Select(x => new { x.Parent.Title }).ToList();
            var totalCount = qry.Count();
            var transactionWidgets = qry.GroupBy(x => x.Title).ToList();
            foreach (var item in transactionWidgets)
            {
                var c = item.Key;
                var k = item.Count();
                var temp = new WidgetDto
                {
                    Description = "Inventory",
                    Size = "",
                    StylingClass = "",
                    Title = item.Key,
                    Type = "",
                    Value = (item.Count() * 100 / totalCount).ToString(),
                };
                response.Add(temp);
            }
            return Result<List<WidgetDto>>.Success(response);
        }

        #endregion

    }
}
