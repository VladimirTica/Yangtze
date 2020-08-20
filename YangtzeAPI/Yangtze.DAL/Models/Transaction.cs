using System;
using System.Collections.Generic;

namespace Yangtze.DAL.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public string Code { get; set; }
        public short Type { get; set; }
        public short Mode { get; set; }
        public short Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Description { get; set; }

        public virtual Order Order { get; set; }
        public virtual User User { get; set; }
    }
}
