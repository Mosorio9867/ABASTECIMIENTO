using ACME_ABASTECIMIENTO.Models;
using Microsoft.AspNetCore.Mvc;

namespace ACME_ABASTECIMIENTO.Controllers;

[ApiController]
[Route("[controller]")]
public class AbastecimientoController : ControllerBase
{
    public AbastecimientoController()
    {
    }

    [HttpGet(Name = "SendInformation")]
    public List<AbastecimientoParametro> SendInformation()
    {
        return new List<AbastecimientoParametro>();
    }
}
