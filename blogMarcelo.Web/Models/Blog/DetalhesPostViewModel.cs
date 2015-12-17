using BlogMarcelo.DB.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace blogMarcelo.Web.Models.Blog
{
    public class DetalhesPostViewModel
    {
            public int Id { get; set; }
            public string Titulo { get; set; }
            public string Autor { get; set; }
            public string Resumo { get; set; }
            public DateTime DataPublicacao { get; set; }
            public bool Visivel { get; set; } 
            public int QtdeComentarios { get; set; }
            public string Descricao { get; set; }
            public IList<TagClass> Tags { get; set; }

        //cadastrar comentario//

        [DisplayName("Nome")]
        [StringLength(100, ErrorMessage = "O campo nome deve possuir no máximo {1} caracteres.")]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string ComentarioNome { get; set; }

        [DisplayName("E-mail")]
        [StringLength(100, ErrorMessage = "O campo E-mail deve possuir no máximo {1} caracteres.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string ComentarioEmail { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        public string Comentariodescricao { get; set; }

        [DisplayName("página Web")]
        [StringLength(100, ErrorMessage = "O campo Página Web deve possuir no máximo {1} caracteres.")]
        public string ComentarioPaginaWeb { get; set; }

        //listar comentarios
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public IList<Comentario> Comentarios { get; set; }
    }
}