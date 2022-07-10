using Kata.API.Data;
using Kata.API.Services;
using Microsoft.AspNetCore.Mvc;


namespace Kata.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BasketController : ControllerBase
{
    private readonly ILogger<BasketController> _logger;
    private readonly IBasketService _basketService;

    public BasketController(
        ILogger<BasketController> logger,
        IBasketService basketService)
    {
        _logger = logger;
        _basketService = basketService;
    }

    [HttpGet("add/{sku}/{quantity}")]
    public IActionResult AddItemToBasket(string sku, int quantity)
    {
        _logger.LogInformation($"Given sku: {sku} and quantity: {quantity} to add an item to the basket.");
        if (string.IsNullOrWhiteSpace(sku))
        {
            _logger.LogError("SKU id is null or empty");
            return BadRequest("SKU id cannot be null or empty");
        }

        if (quantity <= 0)
        {
            _logger.LogError("Quantity is zero or less to add to the Basket");
            return BadRequest("Sorry, cannot add zero or less items to the Basket");
        }

        var product = Products.Items.FirstOrDefault(item => item.SKU.Equals(sku.Trim(), StringComparison.CurrentCultureIgnoreCase));

        if (product == null)
        {
            _logger.LogError("The requested item cannot be found to add it to the basket");
            return NotFound($"The requested item cannot be found in the available items. Please try with the different sku. Given sku: '{sku}'");
        }

        try
        {
            _basketService.AddItem(product, quantity);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error adding an item to the basket. Error message: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get")]
    public IActionResult ViewBasket()
    {
        try
        {
            return Ok(_basketService.GetBasket());
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving basket items. Error message: {ex.Message}");
            return BadRequest("Unable to retrieve Basket at the moment. Please try again later");
        }
    }

    [HttpDelete("clear")]
    public IActionResult ClearBasket()
    {
        try
        {
            _basketService.ClearBasket();
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error clearing basket items. Error message: {ex.Message}");
            return BadRequest("Unable to empty Basket at the moment. Please try again later");
        }
    }
}