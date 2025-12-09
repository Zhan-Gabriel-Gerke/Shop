using ShopTARgv24.Core.Dto;
namespace ShopTARgv24.Core.ServiceInterface;

public interface IEmailService
{
    void SendEmail(EmailDto dto);
    void SendEmailToken(EmailTokenDto dto, string token);
}