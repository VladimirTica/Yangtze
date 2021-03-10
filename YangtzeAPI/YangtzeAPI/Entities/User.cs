using System;
using System.Collections.Generic;

namespace YangtzeAPI.Entities
{
    public partial class User
    {
        public User()
        {
            Cart = new HashSet<Cart>();
            Order = new HashSet<Order>();
            Product = new HashSet<Product>();
            Transaction = new HashSet<Transaction>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int Status { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime RegisteredAt { get; set; }
        public byte? PremiumMember { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
