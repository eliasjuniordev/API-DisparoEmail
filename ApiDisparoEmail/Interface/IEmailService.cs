using ApiDisparoEmail.Model;
using System.Threading.Tasks;

namespace ApiDisparoEmail.Interface
{
   

        public interface IEmailService
        {
            Task EnvioEmail(Email emailRequest);
        }
    
}
