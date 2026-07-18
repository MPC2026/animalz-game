// Models for AnimalZ game

namespace AnimalZ.Models;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class Animal
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Species { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsZooAnimal { get; set; } = true;
    public Dictionary<string, string> DefaultColors { get; set; } = new();
}

public class UserAnimal
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = string.Empty;
    public string AnimalId { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public string Species { get; set; } = string.Empty;  // Added for display in UI
    public string ImageUrl { get; set; } = string.Empty;  // Added for display in UI
    public Dictionary<string, object>? Customization { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class Park
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MaxCapacity { get; set; } = 100;
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class ParkUser
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ParkId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
}