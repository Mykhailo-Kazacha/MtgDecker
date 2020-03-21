using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApplication1.Models;

namespace WebApplication1.Infrastructure
{
    public class DecklistModelBinder :IModelBinder
    {
        private MtgDeckerContext db;
        private List<Deck> CreatedDecks = new List<Deck>();
        private List<Card> CreatedCards = new List<Card>();

        public DecklistModelBinder(MtgDeckerContext context)
        {
            this.db = context;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // в случае ошибки возвращаем исключение
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var deckName =  bindingContext.ValueProvider.GetValue("DeckName").ToString();
            var comment = bindingContext.ValueProvider.GetValue("Comment").ToString();
            string cardName;
            int quantity;
            int i = 0;
            
            List<DecklistEntry> decklistEntries = new List<DecklistEntry>();
            while (((cardName = bindingContext.ValueProvider.GetValue("DecklistEntries["+i+"].Card.Name").FirstValue) != null)
                && (int.TryParse(bindingContext.ValueProvider.GetValue("DecklistEntries[" + i + "].Card.Quantity").FirstValue,out quantity))
                && (quantity != 0))
                {
                DecklistEntry entry = new DecklistEntry();
                int index = 0;

                bool isSideboard;
                bool.TryParse(bindingContext.ValueProvider.GetValue("DecklistEntries[" + i + "].IsSideboard").FirstValue, out isSideboard);
                entry.IsSideboard = isSideboard;

                Card card = null;

                if (db.Cards.Where(c => c.Name == cardName).Any())
                {
                    card = db.Cards.Where(c => c.Name == cardName).First();
                }
                else if (CreatedCards.Where(c => c.Name == cardName).Any())
                {
                    card = CreatedCards.Where(c => c.Name == cardName).First();                  
                }
                else
                {
                   card = new Card { Name = cardName, Legality = new List<int>() };
                   CreatedCards.Add(card);
                }

                
                Deck deck = null;
                if (db.Decks.Where(d => d.Name == deckName).Any())
                {
                    deck = db.Decks.Where(d => d.Name == deckName).First();
                }
                else if (CreatedDecks.Where(d => d.Name == deckName).Any())
                {
                    deck = CreatedDecks.Where(d => d.Name == deckName).First();
                }
                else
                {
                    deck = new Deck { Name = deckName, DeckId = 0, CreatedAt = DateTime.Now, ChangedAt = DateTime.Now };
                    CreatedDecks.Add(deck);
                }

                int decklistId = 1;
                if (db.DecklistEntries.Any())
                {
                    decklistId = db.DecklistEntries.Max(dle => dle.DecklistId) + 1;
                }

                decklistEntries.Add(new DecklistEntry {DecklistId = decklistId, Card = card, CardId = card.CardId, Deck = deck, DeckId= deck.DeckId,
               Index = index, IsSideboard = isSideboard, Comment = comment, Quantity = quantity});
                i++;
                };

            



            // Объединяем полученные значения в один объект DateTime
            var result = new Decklist { DeckName = deckName, Comment=comment, DecklistEntries = decklistEntries};

            // устанавливаем результат привязки
            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}
