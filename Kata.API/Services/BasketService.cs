using Kata.API.Models;
using Kata.API.Models.Enums;

namespace Kata.API.Services;
public class BasketService : IBasketService
{
    public ILogger<BasketService> _logger { get; }

    public Basket _basket = new Basket();

    public BasketService(ILogger<BasketService> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Adds an item to the basket
    /// </summary>
    /// <param name="product"> - product to add to the basket</param>
    /// <param name="quantity"> -count to add</param>
    /// <exception cref="Exception"></exception>
    public void AddItem(Product product, int quantity)
    {
        decimal price;
        BasketItem basketItem;
        if (_basket.BasketItems != null && _basket.BasketItems.Any())
        {
            basketItem = _basket.BasketItems.FirstOrDefault(item => item.SKU.Equals(product.SKU, StringComparison.CurrentCultureIgnoreCase));

            //if item already in the basket, it removes it and updates their count and price after promotion if applied.
            if (basketItem != null)
            {
                quantity += basketItem.Count;
                _basket.BasketItems.Remove(basketItem);
            }
        }

        if (product.Promotion == null)
        {
            //if there is no promotion, then it calculates price with original unit price
            price = product.UnitPrice * quantity;
        }
        else
        {
            try
            {
                //gets the item price after the promotion
                price = GetTotalItemsPriceAfterPromotion(product, quantity, product.Promotion.PromotionType);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error calculating the total items price after promotion. Error message: {ex.Message}");
                throw new Exception($"Error calculating the total items price after promotion. Error message: {ex.Message}");
            }
        }
        
        if (_basket.BasketItems == null)
            _basket.BasketItems = new List<BasketItem>();

        _basket.BasketItems.Add(new BasketItem
        {
            SKU = product.SKU,
            Count = quantity,
            TotalItemsPrice = price
        });
        _basket.TotalPrice = _basket.BasketItems.Sum(p => p.TotalItemsPrice);
    }

    /// <summary>
    /// Gets the basket items in ascending order of the sku's
    /// </summary>
    /// <returns>- Basket</returns>
    public Basket GetBasket()
    {
        _basket.BasketItems = _basket.BasketItems.OrderBy(item => item.SKU).ToList();
        return _basket;
    }

    /// <summary>
    /// Clears all the items from the basket
    /// </summary>
    public void ClearBasket()
    {
        _basket.BasketItems.Clear();
        _basket.TotalPrice = 0;
    }

    /// <summary>
    /// Gets the price of an item after the promotion
    /// </summary>
    /// <param name="product"> -Product to check their price </param>
    /// <param name="quantity">- count/quantity </param>
    /// <param name="promotionType">- Promotion type</param>
    /// <returns></returns>
    private decimal GetTotalItemsPriceAfterPromotion(Product product, int quantity, PromotionType promotionType)
    {
        var promotion = product.Promotion;

        // If there is no promotion or item count is less than the promotion eligibility,
        // then returns actual product price
        if (promotion == null || quantity < promotion.NumberOfItemsToBuyForEligibility)
            return quantity * product.UnitPrice;

        int reminder = quantity % (promotion.NumberOfItemsToBuyForEligibility);
        bool reminderIsZero = (reminder == 0);

        //number of items eligible for promotion
        int itemsElgibleForpromotion = quantity - reminder;

        //number of sets that needs to apply promotion
        int quotient = itemsElgibleForpromotion / (promotion.NumberOfItemsToBuyForEligibility);

        decimal price;

        switch (promotionType)
        {
            case PromotionType.Percentage:
                // promotion price = items eligible for promotion times original price times percentage value.
                decimal promotionPrice = itemsElgibleForpromotion * product.UnitPrice * (promotion.Value / 100);
                //subtracting promotion price for eligible items from the original price
                price = (itemsElgibleForpromotion * product.UnitPrice) - promotionPrice; 
                break;
            case PromotionType.ReduceCost:
                //promotion that needs to apply for every set (3 for 40). So total 4 sets for example.
                price = quotient * promotion.Value;
                break;
            default:
                price = product.UnitPrice * quantity;
                break;
        }

        // if there is any reminder left, adds its original price to the reminder ones.
        if (!reminderIsZero)
            price += reminder * product.UnitPrice;

        return price;
    }
}
