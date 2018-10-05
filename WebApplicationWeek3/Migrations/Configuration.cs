namespace WebApplicationWeek3.Migrations
{
    using ClubDomain.Classes.ClubModels;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApplicationWeek3.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplicationWeek3.Models.ApplicationDbContext>
    {
        public Configuration()
            
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApplicationWeek3.Models.ApplicationDbContext context)
        {
            var manager =
                new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(context));

            var roleManager =
                new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(context));

            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "Admin" }
                );
            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "ClubAdmin" }
                );
            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "member" }
                );

            PasswordHasher ps = new PasswordHasher();

            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "powell.paul@itsligo.ie",
                    EmailConfirmed = true,
                    JoinDate = DateTime.Now,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = "Paul",
                    SurName = "Powell",
                    PasswordHash = ps.HashPassword("Ppowell$1")
                });

            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    UserName = "ITS FC Admin",
                    Email = "radp2016@outlook.com",
                    EmailConfirmed = true,
                    JoinDate = DateTime.Now,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = "Rad",
                    SurName = "Paulner",
                    PasswordHash = ps.HashPassword("radP2016$1")
                });
            context.SaveChanges();

            context.Clubs.AddOrUpdate(c => c.ClubId,
                new Club
                {
                    ClubId = 1,
                    ClubName = "Trad Music Club",
                    CreationDate = new DateTime(2008, 08, 08),
                    adminID = 1,
                }
                );

            context.Clubs.AddOrUpdate(c => c.ClubId,
                new Club
                {
                    ClubId = 2,
                    ClubName = "Badminton Club",
                    CreationDate = new DateTime(2010, 10, 10),
                    adminID = 1,
                    //clubMembers = new ICollection<Member>
                }
                );

            context.Clubs.AddOrUpdate(c => c.ClubId,
                new Club
                {
                    ClubId = 3,
                    ClubName = "International Club",
                    CreationDate = new DateTime(2012, 12, 12),
                    adminID = 1,
                }
                );

            context.Members.AddOrUpdate(m => m.MemberID,
                new Member
                {
                    MemberID = 1,
                    StudentID="S00181111",
                    approved = true,
                }
                );

            context.Members.AddOrUpdate(m => m.MemberID,
                new Member
                {
                    MemberID = 2,
                    StudentID = "S00182222",
                    approved = true,
                }
                );

            context.ClubEvents.AddOrUpdate(e => e.EventID,
                new ClubEvent
                {
                    EventID = 1,
                    Location = "IT",
                }
                );

            context.ClubEvents.AddOrUpdate(e => e.EventID,
                new ClubEvent
                {
                    EventID = 2,
                    Location = "IT",
                }
                );

            context.ClubEvents.AddOrUpdate(e => e.EventID,
                new ClubEvent
                {
                    EventID = 3,
                    Location = "Arena",
                }
                );

            context.ClubEvents.AddOrUpdate(e => e.EventID,
                new ClubEvent
                {
                    EventID = 4,
                    Location = "Arena",
                }
                );

            ApplicationUser admin = manager.FindByEmail("powell.paul@itsligo.ie");
            if (admin != null)
            {
                manager.AddToRoles(admin.Id, new string[] { "Admin", "member", "ClubAdmin" });
            }

        }
    }
}
