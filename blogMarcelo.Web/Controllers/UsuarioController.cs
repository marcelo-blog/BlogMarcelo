using blogMarcelo.Web.Models.Usuario;
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
    public class UsuarioController : Controller
    {
        public ActionResult Index()
        {
            var conexaoBanco = new ConexaoBanco();

            var usuarios = (from p in conexaoBanco.Usuarios
                            orderby p.Nome
                            select p).ToList();

            return View(usuarios);
        }

        public ActionResult CadastrarUsuario()
        {
            return View();
        }


        [HttpPost] // usar somente quando salvar
        public ActionResult CadastrarUsuario(CadastrarUsuarioViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                var conexao = new ConexaoBanco();
                var usuario = new Usuario();

                usuario.Login = ViewModel.Login.ToUpper();
                usuario.Nome = ViewModel.Nome;
                usuario.Senha = ViewModel.Senha;

                conexao.Usuarios.Add(usuario);
                try
                {

                    var jaExiste = (from p in conexao.Usuarios
                                    where p.Login.ToUpper() == usuario.Login
                                    select p).Any();
                    if (jaExiste)
                    {
                        throw new Exception(string.Format("Já existe login cadastrado com o login {0} ", usuario.Login));
                    }
                    conexao.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("Ocorreu um erro no banco ao incluir", exp.Message);
                }
            }
            return View(ViewModel);
        }


        public ActionResult EditarUsuario(int id)
        {
            var conexao = new ConexaoBanco();
            var usuario = (from x in conexao.Usuarios
                           where x.Id == id
                           select x).FirstOrDefault();

            if (usuario == null)
            {
                throw new Exception(string.Format("Usuario não {0} não encontrado", id));
            }

            var viewModel = new CadastrarUsuarioViewModel();

            viewModel.Nome = usuario.Nome;
            viewModel.Login = usuario.Login.ToUpper(); 
            viewModel.Senha = usuario.Senha;
            viewModel.id = usuario.Id;

            return View(viewModel);
        }


        [HttpPost]
        public ActionResult EditarUsuario(CadastrarUsuarioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexao = new ConexaoBanco();

                var usuario = (from x in conexao.Usuarios
                               where x.Id == viewModel.id
                               select x).FirstOrDefault();

                usuario.Id = viewModel.id;
                usuario.Nome = viewModel.Nome;
                usuario.Login = viewModel.Login.ToUpper();
                usuario.Senha = viewModel.Senha;

                try
                {
                    var jaExiste = (from p in conexao.Usuarios
                                    where p.Login.ToUpper() == usuario.Login
                                      && p.Id != usuario.Id
                                    select p).Any();
                    if (jaExiste)
                    {
                        throw new Exception(string.Format("Já existe login cadastrado com o login {0} ", usuario.Login));
                    }

                    conexao.SaveChanges();
                    return RedirectToAction("index");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("Ocorreu um erro ao gravar no banco", exp.Message);
                }
            }

            return View(viewModel);
        }

        public ActionResult ExcluirUsuario(int id)
        {
            var conexao = new ConexaoBanco();

            var usuario = (from p in conexao.Usuarios
                           where p.Id == id
                           select p).FirstOrDefault();
            if (usuario == null)
            {
                throw new Exception(string.Format("Usuário código {0} não foi encontrado.", id));
            }
            conexao.Usuarios.Remove(usuario);
            conexao.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}