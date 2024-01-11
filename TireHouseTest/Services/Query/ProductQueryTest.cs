using TireHouse.DTO;
using TireHouse.Service;

namespace TireHouseTest.Services.Query;

public class ProductQueryTest : QueryTestBase
{
    [Theory]
    [InlineData("Productname 1", "Manufactorer 1", "Size 1")]
    [InlineData("Productname 2", "Manufactorer 2", "Size 2")]
    [InlineData("Productname 3", "Manufactorer 3", "Size 3")]
    [InlineData("Productname 4", "Manufactorer 4", "Size 4")]
    public void Get(string productname, string manufactorer, string size)
    {
        Product product = GetTestRecord(productname, manufactorer, size);
        _unitOfWork.ProductRepository.Insert(product);
        _unitOfWork.SaveChanges();

        ProductQueryService queryService = new(_unitOfWork);

        var retrievedProduct = queryService.Get(product.Id);

        Assert.True(retrievedProduct.Id == product.Id);
    }

    [Theory]
    [InlineData("Productname 1", "Manufactorer 1", "Size 1")]
    [InlineData("Productname 2", "Manufactorer 2", "Size 2")]
    [InlineData("Productname 3", "Manufactorer 3", "Size 3")]
    [InlineData("Productname 4", "Manufactorer 4", "Size 4")]
    public void Set(string productname, string manufactorer, string size)
    {
        Product product = GetTestRecord(productname, manufactorer, size);
        List<Product> exceptedSet = new();
        exceptedSet.Add(product);

        _unitOfWork.ProductRepository.Insert(product);
        _unitOfWork.SaveChanges();

        ProductQueryService queryService = new(_unitOfWork);

        var retrievedProduct = queryService.Set();

        Assert.True(exceptedSet.Last().Name == retrievedProduct.Last().Name &&
                    exceptedSet.Last().Manufactorer == retrievedProduct.Last().Manufactorer &&
                    exceptedSet.Last().Size == retrievedProduct.Last().Size);
    }

    [Theory]
    [InlineData("Productname 1", "Manufactorer 1", "Size 1")]
    [InlineData("Productname 2", "Manufactorer 2", "Size 2")]
    [InlineData("Productname 3", "Manufactorer 3", "Size 3")]
    [InlineData("Productname 4", "Manufactorer 4", "Size 4")]
    public void ExpressionSet(string productname, string manufactorer, string size)
    {
        Product product = GetTestRecord(productname, manufactorer, size);
        List<Product> exceptedSet = new();
        exceptedSet.Add(product);

        _unitOfWork.ProductRepository.Insert(product);
        _unitOfWork.SaveChanges();

        ProductQueryService queryService = new(_unitOfWork);

        var retrievedProduct = queryService.Set(x => x.Id == product.Id);

        Assert.True(exceptedSet.Last().Name == retrievedProduct.Last().Name &&
                    exceptedSet.Last().Manufactorer == retrievedProduct.Last().Manufactorer &&
                    exceptedSet.Last().Size == retrievedProduct.Last().Size);
    }

    private static Product GetTestRecord(string productname, string manufactorer, string size)
    {
        Product product = new()
        {
            Name = productname,
            Manufactorer = manufactorer,
            Size = size
        };

        return product;
    }
}
