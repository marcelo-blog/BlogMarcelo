using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace blogMarcelo.Web.Models.Administracao
{
    public class CadastrarPostViewModel
    {
        [DisplayName("Codigo")]
        public int Id { get; set; }

        [DisplayName("Título")]
        [Required(ErrorMessage = "O campo Título é obrigatório.")]
        [StringLength(100, ErrorMessage = "A quantidade de caracteres no campo título deve ser de até {1} caracteres." ) ]
        public string Titulo { get; set; }

        [DisplayName("Autor")]
        [Required(ErrorMessage = "O campo Autor é obrigatório.")]
        [StringLength(50, ErrorMessage = "A quantidade de caracteres no campo Autor deve ser de ate {1} caracteres.")]
        public string Autor { get; set; }

        [DisplayName("Resumo")]
        [Required(ErrorMessage = "O campo Resumo é obrigatório.")]
        [StringLength(1000, MinimumLength = 20, ErrorMessage = "A quantidade de caracteres no campo Resumo deve ser entre {2} e {1}.")]
        public string Resumo { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        public string Descricao { get; set; }

        [DisplayName("Data de Publicação")]
        [Required(ErrorMessage = "O campo Publicação é obrigatório.")]
        public DateTime DataPublicacao { get; set; }

        [DisplayName("Hora da Publicação")]
        [Required(ErrorMessage = "O campo Publicação é obrigatório.")]
        public DateTime HoraPublicacao { get; set; }

        [DisplayName("Visível")]
        [Required(ErrorMessage = "O campo Visível é obrigatório.")]
        public bool Visivel { get; set; }

        public List<string> Tags {get; set;}
         
    }
}