using GenCore.Data.Repositories.Implementation;
using GenCore.Data.Repositories.Interface;
using GenCryptography.Service.Utilities.Interface;

string connectionString = "Data Source=localhost;Initial Catalog=CwRetail;Persist Security Info=true;User ID=TestLogin; Password = ABC123";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IDecryptor>();
builder.Services.AddTransient<IUserVerificationRepository>(s => new UserVerificationRepository(connectionString));

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
