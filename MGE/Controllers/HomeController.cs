﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MGE.Models;
using MGE.RequestModel;
using MGE.ViewModels;

namespace MGE.Controllers
{
    public class HomeController : Controller
    {

        private readonly CategoriasService _categoriasService;
        private readonly ItensService _itensService;
        private readonly ParametrosService _parametrosService;

        public HomeController(CategoriasService categoriasService, ItensService itensService, ParametrosService parametrosService)
        {
            _categoriasService = categoriasService;
            _itensService = itensService;
            _parametrosService = parametrosService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SessaoCategoria()
        {
            var viewModel = new CategoriasViewModel();
            viewModel.MsgSucess = (string)TempData["cad-categoria-sucess"];
            viewModel.MsgFail = (string) TempData["cad-catagoria-error"];

            List<CategoriasEntity> lista = _categoriasService.BuscarTodos();

            foreach (var c in lista)
            {
                viewModel.Categorias.Add(new CategoriasEntity()
                {
                    Id = c.Id,
                    Descricao = c.Descricao
                });
            }
            
            return View(viewModel);
        }
        
        [HttpPost]
        public IActionResult SessaoCategoria(CategoriasRequestModel rm)
        {
            var descricao = rm.Descricao;

            if (descricao == null)
            {
                TempData["cad-catagoria-error"] = "Campo descrição deve ser preenchido!";
                return RedirectToAction("SessaoCategoria");
            }

            _categoriasService.InserirCategoria(descricao);
            TempData["cad-categoria-sucess"] = "Categoria cadastrada com sucesso!";
            
            return RedirectToAction("SessaoCategoria");
        }
        
        [HttpGet]
        public IActionResult EditarCategoria(int id)
        {
            var c = _categoriasService.BuscarPeloId(id);

            var viewModel = new CategoriasEditViewModel();
            viewModel.Id = c.Id;
            viewModel.msgFail = (string)TempData["edt-catagoria-error"];
            
            viewModel.Categoria = new Categ()
            {
                Descricao = c.Descricao
            };
            
            return View(viewModel);
        }
        
        [HttpPost]
        public IActionResult EditarCategoria(CategoriasRequestModel rm)
        {
            var ed = rm.Ed;
            var descricao = rm.Descricao;
            
            if (descricao == null)
            {
                TempData["edt-catagoria-error"] = "Campo descrição deve ser preenchido!";
                return RedirectToAction("EditarCategoria");
            }

            _categoriasService.EditarCategortia(ed, descricao);
            
            return RedirectToAction("SessaoCategoria");
        }
        
        public IActionResult ExcluirCategoria(int id)
        {
            _categoriasService.Remover(id);
            
            return RedirectToAction("SessaoCategoria");
        }
        
        [HttpGet]
        public IActionResult SessaoIten()
        {
            
            
            return View();
        }
        
        [HttpGet]
        public IActionResult SessaoParametro()
        {
            
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}