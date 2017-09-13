using EFcoreApp.Context;
using System;
using Microsoft.Extensions.DependencyInjection;
using EFcoreApp.Models;
using EFcoreApp.MapsModels;

namespace EFcoreApp.StaticClasses
{
    public static class SampleData
    {
        public static void Init(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<MobileContext>();
           
           
            context.Places.AddRange(
                    new Place { Name="Красноярск",Title="Красноярск во всей красе", Description="Город расположен бла бла бла", lat= 56.001089,lng= 92.874481 },
                    new Place { Name = "Амга", Title = "немного якутии", Description = "Амга, Респ. Саха (Якутия), 678606", lat = 60.893764, lng = 131.970505 },
                     new Place { Name = "Якутск", Title = "еще немного якутии", Description = "Якутск, Респ. Саха (Якутия), 6770", lat = 62.032570, lng = 129.675957 }

                );
            context.SaveChanges();
        }
    }
}
