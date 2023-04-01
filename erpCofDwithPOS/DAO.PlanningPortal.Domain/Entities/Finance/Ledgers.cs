using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.PlanningPortal.Domain.Common.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.PlanningPortal.Domain.Entities.Finance
{


    #region Finance 
    /// <summary>
    /// / Customer  + Vender + Expenses 
    /// </summary>
    // Ledger Groupu 
    [Table(nameof(LedgerGroup))]
    public class LedgerGroup
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long? ParentId { get; set; }

        /// Auditable Entities 
        public bool IsDeleted { get; set; }
        /// Auditable Entities 
        /// 
        [ForeignKey("ParentId")]
        public virtual LedgerGroup Parent { get; set; }

        public ICollection<LedgerGroup> ChildLedgerGroups { get; set; }
        public ICollection<Ledger> ChildLedgers { get; set; }

    }
    [Table(nameof(Ledger))]
    public class Ledger
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Nature { get; set; }

        /// Auditable Entities 
        public bool IsDeleted { get; set; }
        /// Auditable Entities 

        /// Relations
        public int? Userkey { get; set; }
        [ForeignKey("Userkey")]
        public virtual ApplicationUser User { get; set; }
        public long? PersonalInfoID { get; set; }
        [ForeignKey("PersonalInfoID")]
        public virtual PersonalInfo PersonalInfo { get; set; }
        public long ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual LedgerGroup LedgerGroup { get; set; }
        public ICollection<Transaction> ChildLedgers { get; set; }
        /// Relations 
    }

    [Table(nameof(PersonalInfo))]
    public class PersonalInfo
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LatName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public string Address { get; set; }
        public int? CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual Cities City { get; set; }
        public string NTN { get; set; }



    }

    [Table(nameof(Cities))]
    public class Cities
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    #endregion




    [Table(nameof(TransactionsAttachments))]
    public class TransactionsAttachments
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Bucket { get; set; }

    }

    #region Transaction 

    // Parent Transaction 
    [Table(nameof(ParentTransaction))]
    public class ParentTransaction : IBaseEntity<long>, IFullAuditableEntity
    {
        public DateTime Date { get; set; }
        public long Id { get; set; }
        public int TransactionType { get; set; }
        public string Description { get; set; }


        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<TransactionsAttachments> TransactionsAttachments { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual ApplicationUser CreatedByUser { get; set; }

    }


    [Table(nameof(Transaction))]
    public class Transaction : IBaseEntity<long>, IFullAuditableEntity
    {
        public long Id { get; set; }
        public double Qty { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public bool IsDr { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }

        /// Relations 

        public long ParentTransactionID { get; set; }
        [ForeignKey("ParentTransactionID")]
        public virtual ParentTransaction ParentTransaction { get; set; }
        public long? ItemBatchId { get; set; }
        [ForeignKey("ItemBatchId")]
        public virtual ItemBatch ItemBatch { get; set; }

        public long CostCenterId { get; set; }
        [ForeignKey("CostCenterId")]
        public virtual CostCenter CostCenter { get; set; }

        public long LedgerId { get; set; }
        [ForeignKey("LedgerId")]
        public virtual Ledger Ledger { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual ApplicationUser CreatedByUser { get; set; }

        /// Relations 



    }


    [Table(nameof(Contract))]
    public class Contract : IBaseEntity<long>, IFullAuditableEntity
    {
        public long Id { get; set; }
        public double RentAmount { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double SecurityAmount { get; set; }
        public int DueDate { get; set; }
        public int Frequancy { get; set; }
        public bool isActive { get; set; }
        public DateTime NextDueDate { get; set; }

        /// Relations 

        public long? ItemBatchId { get; set; }
        [ForeignKey("ItemBatchId")]
        public virtual ItemBatch ItemBatch { get; set; }

        public long CostCenterId { get; set; }
        [ForeignKey("CostCenterId")]
        public virtual CostCenter CostCenter { get; set; }

        public long LedgerId { get; set; }
        [ForeignKey("LedgerId")]
        public virtual Ledger Ledger { get; set; }
        /// Relations 


        /// Auditable Entities 
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual ApplicationUser CreatedByUser { get; set; }
        /// Auditable Entities 

    }



    #endregion




    /// <summary>
    /// Inventory
    /// </summary>

    /// Item 
    [Table(nameof(Item))]
    public class Item
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }

        /// Relations 
        public long ParentID { get; set; }
        [ForeignKey("ParentID")]
        public virtual ItemGroup Parent { get; set; }

        public ICollection<ItemBatch> Batches { get; set; }
        /// Relations 

    }

    /// Item Group 
    [Table(nameof(ItemGroup))]
    public class ItemGroup
    {
        public long Id { get; set; }
        public string Title { get; set; }

        /// Relations 
        public long? ParentID { get; set; }
        [ForeignKey("ParentID")]
        public virtual ItemGroup Parent { get; set; }

        public ICollection<ItemGroup> ChildGroups { get; set; }
        public ICollection<Item> ChildItem { get; set; }
        /// Relations 

    }
    /// Item Batch 
    [Table(nameof(ItemBatch))]
    public class ItemBatch
    {
        public long Id { get; set; }
        public string Title { get; set; }

        /// Relations 
        public long? ParentID { get; set; }
        [ForeignKey("ParentID")]
        public virtual Item Parent { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser Agent { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Contract> Contracts { get; set; }

        /// Relations 
        /// 
    }

    // GoDown 
    [Table(nameof(Warehouse))]
    public class Warehouse
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        /// Relations 
        public int CityID { get; set; }
        [ForeignKey("CityID")]
        public virtual Cities City { get; set; }
        /// Relations 
    }
    // Cost Center 
    [Table(nameof(CostCenter))]
    public class CostCenter
    {
        public long Id { get; set; }
        public string Title { get; set; }

        /// Relations 
        public long CostCategoryId { get; set; }
        [ForeignKey("CostCategoryId")]
        public virtual CostCategory Category { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        /// Relations 
        /// 
    }
    // Cost Category 
    [Table(nameof(CostCategory))]
    public class CostCategory
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public ICollection<CostCenter> CostCenter { get; set; }
    }

    /// Stock Keeper 
    //-> batchID ,GowDown, TransactionID ,Opening, Inward , Outward , Clossing

    [Table(nameof(WarehouseStock))]
    public class WarehouseStock
    {
        public long Id { get; set; }

        public long WarehouseID { get; set; }

        public long ItemBatchID { get; set; }
        public double OpeningBalance { get; set; }
        public double OpeningRate { get; set; }

        public double InwardQty { get; set; }
        public double InwardRate { get; set; }
        public double OutwardQty { get; set; }
        public double OutwardRate { get; set; }


        /// Auditable Entities 
        public bool IsDeleted { get; set; }
    }

    /////////// branch Accounting 

    [Table(nameof(Branch))]
    public class Branch
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
    }



    [Table(nameof(BrachBalances))]
    public class BrachBalances
    {
        public long Id { get; set; }
        public long BranchID { get; set; }
        public long LedgerID { get; set; }
        public double Opening { get; set; }
        public double OpeningDate { get; set; }
        public int MyProperty { get; set; }
    }

    [Table(nameof(LedgerSMS))]
    public class LedgerSMS
    {
        public long Id { get; set; }
        public long LedgerId { get; set; }
        [ForeignKey("LedgerId")]
        public virtual Ledger Ledger { get; set; }
        public bool IsSent { get; set; }
    }
}
