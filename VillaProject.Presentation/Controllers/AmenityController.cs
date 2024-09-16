using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VillaProject.Application.Utility;
using VillaProject.Domain.Entities;
using VillaProject.Presentation.ViewModels;
/// <summary>
/// Controller for Ametiy Entity.
/// </summary>
[Authorize(Roles =SD.Role_Admin)]
public class AmenityController : Controller
{
    private readonly ILogger<VillaController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public AmenityController(IUnitOfWork _unitOfWork, ILogger<VillaController> _logger)
    {
        this._unitOfWork = _unitOfWork;
        this._logger = _logger;
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Inside Index Method");
        var result = _unitOfWork.Amenitys.GetAll();
        return View(result);
    }

    public IActionResult Create()
    {
        _logger.LogInformation("Inside Create Method");
        AmenityViewModel amenityViewModel = new()
        {
            AmenityList = _unitOfWork.Villas.GetAll().Select(opt => new SelectListItem
            {
                Value = opt.ID.ToString(),
                Text = opt.VillaName
            })
        };
        return View(amenityViewModel);
    }
    [HttpPost]
    public IActionResult Create(AmenityViewModel amenityViewModel)
    {
        _logger.LogInformation("Inside Create Method : Post Form Creator");
        bool isExist = _unitOfWork.Amenitys.CheckExistance(opt => opt.ID == amenityViewModel.Amenity!.ID);
        ModelState.Remove("Villa");
        if (ModelState.IsValid && !isExist)
        {
            _unitOfWork.Amenitys.Add(amenityViewModel.Amenity!);
            _unitOfWork.Save();
            TempData["success"] = "Entity Has Been Created Successfully";
            _logger.LogInformation("InSide Create Action Method : Entity Has Been Created");
            return RedirectToAction(nameof(Index));
        }
        if (isExist)
        {
            TempData["error"] = "Entity Is Already Created!";
        }
        TempData["error"] = "Entity Has Not Been Created";
        amenityViewModel = new()
        {
            AmenityList = _unitOfWork.Villas.GetAll().Select(opt => new SelectListItem
            {
                Value = opt.ID.ToString(),
                Text = opt.VillaName
            })
        };
        return View(amenityViewModel);
    }

    public IActionResult Update(int ID)
    {
        _logger.LogInformation("InSide Update Action Method");
        bool isExist = _unitOfWork.Amenitys.CheckExistance(opt => opt.ID == ID);
        if (isExist is false)
        {
            TempData["error"] = "Entity Has Not Been Updated Successfully";
            return NotFound();
        }
        AmenityViewModel amenityViewModel = new()
        {
            Amenity = _unitOfWork.Amenitys.GetElementByID(ID: ID),
            AmenityList = _unitOfWork.Villas.GetAll().Select(opt => new SelectListItem
            {
                Value = opt.ID.ToString(),
                Text = opt.VillaName,
            })
        };
        return View(amenityViewModel);
    }

    [HttpPost]
    public IActionResult Update(AmenityViewModel amenityViewModel)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Amenitys.Update(amenityViewModel.Amenity!);
            _unitOfWork.Save();
            TempData["success"] = "Entity Has Been Updated Successfully";
            _logger.LogInformation("InSide Update Action Method : Entity Has Been Updated");
            return RedirectToAction(nameof(Index));
        }
        TempData["error"] = "Entity Has Not Been Updated Successfully";
        return View();
    }

    public IActionResult Delete(int ID)
    {
        _logger.LogInformation("InSide Delete Action Method");
        bool isExist = _unitOfWork.Amenitys.CheckExistance(opt => opt.ID == ID);
        if (isExist is false)
        {
            TempData["error"] = "Entity Has Not Been Updated Successfully";
            return NotFound();
        }
        AmenityViewModel amenityViewModel = new()
        {
            Amenity = _unitOfWork.Amenitys.GetElementByID(ID: ID),
            AmenityList = _unitOfWork.Villas.GetAll().Select(opt => new SelectListItem
            {
                Value = opt.ID.ToString(),
                Text = opt.VillaName,
            })
        };
        return View(amenityViewModel);
    }

    [HttpPost]
    public IActionResult Delete(AmenityViewModel amenityViewModel)
    {
       var result = _unitOfWork.Amenitys.GetElementByID(opt => opt.ID == amenityViewModel.Amenity!.ID);

        if (result is not null)
        {
            _unitOfWork.Amenitys.Delete(result);
            _unitOfWork.Save();
            TempData["success"] = "Entity Has Been Deleted Successfully";
            _logger.LogInformation("InSide Delete Action Method : Entity Has Been Deleted");
            return RedirectToAction(nameof(Index));
        }
        TempData["error"] = "Entity Has Not Been Deleted!!";
        return View();
    }
}