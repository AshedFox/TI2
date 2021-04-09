using RSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RSAWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RSACipher rsaCipher;

        public MainWindow()
        {
            InitializeComponent();

            rsaCipher = new RSACipher();

            KeyLengthTextBox.Text = rsaCipher.MaxKeysLength.ToString();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (uint.TryParse(KeyLengthTextBox.Text, out uint result))
            {
                if (result <= 2048 && result > 0)
                {
                    rsaCipher.SetupKeys((int)result);
                    MessageBox.Show("Длина ключа успешно изменена: новые ключи сгенерированы", "Сообщение",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Ограничение длины ключа - от 1 до 2048 бит", "Ошибка",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Некорректное значение длины ключа", "Ошибка", 
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
            KeyLengthTextBox.Text = rsaCipher.MaxKeysLength.ToString();
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(InputTextBox.Text))
            {
                string result = rsaCipher.Encrypt(InputTextBox.Text);

                ResultTextBox.Text = result;
            }
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(InputTextBox.Text))
            {
                var result = rsaCipher.Decrypt(InputTextBox.Text);

                if (result.resultCode == 0)
                {
                    ResultTextBox.Text = result.output;
                }
                else
                {
                    MessageBox.Show("Не удалось расшифровать текст", "Ошибка",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
