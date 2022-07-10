using Kata.API.Models;

namespace Kata.API.Services;

public interface IBasketService
{
    /// <summary>
    /// Adds an item to the basket
    /// </summary>
    /// <param name="product"> - product to add to the basket</param>
    /// <param name="count"> -count to add</param>
    /// <exception cref="Exception"></exception>
    void AddItem(Product product, int quantity);
    
    /// <summary>
    /// Gets the basket items in ascending order of the sku's
    /// </summary>
    /// <returns>- Basket</returns>
    Basket GetBasket();

    /// <summary>
    /// Clears all the items from the basket
    /// </summary>
    void ClearBasket();
}
