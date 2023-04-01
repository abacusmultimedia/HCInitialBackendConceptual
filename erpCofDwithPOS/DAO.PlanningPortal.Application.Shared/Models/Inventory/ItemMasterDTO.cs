using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Application.Shared.Models.Inventory
{
    public class ItemMasterDTO
    {
        public long ItemId { get; set; }
        public bool expand { get; set; }
        public bool Checked { get; set; }
        public string SubItemCode { get; set; } // This should be Key []
        public string ShortEng { get; set; }
        public string Department { get; set; }
        public Nullable<decimal> Selling_Price { get; set; }
        public Nullable<decimal> Cost_Price { get; set; }

        public Nullable<int> Rating { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsAproved { get; set; }
    }


    public class ItemMasterCreationDTO
    {
        public long ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ShortName { get; set; }
        public Nullable<int> Rating { get; set; }
        public bool Default { get; set; }
        public bool Expiration { get; set; }
        //public DateTime ExpiryDays { get; set; }
        public string ReminderDays { get; set; }
        public int PackingSize { get; set; }

        public int Department { get; set; }
        public int Category { get; set; }
        public int SubCategory { get; set; }
        public int Brand { get; set; }
        public int Model { get; set; }
        public int Color { get; set; }


        public int ItemType { get; set; }
        public int StockMethod { get; set; }
        public decimal Tax { get; set; }
        public decimal EstimationProfit { get; set; }
        public bool Perishable { get; set; }
        public bool Consignment { get; set; }

        //public DateTime DraftedOn { get; set; }
        //public DateTime LastActive { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public DateTime LastModified { get; set; }
        //public DateTime LastDeleted { get; set; }
        #region Approved 
        public int ApprovedType { get; set; }
        public int ApprovedBy { get; set; }
        //public DateTime ApprovedOn { get; set; }
        public DateTime RejectedOn { get; set; }
        #endregion

        public List<subItemDTO> SubItems { get; set; }


    }

    public class subItemList
    {
        public string ScanBarcode { get; set; }
        public string ShortName { get; set; }
        public int PackingUnit { get; set; }
        public int PackingQty { get; set; }
        public double CostPrice { get; set; }
        public double AvgPrice { get; set; }
        public double TaxAmount { get; set; }
        public double EPAmount { get; set; }
        public int SalesPrice { get; set; }

    }
    public class subItemDTO
    {
        public long Id { get; set; }
        public Nullable<int> Seq { get; set; }
        public string SubItemCode { get; set; } // This should be Key []
        public string ShortEng { get; set; }
        public string ShortArb { get; set; }
        public int Category { get; set; }
        public Nullable<int> UnitID { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<decimal> CostPrice { get; set; }
        public Nullable<decimal> AvgPrice { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> EstProfit { get; set; }
        public Nullable<decimal> Selling_Price { get; set; }


        public Nullable<decimal> Stock { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> Unit { get; set; }
        public Nullable<decimal> EpAmount { get; set; }
        public string ImageUrl { get; set; }


    }


}
