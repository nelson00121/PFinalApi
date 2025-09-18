using Database.Models;
using Database.Data;
using Bucket;
using Api.Models;
using Bycript;
using HotChocolate.Authorization;
namespace Api.Types;

[QueryType]
public class Query
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Animale> GetAnimales([Service] DatabseContext db) => db.Animales;


    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    [UseSorting]

    public IQueryable<Animale> Getanimal([Service] DatabseContext db) => db.Animales;



    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Alimento> GetAlimentos([Service] DatabseContext db) => db.Alimentos;



    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    [UseSorting]


    public IQueryable<Alimento> GetAlimento([Service] DatabseContext db) => db.Alimentos;


    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Archivo> GetArchivos([Service] DatabseContext db) => db.Archivos;



    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    [UseSorting]

    public IQueryable<Archivo> GetArchivo([Service] DatabseContext db) => db.Archivos;


    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Reporte> GetReportes([Service] DatabseContext db) => db.Reportes;


    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    [UseSorting]

    public IQueryable<Reporte> GetReporte([Service] DatabseContext db) => db.Reportes;


    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<UnidadesDeMedidaAlimento> GetUnidadesDeMedidaAlimentos([Service] DatabseContext db) => db.UnidadesDeMedidaAlimentos;


    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    [UseSorting]

    public IQueryable<UnidadesDeMedidaAlimento> GetUnidadDeMedidaAlimento([Service] DatabseContext db) => db.UnidadesDeMedidaAlimentos;



    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Vacuna> GetVacunas([Service] DatabseContext db) => db.Vacunas;


    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    [UseSorting]


    public IQueryable<Vacuna> GetVacuna([Service] DatabseContext db) => db.Vacunas;



    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<EstadosAnimale> GetEstadosAnimales([Service] DatabseContext db) => db.EstadosAnimales;


    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    [UseSorting]

    public IQueryable<EstadosAnimale> GetEstadoAnimal([Service] DatabseContext db) => db.EstadosAnimales;




    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Raza> GetRazas([Service] DatabseContext db) => db.Razas;


    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    [UseSorting]

    public IQueryable<Raza> GetRaza([Service] DatabseContext db) => db.Razas;



    public string GetMinioEndpoint([Service] IMinioService minioService) => minioService.GetBucketAddress();


    public async Task<string> GetImageUrl(string fileName, [Service] IMinioService minioService)
    {
        return await minioService.GetImageUrlAsync(fileName);
    }


    public async Task<Usuario> Login([Service] DatabseContext db, [Service] IBCryptService bycript, LoginDTO dto)
    {

        Usuario user = db.Usuarios.FirstOrDefault(X => X.Email == dto.User);

        if ((user == null) || (!bycript.VerifyText(dto.Password, user.Password)))

            new GraphQLException("Usuario o contraseña incorrectos");

         

        return user;

    }

    //&& y
    //|| o :  00=0, 01=1, 10=1, 11=1
    // ! no




}
