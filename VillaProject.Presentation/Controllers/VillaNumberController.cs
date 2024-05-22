using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VillaProject.Domain.Entities;
using VillaProject.Presentation.ViewModels;

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
        var result = _applicationDBContext.VillaNumbers.Include(opt => opt.Villa).ToList();
        return View(result);
    }

    public IActionResult Create()
    {
        VillaNumberViewModel villaNumberViewModel = new VillaNumberViewModel()
        {
            VillaList = _applicationDBContext.Villas.Select(opt => new SelectListItem
            {
                Text = opt.VillaName,
                Value = opt.ID.ToString()
            }).ToList()
        };
        return View(villaNumberViewModel);
    }

    [HttpPost]
    public IActionResult Create(VillaNumberViewModel villaNumberVM)
    {
        _logger.LogInformation("InSide Create Action Method");
        bool roomIdExistance = _applicationDBContext.VillaNumbers.Any(opt => opt.VillaNumberID == villaNumberVM.VillaNumber!.VillaNumberID);
        ModelState.Remove("Villa");
        if (ModelState.IsValid && !roomIdExistance)
        {
            _applicationDBContext.VillaNumbers.Add(villaNumberVM.VillaNumber!);
            _applicationDBContext.SaveChanges();
            TempData["success"] = "Entity Has Been Created Successfully";
            _logger.LogInformation("InSide Create Action Method : Entity Has Been Created");
            return RedirectToAction(nameof(Index));
        }
        if (roomIdExistance)
        {
            TempData["error"] = "Entity Is Already Created!";
            villaNumberVM.VillaList = _applicationDBContext.Villas.Select(opt => new SelectListItem
            {
                Text = opt.VillaName,
                Value = opt.ID.ToString()
            }).ToList();
            return View(villaNumberVM);
        }
        TempData["error"] = "Entity Has Not Been Created Successfully";
        villaNumberVM.VillaList = _applicationDBContext.Villas.Select(opt => new SelectListItem
        {
            Text = opt.VillaName,
            Value = opt.ID.ToString()
        }).ToList();
        return View(villaNumberVM);
    }

    public IActionResult Update(int ID)
    {
        _logger.LogInformation("InSide Update Action Method");

        VillaNumberViewModel villaNumberViewModel = new()
        {
            VillaList = _applicationDBContext.Villas.Select(opt => new SelectListItem
            {
                Text = opt.VillaName,
                Value = opt.ID.ToString()
            }).ToList(),
            VillaNumber = _applicationDBContext.VillaNumbers.Find(ID)
        };
        if (villaNumberViewModel.VillaNumber is null)
        {
            TempData["error"] = "Entity Has Not Been Updated Successfully";
            return NotFound("UnExpected");
        }
        return View(villaNumberViewModel);
    }

    [HttpPost]
    public IActionResult Update(VillaNumberViewModel villaNumberViewModel)
    {
        if (ModelState.IsValid)
        {
            _applicationDBContext.VillaNumbers.Update(villaNumberViewModel.VillaNumber!);
            _applicationDBContext.SaveChanges();
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
        VillaNumberViewModel villaNumberViewModel = new()
        {
            VillaNumber = _applicationDBContext.VillaNumbers.Find(ID),
            VillaList = _applicationDBContext.Villas.Select(opt => new SelectListItem
            {
                Text = opt.VillaName,
                Value = opt.ID.ToString()
            }).ToList(),
        };
        if (villaNumberViewModel is null)
        {
            TempData["error"] = "Entity Has Not Been Deleted Successfully";
            return NotFound("UnExpected");
        }
        return View(villaNumberViewModel);
    }

    [HttpPost]
    public IActionResult Delete(VillaNumberViewModel villaNumberViewModel)
    {
        var result = _applicationDBContext.VillaNumbers.FirstOrDefault(opt => opt.VillaNumberID == villaNumberViewModel.VillaNumber!.VillaNumberID);
        
        if (result is not null)
        {
            _applicationDBContext.VillaNumbers.Remove(result);
            _applicationDBContext.SaveChanges();
            TempData["success"] = "Entity Has Been Deleted Successfully";
            _logger.LogInformation("InSide Delete Action Method : Entity Has Been Deleted");
            return RedirectToAction(nameof(Index));
        }
        TempData["error"] = "Entity Has Not Been Deleted Successfully";
        return View();
    }

}