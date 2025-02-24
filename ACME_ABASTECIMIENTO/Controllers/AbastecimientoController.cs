using ACME_ABASTECIMIENTO.Models;
using ACME_ABASTECIMIENTO.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ACME_ABASTECIMIENTO.Controllers;

[ApiController]
[Route("[controller]")]
public class AbastecimientoController : ControllerBase
{
    private readonly IAbastecimientoService _abastecimientoService;

    public AbastecimientoController(IAbastecimientoService abastecimientoService)
    {
        _abastecimientoService = abastecimientoService;
    }

    [HttpPost("EnviarInformacion")]
    public async Task<AbastecimientoRespuesta> EnviarInformacion(AbastecimientoParametro abastecimientoParametro)
    {
        return await _abastecimientoService.EnviarInformacion(abastecimientoParametro);
    }
}
