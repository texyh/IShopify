
public class OrderItemSummaryViewModel
{
    public int ProductId {get; set;}

    public string imageUrl {get; set;}

    public string ProductName {get; set;}

    public decimal Price {get; set;}

    public int Quantity {get; set;}

    public decimal Total => Price * Quantity;
}