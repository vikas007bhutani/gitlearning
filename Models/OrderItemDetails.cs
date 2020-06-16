using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class OrderItemDetails
    {
        public long Id { get; set; }
        public string TransId { get; set; }
        public string StockId { get; set; }
        public long? OrderId { get; set; }
        public int? OrderType { get; set; }
        public string OrderTypePrefix { get; set; }
        public int? Qty { get; set; }
        public int? CurrencyType { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceInr { get; set; }
        public decimal? ConversionRate { get; set; }
        public int? Unit { get; set; }
        public int? ItemType { get; set; }
        public string Shape { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Category { get; set; }
        public string Photo { get; set; }
        public string ItemDesc { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public bool? IsActive { get; set; }

        public virtual OrderMaster Order { get; set; }
    }
}
