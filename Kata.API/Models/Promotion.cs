using Kata.API.Models.Enums;

namespace Kata.API.Models;

/// <summary>
/// Product promotion
/// </summary>
public class Promotion
{
    /// <summary>
    /// Number of items that needs to buy to be eligible for the offer
    /// </summary>
    public int NumberOfItemsToBuyForEligibility { get; set; } //_ApplypromotionFor 

    /// <summary>
    /// type of promotion
    /// </summary>
    public PromotionType PromotionType { get; set; } = PromotionType.None;

    /// <summary>
    /// value of the promotion - percentage/price/something else
    /// </summary>
    public decimal Value { get; set; }
}
