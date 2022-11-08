namespace DotnetBackend.DTOs
{
    public class CreateExchangeDTO
    {
        public int sender { get; set; }
        public int reciever { get; set; }
        public string itemName { get; set; } = "Out of Stock";
        public string itemDescription { get; set; } = "";
        public int itemQuantity { get; set; }
    }
}