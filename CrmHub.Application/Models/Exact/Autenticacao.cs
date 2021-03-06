﻿using CrmHub.Application.Integration.Enuns;
using System;
using System.ComponentModel.DataAnnotations;

namespace CrmHub.Application.Models.Exact
{
    public class Autenticacao
    {
        [Required]
        public string TipoCRM { get; set; }

        public string TokenCRM { get; set; }

        [Required]
        public string UsuarioCRM { get; set; }

        [Required]
        public string UsuarioToken { get; set; }

        [EmailAddress]
        public string EmailVendedor { get; set; }

        public string EmpresaCliente { get; set; }

        public eCrmName Crm() { return (eCrmName)Enum.Parse(typeof(eCrmName), TipoCRM); }
    }
}
