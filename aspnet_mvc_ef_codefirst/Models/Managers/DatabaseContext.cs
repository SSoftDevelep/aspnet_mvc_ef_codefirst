using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace aspnet_mvc_ef_codefirst.Models.Managers
{
    public class DatabaseContext : DbContext  //DbContext EntityFramework icinde oldugundan Database islemleri icin miras aliriz. 
    {
        public DbSet<Kisiler> Kisiler { get; set; }  //Kisiler tablosunun verilerini temsil et demek.
        public DbSet<Adresler> Adresler { get; set; }

        public DatabaseContext()  //veritabani olusturulurken calismasini saglamak icin. (constructor)
        {
            Database.SetInitializer(new VeritabaniOlusturucu());
        }

    }


    public class VeritabaniOlusturucu : CreateDatabaseIfNotExists<DatabaseContext>  //veritabani yoksa database olustur classindan miras aldik.
    {
        protected override void Seed(DatabaseContext context)  //seed methoduyla veritabani tablolarimizi doldurduk.
        {

            //Kisiler insert
            for (int i = 0; i < 10; i++)
            {
                Kisiler kisi = new Kisiler();
                kisi.Ad = FakeData.NameData.GetFirstName();
                kisi.Soyad = FakeData.NameData.GetSurname();
                kisi.Yas = FakeData.NumberData.GetNumber(10, 90);

                context.Kisiler.Add(kisi);
            }

            context.SaveChanges(); //insert islemi.





            //Adresler insert
            List<Kisiler> tumkisiler = context.Kisiler.ToList();  //select *from Kisiler

            foreach (Kisiler kisim in tumkisiler)
            {
                for (int i = 0; i < FakeData.NumberData.GetNumber(1, 5); i++)
                {
                    Adresler adres = new Adresler();
                    adres.AdresTanim = FakeData.PlaceData.GetAddress();
                    adres.Kisi = kisim;

                    context.Adresler.Add(adres);
                }
            }

            context.SaveChanges();
        }

    }
}