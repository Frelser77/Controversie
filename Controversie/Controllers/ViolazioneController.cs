using Controversie.Models;
using System;
using System.Web.Mvc;

namespace Controversie.Controllers
{
    public class ViolazioneController : Controller
    {
        private DataAccess dataAccess = new DataAccess();

        public ActionResult Index()
        {
            var violazioni = dataAccess.GetViolazioniContestabili();
            return View(violazioni);
        }

        // GET: Violazione/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Violazione/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Violazione violazione)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dataAccess.AddViolazione(violazione);
                    TempData["SuccessMessage"] = "Violazione aggiunta con successo.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Errore durante l'aggiunta della violazione: {ex.Message}";
            }
            return View(violazione);
        }

        // GET: Violazione/Details/5
        public ActionResult Details(int id)
        {
            var violazione = dataAccess.GetViolazioneById(id);
            if (violazione == null)
            {
                return HttpNotFound();
            }
            return View(violazione);
        }

        // GET: Violazione/Edit/5
        public ActionResult Edit(int id)
        {
            var violazione = dataAccess.GetViolazioneById(id);
            if (violazione == null)
            {
                return HttpNotFound();
            }
            return View(violazione);
        }

        // POST: Violazione/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Violazione violazione)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dataAccess.UpdateViolazione(violazione);
                    TempData["SuccessMessage"] = "Violazione aggiornata con successo.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Errore durante l'aggiornamento della violazione: {ex.Message}";
            }
            return View(violazione);
        }

        // GET: Violazione/Delete/5
        public ActionResult Delete(int id)
        {
            var violazione = dataAccess.GetViolazioneById(id);
            if (violazione == null)
            {
                return HttpNotFound();
            }
            return View(violazione);
        }

        // POST: Violazione/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                dataAccess.DeleteViolazione(id);
                TempData["SuccessMessage"] = "Violazione eliminata con successo.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Errore durante l'eliminazione della violazione: {ex.Message}";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ToggleContestabile(int id)
        {
            try
            {
                dataAccess.ToggleContestabile(id);
                TempData["SuccessMessage"] = "Stato contestabile della violazione cambiato con successo.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Errore durante il cambio dello stato contestabile: {ex.Message}";
            }
            return RedirectToAction("Index");
        }
    }
}