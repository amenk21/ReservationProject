using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Domain.Handlers;
using Domain.Commands;
using Domain.Models;
using Data.Context;
using Domain.Queries;
using Domain.Models.Domain.Models;
using Domain.Commands.UtilisateurCommands;
using Domain.Handlers.UtilisateurHandlers;

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
//Utilisateur
builder.Services.AddTransient<IRequestHandler<AddGenericCommand<Utilisateur>, Utilisateur>, AddGenericHandler<Utilisateur>>();
builder.Services.AddTransient<IRequestHandler<GetGenericQuery<Utilisateur>, IEnumerable<Utilisateur>>, GetGenericHandler<Utilisateur>>();
builder.Services.AddTransient<IRequestHandler<GetByIdGenericQuery<Utilisateur>, Utilisateur>, GetByIdGenericHandler<Utilisateur>>();
builder.Services.AddTransient<IRequestHandler<PutGenericCommand<Utilisateur>, Utilisateur>, PutGenericHandler<Utilisateur>>();
builder.Services.AddTransient<IRequestHandler<DeleteGenericCommand<Utilisateur>, Unit>, DeleteGenericHandler<Utilisateur>>();
builder.Services.AddTransient<IRequestHandler<LoginUtilisateurCommand, Utilisateur?>, LoginUtilisateurHandler>();

//Filiale
builder.Services.AddTransient<IRequestHandler<AddGenericCommand<Filiale>, Filiale>, AddGenericHandler<Filiale>>();
builder.Services.AddTransient<IRequestHandler<GetGenericQuery<Filiale>, IEnumerable<Filiale>>, GetGenericHandler<Filiale>>();
builder.Services.AddTransient<IRequestHandler<GetByIdGenericQuery<Filiale>, Filiale>, GetByIdGenericHandler<Filiale>>();
builder.Services.AddTransient<IRequestHandler<PutGenericCommand<Filiale>, Filiale>, PutGenericHandler<Filiale>>();
builder.Services.AddTransient<IRequestHandler<DeleteGenericCommand<Filiale>, Unit>, DeleteGenericHandler<Filiale>>();
//Salle
builder.Services.AddTransient<IRequestHandler<AddGenericCommand<Salle>, Salle>, AddGenericHandler<Salle>>();
builder.Services.AddTransient<IRequestHandler<GetGenericQuery<Salle>, IEnumerable<Salle>>, GetGenericHandler<Salle>>();
builder.Services.AddTransient<IRequestHandler<GetByIdGenericQuery<Salle>, Salle>, GetByIdGenericHandler<Salle>>();
builder.Services.AddTransient<IRequestHandler<PutGenericCommand<Salle>, Salle>, PutGenericHandler<Salle>>();
builder.Services.AddTransient<IRequestHandler<DeleteGenericCommand<Salle>, Unit>, DeleteGenericHandler<Salle>>();

//Reservation 
builder.Services.AddTransient<IRequestHandler<AddGenericCommand<Reservations>, Reservations>, AddGenericHandler<Reservations>>();
builder.Services.AddTransient<IRequestHandler<GetGenericQuery<Reservations>, IEnumerable<Reservations>>, GetGenericHandler<Reservations>>();
builder.Services.AddTransient<IRequestHandler<GetByIdGenericQuery<Reservations>, Reservations>, GetByIdGenericHandler<Reservations>>();
builder.Services.AddTransient<IRequestHandler<PutGenericCommand<Reservations>, Reservations>, PutGenericHandler<Reservations>>();
builder.Services.AddTransient<IRequestHandler<DeleteGenericCommand<Reservations>, Unit>, DeleteGenericHandler<Reservations>>();



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
