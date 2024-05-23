using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VillaProject.Domain.Entities;
/// <summary>
/// Controller for Villa Entity.
/// </summary>
public class VillaController : Controller
{
    private readonly ILogger<VillaController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public VillaController(IUnitOfWork _unitOfWork, ILogger<VillaController> _logger)
    {
        this._unitOfWork = _unitOfWork;
        this._logger = _logger;
    }

    //Defualt EndPoint Of Villa Controller
    public IActionResult Index()
    {
        _logger.LogInformation("InSide Index Action Method : Show All Villa Entity");
        var result = _unitOfWork.Villas.GetAll();
        return View(result);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Villa villa)
    {
        _logger.LogInformation("InSide Create Action Method");
        //Custome Validation
        if (villa.VillaName == villa.Description)
        {
            ModelState.AddModelError("VillaName",
            "VillaName and Description Can Not Match Each Other");
        }
        if (ModelState.IsValid)
        {
            _logger.LogInformation("InSide Create Action Method : Create New Villa Entity");
            _unitOfWork.Villas.Add(villa);
            _unitOfWork.Save();
            TempData["success"] = "Entity Has Been Created Successfully";
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Update(int ID)
    {
        _logger.LogInformation("InSide Update Action Method");
        var result = _unitOfWork.Villas.GetElementByID(ID:ID);
        if (result is null)
        {
            TempData["error"] = "Entity Has Not Been Updated Successfully";
            return NotFound("CaptureUnExpected");
        }
        return View(result);
    }

    [HttpPost]
    public IActionResult Update(Villa villa)
    {
        _logger.LogInformation("InSide Update Action Method");
        if (ModelState.IsValid)
        {
            _unitOfWork.Villas.Update(villa);
            _unitOfWork.Save();
             TempData["success"] = "Entity Has Been Updated Successfully";
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Delete(int ID)
    {
        Villa? result = _unitOfWork.Villas.GetElementByID(objectVilla => objectVilla.ID == ID);
        if (result is null)
        {
            _logger.LogInformation("InSide Delete Action Method : ID Not Exist");
            TempData["error"] = "Entity Has Not Been Deleted Successfully";
            return View("CaptureUnExpected");
        }
        return View(result);
    }

    [HttpPost]
    public IActionResult Delete(Villa villa)
    {
        Villa? result = _unitOfWork.Villas.GetElementByID(objectVilla => objectVilla.ID == villa.ID);
        if (result is not null)
        {
            _logger.LogInformation("InSide Delete Action Method : Entity Has Been Deleted!");
            _unitOfWork.Villas.Delete(result);
            _unitOfWork.Save();
            TempData["success"] = "Entity Has Been Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }
        TempData["error"] = "Entity Has Not Been Deleted Successfully";
        return View();
    }

}