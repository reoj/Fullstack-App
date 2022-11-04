using exam_webapi.Models;

namespace exam_webapi.Repositories
{
    public class StaticData
    {
        public static List<User> UsersContext = new List<User>();
        public static List<InventoryItem> InventoryContext = new List<InventoryItem>();
    }
}
