using Microsoft.EntityFrameworkCore;
using PhoneDirectory.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.DAL.EF
{
    public class PhoneDirectoryContext : DbContext
    {
        private string connectionString;
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<StructuralDivision> StructuralDivisions { get; set; }
        public DbSet<DivisionPost> DivisionPosts { get; set; }
        public DbSet<PersonalNumber> PersonalNumbers { get; set; }
        public DbSet<DepartmentNumber> DepartmentNumbers { get; set; }
        public DbSet<DepartmentMobNumber> DepartmentMobNumbers { get; set; }

        public PhoneDirectoryContext(string connectionString)
        {
            this.connectionString = connectionString;
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Связи
            modelBuilder.Entity<User>()
                .HasOne(p => p.Role)
                .WithMany(t => t.Users)
                .HasForeignKey(p => p.RoleId);
            modelBuilder.Entity<User>()
                .HasOne(p => p.StructuralDivision)
                .WithMany(t => t.Users)
                .HasForeignKey(p => p.StrucDivId);
            modelBuilder.Entity<User>()
                .HasOne(p => p.Post)
                .WithMany(t => t.Users)
                .HasForeignKey(p => p.PostId);
            modelBuilder.Entity<User>()
                .HasOne(p => p.PersonalNumber)
                .WithMany(t => t.Users)
                .HasForeignKey(p => p.Id);
            modelBuilder.Entity<User>()
                .HasOne(p => p.DepartmentNumber)
                .WithMany(t => t.Users)
                .HasForeignKey(p => p.StrucDivId);
            modelBuilder.Entity<User>()
               .HasOne(p => p.DepartmentMobNumber)
               .WithMany(t => t.Users)
               .HasForeignKey(p => p.StrucDivId);
            modelBuilder.Entity<DivisionPost>()
                .HasOne(p => p.StructuralDivision)
                .WithMany(t => t.DivisionPosts)
                .HasForeignKey(p => p.StrucDivId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<DivisionPost>()
                .HasOne(p => p.Post)
                .WithMany(t => t.DivisionPosts)
                .HasForeignKey(p => p.PostId);
            modelBuilder.Entity<DivisionPost>()
               .HasKey(p => new { p.StrucDivId, p.PostId });
            modelBuilder.Entity<User>()
                .HasAlternateKey(p => p.Login);
            // Начальные данные 
            modelBuilder.Entity<StructuralDivision>().HasData(new StructuralDivision[]
               {
                    new StructuralDivision { Id = 1, NameStrucDiv = "Дирекция" },
                    new StructuralDivision { Id = 2, NameStrucDiv = "Бухгалтерия" },
                    new StructuralDivision { Id = 3, NameStrucDiv = "Отдел ГИПов" },
                    new StructuralDivision { Id = 4, NameStrucDiv = "Электротехнический отдел" },
                    new StructuralDivision { Id = 5, NameStrucDiv = "Архитектурно-конструкторский отдел" },
                    new StructuralDivision { Id = 6, NameStrucDiv = "Сантехнический отдел" },
                    new StructuralDivision { Id = 7, NameStrucDiv = "Отдел отопления и вентиляции" },
                    new StructuralDivision { Id = 8, NameStrucDiv = "Технологический отдел" },
               });
            modelBuilder.Entity<Post>().HasData(new Post[]
              {
                    new Post { Id = 1, NamePost = "Директор" },
                    new Post { Id = 2, NamePost = "Главный бухгалтер" },
                    new Post { Id = 3, NamePost = "ГИП" },
                    new Post { Id = 4, NamePost = "Инженер-электрик" },
                    new Post { Id = 5, NamePost = "Главный конструктор" },
                    new Post { Id = 6, NamePost = "Сантехник 2 кат." },
                    new Post { Id = 7, NamePost = "Инженер вентиляции 2 кат." },
                    new Post { Id = 8, NamePost = "Главный технолог" },
              });
            modelBuilder.Entity<DivisionPost>().HasData(new DivisionPost[]
             {
                    new DivisionPost { Id = 1, StrucDivId = 1, PostId=1 },
                    new DivisionPost { Id = 2, StrucDivId = 2, PostId=2 },
                    new DivisionPost { Id = 3, StrucDivId = 3, PostId=3 },
                    new DivisionPost { Id = 4, StrucDivId = 4, PostId=4 },
                    new DivisionPost { Id = 5, StrucDivId = 5, PostId=5 },
                    new DivisionPost { Id = 6, StrucDivId = 6, PostId=6 },
                    new DivisionPost { Id = 7, StrucDivId = 7, PostId=7 },
                    new DivisionPost { Id = 8, StrucDivId = 8, PostId=8 },
             });
            modelBuilder.Entity<Role>().HasData(new Role[]
                {
                    new Role { Id = 1, Name = "Администратор", Design = "admin" },
                    new Role { Id = 2, Name = "Сотрудник", Design = "employee" }
                });
            modelBuilder.Entity<PersonalNumber>().HasData(new PersonalNumber[]
            {
                    new PersonalNumber { Id = 1, UserId = 1, PersonalNum="+375-29-345-6792" },
                    new PersonalNumber { Id = 2, UserId = 2, PersonalNum="+375-29-624-3392" },
                    new PersonalNumber { Id = 3, UserId = 3, PersonalNum="+375-29-547-1633" },
                    new PersonalNumber { Id = 4, UserId = 4, PersonalNum="+375-29-277-4532" },
                    new PersonalNumber { Id = 5, UserId = 5, PersonalNum="+375-29-735-8891" },
                    new PersonalNumber { Id = 6, UserId = 6, PersonalNum="+375-29-611-2453" },
                    new PersonalNumber { Id = 7, UserId = 7, PersonalNum="+375-29-332-1117" },
                    new PersonalNumber { Id = 8, UserId = 8, PersonalNum="+375-29-728-1163" }
            });
            modelBuilder.Entity<DepartmentNumber>().HasData(new DepartmentNumber[]
            {
                    new DepartmentNumber { Id = 1, StrucDivId = 1, StrucDivNum="(80232)242136" },
                    new DepartmentNumber { Id = 2, StrucDivId = 2, StrucDivNum="(80232)562345" },
                    new DepartmentNumber { Id = 3, StrucDivId = 3, StrucDivNum="(80232)327544" },
                    new DepartmentNumber { Id = 4, StrucDivId = 4, StrucDivNum="(80232)442235" },
                    new DepartmentNumber { Id = 5, StrucDivId = 5, StrucDivNum="(80232)783346" },
                    new DepartmentNumber { Id = 6, StrucDivId = 6, StrucDivNum="(80232)245632" },
                    new DepartmentNumber { Id = 7, StrucDivId = 7, StrucDivNum="(80232)338035" },
                    new DepartmentNumber { Id = 8, StrucDivId = 8, StrucDivNum="(80232)119735" },
            });
            modelBuilder.Entity<DepartmentMobNumber>().HasData(new DepartmentMobNumber[]
           {
                    new DepartmentMobNumber { Id = 1, StrucDivId = 1, StrucDivMobNum="+375-29-343-1897" },
                    new DepartmentMobNumber { Id = 2, StrucDivId = 2, StrucDivMobNum="+375-29-625-1196" },
                    new DepartmentMobNumber { Id = 3, StrucDivId = 3, StrucDivMobNum="+375-29-237-1192" },
                    new DepartmentMobNumber { Id = 4, StrucDivId = 4, StrucDivMobNum="+375-29-238-4196" },
                    new DepartmentMobNumber { Id = 5, StrucDivId = 5, StrucDivMobNum="+375-29-711-1787" },
                    new DepartmentMobNumber { Id = 6, StrucDivId = 6, StrucDivMobNum="+375-29-612-3218" },
                    new DepartmentMobNumber { Id = 7, StrucDivId = 7, StrucDivMobNum="+375-29-353-1142" },
                    new DepartmentMobNumber { Id = 8, StrucDivId = 8, StrucDivMobNum="+375-29-792-3355" }
           });
            modelBuilder.Entity<User>().HasData(new User[]
             {
                    new User { Id = 1, Login = "admin", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 1, Surname = "Админов", Name = "Админ", Patronymic = "Админович",  PostId = 1, StrucDivId = 1, Email = "admin@domain.by" },
                    new User { Id = 2, Login = "petrov", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 2, Surname = "Петров", Name = "Петр", Patronymic = "Петрович",  PostId = 2, StrucDivId = 2, Email = "petrov@domain.by" },
                    new User { Id = 3, Login = "sokolova", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 2, Surname = "Соколова", Name = "Ирина", Patronymic = "Петровна",  PostId = 3, StrucDivId = 3, Email = "sokolova@domain.by" },
                    new User { Id = 4, Login = "pushkina", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 2, Surname = "Пушкина", Name = "Оксана", Patronymic = "Николаевна",  PostId = 4, StrucDivId = 4, Email = "pushkina@domain.by" },
                    new User { Id = 5, Login = "ivanova", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 2, Surname = "Иванова", Name = "Ольга", Patronymic = "Сергеевна",  PostId = 5, StrucDivId = 5, Email = "ivanova@domain.by" },
                    new User { Id = 6, Login = "kirilov", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 2, Surname = "Кирилов", Name = "Сергей", Patronymic = "Петрович",  PostId = 6, StrucDivId = 6, Email = "kirilov@domain.by" },
                    new User { Id = 7, Login = "budnik", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 2, Surname = "Будник", Name = "Павел", Patronymic = "Семенов",  PostId = 7, StrucDivId = 7, Email = "budnik@domain.by" },
                    new User { Id = 8, Login = "solncev", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 2, Surname = "Солнцев", Name = "Анатолий", Patronymic = "Владимирович",  PostId = 8, StrucDivId = 8, Email = "solncev@domain.by" }

             });
        }
    }
}
