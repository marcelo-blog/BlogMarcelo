﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace blogMarcelo.Web.Models.Login
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Display(Name = "Lembrar ?")]
        public bool Lembrar { get; set; }


    }
}