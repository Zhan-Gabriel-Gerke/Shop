using System.ComponentModel.DataAnnotations;

namespace ShopTARgv24.Models.Accounts;

public class ForgotPasswordViewModel
{
    [Required] 
    [EmailAddress] 
    public string Email { get; set; } = string.Empty;
    
}