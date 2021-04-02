using API.Data.Entities;
using API.Data.Entities.Locations;
using API.Data.Entities.Messaging;
using API.Data.Entities.Users;
using API.Data.Static;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<UserFriendship> UserFriendships { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<PrivateMessage> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFriendship>()
                .HasKey(e => new { e.UserId, e.User2Id });

            modelBuilder.Entity<UserFriendship>()
                .HasOne(e => e.User)
                .WithMany(e => e.Friends)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserFriendship>()
                .Ignore(c => c.Id)
                .HasOne(e => e.User2);

            //SeedCountryAndCityData(modelBuilder);
        }

        private void SeedCountryAndCityData(ModelBuilder modelBuilder)
        {
            //using (var client = new System.Net.WebClient())
            //{
            //    var json = client.DownloadString("https://raw.githubusercontent.com/dr5hn/countries-states-cities-database/master/countries%2Bstates%2Bcities.json");
            //    var list = JsonConvert.DeserializeObject<List<Country>>(json);

            //    //int countryId = 1;
            //    //int stateId= 1;
            //    //int cityId = 1;

            //    modelBuilder.Entity<Country>().HasMany(country => country.States).WithOne(x => x.Country).HasForeignKey(x => x.CountryCode);
            //    modelBuilder.Entity<State>().HasMany(state => state.Cities).WithOne(x => x.State).HasForeignKey(x => x.StateCode);

            //    foreach (var country in list)
            //    {
            //        foreach (var state in country.States)
            //        {
            //            state.Country = country;
            //            state.CountryCode = country.CountryCode;

            //            modelBuilder.Entity<State>().HasData(state);

            //            foreach (var city in state.Cities)
            //            {
            //                city.State = state;
            //                city.State.StateCode = state.StateCode;
            //            }
            //        }
            //    }

            //    modelBuilder.Entity<State>().HasData(list.Select(x => x.States));



            //    //modelBuilder.Entity<Country>().HasMany(x => x.States).WithOne();
            //    //modelBuilder.Entity<State>().HasOne(x => x.Country).WithMany().HasForeignKey(x => x.CountryCode);
            //    //modelBuilder.Entity<City>().HasData(list.SelectMany(x => x.States.SelectMany(x => x.Cities)));


            //    modelBuilder.Entity<Country>().HasData(list);
            //}

            //using (var client = new System.Net.WebClient())
            //{
            //    var json = client.DownloadString("https://raw.githubusercontent.com/lutangar/cities.json/master/cities.json");
            //    var list = JsonConvert.DeserializeObject<List<City>>(json);

            //    int cityId = 1;


            //    foreach (var city in list)
            //    {
            //        //city.Id = cityId;
            //        cityId++;
            //    }

            //    modelBuilder.Entity<City>().HasNoKey().HasData(list);
            //}
        }
    }
}