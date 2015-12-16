using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace blogMarcelo.Web.Models.Usuario
{
    public class CadastrarUsuarioViewModel
    {
        [DisplayName("Código")]
        public int id { get; set; }

        [DisplayName("Login")]
        [Required(ErrorMessage = "O campo Login é obrigatório.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "A quantidade de caracteres no campo Login deve ser entre {0} e {1}.")]
        public string Login { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "A quantidade de caracteres no campo Nome deve ser entre {0} e {1}.")]
        public string Nome { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "A quantidade de caracteres no campo senha deve ser entre {0} e {1}.")]
        public string Senha { get; set; }

        [DisplayName("Confirmar Senha")]
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "O campo confirmar senha deve possuir no minimo {0} e {1}.")]
        [Compare("Senha", ErrorMessage = "As senhas digitadas nao conferem !" )]
        public string ConfirmarSenha { get; set; }
    }
}