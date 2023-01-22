﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IEmailService
    {
        public bool Send(
            string toName,
            string toEmail,
            string subject,
            string body,
            string fromName = "Equipe API marquinhos",
            string fromEmail = "hortaemcasa2017@gmail.com");
    }
}
