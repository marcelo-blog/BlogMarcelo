﻿using BlogMarcelo.DB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogMarcelo.DB.Classes
{
    public class Post: ClasseBase 
    {
        public string  Autor { get; set; }
        public DateTime  DataPublicacao { get; set; }
        public string  Descricao { get; set; }
        public string  Resumo { get; set; }
        public string  Titulo { get; set; }
        public Boolean Visivel { get; set; }

        public virtual IList<Comentario> Comentarios { get; set; }
        public virtual IList<Arquivo> Arquivos { get; set; }
        public virtual IList<Imagem> Imagens { get; set; }
        public virtual IList<Visita> Visitas { get; set; }
        public virtual IList<PostTag> PostTag { get; set; }
    }
}
