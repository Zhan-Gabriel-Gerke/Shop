using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Models;
using ShopTARgv24.Models.Accounts;

namespace ShopTARgv24.Controllers;

public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailServices;

        public AccountsController
            (
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                IEmailService emailServices
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailServices = emailServices;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = vm.Email,
                    Name = vm.Name,
                    Email = vm.Email,
                    City = vm.City,
                };

                var result = await _userManager.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Accounts", new { userId = user.Id, token = token }, Request.Scheme);

                    EmailTokenDto newsignup = new();
                    newsignup.Token = token;
                    newsignup.Body = $"Please registrate your account by: <a href=\"{confirmationLink}\">clicking here </a>";
                    newsignup.Subject = "CRUD registration";
                    newsignup.To = user.Email;

                    if(_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administrations");
                    }

                    _emailServices.SendEmailToken(newsignup, token);
                    List<string> errordatas =
                        [
                        "Area", "Accounts",
                        "Issue", "Success",
                        "StatusMessage", "Registration Success",
                        "ActedOn", $"{vm.Email}",
                        "CreatedAccountData", $"{vm.Email}\n{vm.City}\n[password hidden]\n[password hidden]"
                        ];
                    ViewBag.ErrorDatas = errordatas;
                    ViewBag.ErrorTitle = "You have successfully registrated";
                    ViewBag.ErrorMessage = "Before you can logi in, please confirm email from the link" +
                        "\nwe have emailed to your email address";

                    return View("ConfirmEmailMessage");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userid, string token)
        {
            if (userid == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The user with id of {userid} is not valid";
                return View("NotFound");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            List<string> errordatas =
                        [
                        "Area", "Accounts",
                        "Issue", "Success",
                        "StatusMessage", "Registration successful",
                        "ActedOn", $"{user.Email}",
                        "CreatedAccountData", $"{user.Email}\n{user.Name}\n{user.City}\n[password hidden]\n[password hidden]",
                        ];
            if (result.Succeeded)
            {
                errordatas =
                        [
                        "Area", "Accounts",
                        "Issue", "Success",
                        "StatusMessage", "Registration successful",
                        "ActedOn", $"{user.Email}",
                        "CreatedAccountData", $"{user.Email}\n{user.Name}\n{user.City}\n[password hidden]\n[password hidden]",
                        ];
                ViewBag.ErrorDatas = errordatas;
                return View();
            }
            ViewBag.ErrorDatas = errordatas;
            ViewBag.ErrorTitle = "Email cannot be confirmed";
            ViewBag.ErrorMessege = $"The user email, with userid of {userid}, cannot be confirmed.";
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id?? HttpContext.TraceIdentifier});
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            return View();
        }
    }