using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAppProjectMJB.Models;

namespace WebAppProjectMJB.Data
{
    public class WebAppProjectMJBContext : DbContext
    {
        public WebAppProjectMJBContext (DbContextOptions<WebAppProjectMJBContext> options)
            : base(options)
        {
        }

        public DbSet<WebAppProjectMJB.Models.Game> Game { get; set; }

        public DbSet<WebAppProjectMJB.Models.GameConsole> GameConsole { get; set; }

        public DbSet<WebAppProjectMJB.Models.Accessories> Accessories { get; set; }

        public DbSet<WebAppProjectMJB.Models.User> User { get; set; }

        public DbSet<WebAppProjectMJB.Models.FeedbackMessage> FeedbackMessage { get; set; }
    }
}
