﻿using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class OrderMaster
    {
        public OrderMaster()
        {
            CustomerDetails = new HashSet<CustomerDetails>();
            OrderItemDetails = new HashSet<OrderItemDetails>();
            OrderPayment = new HashSet<OrderPayment>();
        }

        public long Id { get; set; }
        public long? MirrorId { get; set; }
        public DateTime? SaleDate { get; set; }
        public string TransactionId { get; set; }
        public int? DelieveryType { get; set; }
       
        public int? PortType { get; set; }
        public string Description { get; set; }
        public string PortName { get; set; }
        public string PassportNo { get; set; }
        public int? Unit { get; set; }
        public DateTime? DeliveryFrom { get; set; }
        public DateTime? DeliveryTo { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public bool? IsActive { get; set; }

        public int? salestatus { get; set; }
        public virtual MirrorList Mirror { get; set; }
        public virtual ICollection<CustomerDetails> CustomerDetails { get; set; }
        public virtual ICollection<OrderItemDetails> OrderItemDetails { get; set; }
        public virtual ICollection<OrderPayment> OrderPayment { get; set; }
    }
}
