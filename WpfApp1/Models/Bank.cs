using System;
using System.Windows;

namespace WpfApp1.Models
{
    // Класс для управления финансами игрока
    public class Bank
    {
        private int bank; // Общая сумма в банке
        private int totalWin; // Общая сумма выигрышей
        private int totalLose; // Общая сумма проигрышей
        private int bet; // Текущая ставка

        // Конструктор для инициализации банка с начальной суммой
        public Bank(int initialBank)
        {
            bank = initialBank;
            totalWin = 0;
            totalLose = 0;
            bet = 0;
        }

        // Свойства для получения текущего состояния банка и ставок
        public int BankAmount => bank;
        public int TotalWin => totalWin;
        public int TotalLose => totalLose;
        public int CurrentBet => bet;

        // Метод для размещения ставки
        public void PlaceBet(int amount)
        {
            if (amount > 0)
            {
                if (amount > bank)
                {
                    MessageBox.Show("Сумма больше, чем у вас есть!");
                }
                else
                {
                    bet += amount;
                    bank -= Math.Abs(amount); // Используем Math.Abs для учета знака перед суммой
                }
            }
            else
            {
                bet += amount;
                bank += Math.Abs(amount);
            }
        }

        // Метод для обработки выигрыша
        public void Win()
        {
            totalWin += bet;
            bank += bet * 2; // Игрок выигрывает свою ставку и получает ее обратно плюс выигрыш
            bet = 0;
        }

        // Метод для обработки проигрыша
        public void Lose()
        {
            totalLose += bet;
            // Ставка уже вычтена из банка при размещении, поэтому здесь ничего не нужно вычитать
            bet = 0;
        }

        // Метод для обработки ничьи
        public void Push()
        {
            bank += bet; // Возвращаем ставку в банк при ничье
            bet = 0;
        }

        // Метод для обработки сдачи
        public void Surrender()
        {
            totalLose += bet / 2;
            bank += bet / 2; // Возвращаем половину ставки в банк при сдаче
            bet = 0;
        }

        // Метод для сброса текущей ставки
        public void ResetBet()
        {
            bet = 0;
        }
    }
}
