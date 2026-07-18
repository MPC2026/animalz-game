// Supabase service for AnimalZ game
// Using Supabase .NET SDK v1.2.0

using System.Text.Json;
using AnimalZ.Models;

namespace AnimalZ.Services;

public class SupabaseService : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly string _supabaseUrl;
    private readonly string _anonKey;
    private bool _disposed = false;

    public SupabaseService(string supabaseUrl, string anonKey)
    {
        _supabaseUrl = supabaseUrl.TrimEnd('/');
        _anonKey = anonKey;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("apikey", _anonKey);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_anonKey}");
    }

    // Authentication - using REST API directly for simplicity
    public async Task<User?> SignUpAsync(string email, string password, string displayName)
    {
        var signUpData = new
        {
            email = email,
            password = password,
            data = new { display_name = displayName }
        };

        var json = JsonSerializer.Serialize(signUpData);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_supabaseUrl}/auth/v1/signup", content);
        
        if (response.IsSuccessStatusCode)
        {
            var responseJson = await response.Content.ReadAsStringAsync();
            // Parse user from response - simplified for now
            return new User { Email = email, DisplayName = displayName };
        }
        return null;
    }

    public async Task<User?> SignInAsync(string email, string password)
    {
        var signInData = new { email = email, password = password };
        var json = JsonSerializer.Serialize(signInData);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_supabaseUrl}/auth/v1/token", content);
        
        if (response.IsSuccessStatusCode)
        {
            return new User { Email = email };
        }
        return null;
    }

    public async Task SignOutAsync()
    {
        await _httpClient.PostAsync($"{_supabaseUrl}/auth/v1/logout", null);
    }

    // Animal methods - using REST API
    public async Task<List<Animal>> GetZooAnimalsAsync()
    {
        var response = await _httpClient.GetAsync($"{_supabaseUrl}/rest/v1/animals?is_zoo_animal=eq.true");
        
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            // Parse JSON to Animal list - simplified for now
            return new List<Animal>();
        }
        return new List<Animal>();
    }

    public async Task<UserAnimal?> AdoptAnimalAsync(string userId, string animalId, string nickname)
    {
        var data = new
        {
            user_id = userId,
            animal_id = animalId,
            nickname = nickname
        };

        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        _httpClient.DefaultRequestHeaders.Add("Prefer", "return=representation");

        var response = await _httpClient.PostAsync($"{_supabaseUrl}/rest/v1/user_animals", content);
        
        if (response.IsSuccessStatusCode)
        {
            return new UserAnimal { UserId = userId, AnimalId = animalId, Nickname = nickname };
        }
        return null;
    }

    // Park methods
    public async Task<List<Park>> GetParksAsync()
    {
        var response = await _httpClient.GetAsync($"{_supabaseUrl}/rest/v1/parks");
        
        if (response.IsSuccessStatusCode)
        {
            return new List<Park>(); // Parse JSON to Park list
        }
        return new List<Park>();
    }

    public async Task<Park?> CreateParkAsync(string name, string description)
    {
        var data = new { name = name, description = description };
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        _httpClient.DefaultRequestHeaders.Add("Prefer", "return=representation");

        var response = await _httpClient.PostAsync($"{_supabaseUrl}/rest/v1/parks", content);
        
        if (response.IsSuccessStatusCode)
        {
            return new Park { Name = name, Description = description };
        }
        return null;
    }

    public async Task<bool> JoinParkAsync(string userId, string parkId)
    {
        var data = new { park_id = parkId, user_id = userId };
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        try
        {
            await _httpClient.PostAsync($"{_supabaseUrl}/rest/v1/park_users", content);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _httpClient?.Dispose();
            _disposed = true;
        }
    }
}