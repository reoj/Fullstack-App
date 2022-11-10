using System.ComponentModel.DataAnnotations;

namespace DotnetBackend.Models
{
    public class Exchange
    {
        public Guid Id { get; set; } = new Guid();
        public User sender { get; set; } = null!;
        public User reciever { get; set; } = null!;
        public string itemName { get; set; } = "Out of Stock";
        public string itemDescription { get; set; } = "";
        public int itemQuantity { get; set; }
    }
}