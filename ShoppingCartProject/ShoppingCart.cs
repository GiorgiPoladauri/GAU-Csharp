using System;
using System.Collections.Generic;

namespace ShoppingCartProject
{
    public class ShoppingCart
    {
        public List<Product> Products { get; private set; }

        private int ProductLimit = 10;
        public delegate void CartLimitExceededHandler(object sender, string message);
        public event CartLimitExceededHandler CartLimitExceeded;

        public ShoppingCart()
        {
            Products = new List<Product>();
        }

        public void AddToCart(Product product)
        {
            try
            {
                if (GetTotalProductCount() >= ProductLimit)
                {
                    throw new Exception("Ouh, cart limit was suspended I guess...");
                }

                Products.Add(product);
            }

            catch (Exception ex)
            {
                CartLimitExceeded?.Invoke(this, ex.Message);
            }
        }


        public int GetTotalProductCount()
        {
            int totalCount = 0;
            foreach (var product in Products)
            {
                totalCount += product.Quantity;
            }
            return totalCount;
        }

        public decimal GetTotalAmount()
        {
            decimal totalAmount = 0;
            foreach (var product in Products)
            {
                totalAmount += product.GetTotalPrice();
            }
            return totalAmount;
        }

        public void ClearCart()
        {
            Products.Clear();
        }
    }
}
