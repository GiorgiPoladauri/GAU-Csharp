using Newtonsoft.Json.Linq;

namespace WordleAPI.WinForms;

public class GameForm : Form
{
    private Guid _gameId;
    private int _currentRow = 0;
    private const int MaxRows = 6;
    private const int WordLen = 5;

    private readonly Label[,] _cells = new Label[MaxRows, WordLen];
    private TextBox _inputBox = null!;
    private Button _guessBtn = null!;
    private Label _messageLabel = null!;

    // Colors matching Wordle
    private static readonly Color BgColor = Color.FromArgb(18, 18, 19);
    private static readonly Color EmptyColor = Color.FromArgb(58, 58, 60);
    private static readonly Color CorrectColor = Color.FromArgb(83, 141, 78);
    private static readonly Color PresentColor = Color.FromArgb(181, 159, 59);
    private static readonly Color AbsentColor = Color.FromArgb(58, 58, 60);

    public GameForm()
    {
        InitializeComponent();
        _ = StartGameAsync();
    }

    private void InitializeComponent()
    {
        Text = "Wordle";
        Size = new Size(400, 600);
        StartPosition = FormStartPosition.CenterParent;
        BackColor = BgColor;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        var title = new Label { Text = "WORDLE", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(130, 15) };

        // Build 6×5 grid of letter cells
        int cellSize = 52, gap = 6, startX = 55, startY = 60;
        for (int r = 0; r < MaxRows; r++)
            for (int c = 0; c < WordLen; c++)
            {
                var lbl = new Label
                {
                    Size = new Size(cellSize, cellSize),
                    Location = new Point(startX + c * (cellSize + gap), startY + r * (cellSize + gap)),
                    BackColor = EmptyColor,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 18, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BorderStyle = BorderStyle.None
                };
                _cells[r, c] = lbl;
                Controls.Add(lbl);
            }

        int inputY = startY + MaxRows * (cellSize + gap) + 15;
        _inputBox = new TextBox
        {
            Location = new Point(55, inputY),
            Width = 200,
            MaxLength = 5,
            BackColor = Color.FromArgb(40, 40, 40),
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 14),
            CharacterCasing = CharacterCasing.Upper,
            BorderStyle = BorderStyle.FixedSingle
        };
        _guessBtn = new Button
        {
            Text = "Guess",
            Location = new Point(265, inputY),
            Size = new Size(80, _inputBox.Height + 4),
            BackColor = CorrectColor,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 11)
        };
        _messageLabel = new Label
        {
            Location = new Point(20, inputY + 45),
            Size = new Size(340, 40),
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 10),
            TextAlign = ContentAlignment.MiddleCenter
        };

        _guessBtn.Click += GuessBtn_Click;
        _inputBox.KeyDown += (_, e) => { if (e.KeyCode == Keys.Enter) GuessBtn_Click(null, EventArgs.Empty); };

        Controls.AddRange(new Control[] { title, _inputBox, _guessBtn, _messageLabel });
    }

    private async Task StartGameAsync()
    {
        _messageLabel.Text = "Starting game...";
        var (ok, gameId, error) = await Program.Api.StartGameAsync();
        if (ok)
        {
            _gameId = gameId;
            _messageLabel.Text = "Guess the 5-letter word!";
            _inputBox.Focus();
        }
        else
        {
            _messageLabel.Text = error ?? "Failed to start";
            _guessBtn.Enabled = false;
        }
    }

    private async void GuessBtn_Click(object? sender, EventArgs e)
    {
        var word = _inputBox.Text.Trim().ToUpper();
        if (word.Length != 5) { _messageLabel.Text = "Word must be 5 letters!"; return; }

        _guessBtn.Enabled = false;
        var (ok, result, error) = await Program.Api.GuessAsync(_gameId, word);

        if (!ok) { _messageLabel.Text = error; _guessBtn.Enabled = true; return; }

        // Fill in the grid row
        var letterResults = result!.letterResults as JArray;
        if (letterResults != null)
        {
            for (int c = 0; c < WordLen; c++)
            {
                var lr = letterResults[c];
                _cells[_currentRow, c].Text = ((string)lr["letter"]!).ToUpper();
                _cells[_currentRow, c].BackColor = (string)lr["statusText"]! switch
                {
                    "correct" => CorrectColor,
                    "present" => PresentColor,
                    _ => AbsentColor
                };
            }
        }

        _currentRow++;
        _messageLabel.Text = (string?)result!.message ?? "";
        _inputBox.Clear();

        bool gameOver = (string?)result!.gameStatusText == "Won" || (string?)result!.gameStatusText == "Lost";
        if (gameOver)
        {
            _guessBtn.Enabled = false;
            _inputBox.Enabled = false;
            await Task.Delay(200);
            MessageBox.Show((string?)result!.message, "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            _guessBtn.Enabled = true;
            _inputBox.Focus();
        }
    }
}