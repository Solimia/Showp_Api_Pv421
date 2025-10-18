using Microsoft.Azure.Documents;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public string? UserId { get; set; }
        public User? User { get; set; }

        public ICollection<OrderDetails> Items { get; set; } = new List<OrderDetails>();
    }
}