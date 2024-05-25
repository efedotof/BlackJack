using System;
using System.Collections.Generic;

namespace WpfApp1.Models
{
    public class Player
    {
        public List<Card> Hand { get; private set; }

        public Player()
        {
            Hand = new List<Card>();
        }

        // Метод для добавления карты в руку игрока
        public void AddCard(Card card)
        {
            Hand.Add(card);
        }

        // Метод для очистки руки игрока
        public void ClearHand()
        {
            Hand.Clear();
        }

        // Метод для вычисления суммы очков в руке игрока
        public int CalculateScore()
        {
            int value = 0;
            int aces = 0;

            foreach (Card card in Hand)
            {
                int cardValue = GetCardValue(card);
                if (cardValue == 11)
                {
                    aces++;
                }
                value += cardValue;
            }

            // Если значение больше 21 и есть тузы, пересчитываем их как 1 вместо 11
            while (value > 21 && aces > 0)
            {
                value -= 10;
                aces--;
            }

            return value;
        }

        // Вспомогательный метод для получения значения карты
        private int GetCardValue(Card card)
        {
            int cardValue = (int)card.CardRank;
            if (cardValue >= 10) // Для карт-изображений (валет, дама, король)
            {
                return 10;
            }
            if (cardValue == 1) // Для туза
            {
                return 11;
            }
            return cardValue;
        }
    }
}
