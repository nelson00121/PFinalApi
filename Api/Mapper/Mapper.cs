

using Database.DTOS;
using Database.Models;
using Riok.Mapperly.Abstractions;

namespace Api.Mapper
{
    [Mapper]
    public partial class Mapa
    {
        public partial Alimento AlimentoCreateDtoToAlimento(AlimentoCreateDto dto);
        public partial Animale AnimaleCreateDtoToAnimale(AnimaleCreateDto dto);
        public partial Archivo ArchivoCreateDtoToArchivo(ArchivoCreateDto dto);
        public partial BitacoraAlimento BitacoraAlimentoCreateDtoToBitacoraAlimento(BitacoraAlimentoCreateDto dto);
        public partial BitacoraPeso BitacoraPesoCreateDtoToBitacoraPeso(BitacoraPesoCreateDto dto);
        public partial BitacorasVacuna BitacorasVacunaCreateDtoToBitacorasVacuna(BitacorasVacunaCreateDto dto);
        public partial CompraAlimento CompraAlimentoCreateDtoToCompraAlimento(CompraAlimentoCreateDto dto);
        public partial Empleado EmpleadoCreateDtoToEmpleado(EmpleadoCreateDto dto);
        public partial EstadosAnimale EstadosAnimaleCreateDtoToEstadosAnimale(EstadosAnimaleCreateDto dto);
        public partial HistorialAlimenticio HistorialAlimenticioCreateDtoToHistorialAlimenticio(HistorialAlimenticioCreateDto dto);
        public partial Raza RazaCreateDtoToRaza(RazaCreateDto dto);
        public partial Reporte ReporteCreateDtoToReporte(ReporteCreateDto dto);
        public partial TiposAlimento TiposAlimentoCreateDtoToTiposAlimento(TiposAlimentoCreateDto dto);
        public partial UnidadesDeMedidaAlimento UnidadesDeMedidaAlimentoCreateDtoToUnidadesDeMedidaAlimento(UnidadesDeMedidaAlimentoCreateDto dto);
        public partial Usuario UsuarioCreateDtoToUsuario(UsuarioCreateDto dto);
        public partial Vacuna VacunaCreateDtoToVacuna(VacunaCreateDto dto);
    }
}