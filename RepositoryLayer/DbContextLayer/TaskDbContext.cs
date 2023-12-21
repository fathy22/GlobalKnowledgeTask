using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.DbContextLayer
{
    public class TaskDbContext:DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) 
        { 
        }
        DbSet<ProfileImage> ProfileImages { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Blog> Blogs { get; set; }
        DbSet<Comment> Comments { get; set; }
    }
}
