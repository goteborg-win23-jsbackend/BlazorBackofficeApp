using BlazorBackofficeApp.Components.Pages;
using BlazorBackofficeApp.Data;
using BlazorBackofficeApp.Data.Entities;
using BlazorBackofficeApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace BlazorBackofficeApp.Services;

public class UserService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, HttpClient httpClient)
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly HttpClient _httpClient = httpClient;
    
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

    public async Task<List<SubscibersModel>> GetAllUsersAsync()
    {
        HttpClient client = new HttpClient();

        var url = "https://newsletterprovider-silicon-cl.azurewebsites.net/api/GetAllSubscribers?code=QvBLt9AWE0Qc2qxxer8Vv6rOpZ-x7Q0xDwfU4NqDC5yJAzFu_EC08g%3D%3D";

        var response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            var subscribers = JsonConvert.DeserializeObject<List<SubscibersModel>>(responseBody)!;
            return new List<SubscibersModel>(subscribers);
        }
        else
        {
            return null!;
        }
    }
    public async Task<bool> DeleteSubscriber(string email)
    {
        try
        {
            var emailObject = new EmailObject { Email = email };
            var response = await _httpClient.PostAsJsonAsync("https://newsletterprovider-silicon-cl.azurewebsites.net/api/Unsubscribe?code=bSWxf0Zb8QqeQRM0TVMybkHt5y5HgypnbwdEfNKXKP_XAzFu7F3Lzg%3D%3D", emailObject);


            if (response.IsSuccessStatusCode)
            {
                return true;
                
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
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
    public async Task<List<UserWithRoles>> GetAllUsersWithRolesAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var userWithRolesList = new List<UserWithRoles>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userWithRolesList.Add(new UserWithRoles
            {
                User = user,
                Roles = roles.ToList()
            });
        }

        return userWithRolesList;
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
