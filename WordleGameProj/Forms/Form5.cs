using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WordleGameProj.Data;
using WordleGameProj.Models;

namespace WordleGameProj
{
    public partial class Form5 : Form
    {
        private readonly User currentUser;
        private readonly string correctWord;
        private int currentRow = 0;
        private const int MaxRows = 6, MaxCols = 5;

        public Form5(User user)
        {
            InitializeComponent();
            currentUser = user;
            Text = $"Wordle for {user.Email}";

            // Pick a word
            var words = new[] { "APPLE", "BRAVE", "CRISP", "DREAM", "EAGER" };
            correctWord = words[new Random().Next(words.Length)];

            BuildGrid();
            BuildKeyboard();
        }

        private void BuildGrid()
        {
            for (int r = 0; r < MaxRows; r++)
            {
                for (int c = 0; c < MaxCols; c++)
                {
                    var lbl = new Label
                    {
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Segoe UI", 18, FontStyle.Bold),
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.LightGray,
                        ForeColor = Color.White,
                        Name = $"cell_{r}_{c}"
                    };
                    tableGrid.Controls.Add(lbl, c, r);
                }
            }
        }

        private void BuildKeyboard()
        {
            string[] rows = { "QWERTYUIOP", "ASDFGHJKL", "ZXCVBNM←⏎" };
            for (int r = 0; r < rows.Length; r++)
            {
                for (int c = 0; c < rows[r].Length; c++)
                {
                    var key = rows[r][c].ToString();
                    var btn = new Button
                    {
                        Text = key,
                        Dock = DockStyle.Fill,
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                        BackColor = Color.LightGray,
                        ForeColor = Color.Black,
                        Name = $"key_{key}"
                    };
                    btn.Click += KeyboardButton_Click;
                    tableKeys.Controls.Add(btn, c, r);
                }
            }
        }

        private void KeyboardButton_Click(object sender, EventArgs e)
        {
            if (currentRow >= MaxRows) return;

            var btn = (Button)sender;
            string key = btn.Text;
            if (key == "←")
                DeleteLetter();
            else if (key == "⏎")
                SubmitGuess();
            else
                AddLetter(key[0]);
        }

        private void AddLetter(char letter)
        {
            for (int c = 0; c < MaxCols; c++)
            {
                var lbl = (Label)tableGrid.GetControlFromPosition(c, currentRow);
                if (string.IsNullOrEmpty(lbl.Text))
                {
                    lbl.Text = letter.ToString();
                    break;
                }
            }
        }

        private void DeleteLetter()
        {
            for (int c = MaxCols - 1; c >= 0; c--)
            {
                var lbl = (Label)tableGrid.GetControlFromPosition(c, currentRow);
                if (!string.IsNullOrEmpty(lbl.Text))
                {
                    lbl.Text = "";
                    break;
                }
            }
        }

        private void SubmitGuess()
        {
            // Read guess
            string guess = "";
            for (int c = 0; c < MaxCols; c++)
                guess += ((Label)tableGrid.GetControlFromPosition(c, currentRow)).Text;

            if (guess.Length < MaxCols) return;

            // Color row & keys
            for (int c = 0; c < MaxCols; c++)
            {
                var lbl = (Label)tableGrid.GetControlFromPosition(c, currentRow);
                char g = guess[c];
                if (g == correctWord[c])
                    lbl.BackColor = Color.Green;
                else if (correctWord.Contains(g))
                    lbl.BackColor = Color.Goldenrod;
                else
                    lbl.BackColor = Color.DarkGray;

                var keyBtn = tableKeys.Controls.Find($"key_{g}", true).FirstOrDefault() as Button;
                if (keyBtn != null)
                {
                    if (lbl.BackColor == Color.Green)
                        keyBtn.BackColor = Color.Green;
                    else if (lbl.BackColor == Color.Goldenrod && keyBtn.BackColor != Color.Green)
                        keyBtn.BackColor = Color.Goldenrod;
                    else if (lbl.BackColor == Color.DarkGray)
                        keyBtn.BackColor = Color.DarkGray;
                    keyBtn.ForeColor = Color.White;
                }
            }

            // Win or move on
            if (guess == correctWord || currentRow == MaxRows - 1)
                EndGame(guess == correctWord);
            else
                currentRow++;
        }

        private void EndGame(bool won)
        {
            using (var ctx = new WordleContext())
            {
                var userInDb = ctx.Users.Find(currentUser.UserId);
                userInDb.GamesPlayed++;
                if (won)
                {
                    userInDb.GamesWon++;
                    userInDb.CurrentStreak++;
                    if (userInDb.CurrentStreak > userInDb.MaxStreak)
                        userInDb.MaxStreak = userInDb.CurrentStreak;
                }
                else
                {
                    userInDb.CurrentStreak = 0;
                }

                int score = 10 - currentRow;
                ctx.Games.Add(new Game
                {
                    CorrectWord = correctWord,
                    AttemptsUsed = currentRow + 1,
                    PlayedOn = DateTime.Now,
                    Score = score,
                    UserId = userInDb.UserId
                });
                ctx.SaveChanges();
            }

            MessageBox.Show(
                won
                  ? $"You won in {currentRow + 1} tries!"
                  : $"You lost. The word was {correctWord}.",
                "Game Over",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            tableKeys.Enabled = false;
        }
    }
}
