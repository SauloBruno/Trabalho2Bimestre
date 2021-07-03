using System;
using System.Collections;
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

        public IActionResult SessaoHome()
        {
            var l1 = _itensService.BuscarTodos();
            List<ItemApoioEntity> l2 = new List<ItemApoioEntity>();
            List<ItemApoioEntity> l3 = new List<ItemApoioEntity>();

            
            foreach (var l in l1)
            {
                l2.Add(new ItemApoioEntity()
                {
                    Nome = l.Nome,
                    ConsumoKwt = (l.ConsumoWatts * (l.HorasUsoDiario * 30)) / 1000,
                    ConsumoMesal = (((double)l.ConsumoWatts * (l.HorasUsoDiario * 30)) / 1000) * 0.30  
                });
            }

            ItemApoioEntity p = new ItemApoioEntity();
            ItemApoioEntity s = new ItemApoioEntity();
            
            for (int i = 0; i < l2.Count; i++)
            {
                for (int j = 0; j < l2.Count - 1; j++)
                {
                    if (l2[j].ConsumoMesal < l2[j + 1].ConsumoMesal)
                    {
                        p = l2[j];
                        s = l2[j + 1];
                        l2[j + 1] = p;
                        l2[j] = s;
                    }
                }
            }
            
            var viewModel = new HomeViewModel();

            for (int i = 0; i < 5; i++)
            {
                viewModel.it.Add(new ItemApoioEntity()
                {
                    Nome = l2[i].Nome,
                    ConsumoKwt = l2[i].ConsumoKwt,
                    ConsumoMesal = l2[i].ConsumoMesal
                });
            }

            foreach (var l in l2)
            {
                viewModel.dados.ConsumoMensal += (double)l.ConsumoKwt;
                viewModel.dados.TotalMensal += l.ConsumoMesal;
            }

            if (viewModel.dados.ConsumoMensal < 250)
            {
                viewModel.dados.Status = "Baixo";
            }
            else if (viewModel.dados.ConsumoMensal >= 250 && viewModel.dados.ConsumoMensal < 500)
            {
                viewModel.dados.Status = "Média";
            }
            else
            {
                viewModel.dados.Status = "Alto";
            }
            
            return View(viewModel);
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
            var viewModel = new ItensViewModel();   
            viewModel.MsgSucess = (string)TempData["cad-item-sucess"];
            viewModel.MsgFail = (string) TempData["cad-item-error"];

            List<CategoriasEntity> l1 = _categoriasService.BuscarTodos();
            List<ItensEntity> l2 = _itensService.BuscarTodos();

            foreach (var c in l1)
            {
                viewModel.Categorias.Add(new CategoriasEntity()
                {
                    Id = c.Id,
                    Descricao = c.Descricao
                });
            }

            foreach (var i in l2)
            {
                viewModel.Itens.Add(new ItensEntity()
                {
                    Id = i.Id,
                    Nome = i.Nome,
                    Descricao = i.Descricao,
                    ConsumoWatts = i.ConsumoWatts,
                    HorasUsoDiario = i.HorasUsoDiario,
                    DataFabricacao = i.DataFabricacao,
                    Categoria = i.Categoria
                });
            }
            
            return View(viewModel);
        }
        
        [HttpPost]
        public IActionResult SessaoIten(ItensRequestModel rm)
        {
            var categoria = _categoriasService.BuscarPeloId(rm.Categoria);
            var nome = rm.Nome;
            var descricao = rm.Descricao;
            var consumoWatts = rm.ConsumoWatts;
            var horaUso = rm.HorasUsoDiario;
            var dataFabricacao = rm.DataFabricacao;
            
            if (nome == null)
            {
                TempData["cad-item-error"] = "Campo nome deve ser preenchido!";
                return RedirectToAction("SessaoIten");
            }
            
            if (descricao == null)
            {
                TempData["cad-item-error"] = "Campo descrição deve ser preenchido!";
                return RedirectToAction("SessaoIten");
            }

            if (consumoWatts == 0)
            {
                TempData["cad-item-error"] = "Consumo em Watts não pode ser 0!";
                return RedirectToAction("SessaoIten");
            }
            
            if (horaUso == 0)
            {
                TempData["cad-item-error"] = "Horas de uso diarias não pode ser 0!";
                return RedirectToAction("SessaoIten");
            }
            
            if (dataFabricacao == null || !dataFabricacao.Contains("/"))
            {
                TempData["cad-item-error"] = "Data de fabricação deve ser preenchida corretamente!";
                return RedirectToAction("SessaoIten");
            }
            
            TempData["cad-item-sucess"] = "Item inserido com sucesso!";
            _itensService.InserirItem(categoria, nome, descricao, consumoWatts, horaUso, dataFabricacao);
            
            return RedirectToAction("SessaoIten");
        }
        
        public IActionResult ExcluirItem(Guid id)
        {
            _itensService.Remover(id);
            
            return RedirectToAction("SessaoIten");
        }
        [HttpGet]
        public IActionResult EditarItem(Guid id)
        {
            var it = _itensService.BuscarPeloId(id);

            var viewModel = new ItensEditViewModel();
            viewModel.Id = it.Id;
            viewModel.Item.IdCat = it.Categoria.Id;
            viewModel.Item.Nome = it.Nome;
            viewModel.Item.Descricao = it.Descricao;
            viewModel.Item.ConsumoWatts = it.ConsumoWatts;
            viewModel.Item.HorasUsoDiario = it.HorasUsoDiario;
            viewModel.Item.DataFabricacao = it.DataFabricacao.ToString("dd/MM/yyyy");
            viewModel.MsgFail = (string) TempData["Edit-iten-error"];

            var l1 = _categoriasService.BuscarTodos();
            foreach (var l in l1)
            {
                viewModel.Categorias.Add(new CategoriasEntity()
                {
                    Id = l.Id,
                    Descricao = l.Descricao
                });
            }

            return View(viewModel);
        }
        
        [HttpPost]
        public IActionResult EditarItem(ItensRequestModel rm)
        {
            var ed = rm.Ed;
            var categoria = _categoriasService.BuscarPeloId(rm.Categoria);
            var nome = rm.Nome;
            var descricao = rm.Descricao;
            var consumoWatts = rm.ConsumoWatts;
            var horaUso = rm.HorasUsoDiario;
            var dataFabricacao = rm.DataFabricacao;
            
            if (nome == null)
            {
                TempData["Edit-iten-error"] = "Campo nome deve ser preenchido!";
                return RedirectToAction("EditarItem");
            }
            
            if (descricao == null)
            {
                TempData["Edit-iten-error"] = "Campo descrição deve ser preenchido!";
                return RedirectToAction("EditarItem");
            }

            if (consumoWatts == 0)
            {
                TempData["Edit-iten-error"] = "Consumo em Watts não pode ser 0!";
                return RedirectToAction("EditarItem");
            }
            
            if (horaUso == 0)
            {
                TempData["Edit-iten-error"] = "Horas de uso diarias não pode ser 0!";
                return RedirectToAction("EditarItem");
            }
            
            if (dataFabricacao == null || !dataFabricacao.Contains("/"))
            {
                TempData["Edit-iten-error"] = "Data de fabricação deve ser preenchida corretamente!";
                return RedirectToAction("EditarItem");
            }
            
            _itensService.EditarItem(ed, categoria, nome, descricao, consumoWatts, horaUso, dataFabricacao);
            return RedirectToAction("SessaoIten");
        }
        
        [HttpGet]
        public IActionResult SessaoParametro()
        {
            var viewModel = new ParametrosViewModel();
            viewModel.MsgSucess = (string)TempData["cad-parametro-sucess"];
            viewModel.MsgFail = (string) TempData["cad-parametro-error"];

            List<ParametrosEntity> lista = _parametrosService.BuscarTodos();

            foreach (var p in lista)
            {
                viewModel.parametros.Add(new ParametrosEntity()
                {
                    Id = p.Id,
                    ValorKwh = p.ValorKwh,
                    FaixaConsumoAlto = p.FaixaConsumoAlto,
                    FaixaConsumoMedio = p.FaixaConsumoMedio
                });
            }
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SessaoParametro(ParametrosRequestModel rm)
        {
            var valorKwh = rm.ValorKwh;
            var faixaConsumoAlto = rm.FaixaConsumoAlto;
            var faixaConsumoMedio = rm.FaixaConsumoMedio;
            
            if (valorKwh == 0)
            {
                TempData["cad-parametro-error"] = "Valor de Kwh não pode ser 0!";
                return RedirectToAction("SessaoParametro");
            }

            if (faixaConsumoAlto == 0)
            {
                TempData["cad-parametro-error"] = "Faixa de consumo alto não pode ser 0!";
                return RedirectToAction("SessaoParametro");
            }
            
            if (faixaConsumoMedio == 0)
            {
                TempData["cad-parametro-error"] = "Faixa de consumo medio não pode ser 0!";
                return RedirectToAction("SessaoParametro");
            }
            
            _parametrosService.InserirParametro(valorKwh, faixaConsumoAlto, faixaConsumoMedio);
            TempData["cad-parametro-sucess"] = "Parametro cadastrada com sucesso!";
            
            return RedirectToAction("SessaoParametro");
        }
        
        public IActionResult ExcluirParametro(int id)
        {
            _parametrosService.Remover(id);
            
            return RedirectToAction("SessaoParametro");
        }
        
        
        [HttpGet]
        public IActionResult EditarParametro(int id)
        {
            var p = _parametrosService.BuscarPeloId(id);

            var viewModel = new ParametrosEditViewModel();
            viewModel.Id = p.Id;
            viewModel.msgFail = (string)TempData["edt-parametro-error"];


            viewModel.Parametro = new Param()
            {
                ValorKwh = p.ValorKwh,
                FaixaConsumoAlto = p.FaixaConsumoAlto,
                FaixaConsumoMedio = p.FaixaConsumoMedio
            };
            
            return View(viewModel);
        }
        
        [HttpPost]
        public IActionResult EditarParametro(ParametrosRequestModel rm)
        {
            var ed = rm.Ed;
            var valorKwh = rm.ValorKwh;
            var faixaConsumoAlto = rm.FaixaConsumoAlto;
            var faixaConsumoMedio = rm.FaixaConsumoMedio;

            if (valorKwh == 0)
            {
                TempData["edt-parametro-error"] = "Valor de Kwh não pode ser 0!";
                return RedirectToAction("EditarParametro");
            }

            if (faixaConsumoAlto == 0)
            {
                TempData["edt-parametro-error"] = "Faixa de consumo alto não pode ser 0!";
                return RedirectToAction("EditarParametro");
            }
            
            if (faixaConsumoMedio == 0)
            {
                TempData["edt-parametro-error"] = "Faixa de consumo medio não pode ser 0!";
                return RedirectToAction("EditarParametro");
            }
            
            _parametrosService.Editar(ed, valorKwh, faixaConsumoAlto, faixaConsumoMedio);           
            return RedirectToAction("SessaoParametro");
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}