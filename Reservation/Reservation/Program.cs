using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Domain.Handlers;
using Domain.Commands;
using Domain.Models;
using Data.Context;
using Domain.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Server=localhost;Database=ReservationDb;Trusted_Connection=True;TrustServerCertificate=True;"));

builder.Services.AddRazorPages();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AddGenericHandler<>).Assembly);
});

builder.Services.AddScoped(typeof(Domain.Interface.IGenericRepository<>), typeof(Data.Repositories.GenericRepository<>));
builder.Services.AddTransient<IRequestHandler<AddGenericCommand<Utilisateur>, Utilisateur>, AddGenericHandler<Utilisateur>>();
builder.Services.AddTransient<IRequestHandler<GetGenericQuery<Utilisateur>, IEnumerable<Utilisateur>>, GetGenericHandler<Utilisateur>>();
builder.Services.AddTransient<IRequestHandler<GetByIdGenericQuery<Utilisateur>, Utilisateur>, GetByIdGenericHandler<Utilisateur>>();
builder.Services.AddTransient<IRequestHandler<PutGenericCommand<Utilisateur>, Utilisateur>, PutGenericHandler<Utilisateur>>();
builder.Services.AddTransient<IRequestHandler<DeleteGenericCommand<Utilisateur>, Unit>, DeleteGenericHandler<Utilisateur>>();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
