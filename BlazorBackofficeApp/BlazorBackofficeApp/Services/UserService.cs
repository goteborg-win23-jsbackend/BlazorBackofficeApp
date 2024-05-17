using BlazorBackofficeApp.Data;
using BlazorBackofficeApp.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorBackofficeApp.Services;

public class UserService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<bool> DeleteUser(string userId)
    {
        var userToDelete = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        
        if (userToDelete != null)
        {
            _context.Remove(userToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<List<SubscribersEntity>> GetAllSubscribersAsync()
    {
        return await _context.Subscribers.ToListAsync();
    }

}
