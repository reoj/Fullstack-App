using exam_webapi.Models;

namespace exam_webapi.Repositories
{
    /// <summary>
    /// Represents Lists that emulate a Database
    /// </summary>
    public class StaticData
    {
        public static List<User> UsersContext = new();
        public static List<InventoryItem> InventoryContext = new();
    }
}
