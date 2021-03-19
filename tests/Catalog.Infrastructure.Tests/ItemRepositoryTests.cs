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
    }
}