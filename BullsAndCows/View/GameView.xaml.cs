using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BullsAndCows.Model;

namespace BullsAndCows.View
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class GameView : Window
    {
        bool debugInfo = true;

        bool isStart = false;
        long Step = 0;
        long Code = 0;
        static long Game = 0;
        long TryCode = 0;
        static History history = new History();
        static Steps steps = new Steps();

        public GameView()
        {
            InitializeComponent();
            GenerateNewGame();


        }
        public void GenerateNewGame()
        {
            Random rnd = new Random();
            byte[] code = new byte[4];
            code[0] = (byte)rnd.Next(1, 9);
            do
            {
                code[1] = (byte)rnd.Next(1, 9);
            } while (code[1]==code[0]);
            do
            {
                code[2] = (byte)rnd.Next(1, 9);
            } while (code[2] == code[0] || code[2] == code[1]);
            do
            {
                code[3] = (byte)rnd.Next(1, 9);
            } while (code[3] == code[0] || code[3] == code[1] || code[3] == code[2]);
            Code = ((code[0] * 1000)+(code[1] * 100) +(code[2] * 10) + code[3]);
            Game++;
            Step = 0;
            long TryCode = 0;
            if(debugInfo == true)
            {
                Title = string.Format("Игра BullAndCow, debug!!! code = {0}", Code);
            }
            else
            {
                Title = "Игра BullAndCow";
            }
            
            
        }

        public void TryStep()
        {
            
            if (!isCodeCorrect(TextBoxEnter.Text))
            {
                MessageBox.Show("Неккоректная числовая последовательность, для примера введена допустимая числовая последовательность. О правилах игры вы можете прочитать в меню кликнув на кнопку [О программе]", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                TextBoxEnter.Text = "1234";
                
            }
            else
            {
                Step++;
                TryCode = long.Parse(TextBoxEnter.Text);
                string tryequals = GetEquals(TextBoxEnter.Text);
                DataGridSteps.Items.Clear();
                if (tryequals != "Non")
                {
                    steps.AddStepsItem(new StepsItem(Step, TryCode, tryequals));
                    LabelResult.Content = string.Format("Результат по {0} - {1}, Пробуйте еще", TryCode, tryequals);
                    foreach (StepsItem si in steps.GetStepsItems)
                    {

                        DataGridSteps.Items.Add(si);
                    }
                }
            }
            
        }

        public void Restart(bool isFinish)
        {
            history.AddHistoryItem(new HistoryItem(Game, Code,Step, isFinish));
            DataGridHistory.Items.Clear();
            foreach (HistoryItem hi in history.GetHistoryItems)
            {
                
                DataGridHistory.Items.Add(hi);
            }
            steps.Clear();
            GenerateNewGame();
        }

        private bool isCodeCorrect(string enterString)
        {
            //TODO: Добавить пасхалки с числами в проверку
            if (enterString.Length!=4)
            {
                return false;
            }
            long enterCode = long.Parse(enterString);
            byte[] codeitems = new byte[4];
            for(int i = 3; i >= 0; i--)
            {
                codeitems[i] = Convert.ToByte(enterCode % 10);
                enterCode = enterCode / 10;
            }
            for(int i = 3; i >= 0; i--)
            {
                for(int j = 3; j >= 0; j--)
                {
                    if (i != j)
                    {
                        if (codeitems[i] == codeitems[j])
                        {
                            return false;
                        }
                    }
                }

            }




            return true;
        }

        private string GetEquals(string tryCode)
        {
            long bulls = 0;
            long cows = 0;

            long enterCode = long.Parse(tryCode); //локальный код
            byte[] codeItems = new byte[4];
            for (int i = 3; i >= 0; i--)
            {
                codeItems[i] = Convert.ToByte(enterCode % 10);
                enterCode = enterCode / 10;
            }

            long globalCode = Code;
            byte[] globalCodeItems = new byte[4];
            for (int i = 3; i >= 0; i--)
            {
                globalCodeItems[i] = Convert.ToByte(globalCode % 10);
                globalCode = globalCode / 10;
            }

            //проверка на быков
            for (int i = 0;i<4;i++)
            {
                if (codeItems[i] == globalCodeItems[i])
                {
                    bulls++;
                }
            }

            //проверка на коров

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (codeItems[i] == globalCodeItems[j])
                    {
                        cows++;
                    }
                }
            }
            cows -= bulls;
            if(bulls == 4)
            {
                MessageBox.Show("Поздравляем вы победили!", "Победа", MessageBoxButton.OK, MessageBoxImage.Information);
                Restart(true);
                isStart = false;
                ButtonRestart.Content = "Снова";
                ButtonTry.IsEnabled = false;
                TextBoxEnter.IsEnabled = false;
                return "Non";
                

            }
            return string.Format("Б{0}К{1}", bulls, cows);
        }

        

        private void ButtonTry_Click(object sender, RoutedEventArgs e)
        {
            TryStep();
        }

        private void ButtonRestart_Click(object sender, RoutedEventArgs e)
        {
            
            if (isStart == false)
            {
                ButtonRestart.Content = "Рестарт";
                isStart = true;
                ButtonTry.IsEnabled = true;
                TextBoxEnter.IsEnabled = true;
            }
            else
            {
                Restart(false);
            }
            
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = new MainWindow();
            if(Step != 0)
            {
                Restart(false);
            }
            w.Show();
            this.Close();
        }

        private void TextBoxEnter_GotFocus(object sender, RoutedEventArgs e)
        {
            if(TextBoxEnter.Text == "Введите число")
            {
                TextBoxEnter.Text = "";
                TextBoxEnter.Foreground = Brushes.Black;
            }
            


        }

        private void TextBoxEnter_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxEnter.Text == "")
            {
                TextBoxEnter.Text = "Введите число";
                TextBoxEnter.Foreground = Brushes.Gray;
            }
        }

        private void TextBoxEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                ButtonTry_Click(sender, e);
            }
        }
    }
}
