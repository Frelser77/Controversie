using Controversie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Controversie.Controllers
{
    public class VerbaleController : Controller
    {
        private DataAccess dataAccess = new DataAccess();

        // GET: Verbale
        public ActionResult Index()
        {
            List<Verbale> verbali = dataAccess.GetVerbali().OrderByDescending(v => v.DataViolazione).ToList();
            return View(verbali);
        }

        public ActionResult Create()
        {
            // Anagrafiche
            ViewBag.Anagrafiche = dataAccess.GetAnagrafica().Select(a => new SelectListItem
            {
                Value = a.IdAnagrafica.ToString(),
                Text = a.Cognome + " " + a.Nome
            }).ToList();

            // Tipi di violazione
            IEnumerable<Violazione> violazioni = dataAccess.GetTipiViolazione();
            ViewBag.TipiViolazione = violazioni.Select(v => new SelectListItem
            {
                Value = v.IdViolazione.ToString(),
                Text = $"{v.Descrizione} - Punti Decurtati: {v.PuntiDecurtati}"
            }).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Verbale verbale)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Violazione violazioneSelezionata = dataAccess.GetTipiViolazione().FirstOrDefault(v => v.IdViolazione == verbale.Fk_IdViolazione);

                    if (violazioneSelezionata != null)
                    {
                        verbale.DecurtamentoPunti = violazioneSelezionata.PuntiDecurtati;
                    }

                    dataAccess.AddVerbale(verbale);
                    TempData["SuccessMessage"] = "Verbale creato con successo.";
                    return RedirectToAction("Index");
                }

                // Se ModelState non è valido, ricarica le ViewBag necessarie per la view
                ViewBag.Anagrafiche = dataAccess.GetAnagrafica().Select(a => new SelectListItem
                {
                    Value = a.IdAnagrafica.ToString(),
                    Text = a.Cognome + " " + a.Nome
                }).ToList();

                IEnumerable<Violazione> violazioni = dataAccess.GetTipiViolazione();
                ViewBag.TipiViolazione = violazioni.Select(v => new SelectListItem
                {
                    Value = v.IdViolazione.ToString(),
                    Text = $"{v.Descrizione} - Punti Decurtati: {v.PuntiDecurtati}"
                }).ToList();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Si è verificato un errore durante il salvataggio: " + ex.Message;
            }
            return View(verbale);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var verbale = dataAccess.GetVerbaleById(id);
            if (verbale == null)
            {
                return HttpNotFound();
            }
            return View(verbale);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Verbale verbale)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dataAccess.UpdateVerbale(verbale);
                    TempData["SuccessMessage"] = "Verbale modificato con successo";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // Log dell'errore
                TempData["ErrorMessage"] = "Si è verificato un errore durante l'aggiornamento: " + ex.Message;
            }

            return View(verbale);
        }

        [HttpPost]
        public ActionResult AsPaid(int id)
        {
            try
            {
                dataAccess.VerbaleAsPaid(id);
                TempData["SuccessMessage"] = "Contestabilitá modificata con successo per la violazione con identificativo: " + id;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log dell'errore
                TempData["ErrorMessage"] = "Si è verificato un errore: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}