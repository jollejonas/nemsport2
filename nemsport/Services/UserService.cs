using Microsoft.EntityFrameworkCore;
using nemsport.Models.UserModels; // Adjust the namespace to where your User model is located
using nemsport.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using nemsport.DTOs.UserDTOs;

public class UserService
{
    private readonly nemsportContext _context;

    public UserService(nemsportContext context)
    {
        _context = context;
    }

    public async Task<bool> RegisterUserAsync(RegisterDTO registerDto)
    {
        // Check if user already exists
        if (await _context.User.AnyAsync(u => u.Email == registerDto.Email))
        {
            // User already exists
            return false;
        }

        // Hash the password
        PasswordHasher.CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        // Create new user object
        var user = new User
        {
            FirstName = registerDto.FirstName, // Can be null if not provided
            LastName = registerDto.LastName, // Can be null if not provided
            Email = registerDto.Email,
            PhoneNumber = registerDto.PhoneNumber, // Can be null if not provided
            City = registerDto.City, // Can be null if not provided
            JoinDate = registerDto.JoinDate,
            Role = registerDto.Role,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            // Set additional properties as needed
        };

        // Add and save user in the database
        _context.User.Add(user);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> VerifyUserPasswordAsync(string email, string password)
    {
        var user = await _context.User
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);

        // Check if user exists
        if (user == null)
        {
            return false; // User not found
        }

        // Verify the password
        return PasswordHasher.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
    }

}