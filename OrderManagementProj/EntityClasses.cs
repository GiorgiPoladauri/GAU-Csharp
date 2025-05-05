using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOrderManagementSystemProj
{
    public class EntityClasses
    {
        public class Customer
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public string Phone { get; set; }

            public ICollection<Order> Orders { get; set; }
        }

        public class Supplier
        {
            public int Id { get; set; }
            public string CompanyName { get; set; }
            public string ContactName { get; set; }
            public string ContactTitle { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }

            public ICollection<Product> Products { get; set; }
        }

        public class Product
        {
            public int Id { get; set; }
            public string ProductName { get; set; }
            public int SupplierId { get; set; }
            public decimal UnitPrice { get; set; }
            public string Package { get; set; }
            public bool IsDiscontinued { get; set; }

            public Supplier Supplier { get; set; }
            public ICollection<OrderItem> OrderItems { get; set; }
        }

        public class Order
        {
            public int Id { get; set; }
            public DateTime OrderDate { get; set; }
            public string OrderNumber { get; set; }
            public int CustomerId { get; set; }
            public decimal TotalAmount { get; set; }

            public Customer Customer { get; set; }
            public ICollection<OrderItem> OrderItems { get; set; }
        }

        public class OrderItem
        {
            public int Id { get; set; }
            public int OrderId { get; set; }
            public int ProductId { get; set; }
            public decimal UnitPrice { get; set; }
            public int Quantity { get; set; }

            public Order Order { get; set; }
            public Product Product { get; set; }
        }

    }
}
