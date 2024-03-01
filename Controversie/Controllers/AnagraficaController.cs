using Controversie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Controversie.Controllers
{
    public class AnagraficaController : Controller
    {
        private DataAccess dataAccess = new DataAccess();

        // GET: Anagrafica
        public ActionResult Index()
        {
            try
            {
                List<Anagrafica> list = dataAccess.GetAnagrafica();
                if (list == null || !list.Any())
                {
                    TempData["ErrorMessage"] = "Si è verificato un errore durante il recupero dei dati: ";
                }
                return View(list);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Si è verificato un errore durante il recupero dei dati: " + ex.Message;
                return View(new List<Anagrafica>());
            }
        }

        // GET: Anagrafica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Anagrafica/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Anagrafica anagrafica)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dataAccess.AddAnagrafica(anagrafica);
                    TempData["SuccessMessage"] = "Anagrafica creata con successo.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // Gestire l'errore qui
                ModelState.AddModelError("", "Impossibile salvare i cambiamenti. Riprova, e se il problema persiste vedi l'amministratore di sistema: " + ex.Message);
            }
            return View(anagrafica);
        }

        // GET: Anagrafica/Edit/5
        public ActionResult Edit(int id)
        {
            var anagrafica = dataAccess.GetAnagrafica().FirstOrDefault(a => a.IdAnagrafica == id);
            if (anagrafica == null)
            {
                return HttpNotFound();
            }
            return View(anagrafica);
        }

        // POST: Anagrafica/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Anagrafica anagrafica)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dataAccess.UpdateAnagrafica(anagrafica);
                    TempData["SuccessMessage"] = "Anagrafica modificata con successo.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Impossibile aggiornare i cambiamenti. Riprova.");
            }
            return View(anagrafica);
        }

        // POST: Anagrafica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                dataAccess.DeleteAnagrafica(id);
                TempData["SuccessMessage"] = "Anagrafica eliminata con successo.";
            }
            catch
            {
                TempData["ErrorMessage"] = "Non è stato possibile eliminare l'anagrafica. Potrebbe essere in uso in altre parti dell'applicazione.";
            }

            return RedirectToAction("Index");
        }
    }
}

