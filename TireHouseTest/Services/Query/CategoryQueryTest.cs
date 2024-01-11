using TireHouse.DTO;
using TireHouse.Service;

namespace TireHouseTest.Services.Query;

public class CategoryQueryTest : QueryTestBase
{

    [Theory]
    [InlineData("CategoryName 1", "Description 1")]
    [InlineData("CategoryName 2", "Description 2")]
    [InlineData("CategoryName 3", "Description 3")]
    [InlineData("CategoryName 4", "Description 4")]
    public void Get(string name, string description)
    {
        Category category = GetTestRecord(name, description);
        _unitOfWork.CategoryRepository.Insert(category);
        _unitOfWork.SaveChanges();

        CategoryQueryService queryService = new(_unitOfWork);

        var retrievedCategory = queryService.Get(category.Id);

        Assert.True(retrievedCategory.Id == category.Id);
    }

    [Theory]
    [InlineData("CategoryName 1", "Description 1")]
    [InlineData("CategoryName 2", "Description 2")]
    [InlineData("CategoryName 3", "Description 3")]
    [InlineData("CategoryName 4", "Description 4")]
    public void Set(string name, string description)
    {
        Category category = GetTestRecord(name, description);
        List<Category> exceptedSet = new();
        exceptedSet.Add(category);

        _unitOfWork.CategoryRepository.Insert(category);
        _unitOfWork.SaveChanges();

        CategoryQueryService queryService = new(_unitOfWork);

        var retrievedCategory = queryService.Set();

        Assert.True(exceptedSet.Last().Name == retrievedCategory.Last().Name &&
                    exceptedSet.Last().Description == retrievedCategory.Last().Description);
    }

    [Theory]
    [InlineData("CategoryName 1", "Description 1")]
    [InlineData("CategoryName 2", "Description 2")]
    [InlineData("CategoryName 3", "Description 3")]
    [InlineData("CategoryName 4", "Description 4")]
    public void ExpressionSet(string name, string description)
    {
        Category category = GetTestRecord(name, description);
        List<Category> exceptedSet = new();
        exceptedSet.Add(category);

        _unitOfWork.CategoryRepository.Insert(category);
        _unitOfWork.SaveChanges();

        CategoryQueryService queryService = new(_unitOfWork);

        var retrievedCategory = queryService.Set(x => x.Id == category.Id);

        Assert.True(exceptedSet.Last().Name == retrievedCategory.Last().Name &&
                    exceptedSet.Last().Description == retrievedCategory.Last().Description);
    }

    private static Category GetTestRecord(string name, string description)
    {
        Category category = new()
        {
            Name = name,
            Description = description
        };

        return category;
    }

}
