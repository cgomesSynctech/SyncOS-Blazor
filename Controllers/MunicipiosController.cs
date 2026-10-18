using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modelos;
using info;
using AspNetCoreHero.ToastNotification.Abstractions;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using Services;
using Newtonsoft.Json;

namespace SyncOSCore.Controllers
{
    [AllowAnonymous]
    public class MunicipiosController : Controller
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

        public MunicipiosController(APPDbContext context, INotyfService notyf)
        {
            _notyf = notyf;
            _ctx = context;
        }

        // GET: Municipios
        public async Task<IActionResult> Index(int pagina, string sortOrder, string searchString)
        {
            _usuarioLogado = SetUserLogado();
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            var numeroPagina = (pagina == 0) ? 1 : pagina;
            List<Municipio> listaObj;
            if (String.IsNullOrEmpty(searchString))
            {
                listaObj = await _ctx.Municipios
                    .Include(i => i.UF)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                if ("0123456789".IndexOf(searchString[0]) > -1)
                {
                    listaObj = await _ctx.Municipios
                        .Where(f => f.Id.ToString().StartsWith(searchString))
                        .Include(i => i.UF)
                        .AsNoTracking()
                        .ToListAsync();
                }
                else if (searchString[0] == '*')
                {
                    searchString = searchString.Substring(1);
                    listaObj = await _ctx.Municipios
                        .Where(f => f.Descricao.Contains(searchString))
                        .Include(i => i.UF)
                        .AsNoTracking()
                        .ToListAsync();
                }
                else
                {
                    listaObj = await _ctx.Municipios
                        .Where(f => f.Descricao.StartsWith(searchString))
                        .Include(i => i.UF)
                        .AsNoTracking()
                        .ToListAsync();
                }
            }
            if (ViewData["NameSortParm"].ToString() == "name_desc")
            {
                listaObj = await listaObj.OrderBy(o => o.Descricao).ToListAsync();
            }
            else
            {
                listaObj = await listaObj.OrderByDescending(o => o.Descricao).ToListAsync();
            }

            return View(await listaObj.ToPagedListAsync(numeroPagina, 20));
        }

        // GET: Municipios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            _usuarioLogado = SetUserLogado();
            if (id == null)
            {
                return NotFound();
            }

            var municipio = await _ctx.Municipios
                .Include(m => m.UF)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (municipio == null)
            {
                return NotFound();
            }

            return View(municipio);
        }

        // GET: Municipios/Create
        public IActionResult Create()
        {
            _usuarioLogado = SetUserLogado();
            ViewData["UFId"] = new SelectList(_ctx.UFs, "Id", "Descricao");
            return View();
        }

        // POST: Municipios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UFId,Descricao,Ordem")] Municipio municipio)
        {
            try
            {
                _usuarioLogado = SetUserLogado();
                if (ModelState.IsValid)
                {
                    _ctx.Add(municipio);
                    await _ctx.SaveChangesAsync();
                    var msg = "<p>Novo municipio salvo com sucesso!<p>";
                    _notyf.Information(msg,3);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["UFId"] = new SelectList(_ctx.UFs, "Id", "Id", municipio.UFId);
                
            }
            catch (Exception e)
            {
                var msg = string.Format("<p>Erro ao tentar criar novo municipio!<Br><b><i> <p>Verifique erro a seguir: {0} .</p> </i></b></p><br>", e.Message);
                _notyf.Error(msg, 5);
                Logger.Erro(e.Message);
            }
            return View(municipio);
        }

        // GET: Municipios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            _usuarioLogado = SetUserLogado();
            if (id == null)
            {
                return NotFound();
            }

            var municipio = await _ctx.Municipios.FindAsync(id);
            if (municipio == null)
            {
                return NotFound();
            }
            ViewData["UFId"] = new SelectList(_ctx.UFs, "Id", "Descricao", municipio.UFId);
            return View(municipio);
        }

        // POST: Municipios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UFId,Descricao,Ordem")] Municipio municipio)
        {
            _usuarioLogado = SetUserLogado();
            if (id != municipio.Id)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _ctx.Update(municipio);
                        await _ctx.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MunicipioExists(municipio.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    var msg = "<p>Municipio modificado com sucesso!<p>";
                    _notyf.Information(msg, 3);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                var msg = string.Format("<p>Erro ao tentar alterar o municipio!<Br><b><i> <p>Verifique erro a seguir: {0} .</p> </i></b></p><br>", e.Message);
                _notyf.Error(msg, 5);
                Logger.Erro(e.Message);
            }
            ViewData["UFId"] = new SelectList(_ctx.UFs, "Id", "Id", municipio.UFId);
            return View(municipio);
        }

        // GET: Municipios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            _usuarioLogado = SetUserLogado();
            if (id == null)
            {
                return NotFound();
            }

            var municipio = await _ctx.Municipios
                .Include(m => m.UF)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (municipio == null)
            {
                return NotFound();
            }

            return View(municipio);
        }

        // POST: Municipios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                _usuarioLogado = SetUserLogado();
                var municipio = await _ctx.Municipios.FindAsync(id);
                _ctx.Municipios.Remove(municipio);
                await _ctx.SaveChangesAsync();
                var msg = "<p>Municipio excluido com sucesso!<p>";
                _notyf.Information(msg, 3);
            }
            catch (Exception e)
            {
                var msg = string.Format("<p>Erro ao tentar apagar o municipio!<Br><b><i> <p>Verifique erro a seguir: {0} .</p> </i></b></p><br>", e.Message);
                _notyf.Error(msg, 5);
                Logger.Erro(e.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MunicipioExists(int id)
        {
            return _ctx.Municipios.Any(e => e.Id == id);
        }
    }
}
