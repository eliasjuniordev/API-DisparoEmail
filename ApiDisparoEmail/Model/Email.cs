using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ApiDisparoEmail.Model
{
    public class Email
    {
        public string EmailDestino { get; set; }
        public string Assunto { get; set; }

        public string CorpoEmail { get; set; }

        public List<IFormFile> Anexo { get; set; }
    }
}
