using blogMarcelo.Web.Models.Administracao;
using BlogMarcelo.DB;
using BlogMarcelo.DB.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace blogMarcelo.Web.Controllers
{
    [Authorize]
    public class AdministracaoController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
       
            
        // para abrir a pagina
        public ActionResult CadastrarPost()
        {
            var viewModel = new CadastrarPostViewModel();
            viewModel.DataPublicacao = DateTime.Now;
            viewModel.HoraPublicacao = DateTime.Now;
           // viewModel.Autor = "Marcelo Freitas";
            return View(viewModel);
        }


        // para receber a pagina


        [HttpPost] // usar somente quando salvar
        public ActionResult CadastrarPost(CadastrarPostViewModel ViewModel)
        {
            if (ModelState.IsValid)
            { 
            var conexao = new ConexaoBanco();
            var post = new Post();

            var dataConc = new DateTime(ViewModel.DataPublicacao.Year,
                                         ViewModel.DataPublicacao.Month,
                                         ViewModel.DataPublicacao.Day,
                                         ViewModel.HoraPublicacao.Hour,
                                         ViewModel.HoraPublicacao.Minute,
                                         ViewModel.DataPublicacao.Second);

            post.Titulo = ViewModel.Titulo;
            post.Autor = ViewModel.Autor;
            post.Resumo = ViewModel.Resumo;
            post.Descricao = ViewModel.Descricao;
            post.Visivel = ViewModel.Visivel;
            post.DataPublicacao = dataConc;
            post.PostTag = new List<PostTag>();
                if (ViewModel.Tags != null)
                {
                    foreach (var item in ViewModel.Tags)
                    { 
                      var tagExiste = (from p in conexao.TagClass
                                      where p.Tag.ToLower() == item.ToLower()
                                      select p).Any();
                        if (!tagExiste)
                        {
                            var tagClass = new TagClass();
                            tagClass.Tag = item;
                            conexao.TagClass.Add(tagClass); 
                        }
                        var postTag = new PostTag();
                        postTag.Tag = item;
                        post.PostTag.Add(postTag);   
                     }
                }


                try
                {
                    conexao.Posts.Add(post);
                    conexao.SaveChanges();
                    return RedirectToAction("index");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);
                }

                
            }
            return View(ViewModel);
        }

        
        public ActionResult EditarPost(int id)
        {
            //if (ModelState.IsValid)
            //{
                var conexao = new ConexaoBanco();

                var post = (from x in conexao.Posts
                            where x.Id == id
                            select x).FirstOrDefault();
            if (post == null )
            {
                throw new Exception(string.Format("Post com código {0} não encontrado", id)); 
            }

                var viewModel = new CadastrarPostViewModel();
            
                    viewModel.DataPublicacao = post.DataPublicacao;
                    viewModel.HoraPublicacao = post.DataPublicacao; 
                    viewModel.Autor = post.Autor;
                    viewModel.Titulo = post.Titulo;
                    viewModel.Resumo = post.Resumo;
                    viewModel.Descricao = post.Descricao;
                    viewModel.Visivel = post.Visivel;
                    viewModel.Id = post.Id;
                    viewModel.Tags = (from p in post.PostTag
                              select p.Tag).ToList();            

            return View(viewModel);
        }


        [HttpPost]
        public ActionResult EditarPost(CadastrarPostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexao = new ConexaoBanco();

                var post = (from x in conexao.Posts
                           where x.Id == viewModel.Id
                           select x).FirstOrDefault();
                              

                var dataConc = new DateTime(viewModel.DataPublicacao.Year,
                                         viewModel.DataPublicacao.Month,
                                         viewModel.DataPublicacao.Day,
                                         viewModel.HoraPublicacao.Hour,
                                         viewModel.HoraPublicacao.Minute,
                                         viewModel.DataPublicacao.Second);
                
                post.Autor = viewModel.Autor;
                post.Titulo = viewModel.Titulo;
                post.Resumo = viewModel.Resumo;
                post.Descricao = viewModel.Descricao;
                post.Visivel = viewModel.Visivel;
                post.DataPublicacao = dataConc;

                var postsTagsAtuais = post.PostTag.ToList();
                foreach (var item in postsTagsAtuais)
                {
                    conexao.PostTags.Remove(item);   
                }

                if (viewModel.Tags != null)
                {
                    foreach (var item in viewModel.Tags)
                    {
                        var tagExiste = (from p in conexao.TagClass
                                         where p.Tag.ToLower() == item.ToLower()
                                         select p).Any();
                        if (!tagExiste)
                        {
                            var tagClass = new TagClass();
                            tagClass.Tag = item;
                            conexao.TagClass.Add(tagClass);
                        }
                        var postTag = new PostTag();
                        postTag.Tag = item;
                        post.PostTag.Add(postTag);
                    }
                }


                try
                {
                    conexao.SaveChanges();
                    return RedirectToAction("index");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);  
                }

            }

            return View(viewModel);
        }



        public ActionResult ExcluirPost(int id)
        {
            var conexao = new ConexaoBanco();

            var post = (from p in conexao.Posts
                        where p.Id == id
                        select p).FirstOrDefault();
            if (post == null)
            {
                throw new Exception(string.Format("Post código {0} não existe.", id)); 
            }
            conexao.Posts.Remove(post);
            conexao.SaveChanges();
             
            return RedirectToAction("Index","Blog"); 
        }


        #region ExcluirComentario
        public ActionResult ExcluirComentario(int id)
        {
            var conexaoBanco = new ConexaoBanco();
            var comentario = (from p in conexaoBanco.Comentarios
                              where p.Id == id
                              select p).FirstOrDefault();
            if (comentario == null)
            {
                throw new Exception(string.Format("Comentário código {0} não foi encontrado.", id));
            }
            conexaoBanco.Comentarios.Remove(comentario);
            conexaoBanco.SaveChanges();

            var post = (from p in conexaoBanco.Posts
                        where p.Id == comentario.IdPost
                        select p).First();
            return Redirect(Url.Action("Post", "Blog", new
            {
                ano = post.DataPublicacao.Year,
                mes = post.DataPublicacao.Month,
                dia = post.DataPublicacao.Day,
                titulo = post.Titulo,
                id = post.Id
            }) + "#comentarios");
        }
        #endregion 
    }
}