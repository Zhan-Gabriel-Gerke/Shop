using Microsoft.AspNetCore.Mvc;
using ShopTARgv24.ApplicationServices.Services;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;

namespace ShopTARgv24.Controllers;

public class EmailController : Controller
{
    private readonly IEmailService _emailServices;

    public EmailController(IEmailService emailServices)
    {
        _emailServices = emailServices;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SendEmail(EmailDto dto)
    {
        _emailServices.SendEmail(dto);
        return RedirectToAction(nameof(Index));
    }
}