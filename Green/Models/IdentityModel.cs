using Green.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Green.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        //[Required]
        //[Display(Name = "Date of Birth")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Column(TypeName = "datetime2")]
        //public DateTime BirthDate { get; set; }

        //[Display(Name = "Block User")]
        public Boolean Blocked { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

      //  public System.Data.Entity.DbSet<Green.Models.Patient> Patients { get; set; }
        public DbSet<AdministrationModel> Administrations { get; set; }
        public System.Data.Entity.DbSet<Green.Models.ContactUs> ContactUs { get; set; }
        public System.Data.Entity.DbSet<Green.Models.forms> forms { get; set; }
        public System.Data.Entity.DbSet<Green.Models.Vaccancie> Vaccancies { get; set; }
        public System.Data.Entity.DbSet<Green.Models.Statistic> Statistics { get; set; }
        public System.Data.Entity.DbSet<Green.Models.AnnualR> AnnualRs { get; set; }
        public System.Data.Entity.DbSet<Green.Models.AwardNotice> AwardNotices { get; set; }
        public System.Data.Entity.DbSet<Green.Models.Bidders> Bidders { get; set; }
        public System.Data.Entity.DbSet<Green.Models.Gallery> Galleries { get; set; }

        public System.Data.Entity.DbSet<Green.Models.Telbook> Telbooks { get; set; }
    }
}