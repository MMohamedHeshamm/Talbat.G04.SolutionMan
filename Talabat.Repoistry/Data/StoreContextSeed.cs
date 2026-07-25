using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Talabat.Core.Entites;

namespace Talabat.Repoistory.Data
{
    public static class StoreContextSeed
    {

        public async static Task SeedAsync(StoreContext _dbContext)
        {
            if(!_dbContext.ProductBrands.Any())
            {
                var brandData = File.ReadAllText("C:\\Users\\moham\\source\\repos\\Talbat.G04.Solution\\Talabat.Repoistry\\Data\\DataSeed\\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                if (brands?.Count() > 0)
                {

                    foreach (var brand in brands)
                    {
                        _dbContext.Set<ProductBrand>().Add(brand);
                    }

                    await _dbContext.SaveChangesAsync();
                }



            }

            if (!_dbContext.ProductCategories.Any())
            {
                var CategoryData = File.ReadAllText("../Talabat.Repoistry\\Data\\DataSeed\\categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoryData);

                if (categories?.Count() > 0)
                {

                    foreach (var cat in categories)
                    {
                        _dbContext.Set<ProductCategory>().Add(cat);
                    }

                    await _dbContext.SaveChangesAsync();
                }



            }




            if (!_dbContext.Products.Any())
            {
                var ProductData = File.ReadAllText("../Talabat.Repoistry\\Data\\DataSeed\\products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                if (Products?.Count() > 0)
                {

                    foreach (var product in Products)
                    {
                        _dbContext.Set<Product>().Add(product);
                    }

                    await _dbContext.SaveChangesAsync();
                }



            }


        }

    }
}
