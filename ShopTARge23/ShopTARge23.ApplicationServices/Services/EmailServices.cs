using ShopTARge23.Core.Dto;
using MimeKit;
using ShopTARge23.ApplicationServices.Services;

namespace ShopTARge23.ApplicationServices.Services
{
    internal class EmailServices : IEmailServices
    {
        public void SendEmail(EmailDto dto)
        {
            var email = new MimeMessage();
        }
    }
}
