using API_Project_2_34854673.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


// Add DbContext with SQL Server connection
builder.Services.AddDbContext<NwutechTrendsContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("Server=tcp:apiserv.database.windows.net,1433;Initial Catalog=NWUTechTrends;Persist Security Info=False;User ID=Abby;Password=2024@Azure;Trusted_Connection=True;MultipleActiveResultSets=true; Encrypt=True;"))); /// 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
