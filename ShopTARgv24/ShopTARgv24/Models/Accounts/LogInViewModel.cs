using System.ComponentModel.DataAnnotations;

namespace ShopTARgv24.Models.Accounts;

public class LogInViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required]
    public bool RememberMe { get; set; }
}