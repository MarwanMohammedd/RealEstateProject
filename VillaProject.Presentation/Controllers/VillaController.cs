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
    private readonly ApplicationDBContext _applicationDBContext;
    public VillaController(ApplicationDBContext _applicationDBContext, ILogger<VillaController> _logger)
    {
        this._applicationDBContext = _applicationDBContext;
        this._logger = _logger;
    }

    //Defualt EndPoint Of Villa Controller
    public IActionResult Index()
    {
        _logger.LogInformation("InSide Index Action Method : Show All Villa Entity");
        var result = _applicationDBContext.Villas.ToList();
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
            _applicationDBContext.Villas.Add(villa);
            _applicationDBContext.SaveChanges();
            TempData["success"] = "Entity Has Been Created Successfully";
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Update(int ID)
    {
        _logger.LogInformation("InSide Update Action Method");
        var result = _applicationDBContext.Villas.Find(ID);
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
            _applicationDBContext.Villas.Update(villa);
            _applicationDBContext.SaveChanges();
             TempData["success"] = "Entity Has Been Updated Successfully";
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Delete(int ID)
    {
        Villa? result = _applicationDBContext.Villas.FirstOrDefault(objectVilla => objectVilla.ID == ID);
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
        Villa? result = _applicationDBContext.Villas.FirstOrDefault(objectVilla => objectVilla.ID == villa.ID);
        if (result is not null)
        {
            _logger.LogInformation("InSide Delete Action Method : Entity Has Been Deleted!");
            _applicationDBContext.Villas.Remove(result);
            _applicationDBContext.SaveChanges();
            TempData["success"] = "Entity Has Been Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }
        TempData["error"] = "Entity Has Not Been Deleted Successfully";
        return View();
    }

}