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

var replyAddPersona = client.addPersona(new AddPersonaReq
{
    Persona = new PersonaEntity
    {
        Rut = "19968295-4",
        Nombre = "Sebastian Murquio Castillo",
        Email = "sebastian.murquio@alumnos.ucn.cl",
        Direccion = "Angamos #0610",
        Password = "seba1234"
    }
});

Console.Write("Response AddPersona: " + replyAddPersona.Persona);

var replyAuthenticate = client.authenticate(new AuthenticationReq 
{
    Login = "19968295-4",
    Password = "seba1234"
});

Console.Write("\nResponse Authenticate: " + replyAuthenticate.Persona);

var replyAddFichaMedica = client.addFichaMedica(new AddFichaMedicaReq()
{
    FichaMedica = new FichaMedicaEntity
    {
        Numero = 1,
        NombrePaciente = "Mane",
        Especie = "Gato",
        FechaNacimiento = "2021-11-16",
        Raza = "Calico",
        Color = "Blanco",
        Tipo = "Normal",
        Sexo = SexoEntity.Hembra,
        Duenio = new PersonaEntity
        {
            Rut = "19968462-0",
            Nombre = "Vanesa Briones Ibacache",
            Email = "vanesa.briones@alumnos.ucn.cl",
            Direccion = "Angamos #0610"
        }
    }
});

Console.Write("\nResponse AddFichaMedica: " + replyAddFichaMedica.FichaMedica);

var replyAddControl = client.addControl(new AddControlReq
{
    Control = new ControlEntity
    {
        Fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK"),
        Temperatura = 38.5,
        Peso = 10.5,
        Altura = 0.6,
        Diagnostico = "Sin novedad",
        Veterinario = new PersonaEntity
        {
            Rut = "19968295-4",
            Nombre = "Sebastian Murquio Castillo",
            Email = "sebastian.murquio@alumnos.ucn.cl",
            Direccion = "Angamos #0610"
        }
    },
    NumeroFichaMedica = 1
});

Console.Write("\nResponse AddControl: " + replyAddControl.FichaMedica);

var replyRetrieveFichaMedica = client.retrieveFichaMedica(new RetrieveFichaMedicaReq
{
    NumeroFichaMedica = 1
});

Console.Write("\nResponse RetrieveFichaMedica: " + replyRetrieveFichaMedica.FichaMedica);

using var replySearchFichaMedica = client.searchFichaMedica(new SearchFichaMedicaReq
{
    Query = "1"
});

while (await replySearchFichaMedica.ResponseStream.MoveNext(CancellationToken.None))
{
    Console.Write("\nResponse SearchFichaMedica: " + replySearchFichaMedica.ResponseStream.Current.FichaMedica);
}
