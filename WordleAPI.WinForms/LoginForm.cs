namespace WordleAPI.WinForms;

public class LoginForm : Form
{
    private TextBox _emailBox = null!;
    private TextBox _passwordBox = null!;
    private Button _loginBtn = null!;
    private Button _registerBtn = null!;
    private Label _statusLabel = null!;

    public LoginForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text = "Wordle — Login";
        Size = new Size(400, 280);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        BackColor = Color.FromArgb(18, 18, 19);

        var title = new Label
        {
            Text = "WORDLE",
            Font = new Font("Segoe UI", 22, FontStyle.Bold),
            ForeColor = Color.White,
            AutoSize = true,
            Location = new Point(140, 20)
        };

        var emailLabel = new Label { Text = "Email:", ForeColor = Color.White, Location = new Point(40, 80), AutoSize = true };
        _emailBox = new TextBox { Location = new Point(40, 100), Width = 300, BackColor = Color.FromArgb(40, 40, 40), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle };

        var passLabel = new Label { Text = "Password:", ForeColor = Color.White, Location = new Point(40, 130), AutoSize = true };
        _passwordBox = new TextBox { Location = new Point(40, 150), Width = 300, PasswordChar = '●', BackColor = Color.FromArgb(40, 40, 40), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle };

        _loginBtn = new Button { Text = "Login", Location = new Point(40, 190), Width = 140, BackColor = Color.FromArgb(83, 141, 78), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        _registerBtn = new Button { Text = "Register", Location = new Point(200, 190), Width = 140, BackColor = Color.FromArgb(58, 58, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };

        _statusLabel = new Label { ForeColor = Color.FromArgb(220, 80, 80), Location = new Point(40, 225), Width = 300, AutoSize = false };

        _loginBtn.Click += LoginBtn_Click;
        _registerBtn.Click += RegisterBtn_Click;

        Controls.AddRange(new Control[] { title, emailLabel, _emailBox, passLabel, _passwordBox, _loginBtn, _registerBtn, _statusLabel });
    }

    private async void LoginBtn_Click(object? sender, EventArgs e)
    {
        _statusLabel.Text = "Logging in...";
        var (ok, token, email, userId, error) = await Program.Api.LoginAsync(_emailBox.Text, _passwordBox.Text);
        if (ok)
        {
            Program.Api.SetToken(token!, email!, userId);
            Hide();
            new MainForm().ShowDialog();
            Show();
            _statusLabel.Text = "";
        }
        else _statusLabel.Text = error ?? "Login failed";
    }

    private async void RegisterBtn_Click(object? sender, EventArgs e)
    {
        _statusLabel.Text = "Registering...";
        var (ok, token, email, userId, error) = await Program.Api.RegisterAsync(_emailBox.Text, _passwordBox.Text);
        if (ok)
        {
            Program.Api.SetToken(token!, email!, userId);
            Hide();
            new MainForm().ShowDialog();
            Show();
            _statusLabel.Text = "";
        }
        else _statusLabel.Text = error ?? "Registration failed";
    }
}