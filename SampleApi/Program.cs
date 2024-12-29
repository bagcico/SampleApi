using Bogus;
using Microsoft.EntityFrameworkCore;
using SampleApi.Application.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
     .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
     });

builder.Services.AddOpenApi();

builder.Services.AddDbContext<ShopDbContext>(options =>
{
    options.UseInMemoryDatabase("TestDatabase");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
    SeedDatabase(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void SeedDatabase(ShopDbContext context)
{
    var categoryFaker = new Faker<Category>()
        .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
        .RuleFor(p => p.ContentHtml, f => f.Lorem.Paragraphs(2));

    var categories = categoryFaker.Generate(5);
    context.Categories.AddRange(categories);

    var productFaker = new Faker<Product>()
    .RuleFor(p => p.Name, f => f.Commerce.ProductName())
    .RuleFor(p => p.Price, f => f.Random.Double(1, 10))
    .RuleFor(p => p.ContentHtml, f => f.Lorem.Paragraphs(10))
    .RuleFor(p => p.CategoryId, f => f.PickRandom(categories).Id);

    var products = productFaker.Generate(2500);
    context.Products.AddRange(products);

    context.SaveChanges();

}