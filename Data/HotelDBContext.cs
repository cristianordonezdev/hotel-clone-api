using hotel_clone_api.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Reflection.Emit;
using System;

namespace hotel_clone_api.Data
{
    public class HotelDbContext : IdentityDbContext<IdentityUser>
    {
        public HotelDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<ImageType> ImageTypes { get; set; }
        public DbSet<hotel_clone_api.Models.Domain.Image> Images { get; set; }
        public DbSet<Offer> Offers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var readerRoleId = "f0571c7a-9d14-48c2-ad57-d08430d9e358";
            var writerRoleId = "8e209f7d-2358-44f1-8623-86684bf3081b";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

            //Seeding data for ImageTypes

            var ImagesTypes = new List<ImageType>()
            {
                new ImageType()
                {
                    Id = Guid.Parse("3897b275-7a3f-4a84-a620-105b9b0eb89a"),
                    Type = "room",
                },
                new ImageType()
                {
                    Id = Guid.Parse("e4567686-1b4d-483d-a374-9e99306c8e7b"),
                    Type = "carousel",
                },
                new ImageType()
                {
                    Id = Guid.Parse("de63304d-8500-4570-8333-abb077e5a23f"),
                    Type = "food",
                },
                new ImageType()
                {
                    Id = Guid.Parse("8929b4bf-5be3-4002-8ad6-b9f46f782f16"),
                    Type = "offers",
                },
            };

            modelBuilder.Entity<ImageType>().HasData(ImagesTypes);

            //List<RoomReservation> roomsAvaible = new List<RoomReservation>();
            //for (int i = 0; i < 3; i++)
            //{
            //    roomsAvaible.Add(new RoomReservation()
            //    {
            //        Id = Guid.NewGuid(),
            //        roomId = Guid.Parse("5ec91cd3-2842-4a51-c394-08db81affe60"),
            //    });
            //    roomsAvaible.Add(new RoomReservation()
            //    {
            //        Id = Guid.NewGuid(),
            //        roomId = Guid.Parse("72727aa7-ddc3-4844-8e79-08db827c2d2c"),
            //    });
            //    roomsAvaible.Add(new RoomReservation()
            //    {
            //        Id = Guid.NewGuid(),
            //        roomId = Guid.Parse("eb2fa93b-c9c5-4755-8e7a-08db827c2d2c"),
            //    });
            //}

            //modelBuilder.Entity<RoomReservation>().HasData(roomsAvaible);




            //Making relation between images en rooms
            //modelBuilder.Entity<Image>()
            //   .HasOne(image => image.Room)
            //   .WithMany(room => room.Images)
            //   .HasForeignKey(image => image.RelativeRelationId)
            //   .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Image>()
            //   .HasOne(image => image.Offer)
            //   .WithOne(offer => offer.Image)
            //   .HasForeignKey<Offer>(offer => offer.ImageId)
            //   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}