using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Library.Models;
using RecipeHub.Library.Repositories;

namespace RecipeHub.Library.Services;

public class UserService
{
    private readonly IRepository<User> _users;

    public UserService(IRepository<User> users)
    {
        _users = users;
    }

    public async Task<int> RegisterAsync(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username darf nicht leer sein.");
        if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Password darf nicht leer sein.");

        var all = await _users.GetAllAsync();
        if (all.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Username ist bereits vergeben.");

        var user = new User { Username = username.Trim(), Password = password };
        await _users.AddAsync(user);
        return user.Id;
    }

    // Einfache Authentifizierungsmethode
    public async Task<LoginResult> AuthenticateAsync(string username, string password)
    {
        var all = await _users.GetAllAsync();
        var user = all.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

        if (user is null) return LoginResult.UserNotFound;
        if (user.Password != password) return LoginResult.WrongPassword;
        return LoginResult.Success;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _users.GetAllAsync();
    }

    public async Task<bool> ExistsAsync(int userId)
    {
        var users = await _users.GetAllAsync();
        return users.Any(u => u.Id == userId);
    }

    public async Task<bool> ValidateLoginAsync(string username, string password)
    {
        var user = (await _users.GetAllAsync())
            .FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        return user is not null && user.Password == password;
    }
}

