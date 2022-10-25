 
using MainService.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace MainServiceNew.Context
{
    public class AppDb_Context : DbContext
    {
        public AppDb_Context(DbContextOptions<AppDb_Context> options) : base(options)
        {

        }
        public DbSet<Items> Items { get; set; }
        public DbSet<Detalle> Detalles { get; set; }
    }
}