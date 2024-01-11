using Microsoft.EntityFrameworkCore;
using TireHouse.DTO;
using TireHouse.Service;

namespace TireHouseTest.Services.Command;

public class ProductCommandTest : CommandTestBase
{
    [Theory]
    [InlineData("ProductName 1", "Manufactorer 1", "Size 1")]
    [InlineData("ProductName 2", "Manufactorer 2", "Size 2")]
    [InlineData("ProductName 3", "Manufactorer 3", "Size 3")]
    [InlineData("ProductName 4", "Manufactorer 4", "Size 4")]
    public void Insert(string productname, string manufactorer, string size)
    {
        Product product = GetTestRecord(productname, manufactorer, size);
        ProductCommandService commandService = new(_unitOfWork);
        commandService.Insert(product);

        Assert.NotNull(product.Name);
        Assert.NotNull(product.Manufactorer);
        Assert.True(product.Id > 0);
    }

    [Theory]
    [InlineData("ProductName 1", "Manufactorer 1", "Size 1")]
    [InlineData("ProductName 2", "Manufactorer 2", "Size 2")]
    [InlineData("ProductName 3", "Manufactorer 3", "Size 3")]
    [InlineData("ProductName 4", "Manufactorer 4", "Size 4")]
    public void NotInserted(string productname, string manufactorer, string size)
    {
        Product product = GetTestRecord(productname, manufactorer, size);
        product.Id = 1;

        Assert.Throws<ArgumentException>(() =>
        {
            ProductCommandService commandService = new(_unitOfWork);
            commandService.Insert(product);
        });
    }

    [Theory]
    [InlineData("ProductName 1", "Manufactorer 1", "Size 1")]
    [InlineData("ProductName 2", "Manufactorer 2", "Size 2")]
    [InlineData("ProductName 3", "Manufactorer 3", "Size 3")]
    [InlineData("ProductName 4", "Manufactorer 4", "Size 4")]
    public void Update(string productname, string manufactorer, string size)
    {
        Product newProduct = GetTestRecord(productname, manufactorer, size);
        ProductCommandService commandService = new(_unitOfWork);
        commandService.Insert(newProduct);

        newProduct.Name = $"New {productname}";
        newProduct.Manufactorer = $"New {manufactorer}";
        newProduct.Size = $"New {size}";

        commandService.Update(newProduct);

        Product updatedProduct = _unitOfWork.ProductRepository.Set(x => x.Id == newProduct.Id).Single();

        Assert.NotNull(updatedProduct);
        Assert.True(updatedProduct.Name == newProduct.Name &&
                    updatedProduct.Manufactorer == newProduct.Manufactorer &&
                    updatedProduct.Size == newProduct.Size);

    }

    [Theory]
    [InlineData("ProductName 1", "Manufactorer 1", "Size 1")]
    [InlineData("ProductName 2", "Manufactorer 2", "Size 2")]
    [InlineData("ProductName 3", "Manufactorer 3", "Size 3")]
    [InlineData("ProductName 4", "Manufactorer 4", "Size 4")]
    public void NotUpdated(string productname, string manufactorer, string size)
    {
        Product newProduct = GetTestRecord(productname, manufactorer, size);

        Assert.Throws<DbUpdateConcurrencyException>(() =>
        {
            ProductCommandService commandService = new(_unitOfWork);
            commandService.Update(newProduct);
        });
    }

    [Theory]
    [InlineData("ProductName 1", "Manufactorer 1", "Size 1")]
    [InlineData("ProductName 2", "Manufactorer 2", "Size 2")]
    [InlineData("ProductName 3", "Manufactorer 3", "Size 3")]
    [InlineData("ProductName 4", "Manufactorer 4", "Size 4")]
    public void Delete(string productname, string manufactorer, string size)
    {
        Product product = GetTestRecord(productname, manufactorer, size);
        ProductCommandService commandService = new(_unitOfWork);
        commandService.Insert(product);

        commandService.Delete(product);

        Assert.True(_unitOfWork.ProductRepository.Set(x => x.Id == product.Id).Single().IsDeleted);
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
