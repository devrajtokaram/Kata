using Kata.API.Models;
using Kata.API.Models.Enums;

namespace Kata.API.Data;

public static class Products
{
    public static List<Product> Items
    {
        get
        {
            return new List<Product>()
            {
                new Product()
                {
                    SKU = "A",
                    UnitPrice = 10,
                    Promotion = null
                    //Discount = new Discount()
                    //{
                    //    DiscountType = DiscountType.None
                    //}
                },
                new Product()
                {
                    SKU = "B",
                    UnitPrice = 15,
                    Promotion = new Promotion()
                    {
                        PromotionType = PromotionType.ReduceCost,
                        NumberOfItemsToBuyForEligibility = 3,
                        Value = 40//5
                    }
                },
                new Product()
                {
                    SKU = "C",
                    UnitPrice = 40,
                    Promotion = null
                    //Discount = new Discount()
                    //{
                    //    DiscountType = DiscountType.None
                    //}
                },
                new Product()
                {
                    SKU = "D",
                    UnitPrice = 55,
                    Promotion = new Promotion()
                    {
                        PromotionType = PromotionType.Percentage,
                        NumberOfItemsToBuyForEligibility = 2,
                        Value = 25
                    }
                }
            };
        }
    }
}
