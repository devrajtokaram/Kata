using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Kata.API.Controllers;
using Kata.API.Services;
using Kata.API.Models;

namespace Kata.Tests;

public class BasketControllerTests
{
    private Mock<ILogger<BasketController>> _logger = default!;
    private Mock<IBasketService> _mockBasketService = default!;
    private BasketController _basketController = default!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<BasketController>>();
        _mockBasketService = new Mock<IBasketService>();
        _basketController = new BasketController(
            _logger.Object,
            _mockBasketService.Object
        );
    }

    #region AddItemToBasket tests   
    [Test]
    public void AddItemToBasket_ValidItem_ShouldReturnOk()
    {
        //Arrange
        var item = "A";
        var quantity = 2;

        //Act
        var response = _basketController.AddItemToBasket(item, quantity);
        var result = response as OkResult;

        //Assert
        Assert.IsNotNull(response);
        Assert.IsNotNull(result);
        Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.OK);
    }

    [Test]
    public void AddItemToBasket_InValidSku_ShouldReturnBadRequest()
    {
        //Arrange
        var item = string.Empty;
        var quantity = 2;

        //Act
        var response = _basketController.AddItemToBasket(item, quantity);
        var result = response as BadRequestObjectResult;

        //Assert
        Assert.IsNotNull(response);
        Assert.IsNotNull(result);
        Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.BadRequest);
    }

    [Test]
    public void AddItemToBasket_InValidCount_ShouldReturnBadRequest()
    {
        //Arrange
        var item = "A";
        var quantity = 0;

        //Act
        var response = _basketController.AddItemToBasket(item, quantity);
        var result = response as BadRequestObjectResult;

        //Assert
        Assert.IsNotNull(response);
        Assert.IsNotNull(result);
        Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.BadRequest);
    }

    [Test]
    public void AddItemToBasket_InValidItem_ShouldReturnNotFound()
    {
        //Arrange
        var item = "Invalid SKU";
        var quantity = 1;

        //Act
        var response = _basketController.AddItemToBasket(item, quantity);
        var result = response as NotFoundObjectResult;

        //Assert
        Assert.IsNotNull(response);
        Assert.IsNotNull(result);
        Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.NotFound);
    }

    [Test]
    public void AddItemToBasket_CatchException_ShouldReturnBadRequest()
    {
        //Arrange
        var item = "A";
        var quantity = 1;
        _mockBasketService.Setup(
            service =>
                service.AddItem(It.IsAny<Product>(), It.IsAny<int>())
                ).Throws<Exception>();

        //Act
        var response = _basketController.AddItemToBasket(item, quantity);
        var result = response as BadRequestObjectResult;

        //Assert
        Assert.IsNotNull(response);
        Assert.IsNotNull(result);
        Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.BadRequest);
    }
    #endregion

    #region ViewBasket tests
    [Test]
    public void ViewBasket_ShouldReturnOk()
    {
        //Act
        var response = _basketController.ViewBasket();
        var result = response as OkObjectResult;

        //Assert
        Assert.IsNotNull(response);
        Assert.IsNotNull(result);
        Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.OK);
    }

    [Test]
    public void ViewBasket_CatchException_ShouldReturnBadRequest()
    {
        //Arrange
        _mockBasketService.Setup(
            service =>
                service.GetBasket()
                ).Throws<Exception>();

        //Act
        var response = _basketController.ViewBasket();
        var result = response as BadRequestObjectResult;

        //Assert
        Assert.IsNotNull(response);
        Assert.IsNotNull(result);
        Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.BadRequest);
    }
    #endregion

    #region ClearBasket tests
    [Test]
    public void ClearBasket_ShouldReturnOk()
    {
        //Act
        var response = _basketController.ClearBasket();
        var result = response as OkResult;

        //Assert
        Assert.IsNotNull(response);
        Assert.IsNotNull(result);
        Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.OK);
    }

    [Test]
    public void ClearBasket_CatchException_ShouldReturnBadRequest()
    {
        //Arrange
        _mockBasketService.Setup(
            service =>
                service.ClearBasket()
                ).Throws<Exception>();

        //Act
        var response = _basketController.ClearBasket();
        var result = response as BadRequestObjectResult;

        //Assert
        Assert.IsNotNull(response);
        Assert.IsNotNull(result);
        Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.BadRequest);
    }
    #endregion

}