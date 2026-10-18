using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modelos;
using info;
using Microsoft.AspNetCore.Authorization;
using System.Collections;
using X.PagedList;
using AspNetCoreHero.ToastNotification.Abstractions;

using Newtonsoft.Json;
using NuGet.Protocol;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Services;
using System.Text.RegularExpressions;

namespace SyncOSCore.Controllers
{
    [AllowAnonymous]
    public class ClientesController : Controller
    {
        private readonly APPDbContext _ctx;
        private readonly INotyfService _notyf;
        private Usuario _usuarioLogado;
        public ClientesController(APPDbContext context, INotyfService notyf)
        {
            _ctx = context;
            _notyf = notyf;
        }

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


        public async Task<IActionResult> Bloqueio(int id)
        {
            try
            {
                _usuarioLogado = SetUserLogado();
                if (_usuarioLogado.PerfilId == 1 || _usuarioLogado.PerfilId == 2)
                {
                    var cliente = await _ctx.Clientes.FindAsync(id);
                    cliente.Bloqueado = !cliente.Bloqueado;
                    _ctx.Update(cliente);
                    await _ctx.SaveChangesAsync();
                    var espelho = new EspelharCliente(_ctx, cliente).Salvar();
                    if (cliente.Bloqueado)
                    {
                        _notyf.Success(string.Format("<h3>Aviso</h3><br/><p>Cliente: {0} Bloqueado com sucesso.</p>", cliente.Fantasia), 5);
                    }
                    else
                    {
                        _notyf.Success(string.Format("<h3>Aviso</h3><br/><p>Cliente: {0} Desbloqueado com sucesso.</p>", cliente.Fantasia), 5);
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Home");
            }
        }

        //[Authorize(Roles = "Administrador,Financeiro")]
        public async Task<IActionResult> Ativar(int id)
        {
                try
                {
                     _usuarioLogado = SetUserLogado();
                    if (_usuarioLogado.PerfilId == 1 || _usuarioLogado.PerfilId == 2)
                    { 
                        var cliente = await _ctx.Clientes.FindAsync(id);
                        cliente.Desativado = !cliente.Desativado;
                        cliente.Bloqueado = false;
                        _ctx.Update(cliente);
                        await _ctx.SaveChangesAsync();
                        var espelho = new EspelharCliente(_ctx, cliente).Salvar();
                        if (cliente.Desativado)
                        {
                            _notyf.Success(String.Format("<h3>Aviso</h3><br/><p>Cliente: {0} Desativado com sucesso.</p>", cliente.Fantasia), 5);
                        }
                        else
                        {
                            _notyf.Success(String.Format("<h3>Aviso</h3><br/><p>Cliente: {0} Ativado com sucesso.</p>", cliente.Fantasia), 5);
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                        return RedirectToAction("Login", "Home");
                }
                }
                catch (Exception)
                {
                    return RedirectToAction("Login", "Home");
            }
        }


        //[Authorize(Roles = "Administrador,Financeiro,Suporte,Visitante")]
        [HttpPost]
        public async Task<JsonResult> FiltraMunicipio(string uf)
        {
            var municipios = await _ctx.Municipios.Where(w => w.UFId == uf)
                .OrderBy(o => o.Ordem)
                .ToListAsync();
            var lista = Json(municipios);
            return lista;
        }

        //[Authorize]
        public async Task<IActionResult> Index(int pagina, string filtro, int ativo)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }

            filtro ??= "";
            filtro = filtro.Trim().ToUpper();
            ViewData["Filtro"] = filtro;
            ViewData["Ativo"] = ativo;
            IOrderedQueryable<Cliente> clientes = 
                _ctx.Clientes
                    .Include(c => c.Municipio)
                    .Include(c => c.Revenda)
                    .Include(c => c.UF)
                    .OrderBy(c => c.Razao);

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                if ("0123456789".IndexOf(filtro[0]) > -1)
                {
                    clientes = _ctx.Clientes
                     .Include(c => c.Municipio)
                     .Include(c => c.Revenda)
                     .Include(c => c.UF)
                     .Where(w => w.CPF_CNPJ.Contains(filtro) || w.Id.ToString().Contains(filtro))
                     .OrderBy(c => c.Razao);


                   // clientes = (IOrderedQueryable<Cliente>)clientes.Where(w => w.CPF_CNPJ.Contains(filtro) || w.Id.ToString().Contains(filtro));
                }
                else if (filtro.StartsWith("*"))
                {
                    filtro = filtro[1..];
                    clientes = _ctx.Clientes
                        .Include(c => c.Municipio)
                        .Include(c => c.Revenda)
                        .Include(c => c.UF)
                        .Where(w => w.Fantasia.ToUpper().Contains(filtro) || w.Razao.ToUpper().Contains(filtro))
                        .OrderBy(c => c.Razao);
                    //clientes = (IOrderedQueryable<Cliente>)clientes.Where(w => w.Fantasia.ToUpper().Contains(filtro) || w.Razao.ToUpper().Contains(filtro));

                }
                else
                {
                    clientes = _ctx.Clientes
                        .Include(c => c.Municipio)
                        .Include(c => c.Revenda)
                        .Include(c => c.UF)
                        .Where(w => w.Fantasia.ToUpper().StartsWith(filtro) || w.Razao.ToUpper().StartsWith(filtro))
                        .OrderBy(c => c.Razao);
                    //clientes = (IOrderedQueryable<Cliente>)clientes.Where(w => w.Fantasia.ToUpper().StartsWith(filtro) || w.Razao.ToUpper().StartsWith(filtro));
                }
            }


            switch (ViewData["Ativo"].ToString())
            {
                case "1":
                    clientes = (IOrderedQueryable<Cliente>)clientes.Where(w => !w.Desativado);
                    ViewData["Ativo"] = "1";
                    break;
                case "2":
                    clientes = (IOrderedQueryable<Cliente>)clientes.Where(w => w.Desativado);
                    ViewData["Ativo"] = "2";
                    break;
                case "3":
                    clientes = (IOrderedQueryable<Cliente>)clientes.Where(w => w.Pendencia);
                    ViewData["Ativo"] = "3";
                    break;
            }

            ViewData["Perfil"] = _usuarioLogado.PerfilId;
            var numeroPagina = (pagina == 0) ? 1 : pagina;
            return View(await clientes.ToPagedListAsync(numeroPagina, 20));
        }

        //[Authorize(Roles = "Administrador,Financeiro,Suporte,Visitante")]
        public async Task<IActionResult> Details(int? id)
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

            var cliente = await _ctx.Clientes
                .Include(c => c.Municipio)
                .Include(c => c.Revenda)
                .Include(c => c.UF)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            ViewData["UFId"] = new SelectList(_ctx.UFs, "Id", "Descricao", cliente.UFId);
            ViewData["MunicipioId"] = new SelectList(_ctx.Municipios
                .OrderBy(o => o.Ordem)
                .ThenBy(t => t.Descricao)
                , "Id", "Desc_UF", cliente.MunicipioId); ;
            ViewData["RevendaId"] = new SelectList(_ctx.Revendas, "Id", "Descricao", cliente.RevendaId);

            return View(cliente);
        }

        //[Authorize(Roles = "Administrador,Financeiro,Suporte")]
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
                return View();
            }
            catch (Exception)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Ocorreu um erro ao tentar cadatrar um novo cliente</p>", 5);
                return RedirectToAction("index", "Clientes");
            }

        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador,Financeiro,Suporte,Visitante")]
        public async Task<IActionResult> Create([Bind("Id,RevendaId,Razao,Fantasia,CPF_CNPJ,Endereco,Bairro,CEP,UFId,MunicipioId,Numero,Bloqueado,Desativado,ValorContrato,UltimaAtualizacao,DataInclusao,DataAlteracao,LatitudeLongitude,InscricaoEstadual,ChaveFlexDocs,ChaveCSC,ChaveManifesto,ValorContratoBackup,WhatsApp,Email,Resp_Nome,Resp_Endereco,Resp_Numero,Resp_UF,Resp_Cidade,Resp_CEP,Resp_CPF,Contato")] Cliente cliente)
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
                if (ModelState.IsValid)
                {
                    _ctx.Add(cliente);
                    await _ctx.SaveChangesAsync();

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

        //[Authorize(Roles = "Administrador,Financeiro,Suporte")]
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

            var cliente = await _ctx.Clientes.FindAsync(id);
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
            ViewData["Bloqueado"] = cliente.Bloqueado;
            ViewData["Desativado"] = cliente.Desativado;
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador,Financeiro,Suporte")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RevendaId,Razao,Fantasia,CPF_CNPJ,Endereco,Bairro,CEP,UFId,Numero,Bloqueado,Desativado,ValorContrato,UltimaAtualizacao,DataInclusao,DataAlteracao,MunicipioId,LatitudeLongitude,InscricaoEstadual,ChaveFlexDocs,ChaveCSC,ChaveManifesto,ValorContratoBackup,WhatsApp,Email,Resp_Nome,Resp_Endereco,Resp_Numero,Resp_UF,Resp_Cidade,Resp_CEP,Resp_CPF,Contato,Retaguarda,Nfc,Nf,MDF,VipShop,Sped,Backup,TEF,Outros,EspecieOutros,Certificado")] Cliente cliente)
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
                    var espelho = new EspelharCliente(_ctx, cliente).Salvar();
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

        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
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

            var cliente = await _ctx.Clientes
                .Include(c => c.Municipio)
                .Include(c => c.Revenda)
                .Include(c => c.UF)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            var cliente = await _ctx.Clientes.FindAsync(id);
            _ctx.Clientes.Remove(cliente);
            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> PendenciaFinanceira(int clienteId)
        {
            _usuarioLogado = SetUserLogado();
            var cliente = await _ctx.Clientes.FindAsync(clienteId);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["Razao"] = cliente.Razao;
            ViewData["Fantasia"] = cliente.Fantasia;
            ViewData["CNPJ"] = cliente.CPF_CNPJ;
            return View(cliente);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PendenciaFinanceira([Bind("Id,PendenciaObs")] Cliente cliente)
        {
            _usuarioLogado = SetUserLogado();
            var aux = await _ctx.Clientes.FindAsync(cliente.Id);
            if (aux == null)
            {
                return NotFound();
            }
            aux.Pendencia = !aux.Pendencia;
            aux.PendenciaObs = cliente.PendenciaObs;
            _ctx.Update(aux);
            await _ctx.SaveChangesAsync();
            var espelho = new EspelharCliente(_ctx, aux).Salvar();
            if (aux.Pendencia)
            {
                _notyf.Success(string.Format("<h3>Aviso</h3><br/><p>Cliente: {0} com pendência.</p>", aux.Fantasia), 5);
            }
            else
            {
                _notyf.Success(string.Format("<h3>Aviso</h3><br/><p>Cliente: {0} sem pendência.</p>", aux.Fantasia), 5);
            }
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> ZerarToken(int clienteId)
        {
            _usuarioLogado = SetUserLogado();
            var cliente = await _ctx.Clientes.FindAsync(clienteId);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["Token"] = cliente.TokenAtualiza;
            ViewData["Razao"] = cliente.Razao;
            ViewData["Fantasia"] = cliente.Fantasia;
            ViewData["CNPJ"] = cliente.CPF_CNPJ;
            return View(cliente);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ZerarToken([Bind("Id")] Cliente cliente)
        {
            _usuarioLogado = SetUserLogado();
            var aux = await _ctx.Clientes.FindAsync(cliente.Id);
            if (aux == null)
            {
                return NotFound();
            }
            aux.TokenAtualiza = null;
            aux.RazaoFirebird = null;
            aux.NomeFirebird = null;
            _ctx.Update(aux);
            await _ctx.SaveChangesAsync();
            var espelho = new EspelharCliente(_ctx, aux).Salvar();
                _notyf.Success(string.Format("<h3>Aviso</h3><br/><p>Cliente: {0} Token Zerado.</p>", aux.Fantasia), 5);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> BaseControle(int clienteId)
        {

            _usuarioLogado = SetUserLogado();

            try
            { 
                var cliente = await _ctx.Clientes.FindAsync(clienteId);
                if (cliente == null)
                {
                    return NotFound();
                }
                ViewData["Token"] = cliente.TokenAtualiza;
                ViewData["Razao"] = cliente.Razao;
                ViewData["Fantasia"] = cliente.Fantasia;
                ViewData["CNPJ"] = cliente.CPF_CNPJ;
                ViewData["UFId"] = new SelectList(_ctx.UFs, "Id", "Descricao", "PB");
                ViewData["MunicipioId"] = new SelectList(_ctx.Municipios
                    .Where(w => w.UFId == "PB")
                    .OrderBy(o => o.Ordem)
                    .ThenBy(t => t.Descricao)
                    , "Id", "Desc_UF"); ;
                ViewData["RevendaId"] = new SelectList(_ctx.Revendas, "Id", "Descricao");
                return View(cliente);

            }
            catch (Exception)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Ocorreu um erro ao tentar cadatrar um novo cliente</p>", 5);
                return RedirectToAction("index", "Clientes");
            }



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BaseControle([Bind("Id,RevendaId,Razao,Fantasia,CPF_CNPJ,Endereco,Bairro,CEP,UFId,MunicipioId,Numero,Bloqueado,Desativado,ValorContrato,UltimaAtualizacao,DataInclusao,DataAlteracao,LatitudeLongitude,InscricaoEstadual,ChaveFlexDocs,ChaveCSC,ChaveManifesto,ValorContratoBackup,WhatsApp,Email,Resp_Nome,Resp_Endereco,Resp_Numero,Resp_UF,Resp_Cidade,Resp_CEP,Resp_CPF,Contato")] Cliente cliente)
        {
            _usuarioLogado = SetUserLogado();
            var aux = await _ctx.Clientes.FindAsync(cliente.Id);
            if (aux == null)
            {
                return NotFound();
            }
            aux.TokenAtualiza = null;
            aux.RazaoFirebird = null;
            aux.NomeFirebird = null;
            aux.Resp_Nome = "Controle";
            aux.Contato = "Controle";
            aux.Endereco = "Controle";
            aux.InscricaoEstadual = "Controle";

            aux.Id = await NextID.GetNextId("Cliente", _ctx);
            if (aux.CPF_CNPJ != null && !aux.CPF_CNPJ.EndsWith("99"))
            {
                aux.CPF_CNPJ = aux.CPF_CNPJ[..^2] + "99";
            }
            else
            {
                aux.CPF_CNPJ = aux.CPF_CNPJ[..^2] + "00";
            } 
                try
                {
                var espelho = new EspelharCliente(_ctx, aux).Salvar();
                if (ModelState.IsValid)
                    {
                        _ctx.Add(aux);
                        await _ctx.SaveChangesAsync();
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


        //[Authorize(Roles = "Administrador,Financeiro,Suporte")]
        private bool ClienteExists(int id)
        {
            return _ctx.Clientes.Any(e => e.Id == id);
        }
    }
}
