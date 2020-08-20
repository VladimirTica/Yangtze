using System;
using System.Collections.Generic;

namespace Yangtze.DAL.Models
{
    public partial class User
    {
        public User()
        {
            Cart = new HashSet<Cart>();
            Order = new HashSet<Order>();
            Transaction = new HashSet<Transaction>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int Status { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime RegisteredAt { get; set; }
        public byte? PremiumMember { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
