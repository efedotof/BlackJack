using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfApp1.Models;

namespace WpfApp1
{
    public partial class CardControl : UserControl
    {
        public CardControl()
        {
            InitializeComponent(); // Инициализация компонентов управления
        }

        // Метод для установки отображаемой карты
        public void SetCard(Card card)
        {
            CardImage.Source = card.GetImage(); // Установка изображения карты
            HiddenCardCover.Visibility = System.Windows.Visibility.Collapsed; // Скрытие обложки, скрывающей карту
        }

        // Метод для установки состояния скрытия карты
        public void SetHidden(bool isHidden)
        {
            HiddenCardCover.Visibility = isHidden ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed; // Установка видимости обложки в зависимости от значения isHidden
        }
    }
}
