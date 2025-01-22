using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace TraderApp.Repositories
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        private DateTime dateadded { get; set; }

        public DateTime DateAdded { get => dateadded; set => dateadded = value; }

        public Item(int id, string name, decimal price, int quantity, DateTime dateadded)
        {
            ID = id;
            Name = name; 
            Price = price;
            Quantity = quantity;
            DateAdded = dateadded;
        }
    }
}
