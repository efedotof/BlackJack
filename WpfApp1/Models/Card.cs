using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace WpfApp1.Models
{
    // Класс, представляющий карту
    public class Card
    {
        // Перечисление для рангов карт
        public enum Rank
        {
            Ace = 1,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King
        }

        // Перечисление для мастей карт
        public enum Suit
        {
            Hearts,
            Diamonds,
            Clubs,
            Spades
        }

        // Свойства для масти и ранга карты
        public Suit CardSuit { get; private set; }
        public Rank CardRank { get; private set; }

        // Конструктор, инициализирующий карту с заданным рангом и мастью
        public Card(Rank rank, Suit suit)
        {
            CardRank = rank;
            CardSuit = suit;
        }

        // Метод для получения значения карты
        public int GetValue()
        {
            if (CardRank == Rank.Ace) return 11; // Туз стоит 11 очков
            if (CardRank == Rank.King || CardRank == Rank.Queen || CardRank == Rank.Jack) return 10; // Король, дама и валет стоят 10 очков
            return (int)CardRank; // Остальные карты имеют значение, соответствующее их рангу
        }

        // Метод для получения изображения карты
        public BitmapImage GetImage()
        {
            BitmapImage image = new BitmapImage();
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string imagePath;

            // Преобразование ранга карты в строку
            string rankString = CardRank <= Rank.Ten ? ((int)CardRank).ToString() : CardRank.ToString().ToLower();
            // Формирование пути к изображению
            imagePath = Path.Combine(basePath, "images", "cards", $"{rankString}_of_{CardSuit.ToString().ToLower()}.png");

            // Инициализация изображения
            image.BeginInit();
            image.UriSource = new Uri(imagePath, UriKind.Absolute);
            image.EndInit();

            return image;
        }

        // Переопределение метода ToString для удобного отображения карты
        public override string ToString()
        {
            return $"{CardRank} of {CardSuit}";
        }
    }
}
