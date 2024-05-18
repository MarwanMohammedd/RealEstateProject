using Microsoft.AspNetCore.Mvc;
using VillaProject.Domain.Entities;
/// <summary>
/// Controller for Villa Entity.
/// </summary>
public class VillaController : Controller
{
    private readonly ILogger<VillaController> _logger;
    private readonly ApplicationDBContext _applicationDBContext;
    public VillaController(ApplicationDBContext _applicationDBContext, ILogger<VillaController> _logger)
    {
        this._applicationDBContext = _applicationDBContext;
        this._logger = _logger;
    }

    //Defualt EndPoint Of Villa Controller
    public IActionResult Index(){
        _logger.LogInformation("InSide Index Action Method");
        var result = _applicationDBContext.Villas.ToList();
        return View(result);
    }
}