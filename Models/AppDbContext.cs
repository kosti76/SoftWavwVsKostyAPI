using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SoftWavwVsKostyAPI.Models
{
    public class AppDbContext:DbContext
    {
        public  DbSet<Year> Year { get; set; }
        public  DbSet<Category> Category { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        { 
        
        }
        public AppDbContext() 
        {

        }



    }
    public class Year
    {
        [Key]
        public Int16 code { get; set; }
        public Int16 year { get; set; }
        public Int16 circle1 { get; set; }
        public Int16 circle2 { get; set; }
        //public List<Category> categories { get; set; }
    }

    public class Category
    {
        [Key]
        public Int16 code { get; set; }
        public Int16 year { get; set; }
        public string title { get; set; }
        public Int16 value { get; set; }
        public Int16 maxvalue { get; set; }
    }
}
