using ACME_ABASTECIMIENTO.Models;

namespace ACME_ABASTECIMIENTO.Services.Interfaces
{
    public interface IAbastecimientoService
    {
        public Task<AbastecimientoRespuesta> EnviarInformacion(AbastecimientoParametro abastecimientoParametro);
    }
}
