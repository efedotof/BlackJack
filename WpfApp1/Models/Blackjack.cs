using System;
using System.Collections.Generic;

namespace WpfApp1.Models
{
    // Класс для управления логикой игры в блэкджек
    public class Blackjack
    {
        private Deck _deck; // Колода карт
        private List<Card> _playerHand; // Рука игрока
        private List<Card> _dealerHand; // Рука дилера

        // Конструктор инициализирует колоду и руки игрока и дилера
        public Blackjack()
        {
            _deck = new Deck();
            _playerHand = new List<Card>();
            _dealerHand = new List<Card>();
        }

        // Метод для начала новой игры
        public void StartGame()
        {
            _deck.Shuffle(); // Перемешиваем колоду
            _playerHand.Clear(); // Очищаем руку игрока
            _dealerHand.Clear(); // Очищаем руку дилера

            // Раздаем по две карты игроку и дилеру
            _playerHand.Add(_deck.Draw());
            _dealerHand.Add(_deck.Draw());
            _playerHand.Add(_deck.Draw());
            _dealerHand.Add(_deck.Draw());
        }

        // Метод для взятия карты игроком
        public Card PlayerHit()
        {
            Card drawnCard = _deck.Draw(); // Тянем карту из колоды
            _playerHand.Add(drawnCard); // Добавляем карту в руку игрока
            return drawnCard; // Возвращаем вытянутую карту
        }

        // Метод для взятия карты дилером
        public Card DealerHit()
        {
            Card drawnCard = _deck.Draw(); // Тянем карту из колоды
            _dealerHand.Add(drawnCard); // Добавляем карту в руку дилера
            return drawnCard; // Возвращаем вытянутую карту
        }

        // Метод для подсчета суммы очков в руке
        public int CalculateHandValue(List<Card> hand)
        {
            int value = 0; // Сумма очков
            int aces = 0; // Количество тузов

            // Проходим по всем картам в руке
            foreach (Card card in hand)
            {
                int cardValue = (int)card.CardRank; // Получаем значение карты

                if (cardValue >= 10)
                {
                    cardValue = 10; // Карты с десяткой, валетом, дамой и королем стоят 10 очков
                }
                else if (cardValue == 1)
                {
                    cardValue = 11; // Туз стоит 11 очков
                    aces++;
                }

                value += cardValue; // Добавляем значение карты к общей сумме
            }

            // Если сумма очков больше 21 и есть тузы, уменьшаем сумму, считая тузы за 1 очко
            while (value > 21 && aces > 0)
            {
                value -= 10;
                aces--;
            }
            return value; // Возвращаем итоговую сумму очков
        }

        // Метод для получения руки игрока
        public List<Card> GetPlayerHand()
        {
            return _playerHand;
        }

        // Метод для получения руки дилера
        public List<Card> GetDealerHand()
        {
            return _dealerHand;
        }
    }
}
