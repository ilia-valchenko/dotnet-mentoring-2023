using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", false, true);

// The AddOcelot method adds default ASP.NET services to DI-container.
// You could call another more extended AddOcelotUsingBuilder method while
// configuring services to build and use custom builder via an IMvcCoreBuilder interface object.
// See: https://ocelot.readthedocs.io/en/latest/features/dependencyinjection.html
builder.Services.AddOcelot();

var app = builder.Build();

app.UseRouting();
app.UseOcelot().Wait();
app.Run();
