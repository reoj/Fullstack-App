using DotnetBackend.Models;

namespace DotnetBackend.DTOs
{
    public class GetExchangeDTO
    {
        public Guid Id { get; set; } = new Guid();
        public int Sender { get; set; }
        public int Reciever { get; set; }
        public string ItemName { get; set; } = "Out of Stock";
        public string ItemDescription { get; set; } = "";
        public int ItemQuantity { get; set; }

        public GetExchangeDTO(Exchange ex)
        {
            Id = ex.Id;
            Sender = ex.sender.Id;
            Reciever = ex.reciever.Id;
            ItemName = ex.itemName;
            ItemDescription = ex.itemDescription;
            ItemQuantity = ex.itemQuantity;
        }
    }
}