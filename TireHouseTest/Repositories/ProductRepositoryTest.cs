using Microsoft.EntityFrameworkCore;
using TireHouse.DTO;

namespace TireHouseTest.Repositories;

public class ProductRepositoryTest : RepositoryTestBase
{
    [Theory]
    [InlineData("Product 1", "Manufactorer 1", "Size 1")]
    [InlineData("Product 2", "Manufactorer 2", "Size 2")]
    [InlineData("Product 3", "Manufactorer 3", "Size 3")]
    [InlineData("Product 4", "Manufactorer 4", "Size 4")]
    public void Insert(string name, string manufatcorer, string size)
    {
        Product product = GetTestRecord(name, manufatcorer, size);
        _unitOfWork.ProductRepository.Insert(product);
        _unitOfWork.SaveChanges();

        Assert.NotNull(product.Name);
        Assert.True(product.Id > 0);
    }

    [Theory]
    [InlineData("Product 1", "Manufactorer 1", "Size 1")]
    [InlineData("Product 2", "Manufactorer 2", "Size 2")]
    [InlineData("Product 3", "Manufactorer 3", "Size 3")]
    [InlineData("Product 4", "Manufactorer 4", "Size 4")]
    public void NotInserted(string name, string manufactorer, string size)
    {
        Product product = GetTestRecord(name, manufactorer, size);
        product.Id = 1;

        Assert.Throws<ArgumentException>(() =>
        {
            _unitOfWork.ProductRepository.Insert(product);
            _unitOfWork.SaveChanges();
        });
    }

    [Theory]
    [InlineData("Product 1", "Manufactorer 1", "Size 1")]
    [InlineData("Product 2", "Manufactorer 2", "Size 2")]
    [InlineData("Product 3", "Manufactorer 3", "Size 3")]
    [InlineData("Product 4", "Manufactorer 4", "Size 4")]
    public void Update(string name, string manufactorer, string size)
    {
        Product newProduct = GetTestRecord(name, manufactorer, size);
        _unitOfWork.ProductRepository.Insert(newProduct);
        _unitOfWork.SaveChanges();

        newProduct.Name = $"New {name}";
        newProduct.Manufactorer = $"New {manufactorer}";
        newProduct.Size = $"New {size}";
        _unitOfWork.ProductRepository.Update(newProduct);
        _unitOfWork.SaveChanges();

        Product updatedProduct = _unitOfWork.ProductRepository.Set(x => x.Id == newProduct.Id).Single();

        Assert.NotNull(updatedProduct);
        Assert.True(updatedProduct.Id == newProduct.Id);
    }

    [Theory]
    [InlineData("Product 1", "Manufactorer 1", "Size 1")]
    [InlineData("Product 2", "Manufactorer 2", "Size 2")]
    [InlineData("Product 3", "Manufactorer 3", "Size 3")]
    [InlineData("Product 4", "Manufactorer 4", "Size 4")]
    public void NotUpdated(string name, string manufactorer, string size)
    {
        Product newProduct = GetTestRecord(name, manufactorer, size);

        Assert.Throws<DbUpdateConcurrencyException>(() =>
        {
            _unitOfWork.ProductRepository.Update(newProduct);
            _unitOfWork.SaveChanges();
        });
    }

    [Theory]
    [InlineData("Product 1", "Manufactorer 1", "Size 1")]
    [InlineData("Product 2", "Manufactorer 2", "Size 2")]
    [InlineData("Product 3", "Manufactorer 3", "Size 3")]
    [InlineData("Product 4", "Manufactorer 4", "Size 4")]
    public void DeleteByObject(string name, string manufactorer, string size)
    {
        Product product = GetTestRecord(name, manufactorer, size);
        _unitOfWork.ProductRepository.Insert(product);
        _unitOfWork.SaveChanges();

        _unitOfWork.ProductRepository.Delete(product);
        _unitOfWork.SaveChanges();

        Assert.Null(_unitOfWork.ProductRepository.Set(x => x.Id == product.Id).SingleOrDefault());
    }

    [Theory]
    [InlineData("Product 1", "Manufactorer 1", "Size 1")]
    [InlineData("Product 2", "Manufactorer 2", "Size 2")]
    [InlineData("Product 3", "Manufactorer 3", "Size 3")]
    [InlineData("Product 4", "Manufactorer 4", "Size 4")]
    public void DeleteById(string name, string manufactorer, string size)
    {
        Product product = GetTestRecord(name, manufactorer, size);
        _unitOfWork.ProductRepository.Insert(product);
        _unitOfWork.SaveChanges();

        _unitOfWork.ProductRepository.Delete(product.Id);
        _unitOfWork.SaveChanges();

        Assert.Null(_unitOfWork.ProductRepository.Set(x => x.Id == product.Id).SingleOrDefault());
    }

    [Theory]
    [InlineData("Product 1", "Manufactorer 1", "Size 1")]
    [InlineData("Product 2", "Manufactorer 2", "Size 2")]
    [InlineData("Product 3", "Manufactorer 3", "Size 3")]
    [InlineData("Product 4", "Manufactorer 4", "Size 4")]
    public void GetById(string name, string manufactorer, string size)
    {
        Product product = GetTestRecord(name, manufactorer, size);
        _unitOfWork.ProductRepository.Insert(product);
        _unitOfWork.SaveChanges();

        Product retrievedProduct = _unitOfWork.ProductRepository.Get(product.Id);

        Assert.True(retrievedProduct.Name == product.Name &&
            retrievedProduct.Manufactorer == product.Manufactorer &&
            retrievedProduct.Size == product.Size);
    }

    [Theory]
    [InlineData("Product 1", "Manufactorer 1", "Size 1")]
    [InlineData("Product 2", "Manufactorer 2", "Size 2")]
    [InlineData("Product 3", "Manufactorer 3", "Size 3")]
    [InlineData("Product 4", "Manufactorer 4", "Size 4")]
    public void Set(string name, string manufactorer, string size)
    {
        Product product = GetTestRecord(name, manufactorer, size);
        List<Product> exceptedSet = new();
        exceptedSet.Add(product);

        _unitOfWork.ProductRepository.Insert(product);
        _unitOfWork.SaveChanges();

        var retrievedProduct = _unitOfWork.ProductRepository.Set();

        Assert.True(exceptedSet.Last().Name == retrievedProduct.Last().Name &&
            exceptedSet.Last().Manufactorer == retrievedProduct.Last().Manufactorer &&
            exceptedSet.Last().Size == retrievedProduct.Last().Size);
    }

    private static Product GetTestRecord(string name, string manufactorer, string size)
    {
        Product product = new()
        {
            Name = name,
            Manufactorer = manufactorer,
            Size = size
        };

        return product;
    }
}
