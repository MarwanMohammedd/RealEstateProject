using Microsoft.AspNetCore.Mvc;
using VillaProject.Domain.Entities;

public class VillaNumberController : Controller
{
    private readonly ApplicationDBContext _applicationDBContext;
    private readonly ILogger<VillaNumberController> _logger;
    public VillaNumberController(ApplicationDBContext _applicationDBContext, ILogger<VillaNumberController> _logger)
    {
        this._applicationDBContext = _applicationDBContext;
        this._logger = _logger;
    }

    public IActionResult Index()
    {
        _logger.LogInformation("InSide Index Action Method");
        var result = _applicationDBContext.VillaNumbers.ToList();
        return View(result);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(VillaNumber villaNumber)
    {
        _logger.LogInformation("InSide Create Action Method");
        ModelState.Remove("Villa");
        if (ModelState.IsValid)
        {
            _applicationDBContext.VillaNumbers.Add(villaNumber);
            _applicationDBContext.SaveChanges();
            TempData["success"] = "Entity Has Been Created Successfully";
            _logger.LogInformation("InSide Create Action Method : Entity Has Been Created");
            return RedirectToAction("Index");
        }
        TempData["error"] = "Entity Has Not Been Created Successfully";
        return View();
    }

    public IActionResult Update(int ID)
    {
        _logger.LogInformation("InSide Update Action Method");
        var result = _applicationDBContext.VillaNumbers.Find(ID);
        if (result is null)
        {
            TempData["error"] = "Entity Has Not Been Updated Successfully";
            return NotFound("UnExpected");
        }
        return View(result);
    }

    [HttpPost]
    public IActionResult Update(VillaNumber villaNumber)
    {
        ModelState.Remove("Villa");
        if (ModelState.IsValid)
        {
            _applicationDBContext.VillaNumbers.Update(villaNumber);
            _applicationDBContext.SaveChanges();
            TempData["success"] = "Entity Has Been Updated Successfully";
            _logger.LogInformation("InSide Update Action Method : Entity Has Been Updated");
            return RedirectToAction("Index");
        }
        TempData["error"] = "Entity Has Not Been Updated Successfully";
        return View();
    }

    public IActionResult Delete(int ID)
    {
        _logger.LogInformation("InSide Delete Action Method");
        VillaNumber? result = _applicationDBContext.VillaNumbers.Find(ID);
        if (result is null)
        {
            TempData["error"] = "Entity Has Not Been Deleted Successfully";
            return NotFound("UnExpected");
        }
        return View(result);
    }

    [HttpPost]
    public IActionResult Delete(VillaNumber villaNumber)
    {
        var result = _applicationDBContext.VillaNumbers.FirstOrDefault(opt => opt.VillaNumberID == villaNumber.VillaNumberID);
        if (result is not null)
        {
            _applicationDBContext.VillaNumbers.Remove(result);
            _applicationDBContext.SaveChanges();
            TempData["success"] = "Entity Has Been Deleted Successfully";
            _logger.LogInformation("InSide Delete Action Method : Entity Has Been Deleted");
            return RedirectToAction("Index");
        }
        TempData["error"] = "Entity Has Not Been Deleted Successfully";
        return View();
    }

}