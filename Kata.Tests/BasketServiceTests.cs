using Microsoft.Extensions.Logging;
using Moq;
using Kata.API.Services;
using Kata.API.Models;
using Kata.API.Models.Enums;

namespace Kata.Tests;

public class BasketServiceTests
{
    private Mock<ILogger<BasketService>> _logger = default!;
    private IBasketService _basketService = default!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<BasketService>>();
        _basketService = new BasketService(_logger.Object);
    }

    #region AddItem tests      
    [Test]
    public void AddItemTest_WhenPromotionAvailable_ShouldApplyPromotion()
    {
        //Arrange
        var product = new Product()
        {
            SKU = "B",
            UnitPrice = 15,
            Promotion = new Promotion()
            {
                PromotionType = PromotionType.ReduceCost,
                NumberOfItemsToBuyForEligibility = 3,
                Value = 40
            }
        };
        var quantity = 3;

        //Assert
        Assert.DoesNotThrow(() => _basketService.AddItem(product, quantity));

        var basket = _basketService.GetBasket();
        Assert.IsNotNull(basket);
        Assert.That(basket.TotalPrice, Is.EqualTo(product.Promotion.Value));
    }

    [Test]
    public void AddItemTest_WhenPromotionNotAvailable_ShouldNotApplyPromotion()
    {
        //Arrange
        var product = new Product()
        {
            SKU = "C",
            UnitPrice = 40,
            Promotion = null
        };
        var quantity = 3;

        //Assert
        Assert.DoesNotThrow(() => _basketService.AddItem(product, quantity));

        var basket = _basketService.GetBasket();
        Assert.IsNotNull(basket);
        Assert.That(basket.TotalPrice, Is.EqualTo(product.UnitPrice * quantity));
    }
    #endregion

    #region GetBasket tests
    [Test]
    public void GetBasket_ShouldNotThrowException()
    {
        //Arrange
        var product = new Product()
        {
            SKU = "C",
            UnitPrice = 40,
            Promotion = null
        };
        var quantity = 3;

        //Assert
        Assert.DoesNotThrow(() => _basketService.AddItem(product, quantity));
        Assert.DoesNotThrow(() => _basketService.GetBasket());
    }
    #endregion

    #region ClearBasket tests
    [Test]
    public void ClearBasket_ShouldNotThrowException()
    {
        //Arrange
        var product = new Product()
        {
            SKU = "C",
            UnitPrice = 40,
            Promotion = null
        };
        var quantity = 3;

        //Assert
        Assert.DoesNotThrow(() => _basketService.AddItem(product, quantity));
        Assert.DoesNotThrow(() => _basketService.ClearBasket());
        var basket = _basketService.GetBasket();
        Assert.IsNotNull(basket);
        Assert.That(basket.BasketItems.Count, Is.EqualTo(0));
    }
    #endregion
}
