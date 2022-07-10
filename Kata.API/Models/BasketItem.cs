namespace Kata.API.Models;

/// <summary>
/// Basket item which contains their data
/// </summary>
public class BasketItem
{
    /// <summary>
    /// Unique item SKU id
    /// </summary>
    public string SKU { get; set; } = default!;

    /// <summary>
    /// Count of an item in the basket
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Item(s) total price after promotion offer if there is any.
    /// </summary>
    public decimal? TotalItemsPrice { get; set; } 

}
