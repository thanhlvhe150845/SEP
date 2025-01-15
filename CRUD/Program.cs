using CRUD.Models;
using CRUD.Pages.StudentS;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<PRN211_1Context>();
builder.Services.AddHttpClient("APIClient", client =>
{
    var apiBase = builder.Configuration.GetValue<string>("API_BASE");  
    client.BaseAddress = new Uri(apiBase);  
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
