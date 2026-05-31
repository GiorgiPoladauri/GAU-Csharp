using Newtonsoft.Json.Linq;

namespace WordleAPI.WinForms;

public class MainForm : Form
{
    private Label _welcomeLabel = null!;
    private Button _newGameBtn = null!;
    private Button _statsBtn = null!;
    private Button _historyBtn = null!;
    private Button _logoutBtn = null!;
    private Panel _statsPanel = null!;
    private Label _statsContent = null!;

    public MainForm()
    {
        InitializeComponent();
        _welcomeLabel.Text = $"Welcome, {Program.Api.Email}!";
        _ = LoadStatsAsync();
    }

    private void InitializeComponent()
    {
        Text = "Wordle — Main Menu";
        Size = new Size(500, 420);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.FromArgb(18, 18, 19);

        _welcomeLabel = new Label { ForeColor = Color.White, Font = new Font("Segoe UI", 12), Location = new Point(20, 20), Size = new Size(460, 30) };

        _newGameBtn = new Button { Text = "▶  New Game", Location = new Point(20, 60), Size = new Size(200, 45), BackColor = Color.FromArgb(83, 141, 78), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11) };
        _statsBtn = new Button { Text = "📊  Refresh Stats", Location = new Point(240, 60), Size = new Size(200, 45), BackColor = Color.FromArgb(58, 58, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11) };
        _historyBtn = new Button { Text = "📋  Game History", Location = new Point(20, 115), Size = new Size(200, 45), BackColor = Color.FromArgb(58, 58, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11) };
        _logoutBtn = new Button { Text = "🚪  Logout", Location = new Point(240, 115), Size = new Size(200, 45), BackColor = Color.FromArgb(180, 60, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11) };

        _statsPanel = new Panel { Location = new Point(20, 175), Size = new Size(440, 200), BackColor = Color.FromArgb(30, 30, 30), BorderStyle = BorderStyle.FixedSingle };
        _statsContent = new Label { ForeColor = Color.White, Font = new Font("Consolas", 10), Location = new Point(10, 10), Size = new Size(420, 180), Text = "Loading stats..." };
        _statsPanel.Controls.Add(_statsContent);

        _newGameBtn.Click += (_, _) => { new GameForm().ShowDialog(); _ = LoadStatsAsync(); };
        _statsBtn.Click += async (_, _) => await LoadStatsAsync();
        _historyBtn.Click += HistoryBtn_Click;
        _logoutBtn.Click += (_, _) => Close();

        Controls.AddRange(new Control[] { _welcomeLabel, _newGameBtn, _statsBtn, _historyBtn, _logoutBtn, _statsPanel });
    }

    private async Task LoadStatsAsync()
    {
        var (ok, stats, _) = await Program.Api.GetStatisticsAsync();
        if (ok && stats != null)
        {
            _statsContent.Text =
                $"Games Played : {stats.gamesPlayed}\n" +
                $"Wins         : {stats.wins}\n" +
                $"Win Rate     : {stats.winRate}%\n" +
                $"Current Streak: {stats.currentStreak}\n" +
                $"Max Streak   : {stats.maxStreak}\n" +
                $"Total Points : {stats.totalPoints}";
        }
    }

    private async void HistoryBtn_Click(object? sender, EventArgs e)
    {
        var (ok, games, error) = await Program.Api.GetGamesAsync();
        if (!ok) { MessageBox.Show(error); return; }

        var form = new Form
        {
            Text = "Game History",
            Size = new Size(600, 400),
            BackColor = Color.FromArgb(18, 18, 19),
            StartPosition = FormStartPosition.CenterParent
        };
        var list = new ListView
        {
            Dock = DockStyle.Fill,
            View = View.Details,
            FullRowSelect = true,
            BackColor = Color.FromArgb(30, 30, 30),
            ForeColor = Color.White,
            GridLines = true
        };
        list.Columns.Add("Date", 150);
        list.Columns.Add("Status", 80);
        list.Columns.Add("Attempts", 70);
        list.Columns.Add("Won", 50);

        if (games is JArray arr)
        {
            foreach (var g in arr)
            {
                var item = new ListViewItem(((DateTime)g["startDate"]!).ToLocalTime().ToString("g"));
                item.SubItems.Add((string?)g["status"] ?? "");
                item.SubItems.Add((string?)g["attempts"]?.ToString() ?? "0");
                item.SubItems.Add((bool)g["isWin"]! ? "✅" : "❌");
                if ((bool)g["isWin"]!) item.ForeColor = Color.FromArgb(83, 141, 78);
                list.Items.Add(item);
            }
        }

        form.Controls.Add(list);
        form.ShowDialog(this);
    }
}