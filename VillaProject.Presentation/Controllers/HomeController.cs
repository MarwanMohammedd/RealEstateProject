using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VillaProject.Presentation.Models;

namespace VillaProject.Presentation.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public HomeController(ILogger<HomeController> _logger ,IUnitOfWork _unitOfWork)
    {
        this._logger = _logger;
        this._unitOfWork = _unitOfWork; 
    }
    
    public IActionResult Index()
    {
        HomeViewModel homeViewModel = new(){
            VillaList = _unitOfWork.Villas.GetAll(includeProperties: "Amenities"),
            CheckInDate = DateOnly.FromDateTime(DateTime.Now),
            NumberOfNights = 1
        };
        return View(homeViewModel);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
