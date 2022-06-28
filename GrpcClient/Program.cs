/*
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
*/

using Cl.Ucn.Disc.Pdis.Fivet.gRPC;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("http://localhost:8080");
var client = new FivetService.FivetServiceClient(channel);
var replyAuthenticate = client.authenticate(new AuthenticationReq 
{
    Login = "199682954",
    Password = "seba1234"
});

Console.Write("Response: " + replyAuthenticate.Persona);

PersonaEntity personaEntity = new PersonaEntity();
personaEntity.Rut = "199682954";
personaEntity.Nombre = "Sebastian Murquio-Castillo";
personaEntity.Email = "sebastian.murquio@alumnos.ucn.cl";
personaEntity.Direccion = "Angamos #0610";
personaEntity.Password = "seba1234";

var replyAddPersona = client.addPersona(new AddPersonaReq
{
    Persona = new PersonaEntity(personaEntity)
});

Console.Write("Response: " + replyAddPersona.Persona);