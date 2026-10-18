using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using info;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using X.PagedList;
using Services;
using Modelos;
using Newtonsoft.Json;
using System.Data;

namespace SyncOSCore.Controllers
{
    [AllowAnonymous]
    public class ChamadosController : Controller
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


        public ChamadosController(APPDbContext context, INotyfService notyf)
        {
            _ctx = context;
            _notyf = notyf;
        }


        public async Task<ActionResult> Finalizar(int id)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            var chamado = await _ctx.Chamados.FindAsync(id);
            chamado.TecnicoId = _usuarioLogado.Id;
            chamado.DataFinalizacao = DateTime.Now;
            chamado.ChamadoStatusId = 0;
            if (ModelState.IsValid)
            {
                try
                {
                    _notyf.Success(String.Format("<h3>Aviso</h3><br/><p>Chamado finalizado com sucesso, protocolo: {0}.</p>", chamado.ProtocoloAutorizacao), 5);
                    _ctx.Update(chamado);
                    await _ctx.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
            }
            return RedirectToAction(nameof(Index), new { pagina = 1, status = 1, modulo = 100 });
        }

        // GET: Chamados
        [HttpGet()]
        public async Task<IActionResult> Index(int pagina, int status, int modulo)
        {
            _usuarioLogado = SetUserLogado();

            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            IOrderedQueryable<Chamado> chamados;
            if (modulo == 100 && status == 100)
            {
                chamados = _ctx.Chamados
                    .Include(c => c.Cliente)
                    .Include(c => c.Forma)
                    .Include(c => c.Modulo)
                    .Include(c => c.Motivo)
                    .Include(c => c.Prioridade)
                    .Include(c => c.Status)
                    .Include(c => c.Tecnico)
                    .Include(c => c.Usuario)
                    .OrderByDescending(o => o.ChamadoPrioridadeId)
                    .ThenBy(o => o.Id);
            }
            else if (status == 100)
            {
                chamados = _ctx.Chamados
                    .Include(c => c.Cliente)
                    .Include(c => c.Forma)
                    .Include(c => c.Modulo)
                    .Include(c => c.Motivo)
                    .Include(c => c.Prioridade)
                    .Include(c => c.Status)
                    .Include(c => c.Tecnico)
                    .Include(c => c.Usuario)
                    .Where(w => w.ChamadoModuloId == modulo)
                    .OrderByDescending(o => o.ChamadoPrioridadeId)
                    .ThenBy(o => o.Id);
            }
            else if (modulo == 100)
            {
                chamados = _ctx.Chamados
                    .Include(c => c.Cliente)
                    .Include(c => c.Forma)
                    .Include(c => c.Modulo)
                    .Include(c => c.Motivo)
                    .Include(c => c.Prioridade)
                    .Include(c => c.Status)
                    .Include(c => c.Tecnico)
                    .Include(c => c.Usuario)
                    .Where(w => w.ChamadoStatusId == status)
                    .OrderByDescending(o => o.ChamadoPrioridadeId)
                    .ThenBy(o => o.Id);
            }
            else 
            {
                chamados = _ctx.Chamados
                    .Include(c => c.Cliente)
                    .Include(c => c.Forma)
                    .Include(c => c.Modulo)
                    .Include(c => c.Motivo)
                    .Include(c => c.Prioridade)
                    .Include(c => c.Status)
                    .Include(c => c.Tecnico)
                    .Include(c => c.Usuario)
                    .Where(w => w.ChamadoStatusId == status && w.ChamadoModuloId == modulo)
                    .OrderByDescending(o => o.ChamadoPrioridadeId)
                    .ThenBy(o => o.Id);
            }

            ViewData["ChamadoStatuId"] = new SelectList(_ctx.ChamadoStatus, "Id", "Descricao", status);
            ViewData["ChamadoModuloId"] = new SelectList(_ctx.ChamadoModulo, "Id", "Descricao", modulo);
            ViewData["Status"] = status;
            ViewData["Modulo"] = modulo;

            var numeroPagina = (pagina == 0) ? 1 : pagina;
            return View(await chamados.ToPagedListAsync(numeroPagina, 20));
        }

        [HttpGet()]
        public async Task<IActionResult> Lista(int pagina, int status, int modulo)
        {
            _usuarioLogado = SetUserLogado();

            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            IOrderedQueryable<Chamado> chamados;
            if (modulo == 100 && status == 100)
            {
                chamados = _ctx.Chamados
                    .Include(c => c.Cliente)
                    .Include(c => c.Forma)
                    .Include(c => c.Modulo)
                    .Include(c => c.Motivo)
                    .Include(c => c.Prioridade)
                    .Include(c => c.Status)
                    .Include(c => c.Tecnico)
                    .Include(c => c.Usuario)
                    .OrderByDescending(o => o.ChamadoPrioridadeId)
                    .ThenBy(o => o.Id);
            }
            else if (status == 100)
            {
                chamados = _ctx.Chamados
                    .Include(c => c.Cliente)
                    .Include(c => c.Forma)
                    .Include(c => c.Modulo)
                    .Include(c => c.Motivo)
                    .Include(c => c.Prioridade)
                    .Include(c => c.Status)
                    .Include(c => c.Tecnico)
                    .Include(c => c.Usuario)
                    .Where(w => w.ChamadoModuloId == modulo)
                    .OrderByDescending(o => o.ChamadoPrioridadeId)
                    .ThenBy(o => o.Id);
            }
            else if (modulo == 100)
            {
                chamados = _ctx.Chamados
                    .Include(c => c.Cliente)
                    .Include(c => c.Forma)
                    .Include(c => c.Modulo)
                    .Include(c => c.Motivo)
                    .Include(c => c.Prioridade)
                    .Include(c => c.Status)
                    .Include(c => c.Tecnico)
                    .Include(c => c.Usuario)
                    .Where(w => w.ChamadoStatusId == status)
                    .OrderByDescending(o => o.ChamadoPrioridadeId)
                    .ThenBy(o => o.Id);
            }
            else
            {
                chamados = _ctx.Chamados
                    .Include(c => c.Cliente)
                    .Include(c => c.Forma)
                    .Include(c => c.Modulo)
                    .Include(c => c.Motivo)
                    .Include(c => c.Prioridade)
                    .Include(c => c.Status)
                    .Include(c => c.Tecnico)
                    .Include(c => c.Usuario)
                    .Where(w => w.ChamadoStatusId == status && w.ChamadoModuloId == modulo)
                    .OrderByDescending(o => o.ChamadoPrioridadeId)
                    .ThenBy(o => o.Id);
            }

            ViewData["ChamadoStatuId"] = new SelectList(_ctx.ChamadoStatus, "Id", "Descricao", status);
            ViewData["ChamadoModuloId"] = new SelectList(_ctx.ChamadoModulo, "Id", "Descricao", modulo);
            ViewData["Status"] = status;
            ViewData["Modulo"] = modulo;

            var numeroPagina = (pagina == 0) ? 1 : pagina;
            return View(await chamados.ToPagedListAsync(numeroPagina, 20));
        }









        // GET: Chamados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            if (id == null || _ctx.Chamados == null)
            {
                return NotFound();
            }

            var chamado = await _ctx.Chamados
                .Include(c => c.Cliente)
                .Include(c => c.Forma)
                .Include(c => c.Modulo)
                .Include(c => c.Motivo)
                .Include(c => c.Prioridade)
                .Include(c => c.Status)
                .Include(c => c.Tecnico)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chamado == null)
            {
                return NotFound();
            }

            return View(chamado);
        }

        // GET: Chamados/Create
        public async Task<IActionResult> Create(int Id)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }

            if (Id > 0)
            {
                var cliente = await _ctx.Clientes.FindAsync(Id);
                if ((cliente.Bloqueado || cliente.Desativado) && _usuarioLogado.PerfilId > 2)
                {
                    var msg = string.Format("<h3>Aviso</h3><br/><p>Não é possível abrir chamado para este cliente.<br/>O Cliente: {0} <br/>encontra-se Bloqueado ou Desativado,<br/>favor passar o telefone do financeiro parael.</p>", cliente.Razao);
                    _notyf.Warning(msg, 5);
                    return RedirectToAction("Index", "Clientes");
                }
            }


            var chamado = new Chamado
            {
                ClienteId = Id,
                UsuarioId = _usuarioLogado.Id,
                TecnicoId = _usuarioLogado.Id,
                ChamadoStatusId = 1,
                DataAbertura = DateTime.Now,
                ProtocoloAutorizacao = DateTime.UtcNow.ToString("yyyymmddhhMMss")
            };
            if (Id == 0)
            {
                ViewData["ClienteId"] = new SelectList(_ctx.Clientes.Where(w => !w.Bloqueado).OrderBy(o => o.Razao), "Id", "Razao");
            }
            else
            {
                ViewData["ClienteId"] = new SelectList(_ctx.Clientes.Where(f => f.Id == Id), "Id", "Razao");
            }
            
            ViewData["ChamadoFormaId"] = new SelectList(_ctx.ChamadoForma, "Id", "Descricao");
            ViewData["ChamadoModuloId"] = new SelectList(_ctx.ChamadoModulo, "Id", "Descricao");
            ViewData["ChamadoMotivoId"] = new SelectList(_ctx.Set<ChamadoMotivo>(), "Id", "Descricao");
            ViewData["ChamadoPrioridadeId"] = new SelectList(_ctx.ChamadoPrioridade, "Id", "Descricao");
            ViewData["ChamadoStatusId"] = new SelectList(_ctx.ChamadoStatus, "Id", "Descricao");
            ViewData["TecnicoId"] = new SelectList(_ctx.Usuarios, "Id", "LoginName");
            ViewData["UsuarioId"] = new SelectList(_ctx.Usuarios.Where(f => f.Id == _usuarioLogado.Id), "Id", "LoginName");
            return View(chamado);
        }

        // POST: Chamados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,UsuarioId,TecnicoId,ChamadoStatusId,ChamadoModuloId,ChamadoMotivoId,ChamadoFormaId,ChamadoPrioridadeId,DataAbertura,DataAtendimento,DataFinalizacao,AlegacaoCliente,AlegacaoTecnico,Resolucao,Interlocutor,IsSolucionado,IsImprocedente,ProtocoloAutorizacao")] Chamado chamado)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                chamado.DataAbertura = DateTime.Now;
                chamado.DataAtendimento = DateTime.MinValue;
                chamado.DataFinalizacao = DateTime.MinValue;
                chamado.ChamadoStatusId = 1;
                chamado.UsuarioId = _usuarioLogado.Id;
                chamado.TecnicoId = _usuarioLogado.Id;

                _ctx.Add(chamado);
                await _ctx.SaveChangesAsync();
                ViewData["ClienteId"] = new SelectList(_ctx.Clientes, "Id", "Razao", chamado.ClienteId);
                ViewData["ChamadoFormaId"] = new SelectList(_ctx.ChamadoForma, "Id", "Descricao", chamado.ChamadoFormaId);
                ViewData["ChamadoModuloId"] = new SelectList(_ctx.ChamadoModulo, "Id", "Descricao", chamado.ChamadoModuloId);
                ViewData["ChamadoMotivoId"] = new SelectList(_ctx.Set<ChamadoMotivo>(), "Id", "Descricao", chamado.ChamadoMotivoId);
                ViewData["ChamadoPrioridadeId"] = new SelectList(_ctx.ChamadoPrioridade, "Id", "Descricao", chamado.ChamadoPrioridadeId);
                ViewData["ChamadoStatusId"] = new SelectList(_ctx.ChamadoStatus, "Id", "Descricao", chamado.ChamadoStatusId);
                ViewData["TecnicoId"] = new SelectList(_ctx.Usuarios, "Id", "LoginName", chamado.TecnicoId);
                ViewData["UsuarioId"] = new SelectList(_ctx.Usuarios, "Id", "LoginName", chamado.UsuarioId);
                _notyf.Success(String.Format("<h3>Aviso</h3><br/><p>Chamado aberto com sucesso, protocolo: {0}.</p>", chamado.ProtocoloAutorizacao), 5);
                return RedirectToAction(nameof(Index), new {pagina = 0, status = 1, modulo = 100});
            }
            else
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Erro ao tentar criar novo chamado!</p>", 5);
                return RedirectToAction(nameof(Index), new { pagina = 0, status = 1, modulo = 100 });
            }
        }

        // GET: Chamados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            if (id == null || _ctx.Chamados == null)
            {
                return NotFound();
            }

            var chamado = await _ctx.Chamados.FindAsync(id);


            if (chamado == null)
            {
                return NotFound();
            }
            if (id == 0)
            {
                ViewData["ClienteId"] = new SelectList(_ctx.Clientes, "Id", "Razao", chamado.ClienteId);
            }
            else
            {
                ViewData["ClienteId"] = new SelectList(_ctx.Clientes.Where(w => w.Id == chamado.ClienteId), "Id", "Razao", chamado.ClienteId);
            }
            
            ViewData["ChamadoFormaId"] = new SelectList(_ctx.ChamadoForma, "Id", "Descricao", chamado.ChamadoFormaId);
            ViewData["ChamadoModuloId"] = new SelectList(_ctx.ChamadoModulo, "Id", "Descricao", chamado.ChamadoModuloId);
            ViewData["ChamadoMotivoId"] = new SelectList(_ctx.Set<ChamadoMotivo>(), "Id", "Descricao", chamado.ChamadoMotivoId);
            ViewData["ChamadoPrioridadeId"] = new SelectList(_ctx.ChamadoPrioridade, "Id", "Descricao", chamado.ChamadoPrioridadeId);
            ViewData["ChamadoStatusId"] = new SelectList(_ctx.ChamadoStatus, "Id", "Descricao", chamado.ChamadoStatusId);
            ViewData["TecnicoId"] = new SelectList(_ctx.Usuarios, "Id", "LoginName", chamado.TecnicoId);
            ViewData["UsuarioId"] = new SelectList(_ctx.Usuarios, "Id", "LoginName", chamado.UsuarioId);
            return View(chamado);
        }

        // POST: Chamados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,UsuarioId,TecnicoId,ChamadoStatusId,ChamadoModuloId,ChamadoMotivoId,ChamadoFormaId,ChamadoPrioridadeId,DataAbertura,DataAtendimento,DataFinalizacao,AlegacaoCliente,AlegacaoTecnico,Resolucao,Interlocutor,IsSolucionado,IsImprocedente,ProtocoloAutorizacao")] Chamado chamado)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            if (id != chamado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (chamado.IsSolucionado)
                    {
                        chamado.ChamadoStatusId = 0;
                        chamado.DataFinalizacao = DateTime.Now;
                        _notyf.Success(String.Format("<h3>Aviso</h3><br/><p>Chamado finalizado com sucesso, protocolo: {0}.</p>", chamado.ProtocoloAutorizacao), 5);
                    }
                    chamado.DataAtendimento = DateTime.Now;
                    chamado.TecnicoId = _usuarioLogado.Id;
                    _ctx.Update(chamado);
                    await _ctx.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChamadoExists(chamado.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new {pagina = 1, status = 1, modulo = 100});
            }
            ViewData["ClienteId"] = new SelectList(_ctx.Clientes, "Id", "Razao", chamado.ClienteId);
            ViewData["ChamadoFormaId"] = new SelectList(_ctx.ChamadoForma, "Id", "Descricao", chamado.ChamadoFormaId);
            ViewData["ChamadoModuloId"] = new SelectList(_ctx.ChamadoModulo, "Id", "Descricao", chamado.ChamadoModuloId);
            ViewData["ChamadoMotivoId"] = new SelectList(_ctx.Set<ChamadoMotivo>(), "Id", "Descricao", chamado.ChamadoMotivoId);
            ViewData["ChamadoPrioridadeId"] = new SelectList(_ctx.ChamadoPrioridade, "Id", "Descricao", chamado.ChamadoPrioridadeId);
            ViewData["ChamadoStatusId"] = new SelectList(_ctx.ChamadoStatus, "Id", "Descricao", chamado.ChamadoStatusId);
            ViewData["TecnicoId"] = new SelectList(_ctx.Usuarios, "Id", "LoginName", chamado.TecnicoId);
            ViewData["UsuarioId"] = new SelectList(_ctx.Usuarios, "Id", "LoginName", chamado.UsuarioId);
            return View(chamado);
        }

        // GET: Chamados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            if (id == null || _ctx.Chamados == null)
            {
                return NotFound();
            }

            var chamado = await _ctx.Chamados
                .Include(c => c.Cliente)
                .Include(c => c.Forma)
                .Include(c => c.Modulo)
                .Include(c => c.Motivo)
                .Include(c => c.Prioridade)
                .Include(c => c.Status)
                .Include(c => c.Tecnico)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chamado == null)
            {
                return NotFound();
            }

            return View(chamado);
        }

        // POST: Chamados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            if (_ctx.Chamados == null)
            {
                return Problem("Entity set 'APPDbContext.Chamados'  is null.");
            }
            var chamado = await _ctx.Chamados.FindAsync(id);
            if (chamado != null)
            {
                _ctx.Chamados.Remove(chamado);
            }
            
            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChamadoExists(int id)
        {
            return _ctx.Chamados.Any(e => e.Id == id);
        }
    }
}
