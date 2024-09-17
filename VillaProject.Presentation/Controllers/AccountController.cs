using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VillaProject.Domain.Entities;
using VillaProject.Presentation.ViewModels;
using VillaProject.Application.Utility;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
public class AccountController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager; // used to manage the application user
    private readonly SignInManager<ApplicationUser> _signInManager;  //responsible for logging an user and related operations for application user
    private readonly RoleManager<IdentityRole> _roleManager; // manage the role of user for authorizations operations 
    public AccountController(IUnitOfWork _unitOfWork, UserManager<ApplicationUser> _userManager,
     SignInManager<ApplicationUser> _signInManager, RoleManager<IdentityRole> _roleManager)
    {
        this._unitOfWork = _unitOfWork;
        this._userManager = _userManager;
        this._signInManager = _signInManager;
        this._roleManager = _roleManager;
    }
    public ActionResult Login(string? returnURL = null)
    {


        returnURL ??= Url.Content("~/");

        LogInViewModel logInViewModel = new()
        {
            RedirecURL = returnURL,
        };


        return View(logInViewModel);
    }
    public async Task<ActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    public ActionResult SignUp(string? returnURL = null)
    {
        returnURL ??= Url.Content("~/");

        if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).Wait(); //specify roles in database for using in whole project
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).Wait();
        }

        SignUpViewModel signUpViewModel = new()
        {
            RoleList = _roleManager.Roles.Select(projuction => new SelectListItem
            {
                Text = projuction.Name,
                Value = projuction.Name
            }),
            RedirecURL = returnURL
        };

        return View(signUpViewModel);
    }
    [HttpPost]
    public async Task<ActionResult> SignUp(SignUpViewModel signUpViewModel)
    {
        if (ModelState.IsValid)
        {

            ApplicationUser applicationUser = new()
            {
                Name = signUpViewModel.Name,
                Email = signUpViewModel.Email,
                PhoneNumber = signUpViewModel.PhoneNumber,
                NormalizedEmail = signUpViewModel.Email.ToUpper(),
                EmailConfirmed = true,
                UserName = signUpViewModel.Email,
                CreatedAt = DateTime.Now,
            };

            var result = await _userManager.CreateAsync(applicationUser, signUpViewModel.Password);
            if (result.Succeeded)
            {

                if (!string.IsNullOrEmpty(signUpViewModel.Role))
                {
                    await _userManager.AddToRoleAsync(applicationUser, signUpViewModel.Role);
                }
                else
                {
                    await _userManager.AddToRoleAsync(applicationUser, SD.Role_Customer);
                }

                await _signInManager.SignInAsync(applicationUser, isPersistent: false); // automatic signin the useer 
                if (string.IsNullOrEmpty(signUpViewModel.RedirecURL))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //localredirect always redirect in the same domain
                    return LocalRedirect(signUpViewModel.RedirecURL); // return to the same point where the user was
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(String.Empty, error.Description);
            }
        }
        signUpViewModel.RoleList = _roleManager.Roles.Select(projuction => new SelectListItem
        {
            Text = projuction.Name,
            Value = projuction.Name
        });


        return View(signUpViewModel);
    }

    [HttpPost]
    public async Task<ActionResult> Login(LogInViewModel logInViewModel)
    {

        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(//check the compinations of user email and password
                logInViewModel.Password, logInViewModel.Email, logInViewModel.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(logInViewModel.RedirecURL))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //localredirect always redirect in the same domain
                    return LocalRedirect(logInViewModel.RedirecURL); //a return to the same point where the user was
                }
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Invalid Login Attempt.");
            }
        }

        return View(logInViewModel);
    }
    public ActionResult AccessDenied()
    {
        return View();
    }
}