using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VillaProject.Presentation.ViewModels;

public class VillaNumberController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<VillaNumberController> _logger;
    public VillaNumberController(IUnitOfWork _unitOfWork, ILogger<VillaNumberController> _logger)
    {
        this._unitOfWork = _unitOfWork;
        this._logger = _logger;
    }

    public IActionResult Index()
    {
        _logger.LogInformation("InSide Index Action Method");
        var result = _unitOfWork.VillaNumbers.GetVillaNames("Villa");
        return View(result);
    }

    public IActionResult Create()
    {
        VillaNumberViewModel villaNumberViewModel = new VillaNumberViewModel()
        {
            VillaList = _unitOfWork.Villas.GetAll().Select(opt => new SelectListItem
            {
                Text = opt.VillaName,
                Value = opt.ID.ToString()
            })
        };
        return View(villaNumberViewModel);
    }

    [HttpPost]
    public IActionResult Create(VillaNumberViewModel villaNumberVM)
    {
        _logger.LogInformation("InSide Create Action Method");
        bool roomIdExistance = _unitOfWork.VillaNumbers.CheckExistance(opt => opt.VillaNumberID == villaNumberVM.VillaNumber!.VillaNumberID);
        ModelState.Remove("Villa");
        if (ModelState.IsValid && !roomIdExistance)
        {
            _unitOfWork.VillaNumbers.Add(villaNumberVM.VillaNumber!);
            _unitOfWork.Save();
            TempData["success"] = "Entity Has Been Created Successfully";
            _logger.LogInformation("InSide Create Action Method : Entity Has Been Created");
            return RedirectToAction(nameof(Index));
        }
        if (roomIdExistance)
        {
            TempData["error"] = "Entity Is Already Created!";
            villaNumberVM.VillaList = _unitOfWork.Villas.GetAll().Select(opt => new SelectListItem
            {
                Text = opt.VillaName,
                Value = opt.ID.ToString()
            });
            return View(villaNumberVM);
        }
        TempData["error"] = "Entity Has Not Been Created Successfully";
        villaNumberVM.VillaList = _unitOfWork.Villas.GetAll().Select(opt => new SelectListItem
        {
            Text = opt.VillaName,
            Value = opt.ID.ToString()
        });
        return View(villaNumberVM);
    }

    public IActionResult Update(int ID)
    {
        _logger.LogInformation("InSide Update Action Method");

        VillaNumberViewModel villaNumberViewModel = new()
        {
            VillaList = _unitOfWork.Villas.GetAll().Select(opt => new SelectListItem
            {
                Text = opt.VillaName,
                Value = opt.ID.ToString()
            }),
            VillaNumber = _unitOfWork.VillaNumbers.GetElementByID(ID: ID)
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
            _unitOfWork.VillaNumbers.Update(villaNumberViewModel.VillaNumber!);
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
        VillaNumberViewModel villaNumberViewModel = new()
        {
            VillaNumber = _unitOfWork.VillaNumbers.GetElementByID(ID: ID),
            VillaList = _unitOfWork.Villas.GetAll().Select(opt => new SelectListItem
            {
                Text = opt.VillaName,
                Value = opt.ID.ToString()
            }),
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
        var result = _unitOfWork.VillaNumbers.GetElementByID(opt => opt.VillaNumberID == villaNumberViewModel.VillaNumber!.VillaNumberID);

        if (result is not null)
        {
            _unitOfWork.VillaNumbers.Delete(result);
            _unitOfWork.Save();
            TempData["success"] = "Entity Has Been Deleted Successfully";
            _logger.LogInformation("InSide Delete Action Method : Entity Has Been Deleted");
            return RedirectToAction(nameof(Index));
        }
        TempData["error"] = "Entity Has Not Been Deleted Successfully";
        return View();
    }

}