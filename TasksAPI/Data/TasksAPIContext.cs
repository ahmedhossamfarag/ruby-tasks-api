using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TasksAPI.Models;

namespace TasksAPI.Data
{
    public class TasksAPIContext : DbContext
    {
        public TasksAPIContext (DbContextOptions<TasksAPIContext> options)
            : base(options)
        {
        }

        public DbSet<TasksAPI.Models.User> User { get; set; } = default!;
        public DbSet<TasksAPI.Models.TaskNote> TaskNote { get; set; } = default!;
    }
}
