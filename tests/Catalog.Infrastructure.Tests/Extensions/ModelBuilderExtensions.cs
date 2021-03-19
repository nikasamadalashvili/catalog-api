using System.IO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Catalog.Infrastructure.Tests.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder Seed<T>(this ModelBuilder builder, string file) where T : class
        {
            using var reader  = new StreamReader(file);
            var data = reader.ReadToEnd();
            var deserializedData = JsonConvert.DeserializeObject<T[]>(data);
            builder.Entity<T>().HasData(deserializedData);

            return builder;
        }
    }
}