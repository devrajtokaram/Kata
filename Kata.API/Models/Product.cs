namespace Kata.API.Models;

/// <summary>
/// Product
/// </summary>
public class Product
{
    /// <summary>
    /// unit price
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// unique sku id
    /// </summary>
    public string SKU { get; set; } = default!;

    /// <summary>
    /// product promotion
    /// </summary>
    public Promotion? Promotion { get; set; }
}
