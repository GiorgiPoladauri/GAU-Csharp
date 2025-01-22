using Newtonsoft.Json;
using TraderApp.Repositories;

namespace TraderApp.Managers
{
    public static class ItemManager
    {
        private static string ItemsFilePath = @"..\..\..\SaveFolder\Items.JSON";

        public static void AddItem(Item item)
        {
            var items = LoadItems();
            items.Add(item);
            SaveItems(items);
        }

        public static void DeleteItem(int itemId)
        {
            var items = LoadItems();
            var item = items.Find(i => i.ID == itemId);
            if (item != null)
            {
                items.Remove(item);
                SaveItems(items);
            }
        }

        public static List<Item> LoadItems()
        {
            if (!File.Exists(ItemsFilePath))
            {
                return new List<Item>();
            }

            string forSaleItems = File.ReadAllText(ItemsFilePath);
            return JsonConvert.DeserializeObject<List<Item>>(forSaleItems) ?? new List<Item>();
        }

        public static void SaveItems(List<Item> items)
        {
            string forSaleItems = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(ItemsFilePath, forSaleItems);
        }

        public static void UpdateItem(Item updatedItem)
        {
            var items = LoadItems();
            var item = items.Find(i => i.ID == updatedItem.ID);
            if (item != null)
            {
                item.Name = updatedItem.Name;
                item.Quantity = updatedItem.Quantity;
                item.DateAdded = updatedItem.DateAdded;
                SaveItems(items);
            }
        }
    }
}