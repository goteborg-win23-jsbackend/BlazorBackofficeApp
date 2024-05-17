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

    public async Task<bool> UpdateRole(string userId)
    {
        var userToUpdate = await _userManager.FindByIdAsync(userId);

        if(userToUpdate != null)
        {
            var existingRoles = await _userManager.GetRolesAsync(userToUpdate);

            string desiredRole = "Admin";

            if (!existingRoles.Contains(desiredRole))
            {
                string setRole = "Admin";
                foreach (var role in existingRoles)
                {
                    await _userManager.RemoveFromRoleAsync(userToUpdate, role);
                }

                
                await _userManager.AddToRoleAsync(userToUpdate, setRole);

                return true;
            }
            else
            {
                return false;
            }
        }
       
        else
        {
            return false;
        }
    }

    //var user = await _userManager.GetUserAsync(User);
    //            if (user != null)
    //            {
    //                user.FirstName = viewModel.BasicInfo.FirstName;
    //                user.LastName = viewModel.BasicInfo.LastName;
    //                user.Email = viewModel.BasicInfo.EmailAdress;
    //                user.PhoneNumber = viewModel.BasicInfo.Phone;
    //                user.Bio = viewModel.BasicInfo.Biography;

    //                var result = await _userManager.UpdateAsync(user);
    //                if (!result.Succeeded)
    //                {
    //                    ModelState.AddModelError("IncorrectValues", "Something went wrong! unable to save data.");
    //                    ViewData["ErrorMessage"] = "Something went wrong! Unable to save data.";
    //                }
    //                if (result.Succeeded)
    //                {
    //                    ViewData["SuccessMessage"] = "Data saved successfully";
    //                }
    //            }
}
