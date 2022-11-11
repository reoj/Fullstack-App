namespace DotnetBackend.DTOs
{
    public class CreateExchangeDTO
    {
        public int Sender { get; set; }
        public int Reciever { get; set; }
        public string ItemName { get; set; } = "Out of Stock";
        public string ItemDescription { get; set; } = "";
        public int ItemQuantity { get; set; }
    }
}