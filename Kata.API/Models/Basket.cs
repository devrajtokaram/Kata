namespace Kata.API.Models;

/// <summary>
/// Main basket which contains all the items that have been added and total price after promotion
/// </summary>
public class Basket
{
    /// <summary>
    /// List of basket items
    /// </summary>
    public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();

    /// <summary>
    /// Total price of the basket items after promotion
    /// </summary>
    public decimal? TotalPrice { get; set; }
}
