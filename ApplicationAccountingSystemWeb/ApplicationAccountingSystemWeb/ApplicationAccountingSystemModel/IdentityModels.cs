using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ApplicationAccountingSystemModel;

namespace ApplicationAccountingSystemWeb.Models
{
    // Чтобы добавить данные профиля для пользователя, можно добавить дополнительные свойства в класс ApplicationUser. Дополнительные сведения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public bool IsExecutor { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Requisition> Requisitions { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public ApplicationContext()
            : base("ApplicationContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}