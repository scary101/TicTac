using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private Button[,] buttons;
        private bool player;
        private Random random;

        public MainWindow()
        {
            InitializeComponent();

            buttons = new Button[3, 3]
            {
                { button1, button2, button3 },
                { button4, button5, button6 },
                { button7, button8, button9 }
            };

            random = new Random();
            player= true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Content = player ? "X" : "O";
            button.IsEnabled = false;
            CheckWinner();
            player = !player;
            if (!player)
            {
                Computer();
            }
        }

        private void CheckWinner()
        {
            if (CheckLine(0, 0, 0, 1, 0, 2)
                || CheckLine(1, 0, 1, 1, 1, 2)
                || CheckLine(2, 0, 2, 1, 2, 2)
                || CheckLine(0, 0, 1, 0, 2, 0)
                || CheckLine(0, 1, 1, 1, 2, 1)
                || CheckLine(0, 2, 1, 2, 2, 2)
                || CheckLine(0, 0, 1, 1, 2, 2)
                || CheckLine(0, 2, 1, 1, 2, 0))
            {
                MessageBox.Show((player ? "Вы выиграли!" : "Компьютер победил"));
                ResetGame();
            }
            else if (PlayingFieldFull())
            {
                MessageBox.Show("Ничья!");
                ResetGame();
            }
        }

        private bool CheckLine(int r1, int c1, int r2, int c2, int r3, int c3)
        {
            string content1 = buttons[r1, c1].Content.ToString();
            string content2 = buttons[r2, c2].Content.ToString();
            string content3 = buttons[r3, c3].Content.ToString();
            return !string.IsNullOrEmpty(content1) && content1 == content2 && content2 == content3;
        }
        private bool PlayingFieldFull()
        {
            foreach (Button button in buttons)
            {
                if (button.IsEnabled)
                {
                    return false;
                }
            }
            return true;
        }
        private void Computer()
        {
            List<Button> availableButtons = new List<Button>();

            foreach (Button button in buttons)
            {
                if (button.IsEnabled)
                {
                    availableButtons.Add(button);
                }
            }

            if (availableButtons.Count > 0)
            {
                int index = random.Next(0, availableButtons.Count);
                Button randomButton = availableButtons[index];
                randomButton.Content = "O";
                randomButton.IsEnabled = false;
                CheckWinner();
                player = true;
            }
        }

        private void ResetGame()
        {
            foreach (Button button in buttons)
            {
                button.Content = "";
                button.IsEnabled = true;
            }
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
            player = true;
        }
    }
}
    