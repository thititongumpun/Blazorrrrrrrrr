using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlaceApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceApi.Db
{
    public class PlaceDbSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = new PlaceDbContext(serviceProvider.GetRequiredService<DbContextOptions<PlaceDbContext>>()))
            {
                if (context.Places.Any())
                {
                    return;
                }

                context.Places.AddRange(
                    new Place
                    {
                        Id = 1,
                        Name = "Central Rattanathibeth",
                        Location = "Nonthaburi",
                        About = "One of best department in thailand",
                        Reviews = 10,
                        ImageData = GetImage("central.jpg", "image/jpeg"),
                        LastUpdated = DateTime.Now
                    },
                    new Place
                    {
                        Id = 2,
                        Name = "TheMall Ngamwongwan",
                        Location = "Nonthaburi",
                        About = "One of best department in thailand",
                        Reviews = 10,
                        ImageData = GetImage("themall.png", "image/png"),
                        LastUpdated = DateTime.Now
                    },
                    new Place
                    {
                        Id = 3,
                        Name = "Home Pro",
                        Location = "Bkk",
                        About = "Shopping Mall",
                        Reviews = 10,
                        ImageData = GetImage("homepro.jpg", "image/jpg"),
                        LastUpdated = DateTime.Now
                    }
                );

                context.SaveChanges();
            }
        }

        private static string GetImage(string fileName, string fileType)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Db/Images", fileName);
            var imageBytes = File.ReadAllBytes(path);
            return $"data:{fileType};base64,{Convert.ToBase64String(imageBytes)}";
        }
    }
}
