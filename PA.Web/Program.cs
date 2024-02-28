using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using PA.ApplicationCore;
using PA.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpClient<PostesService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7029/");
});
builder.Services.AddHttpClient<SalleService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7029/");
});
builder.Services.AddHttpClient<TypeService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7029/");
});
builder.Services.AddHttpClient<StatusService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7029/");
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;
                                          Initial Catalog=PR3;
                                          Integrated Security=true;
                                          MultipleActiveResultSets=true"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
