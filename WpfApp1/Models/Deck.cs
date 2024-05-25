using System;
using System.Collections.Generic;
using System.Linq;
using static WpfApp1.Models.Card;

namespace WpfApp1.Models
{
    public class Deck
    {
        private List<Card> cards;
        private Random random = new Random();

        public Deck()
        {
            cards = new List<Card>();
            Rank[] ranks = { Rank.Two, Rank.Three, Rank.Four, Rank.Five, Rank.Six, Rank.Seven, Rank.Eight, Rank.Nine, Rank.Ten, Rank.Jack, Rank.Queen, Rank.King, Rank.Ace };

            // Перебираем все масти и ранги для создания колоды
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in ranks)
                {
                    cards.Add(new Card(rank, suit));
                }
            }
        }

        // Метод для тасования колоды
        public void Shuffle()
        {
            cards = cards.OrderBy(c => random.Next()).ToList();
        }

        // Метод для вытягивания карты из колоды
        public Card Draw()
        {
            if (cards.Count == 0)
            {
                throw new InvalidOperationException("Колода пуста.");
            }

            var card = cards.First();
            cards.RemoveAt(0);
            return card;
        }
    }
}
