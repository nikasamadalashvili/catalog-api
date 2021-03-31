using System;
using System.Threading.Tasks;
using Catalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Catalog.Infrastructure.Tests
{
    public class ItemRepositoryTests
    {
        [Fact]
        public async Task should_get_data()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>()
                .UseInMemoryDatabase("test");

            await using var context = new TestCatalogContext(optionsBuilder.Options);
            context.Database.EnsureCreated();
            var itemRepo = new ItemRepository(context);
            var result = await itemRepo.GetAsync();

            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task should_returns_null_with_id_not_present()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>()
                .UseInMemoryDatabase("should_returns_null_with_id_not_present");

            await using var context = new TestCatalogContext(optionsBuilder.Options);
            context.Database.EnsureCreated();
            var itemRepo = new ItemRepository(context);
            var item = await itemRepo.GetAsync(Guid.NewGuid());

            item.ShouldBeNull();
        }

        [Theory]
        [InlineData("b5b05534-9263-448c-a69e-0bbd8b3eb90e")]
        public async Task should_return_record_by_id(string guid)
        {
            var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName:
            "should_return_record_by_id")
            .Options;

            await using var context = new TestCatalogContext(options);
            context.Database.EnsureCreated();

            var sut = new ItemRepository(context);
            var result = await sut.GetAsync(new Guid(guid));

            result.Id.ShouldBe(new Guid(guid));
        }
    }
}