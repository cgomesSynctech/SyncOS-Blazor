using AspNetCoreHero.ToastNotification.Abstractions;
using info;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modelos;
using Newtonsoft.Json;
using Services;
using SyncOSCore.Migrations;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using X.PagedList;

namespace SyncOSCore.Controllers
{
    [AllowAnonymous]
    public class ClientesProspController : Controller
    {
        private readonly APPDbContext _ctx;
        private readonly INotyfService _notyf;
        private Usuario _usuarioLogado;

        private Usuario SetUserLogado()
        {
            try
            {
                var cookieValue = Request.Cookies["UsuarioLogado"];
                var userLogado = JsonConvert.DeserializeObject<Usuario>(cookieValue);
                ViewData["Perfil"] = userLogado.PerfilId;
                ViewData["NomeUsuario"] = userLogado.LoginName;
                return userLogado;
            }
            catch (Exception)
            {
                return new Usuario();
            }
        }
        
        public ClientesProspController(APPDbContext context, INotyfService notyf)
        {
            _ctx = context;
            _notyf = notyf;
        }

        public IActionResult EfetivarVenda(int id)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            var prosp = _ctx.ClienteProsp.Find(id);
            var cliente = new Cliente();
            cliente.UF = prosp.UF;
            cliente.Fantasia = prosp.Fantasia;
            cliente.Bairro = prosp.Bairro;
            cliente.CEP = prosp.CEP;
            cliente.Contato = prosp.Contato;
            cliente.CPF_CNPJ = prosp.CPF_CNPJ;
            cliente.Email = prosp.Email;
            cliente.Endereco = prosp.Endereco;
            cliente.InscricaoEstadual = prosp.InscricaoEstadual;
            cliente.Municipio = prosp.Municipio;
            cliente.MunicipioId = prosp.MunicipioId;    
            cliente.Numero = prosp.Numero;
            cliente.Razao = prosp.Razao;
            cliente.Revenda = prosp.Revenda;
            cliente.RevendaId = prosp.RevendaId;
            cliente.UFId = prosp.UFId;
            cliente.WhatsApp = prosp.WhatsApp;
            cliente.IdClienteProsp = id;
            cliente.Retaguarda = prosp.Retaguarda;
            cliente.Nfc = prosp.Nfc;
            cliente.Nf = prosp.Nf;
            cliente.MDF = prosp.MDF;
            cliente.VipShop = prosp.VipShop;
            cliente.Sped = prosp.Sped;
            cliente.Backup = prosp.Backup;
            cliente.TEF = prosp.TEF;
            cliente.Outros = prosp.Outros;
            cliente.EspecieOutros = prosp.EspecieOutros;
            cliente.Certificado = prosp.Certificado;

            ViewData["UFId"] = new SelectList(_ctx.UFs, "Id", "Descricao", "PB");
            ViewData["MunicipioId"] = new SelectList(_ctx.Municipios
                .Where(w => w.UFId == "PB")
                .OrderBy(o => o.Ordem)
                .ThenBy(t => t.Descricao)
                , "Id", "Desc_UF"); ;
            ViewData["RevendaId"] = new SelectList(_ctx.Revendas, "Id", "Descricao");

            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> EfetivarVenda([Bind("Id,RevendaId,Razao,Fantasia,CPF_CNPJ,Endereco,Bairro,CEP,UFId,MunicipioId,Numero,Bloqueado,Desativado,ValorContrato,UltimaAtualizacao,DataInclusao,DataAlteracao,LatitudeLongitude,InscricaoEstadual,ChaveFlexDocs,ChaveCSC,ChaveManifesto,ValorContratoBackup,WhatsApp,Email,Resp_Nome,Resp_Endereco,Resp_Numero,Resp_UF,Resp_Cidade,Resp_CEP,Resp_CPF,Contato,IdClienteProsp")] Cliente cliente)
        {
            try 
	        {	        
                _usuarioLogado = SetUserLogado();
                cliente.Id = await NextID.GetNextId("Cliente", _ctx);
                cliente.Bloqueado = true;
                cliente.Desativado = false;
                cliente.UltimaAtualizacao = DateTime.MinValue;
                cliente.DataAlteracao = DateTime.UtcNow;
                cliente.CPF_CNPJ = Regex.Replace(cliente.CPF_CNPJ, "[^0-9]", "");
                cliente.InscricaoEstadual = Regex.Replace(cliente.InscricaoEstadual, "[^0-9]", "");
                cliente.CEP = Regex.Replace(cliente.CEP, "[^0-9]", "");
                ViewData["MunicipioId"] = new SelectList(_ctx.Municipios, "Id", "Descricao", cliente.MunicipioId);
                ViewData["RevendaId"] = new SelectList(_ctx.Revendas, "Id", "Descricao", cliente.RevendaId);
                ViewData["UFId"] = new SelectList(_ctx.UFs, "Id", "Descricao", cliente.UFId);
                var prosp = await _ctx.ClienteProsp.FindAsync(cliente.IdClienteProsp);
                if (prosp != null)
                {
                    prosp.StatusId = 5;
                    prosp.Flag = 1;
                    prosp.SetorId = 99;
                }
                
                
                if (ModelState.IsValid)
                {                 
                    _ctx.Update(prosp);
                    _ctx.Add(cliente);
                    await _ctx.SaveChangesAsync();
                    var msg = string.Format("O usuario {0}, Promoveu o Prostecto para Cliente", _usuarioLogado.LoginName);
                    await GravarAlteracaoProsp(msg,prosp.Id );

                    var espelho = new EspelharCliente(_ctx, cliente).Salvar();
                    _notyf.Information("<h3>Aviso</h3><br/><p>Cliente cadatrado com sucesso.</p>", 5);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var erros = string.Empty;
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        erros = error.ErrorMessage;
                    }
                    erros = string.Format("<h3>Aviso</h3><br/><p>Ocorreu um erro ao tentar cadatrar um novo cliente.<br/>{0}</p>", erros);
                    _notyf.Warning(erros, 10);
                    return RedirectToAction("index", "Clientes");
                }

            }
            catch (Exception e)
            {
                var erro = string.Format("<h3>Aviso</h3><br/><p>Ocorreu um erro ao tentar cadatrar um novo cliente.<br/>{0}</p>", e.Message);
                _notyf.Warning(erro, 5);
                return RedirectToAction("index", "Clientes");
            }

        }
            


        [HttpGet]
        public IActionResult Index(string buscaRazao,int? setor, int? status, int pagina = 1, int itensPorPagina = 10 )
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            var query = _ctx.ClienteProsp
                .Include(c => c.Setor)
                .Include(c => c.Status)
                .Include(c => c.Revenda)
                .Include(c => c.Municipio)
                .Include(c => c.UF)
                .AsQueryable();

            if (setor.HasValue && setor != 100)
                query = query.Where(c => c.SetorId == setor.Value);

            if (status.HasValue && status != 100)
                query = query.Where(c => c.StatusId == status.Value);

            if (!string.IsNullOrWhiteSpace(buscaRazao))
                query = query.Where(c => c.Razao.Contains(buscaRazao));

            var resultado = query.ToList();

            var agrupado = resultado
                .GroupBy(c => c.Setor)
                .Select(g =>
                {
                    var totalItens = g.Count();
                    var totalPaginas = (int)Math.Ceiling(totalItens / (double)itensPorPagina);

                    var clientesPaginados = g
                        .OrderBy(c => c.Razao)
                        .Skip((pagina - 1) * itensPorPagina)
                        .Take(itensPorPagina)
                        .ToList();

                    return new VMClienteProsp
                    {
                        SetorId = g.Key.Id,
                        SetorDescricao = g.Key.Descricao,
                        PaginaAtual = pagina,
                        TotalPaginas = totalPaginas,
                        ClientesPaginados = clientesPaginados
                    };
                })
                .ToList();

            ViewData["SetorId"] = new SelectList(_ctx.Setor, "Id", "Descricao", setor);
            ViewData["StatusId"] = new SelectList(_ctx.ClienteProspStatus, "Id", "Descricao", status);
            ViewBag.BuscaRazao = buscaRazao;

            return View(agrupado);
        }

        public IActionResult Create()
        {
            _usuarioLogado = SetUserLogado();

            try
            {
                ViewData["UFId"] = new SelectList(_ctx.UFs, "Id", "Descricao", "PB");
                ViewData["MunicipioId"] = new SelectList(_ctx.Municipios
                    .Where(w => w.UFId == "PB")
                    .OrderBy(o => o.Ordem)
                    .ThenBy(t => t.Descricao)
                    , "Id", "Desc_UF"); ;
                ViewData["RevendaId"] = new SelectList(_ctx.Revendas, "Id", "Descricao");
                ViewData["StatusId"] = new SelectList(_ctx.ClienteProspStatus, "Id", "Descricao");
                return View();
            }
            catch (Exception)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Ocorreu um erro ao tentar cadatrar um novo Prospecto</p>", 5);
                return RedirectToAction("index", "Clientes");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador,Financeiro,Suporte,Visitante")]
        public async Task<IActionResult> Create([Bind("Id,RevendaId,StatusId,Razao,Fantasia,CPF_CNPJ,Endereco,Bairro,CEP,UFId,Numero,ValorNegociacao,ValorManutencaoNegociacao,DataInclusao,DataAlteracao,MunicipioId,InscricaoEstadual,Contato,WhatsApp,Email,Vendedor,Retaguarda,Nfc,Nf,MDF,VipShop,Sped,Backup,TEF,Outros,EspecieOutros,Certificado")] ClienteProsp clienteprosp)
        {
            try
            {
                _usuarioLogado = SetUserLogado();
               // clienteprosp.Id = await NextID.GetNextId("Cliente", _ctx);
                clienteprosp.DataAlteracao = DateTime.UtcNow;
                clienteprosp.CPF_CNPJ = Regex.Replace(clienteprosp.CPF_CNPJ, "[^0-9]", "");
                clienteprosp.InscricaoEstadual = Regex.Replace(clienteprosp.InscricaoEstadual, "[^0-9]", "");
                clienteprosp.CEP = Regex.Replace(clienteprosp.CEP, "[^0-9]", "");
                clienteprosp.SetorId = 1;
                clienteprosp.ValorManutencaoNegociacao = clienteprosp.ValorManutencaoNegociacao ?? 0.0;
                clienteprosp.ValorNegociacao = clienteprosp.ValorNegociacao ?? 0.0;


                if (ModelState.IsValid)
                {
                    _ctx.Add(clienteprosp);
                    await _ctx.SaveChangesAsync();

                    _notyf.Information("<h3>Aviso</h3><br/><p>Prospecto cadatrado com sucesso.</p>", 5);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var erros = string.Empty;
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        erros = error.ErrorMessage;
                    }
                    erros = string.Format("<h3>Aviso</h3><br/><p>Ocorreu um erro ao tentar cadatrar um novo Prospecto.<br/>{0}</p>", erros);
                    _notyf.Warning(erros, 10);
                    return RedirectToAction("index", "ClientesProsp");
                }
            }
            catch (Exception e)
            {
                var erro = string.Format("<h3>Aviso</h3><br/><p>Ocorreu um erro ao tentar cadatrar um novo Prospecto.<br/>{0}</p>", e.Message);
                _notyf.Warning(erro, 5);
                return RedirectToAction("index", "ClientesProsp");
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _ctx.ClienteProsp.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            ViewData["UFId"] = new SelectList(_ctx.UFs, "Id", "Descricao");
            ViewData["MunicipioId"] = new SelectList(_ctx.Municipios
                .OrderBy(o => o.Ordem)
                .ThenBy(t => t.Descricao)
                , "Id", "Desc_UF"); ;
            ViewData["RevendaId"] = new SelectList(_ctx.Revendas, "Id", "Descricao");
            ViewData["StatusId"] = new SelectList(_ctx.ClienteProspStatus, "Id", "Descricao");
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador,Financeiro,Suporte")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RevendaId,StatusId,SetorId,Razao,Fantasia,CPF_CNPJ,Endereco,Bairro,CEP,UFId,Numero,ValorNegociacao,ValorManutencaoNegociacao,DataInclusao,DataAlteracao,MunicipioId,InscricaoEstadual,Contato,WhatsApp,Email,Vendedor,Retaguarda,Nfc,Nf,MDF,VipShop,Sped,Backup,TEF,Outros,EspecieOutros,CertificadoPlanoPgto,FormaPgto")] ClienteProsp cliente)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    cliente.CPF_CNPJ = Regex.Replace(cliente.CPF_CNPJ, "[^0-9]", "");
                    cliente.InscricaoEstadual = Regex.Replace(cliente.InscricaoEstadual, "[^0-9]", "");
                    cliente.CEP = Regex.Replace(cliente.CEP, "[^0-9]", "");
                    _ctx.Update(cliente);
                    await _ctx.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MunicipioId"] = new SelectList(_ctx.Municipios, "Id", "Id", cliente.MunicipioId);
            ViewData["RevendaId"] = new SelectList(_ctx.Revendas, "Id", "Id", cliente.RevendaId);
            ViewData["UFId"] = new SelectList(_ctx.UFs, "Id", "Id", cliente.UFId);
            _notyf.Success(string.Format("<h3>Aviso</h3><br/><p>Cliente: {0} modificado com sucesso.</p>", cliente.Fantasia), 5);
            return View(cliente);
        }


        public async Task<IActionResult> AlteraSetor(int clienteId)
        {
            _usuarioLogado = SetUserLogado();
            var cliente = await _ctx.ClienteProsp.FindAsync(clienteId);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["SetorId"] = new SelectList(_ctx.Setor, "Id", "Descricao");
            return View(cliente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador,Financeiro,Suporte")]
        public async Task<IActionResult> AlteraSetor(int id, [Bind("Id,SetorId")] ClienteProsp cliente)
        {
            _usuarioLogado = SetUserLogado();
            if (id != cliente.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {                    
                    var clienteExistente = await _ctx.ClienteProsp.FindAsync(id);
                    var setorAntigo = await _ctx.Setor.FindAsync(clienteExistente.SetorId);
                    var setorNovo = await _ctx.Setor.FindAsync(cliente.SetorId);
                    clienteExistente.SetorId = cliente.SetorId;
                    
                    var msg = string.Format("{0}, moveu do Setor de {1}, para {2}", _usuarioLogado.LoginName, setorAntigo.Descricao, setorNovo.Descricao);
                    _ctx.Update(clienteExistente);
                    await _ctx.SaveChangesAsync();
                    await GravarAlteracaoProsp(msg, clienteExistente.Id);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            _notyf.Success(string.Format("<h3>Aviso</h3><br/><p>Cliente: {0} modificado com sucesso.</p>", cliente.Fantasia), 5);
            //return View(cliente);
            return RedirectToAction(nameof(Index));
            //}

        }

        public async Task<IActionResult> AlteraStatus(int clienteId)
        {
            _usuarioLogado = SetUserLogado();
            var cliente = await _ctx.ClienteProsp.FindAsync(clienteId);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_ctx.ClienteProspStatus, "Id", "Descricao");
            return View(cliente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador,Financeiro,Suporte")]
        public async Task<IActionResult> AlteraStatus(int id, [Bind("Id,StatusId")] ClienteProsp cliente)
        {
            _usuarioLogado = SetUserLogado();

            if (id != cliente.Id)
            {
                return NotFound();
            }

                try
                {

                    var clienteExistente = await _ctx.ClienteProsp.FindAsync(id);
                //var statusAntigo = await _ctx.Setor.FindAsync(clienteExistente.StatusId);
                //var statusNovo = await _ctx.Setor.FindAsync(cliente.StatusId);
                var statusAntigo = await _ctx.ClienteProspStatus.FindAsync(clienteExistente.StatusId);
                var statusNovo = await _ctx.ClienteProspStatus.FindAsync(cliente.StatusId);

                clienteExistente.StatusId = cliente.StatusId;
                var msg = string.Format("{0}, Alterou o Status do Prospecto de {1}, para {2}", _usuarioLogado.LoginName, statusAntigo.Descricao, statusNovo.Descricao);
                
                    _ctx.Update(clienteExistente);
                    await _ctx.SaveChangesAsync();
                    await GravarAlteracaoProsp(msg, clienteExistente.Id);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            _notyf.Success(string.Format("<h3>Aviso</h3><br/><p>Cliente: {0} modificado com sucesso.</p>", cliente.Fantasia), 5);
            return RedirectToAction(nameof(Index));
            
          
            
        }

        private bool ClienteExists(int id)
        {
            return _ctx.ClienteProsp.Any(e => e.Id == id);
        }
        [HttpPost]
        public async Task GravarAlteracaoProsp(string hist, int clienteId)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                    _usuarioLogado = SetUserLogado();
                    var historico = new ClienteProspHistorico
                    {
                        Data = DateTime.Now,
                        UsuarioId = _usuarioLogado.Id,
                        ClienteProspId = clienteId,
                        Historico = hist
                    };
                    _ctx.Add(historico);
                    await _ctx.SaveChangesAsync();
                //}
            }
            catch (Exception e)
            {
                return;
            }

            return;
        }


    }
}
