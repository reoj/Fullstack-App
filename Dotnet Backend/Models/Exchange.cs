namespace DotnetBackend.Models
{
    public class Exchange
    {
        public User sender { get; set; }
        public User reciever { get; set; }
        public string itemName { get; set; } = "Out of Stock";
        public string itemDescription { get; set; } = "";
        public int itemQuantity { get; set; }
    }
}