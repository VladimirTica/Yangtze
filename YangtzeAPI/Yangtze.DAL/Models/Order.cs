using System;
using System.Collections.Generic;

namespace Yangtze.DAL.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItem = new HashSet<OrderItem>();
            Transaction = new HashSet<Transaction>();
        }

        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public double SubTotal { get; set; }
        public double TotalDiscount { get; set; }
        public double Tax { get; set; }
        public double Shipping { get; set; }
        public double Total { get; set; }
        public string PromoCode { get; set; }
        public double CodeDiscount { get; set; }
        public double GrandTotal { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Description { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
