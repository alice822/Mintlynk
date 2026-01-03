using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MintLynk.Domain.Entities;
using MintLynk.Domain.Entities.SmartLink;
using MintLynk.Domain.Models;
using MintLynk.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<LinkHistory> LinksHistory { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<MiniPage> MiniPages { get; set; }

        public DbSet<NotificationCenter> NotificationCenters { get; set; }

        public DbSet<SmartLinkDto> SmartLinkDtos { get; set; }

        public DbSet<NextIdDto> NextIdDtos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ConfigurePartitionTable<SmartLinkA>(builder, "SmartLinkA");
            ConfigurePartitionTable<SmartLinkB>(builder, "SmartLinkB");
            ConfigurePartitionTable<SmartLinkC>(builder, "SmartLinkC");
            ConfigurePartitionTable<SmartLinkD>(builder, "SmartLinkD");
            ConfigurePartitionTable<SmartLinkE>(builder, "SmartLinkE");
            ConfigurePartitionTable<SmartLinkF>(builder, "SmartLinkF");
            ConfigurePartitionTable<SmartLinkG>(builder, "SmartLinkG");
            ConfigurePartitionTable<SmartLinkH>(builder, "SmartLinkH");
            ConfigurePartitionTable<SmartLinkI>(builder, "SmartLinkI");
            ConfigurePartitionTable<SmartLinkJ>(builder, "SmartLinkJ");
            ConfigurePartitionTable<SmartLinkK>(builder, "SmartLinkK");
            ConfigurePartitionTable<SmartLinkL>(builder, "SmartLinkL");
            ConfigurePartitionTable<SmartLinkM>(builder, "SmartLinkM");
            ConfigurePartitionTable<SmartLinkN>(builder, "SmartLinkN");
            ConfigurePartitionTable<SmartLinkO>(builder, "SmartLinkO");
            ConfigurePartitionTable<SmartLinkP>(builder, "SmartLinkP");
            ConfigurePartitionTable<SmartLinkQ>(builder, "SmartLinkQ");
            ConfigurePartitionTable<SmartLinkR>(builder, "SmartLinkR");
            ConfigurePartitionTable<SmartLinkS>(builder, "SmartLinkS");
            ConfigurePartitionTable<SmartLinkT>(builder, "SmartLinkT");
            ConfigurePartitionTable<SmartLinkU>(builder, "SmartLinkU");
            ConfigurePartitionTable<SmartLinkV>(builder, "SmartLinkV");
            ConfigurePartitionTable<SmartLinkW>(builder, "SmartLinkW");
            ConfigurePartitionTable<SmartLinkX>(builder, "SmartLinkX");
            ConfigurePartitionTable<SmartLinkY>(builder, "SmartLinkY");
            ConfigurePartitionTable<SmartLinkZ>(builder, "SmartLinkZ");
            ConfigurePartitionTable<SmartLink0>(builder, "SmartLink0");
            ConfigurePartitionTable<SmartLink1>(builder, "SmartLink1");
            ConfigurePartitionTable<SmartLink2>(builder, "SmartLink2");
            ConfigurePartitionTable<SmartLink3>(builder, "SmartLink3");
            ConfigurePartitionTable<SmartLink4>(builder, "SmartLink4");
            ConfigurePartitionTable<SmartLink5>(builder, "SmartLink5");
            ConfigurePartitionTable<SmartLink6>(builder, "SmartLink6");
            ConfigurePartitionTable<SmartLink7>(builder, "SmartLink7");
            ConfigurePartitionTable<SmartLink8>(builder, "SmartLink8");
            ConfigurePartitionTable<SmartLink9>(builder, "SmartLink9");
            builder.Entity<LinkHistory>().ToTable("LinkHistory");
            builder.Entity<Group>().ToTable("Group");
            builder.Entity<MiniPage>().ToTable("MiniPage");
            builder.Entity<NotificationCenter>().ToTable("NotificationCenter");
            builder.Entity<SmartLinkDto>().HasNoKey();
            builder.Entity<NextIdDto>().HasNoKey();
        }
        private void ConfigurePartitionTable<TEntity>(ModelBuilder modelBuilder, string tableName)
                where TEntity : SmartLinkBase
        {
            modelBuilder.Entity<TEntity>().ToTable(tableName);
        }

    }
}
