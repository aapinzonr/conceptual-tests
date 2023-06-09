// See https://aka.ms/new-console-template for more information
using EF.CodeFirstDemo;
using EF.CodeFirstDemo.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");
StartDbContext();

void StartDbContext()
{
    using DemoDbContext context = new DemoDbContext();
    DemoProduct product = new DemoProduct
    {
        Name = "Visual Studio Code",
        Enabled = true,
        InInventory = 100,
        MaxQuantity = 11,
        MinQuantity = 1
    };
    context.DemoProduct.Add(product);
    context.SaveChanges();

    Console.WriteLine("Entity Framework Core Code-First sample");
    Console.WriteLine();
}