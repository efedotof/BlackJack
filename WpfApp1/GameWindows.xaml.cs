using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Models;

namespace WpfApp1
{
    // Окно игры
    public partial class GameWindows : Window
    {
        // Поля класса
        private Deck _deck; // Колода карт
        private CardControl _playerCard1; // Контроллер карты игрока 1
        private CardControl _playerCard2; // Контроллер карты игрока 2
        private CardControl _dealerCard1; // Контроллер карты дилера 1
        private CardControl _dealerCard2; // Контроллер карты дилера 2
        private Bank _bank; // Банк игрока
        private Player _player; // Игрок
        private Player _dealer; // Дилер
        private bool isPlay = false; // Флаг, указывающий на то, идет ли игра

        // Конструктор класса
        public GameWindows()
        {
            InitializeComponent();
            // Инициализация объектов
            _bank = new Bank(1000); // Инициализация банка с начальным балансом 1000
            _player = new Player(); // Инициализация игрока
            _dealer = new Player(); // Инициализация дилера
            UpdateBetText(); // Обновление текста ставки
            StartButton.Click += StartButton_Click; // Привязка обработчика события к кнопке "Старт"
        }

        // Обработчик нажатия кнопки "Старт"
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (_bank.CurrentBet == 0)
            {
                MessageBox.Show("Пожалуйста, сделайте ставку перед началом игры.");
                return; // Выйти из метода без начала игры
            }
            else
            {
                // Показать элементы интерфейса
                elementHideDouble.Visibility = Visibility.Visible;
                CurrentBetsPanel.Visibility = Visibility.Collapsed;
                StartButton.Visibility = Visibility.Collapsed;
                // Создание новой колоды и ее перемешивание
                _deck = new Deck();
                _deck.Shuffle();

                // Очистка рук игрока и дилера, а также контейнеров для карт
                _player.Hand.Clear();
                _dealer.Hand.Clear();
                PlayerCards.Children.Clear();
                DealerCards.Children.Clear();

                // Создание контроллеров карт и добавление карт в руки игрока и дилера
                _playerCard1 = new CardControl();
                _playerCard2 = new CardControl();
                _dealerCard1 = new CardControl();
                _dealerCard2 = new CardControl();

                var playerCard1 = _deck.Draw();
                var playerCard2 = _deck.Draw();
                var dealerCard1 = _deck.Draw();
                var dealerCard2 = _deck.Draw();

                _player.Hand.Add(playerCard1);
                _player.Hand.Add(playerCard2);
                _dealer.Hand.Add(dealerCard1);
                _dealer.Hand.Add(dealerCard2);

                _playerCard1.SetCard(playerCard1);
                _playerCard2.SetCard(playerCard2);
                _dealerCard1.SetCard(dealerCard1);
                _dealerCard2.SetCard(dealerCard2);
                _dealerCard2.SetHidden(true);

                PlayerCards.Children.Add(_playerCard1);
                PlayerCards.Children.Add(_playerCard2);
                DealerCards.Children.Add(_dealerCard1);
                DealerCards.Children.Add(_dealerCard2);

                isPlay = true;
            }
        }

        // Обработчик нажатия кнопки "Ещё"
        private void HitButton_Click(object sender, RoutedEventArgs e)
        {
            if (isPlay)
            {
                var drawnCard = _deck.Draw();
                var newCardControl = new CardControl();
                newCardControl.SetCard(drawnCard);
                PlayerCards.Children.Add(newCardControl);
                _player.Hand.Add(drawnCard);
                // Проверка на проигрыш
                if (_player.CalculateScore() > 21)
                {
                    MessageBox.Show("Проигрыш! Вы проиграли.");
                    _bank.Lose();
                    ResetGame();
                }
            }
        }

        // Обработчик нажатия кнопки "Пасс"
        private void StandButton_Click(object sender, RoutedEventArgs e)
        {
            if (isPlay)
            {
                // Показать скрытую карту дилера и добирать карты, пока у дилера не будет 17 очков и более
                _dealerCard2.SetHidden(false);
                while (_dealer.CalculateScore() < 17)
                {
                    var drawnCard = _deck.Draw();
                    var newCardControl = new CardControl();
                    newCardControl.SetCard(drawnCard);
                    DealerCards.Children.Add(newCardControl);
                    _dealer.Hand.Add(drawnCard);
                }

                int playerScore = _player.CalculateScore();
                int dealerScore = _dealer.CalculateScore();

                // Определение победителя и обновление состояния банка
                if (dealerScore > 21 || playerScore > dealerScore)
                {
                    MessageBox.Show("Вы победили!");
                    _bank.Win();
                }
                else if (playerScore < dealerScore)
                {
                    MessageBox.Show("Победил дилер.");
                    _bank.Lose();
                }
                else
                {
                    MessageBox.Show("Ничья.");
                    _bank.Push();
                }

                ResetGame(); // Сброс игры
            }
        }

        // Сброс игры
        private void ResetGame()
        {
            isPlay = false;
            elementHideDouble.Visibility = Visibility.Collapsed;
            CurrentBetsPanel.Visibility = Visibility.Visible;
            StartButton.Visibility = Visibility.Visible;
            _player.Hand.Clear();
            _dealer.Hand.Clear();
            PlayerCards.Children.Clear();
            DealerCards.Children.Clear();
            _bank.ResetBet();
            UpdateBankLabel();
            // Проверка на окончание денег или достижение большой суммы в банке
            if (_bank.BankAmount == 0)
            {
                MessageBox.Show("Вы проиграли все ваши деньги!");
                _bank = new Bank(1000);
                MainWindow mainWindow = new MainWindow(); // Создание нового главного окна
                mainWindow.Show(); // Показ нового главного окна
                this.Close(); // Закрытие текущего окна
            }
            else if (_bank.BankAmount > 200000)
            {
                MessageBox.Show("Поздравляем! Вы выиграли игру!");
                MainWindow mainWindow = new MainWindow(); // Создание нового главного окна
                mainWindow.Show(); // Показ нового главного окна
                this.Close(); // Закрытие текущего окна
            }
        }

        // Обновление текста ставки
        private void UpdateBetText()
        {
            BetTextBlock.Text = $"Текущая ставка: {_bank.CurrentBet}";
            Bets.Content = $"Текущая ставка: {_bank.CurrentBet}";
        }

        // Обновление метки банка
        private void UpdateBankLabel()
        {
            BankLabel.Content = $"Банк: {_bank.BankAmount}$";
        }

        // Обработчик добавления ставки
        private void AddBet_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && int.TryParse(button.Content.ToString(), out int amount))
            {
                _bank.PlaceBet(amount);
                UpdateBetText();
                UpdateBankLabel();
            }
        }

        // Обработчик удаления ставки
        private void RemoveAddBet_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && int.TryParse(button.Content.ToString(), out int amount))
            {
                if (_bank.CurrentBet > 0 && _bank.CurrentBet - Math.Abs(amount) >= 0)
                {
                    _bank.PlaceBet(-Math.Abs(amount));
                    UpdateBetText();
                    UpdateBankLabel();
                }
                else
                {
                    MessageBox.Show("Невозможно уменьшить ставку!");
                }
            }
        }

        // Обработчик кнопки "Сдаться"
        private void giveUp_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(); // Создание нового главного окна
            mainWindow.Show(); // Показ нового главного окна
            this.Close(); // Закрытие текущего окна
        }
    }
}
