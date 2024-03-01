using Controversie.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Controversie.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        private readonly DataAccess _dataAccess;

        public ReportController()
        {
            _dataAccess = new DataAccess();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TotaleVerbaliPerTrasgressore()
        {
            try
            {
                List<TrasgressoreReport> risultati = _dataAccess.GetTotaleVerbaliPerTrasgressore();
                ViewBag.Title = "Risultati Report";
                return View("TotaleVerbaliPerTrasgressore", risultati);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Si è verificato un errore durante il recupero del [Totale verbali per trasgressori]: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult TotalePuntiDecurtatiPerTrasgressore()
        {
            try
            {
                List<TrasgressoreReport> risultati = _dataAccess.GetTotalePuntiDecurtatiPerTrasgressore();
                ViewBag.Title = "Risultati Report";
                return View("TotalePuntiDecurtatiPerTrasgressore", risultati);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Si è verificato un errore durante il recupero del [Totale punti decurtati per tragressore]: " + ex.Message;
                return RedirectToAction("Index");
            }

        }

        public ActionResult ViolazioniOltre10Punti()
        {
            try
            {
                List<ViolazioneReport> risultati = _dataAccess.GetViolazioniOltre10Punti();
                ViewBag.Title = "Risultati Report";
                return View("ViolazioniOltre10Punti", risultati);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Si è verificato un errore durante il recupero delle [Violazioni oltre 10 punti]: " + ex.Message;
                return RedirectToAction("Index");
            }

        }

        public ActionResult ViolazioniImportoMaggiore400()
        {
            try
            {
                List<ViolazioneReport> risultati = _dataAccess.GetViolazioniImportoMaggiore400();
                ViewBag.Title = "Risultati Report";
                return View("ViolazioniImportoMaggiore400", risultati);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Si è verificato un errore durante il recupero delle [Violazione importo maggiore 400]: " + ex.Message;
                return RedirectToAction("Index");
            }

        }
    }
}