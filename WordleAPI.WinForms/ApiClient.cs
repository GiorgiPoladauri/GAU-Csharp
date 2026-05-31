using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace WordleAPI.WinForms;

public class ApiClient
{
    private readonly HttpClient _http;
    private const string BaseUrl = "https://localhost:53113";

    public string? Token { get; private set; }
    public string? Email { get; private set; }
    public Guid UserId { get; private set; }

    public ApiClient()
    {
        _http = new HttpClient(new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (_, _, _, _) => true // allow self-signed cert in dev
        });
        _http.BaseAddress = new Uri(BaseUrl);
    }

    public void SetToken(string token, string email, Guid userId)
    {
        Token = token;
        Email = email;
        UserId = userId;
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

    private StringContent Json(object obj) =>
        new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

    public async Task<(bool ok, string? token, string? email, Guid userId, string? error)> RegisterAsync(string email, string password)
    {
        var res = await _http.PostAsync("/api/account/register", Json(new { email, password }));
        var body = await res.Content.ReadAsStringAsync();
        if (res.IsSuccessStatusCode)
        {
            dynamic d = JsonConvert.DeserializeObject<dynamic>(body)!;
            return (true, (string)d.token, (string)d.email, Guid.Parse((string)d.userId), null);
        }
        dynamic err = JsonConvert.DeserializeObject<dynamic>(body)!;
        return (false, null, null, Guid.Empty, (string?)err?.error ?? "Registration failed");
    }

    public async Task<(bool ok, string? token, string? email, Guid userId, string? error)> LoginAsync(string email, string password)
    {
        var res = await _http.PostAsync("/api/account/login", Json(new { email, password }));
        var body = await res.Content.ReadAsStringAsync();
        if (res.IsSuccessStatusCode)
        {
            dynamic d = JsonConvert.DeserializeObject<dynamic>(body)!;
            return (true, (string)d.token, (string)d.email, Guid.Parse((string)d.userId), null);
        }
        dynamic err = JsonConvert.DeserializeObject<dynamic>(body)!;
        return (false, null, null, Guid.Empty, (string?)err?.error ?? "Login failed");
    }

    public async Task<(bool ok, Guid gameId, string? error)> StartGameAsync()
    {
        var res = await _http.PostAsync("/api/games/start", null);
        var body = await res.Content.ReadAsStringAsync();
        if (res.IsSuccessStatusCode)
        {
            dynamic d = JsonConvert.DeserializeObject<dynamic>(body)!;
            return (true, Guid.Parse((string)d.gameId), null);
        }
        return (false, Guid.Empty, "Could not start game");
    }

    public async Task<(bool ok, dynamic? result, string? error)> GuessAsync(Guid gameId, string word)
    {
        var res = await _http.PostAsync("/api/games/guess", Json(new { gameId, word }));
        var body = await res.Content.ReadAsStringAsync();
        if (res.IsSuccessStatusCode)
            return (true, JsonConvert.DeserializeObject<dynamic>(body), null);
        dynamic err = JsonConvert.DeserializeObject<dynamic>(body)!;
        return (false, null, (string?)err?.error ?? "Guess failed");
    }

    public async Task<(bool ok, dynamic? stats, string? error)> GetStatisticsAsync()
    {
        var res = await _http.GetAsync("/api/statistics");
        var body = await res.Content.ReadAsStringAsync();
        if (res.IsSuccessStatusCode)
            return (true, JsonConvert.DeserializeObject<dynamic>(body), null);
        return (false, null, "Could not load statistics");
    }

    public async Task<(bool ok, dynamic? games, string? error)> GetGamesAsync()
    {
        var res = await _http.GetAsync("/api/games");
        var body = await res.Content.ReadAsStringAsync();
        if (res.IsSuccessStatusCode)
            return (true, JsonConvert.DeserializeObject<dynamic>(body), null);
        return (false, null, "Could not load games");
    }
}