using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private MtgDeckerContext db;

        public HomeController(MtgDeckerContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            ViewBag.Cards = db.Cards;

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult AddCard()
        {
            ViewBag.Formats = db.Formats;

            return View();
        }

        [HttpPost]
        public IActionResult AddCard(Card card)
        { 

            db.Cards.Add(card);
            db.SaveChanges();

            return RedirectToAction("AddCard");
        }

        [HttpGet]
        public IActionResult AddDecklist()
        {
            ViewBag.Formats = db.Formats;

            return View();
        }

        [HttpPost]
        public IActionResult AddDecklist(Decklist decklist)
        {

            db.DecklistEntries.AddRange(decklist.DecklistEntries);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult ViewDeck(string name)
        {
            if (db.Decks.Where(d => d.Name == name).Any())
            {
                Decklist decklist = new Decklist
                {
                    DeckName = name,
                    DecklistEntries = db.DecklistEntries.Include("Deck").Include("Card").Where(dle => dle.Deck.Name == name).ToList<DecklistEntry>()
                };
                return View(decklist);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
