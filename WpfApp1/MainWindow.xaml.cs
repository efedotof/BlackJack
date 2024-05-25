using System.Windows;

namespace WpfApp1
{
    // Главное окно приложения
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик события нажатия кнопки "Начать игру"
        private void start_Click(object sender, RoutedEventArgs e)
        {
            // Создание нового окна игры
            GameWindows gameWin = new GameWindows();
            // Отображение окна игры
            gameWin.Show();
            // Закрытие текущего окна
            this.Close();
        }

        // Обработчик события нажатия кнопки "Информация"
        private void info_Click(object sender, RoutedEventArgs e)
        {
            // Создание нового окна информации
            InfoWindow infoWindow = new InfoWindow();
            // Показ модального окна с информацией
            infoWindow.ShowDialog();
        }

        // Обработчик события нажатия кнопки "Выход"
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            // Закрытие текущего окна
            this.Close();
        }
    }
}
