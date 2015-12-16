﻿using blogMarcelo.Web.Models.Blog;
using BlogMarcelo.DB;
using BlogMarcelo.DB.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace blogMarcelo.Web.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index(int? pagina, string tag, string pesquisa )
        {

            var paginaCorreta = pagina.GetValueOrDefault(1);
            var registroPorPagina = 10;

            var conexaoBanco = new ConexaoBanco();

            // List<Post> posts = (from p in conexaoBanco.Posts
            //                     where p.Visivel == true
            //                     orderby p.DataPublicacao descending 
            //                     select p).ToList();

            var posts = (from p in conexaoBanco.Posts
                         where p.Visivel == true
                         select p);
            if (!string.IsNullOrEmpty(tag))
            {
                posts = (from p in posts
                         where p.PostTag.Any(x => x.Tag.ToUpper() == tag.ToUpper())
                         select p);
            }
            if ( !string.IsNullOrEmpty(pesquisa))
               { 
                 posts = (from p in posts
                          where p.Titulo.ToUpper().Contains(pesquisa.ToUpper())
                          || p.Resumo.ToUpper().Contains(pesquisa.ToUpper())
                          || p.Descricao.ToUpper().Contains(pesquisa.ToUpper())
                          select p);        
                }

            var qtdeRegistros = posts.Count();
            var indiceDaPagina = paginaCorreta - 1;
            var qtdeRegistroPular = (indiceDaPagina * registroPorPagina);
            var qtadePaginas = Math.Ceiling((decimal)qtdeRegistros / registroPorPagina);

            
            var viewModel = new ListarPostsViewModel();
            viewModel.Posts = (from p in posts
                               orderby p.DataPublicacao descending
                               select p).Skip(qtdeRegistroPular).Take(registroPorPagina).ToList();
            viewModel.PaginaAtual = paginaCorreta;
            viewModel.TotalPaginas = (int)qtadePaginas;
            viewModel.Tag = tag;
            viewModel.Tags = (from p in conexaoBanco.TagClass
                              where conexaoBanco.PostTags.Any(x => x.Tag == p.Tag) 
                              orderby p.Tag 
                              select p.Tag).ToList();
            viewModel.Pesquisa = pesquisa; 
            return View(viewModel);
        }

        public ActionResult _Paginacao()
        {
            return PartialView();
        }

        public ActionResult Post(int id)
        {
            var conexao = new ConexaoBanco();

            var post = (from p in conexao.Posts
                        where p.Id == id
                        select p).FirstOrDefault();

            var viewModel = new DetalhesPostViewModel();

            viewModel.DataPublicacao = post.DataPublicacao;
            viewModel.Autor = post.Autor;
            viewModel.Titulo = post.Titulo;
            viewModel.Resumo = post.Resumo;
            viewModel.Descricao = post.Descricao;
            viewModel.Visivel = post.Visivel;
            viewModel.QtdeComentarios = post.Comentarios.Count; 
            viewModel.Id = post.Id;
            viewModel.Tags = (from p in post.PostTag
                              select p.TagClass).ToList();

            if (post == null)
            {
                throw new Exception(string.Format("Post código {0} não encontrado", id));
            }

            return View(viewModel);
        }


    }

       
  }   
