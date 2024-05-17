using BlazorBackofficeApp.Data.Entities;
using BlazorBackofficeApp.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorBackofficeApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<SubscribersEntity> Subscribers { get; set; }
    }

    
}
