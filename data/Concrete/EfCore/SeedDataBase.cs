using System.Linq;
using entity;
using Microsoft.EntityFrameworkCore;

namespace data.Concrete.EfCore
{
    public static class SeedDataBase
    {
        public static void Seed()
        {
            var context = new ShopContext();
            //Bekleyen migration uygulandıysa test verilerini veritabanına ekleyebiliriz
            if(context.Database.GetPendingMigrations().Count()==0)
            {
                if (context.Products.Count()==0)
                {
                    context.Products.AddRange(Products);
                }
            }
            context.SaveChanges();
        }
        private static Product[] Products = {
            new Product(){Name = "Deneme1",Url="Deneme1",City="İzmir",AdultPrice =3000,ChildPrice = 1000,ImageUrl="hotel_3.jpg",IsApproved=true},
            new Product(){Name = "Deneme2",Url="Deneme2",City="Antalya",AdultPrice =5000,ChildPrice = 1500,ImageUrl="hotel_5.jpg",IsApproved=true},
            new Product(){Name = "Deneme3",Url="Deneme3",City="İzmir",AdultPrice =7000,ChildPrice = 2000,ImageUrl="hotel_4.jpg",IsApproved=true},
            new Product(){Name = "Deneme4",Url="Deneme4",City="Antalya",AdultPrice =9000,ChildPrice = 2400,ArrivalDate="17/05/2021",DepartureDate="22/05/2021",ImageUrl="hotel_1.jpg",IsApproved=true},
            new Product(){Name = "Deneme5",Url="Deneme5",City="Antalya",AdultPrice =10000,ChildPrice = 3000,ImageUrl="hotel_2.jpg",IsApproved=true},
        };

    }
}