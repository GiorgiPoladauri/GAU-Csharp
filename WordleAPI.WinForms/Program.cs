namespace WordleAPI.WinForms;

internal static class Program
{
    public static ApiClient Api { get; } = new ApiClient();

    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new LoginForm());
    }
}