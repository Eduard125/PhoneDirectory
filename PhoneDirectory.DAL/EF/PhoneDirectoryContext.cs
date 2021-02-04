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
        public DbSet<StructuralDivision> StructuralDivisions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<DivisionPost> DivisionPosts { get; set; }        
        public DbSet<DepartmentNumber> DepartmentNumbers { get; set; }
        public DbSet<DepartmentMobNumber> DepartmentMobNumbers { get; set; }


        public PhoneDirectoryContext(string connectionString)
        {
            this.connectionString = connectionString;            
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
                .HasOne(p => p.DepartmentNumber)
                .WithMany(t => t.Users)
                .HasForeignKey(p => p.StrucDivId);
            modelBuilder.Entity<User>()
               .HasOne(p => p.DepartmentMobNumber)
               .WithMany(t => t.Users)
               .HasForeignKey(p => p.StrucDivId);
            modelBuilder.Entity<DivisionPost>()
                .HasOne(p => p.Post)
                .WithMany(t => t.DivisionPosts)
                .HasForeignKey(p => p.PostId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<DivisionPost>()
               .HasOne(p => p.DepartmentNumber)
               .WithMany(t => t.DivisionPosts)
               .HasForeignKey(p => p.StrucDivId)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<DivisionPost>()
              .HasOne(p => p.DepartmentMobNumber)
              .WithMany(t => t.DivisionPosts)
              .HasForeignKey(p => p.StrucDivId)
              .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<DivisionPost>()
                .HasOne(p => p.StructuralDivision)
                .WithMany(t => t.DivisionPosts)
                .HasForeignKey(p => p.StrucDivId);
            modelBuilder.Entity<DivisionPost>()
               .HasKey(p => new { p.StrucDivId, p.PostId });
            modelBuilder.Entity<User>()
                .HasAlternateKey(p => p.Login);
            // Начальные данные 

            modelBuilder.Entity<Role>().HasData(new Role[]
                {
                    new Role { Id = 1, Name = "Администратор", Design = "admin" },
                    new Role { Id = 2, Name = "Сотрудник", Design = "employee" }
                });           
            modelBuilder.Entity<StructuralDivision>().HasData(new StructuralDivision[]
               {
                    new StructuralDivision { Id = 1, NameStrucDiv = "Дирекция" },
                    new StructuralDivision { Id = 2, NameStrucDiv = "Бухгалтерия" },
                    new StructuralDivision { Id = 3, NameStrucDiv = "От ГИПов" },
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
                    new Post { Id = 3, NamePost = "Бухгалтер 2 кат" },
                    new Post { Id = 4, NamePost = "ГИП" },
                    new Post { Id = 5, NamePost = "Инженер электрик" },
                    new Post { Id = 6, NamePost = "Электрик 2 кат" },
                    new Post { Id = 7, NamePost = "Главный конструктор" },
                    new Post { Id = 8, NamePost = "Конструктор 2 кат" },
                    new Post { Id = 9, NamePost = "Сантехник 2 кат" },
                    new Post { Id = 10, NamePost = "Инженер вентиляции 2 кат" },
                    new Post { Id = 11, NamePost = "Инженер вентиляции 1 кат" },
                    new Post { Id = 12, NamePost = "Главный технолог" },
                    new Post { Id = 13, NamePost = "Технолог 1 кат" }
            });
            modelBuilder.Entity<DivisionPost>().HasData(new DivisionPost[]
            {
                    new DivisionPost { StrucDivId = 1, PostId=1, Id = 1 },
                    new DivisionPost { StrucDivId = 2, PostId=2, Id = 2 },
                    new DivisionPost { StrucDivId = 2, PostId=3, Id = 3 },
                    new DivisionPost { StrucDivId = 3, PostId=4, Id = 4 },
                    new DivisionPost { StrucDivId = 4, PostId=5, Id = 5 },
                    new DivisionPost { StrucDivId = 4, PostId=6, Id = 6 },
                    new DivisionPost { StrucDivId = 5, PostId=7, Id = 7 },
                    new DivisionPost { StrucDivId = 5, PostId=8, Id = 8 },
                    new DivisionPost { StrucDivId = 6, PostId=9, Id = 9 },
                    new DivisionPost { StrucDivId = 7, PostId=10, Id = 10 },
                    new DivisionPost { StrucDivId = 7, PostId=11, Id = 11 },
                    new DivisionPost { StrucDivId = 8, PostId=12, Id = 12 },
                    new DivisionPost { StrucDivId = 8, PostId=13, Id = 13 }
            });
            modelBuilder.Entity<DepartmentNumber>().HasData(new DepartmentNumber[]
           {
                    new DepartmentNumber { Id = 1, StrucDivId = 1, StrucDivNum="242136", StrucDivNum1="242137" },                   
                    new DepartmentNumber { Id = 2, StrucDivId = 2, StrucDivNum="562345", StrucDivNum1="562346" },                   
                    new DepartmentNumber { Id = 3, StrucDivId = 3, StrucDivNum="327544", StrucDivNum1="327545" },                    
                    new DepartmentNumber { Id = 4, StrucDivId = 4, StrucDivNum="442235", StrucDivNum1="442236" },                    
                    new DepartmentNumber { Id = 5, StrucDivId = 5, StrucDivNum="783346", StrucDivNum1="783347" },                    
                    new DepartmentNumber { Id = 6, StrucDivId = 6, StrucDivNum="245632", StrucDivNum1="245633" },                    
                    new DepartmentNumber { Id = 7, StrucDivId = 7, StrucDivNum="338035", StrucDivNum1="338036" },                    
                    new DepartmentNumber { Id = 8, StrucDivId = 8, StrucDivNum="119735", StrucDivNum1="119736" }                    
           });
            modelBuilder.Entity<DepartmentMobNumber>().HasData(new DepartmentMobNumber[]
          {
                    new DepartmentMobNumber { Id = 1, StrucDivId = 1, StrucDivMobNum="+375-29-343-1897", StrucDivMobNum1="+375-29-343-1898" },                    
                    new DepartmentMobNumber { Id = 2, StrucDivId = 2, StrucDivMobNum="+375-29-625-1196", StrucDivMobNum1="+375-29-625-1197" },                    
                    new DepartmentMobNumber { Id = 3, StrucDivId = 3, StrucDivMobNum="+375-29-237-1192", StrucDivMobNum1="+375-29-237-1193" },                    
                    new DepartmentMobNumber { Id = 4, StrucDivId = 4, StrucDivMobNum="+375-29-238-4196", StrucDivMobNum1="+375-29-238-4197" },                   
                    new DepartmentMobNumber { Id = 5, StrucDivId = 5, StrucDivMobNum="+375-29-711-1787", StrucDivMobNum1="+375-29-711-1788" },                    
                    new DepartmentMobNumber { Id = 6, StrucDivId = 6, StrucDivMobNum="+375-29-612-3218", StrucDivMobNum1="+375-29-612-3219" },                    
                    new DepartmentMobNumber { Id = 7, StrucDivId = 7, StrucDivMobNum="+375-29-353-1142", StrucDivMobNum1="+375-29-353-1143" },                    
                    new DepartmentMobNumber { Id = 8, StrucDivId = 8, StrucDivMobNum="+375-29-792-3355", StrucDivMobNum1="+375-29-792-3356" }                    
          });
           
            modelBuilder.Entity<User>().HasData(new User[]
             {
                    new User { Id = 1, Login = "admin", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 1, PersonalNum="+375-29-345-6792", PersonalNum1="+375-29-345-6794", Surname = "Админов", Name = "Админ", Patronymic = "Админович", StrucDivId = 1, PostId = 1, Email = "admin@domain.by" },
                    new User { Id = 2, Login = "petrov", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 2, PersonalNum="+375-29-345-6793", PersonalNum1="+375-29-345-6795", Surname = "Петров", Name = "Петр", Patronymic = "Петрович", StrucDivId = 2, PostId = 2, Email = "petrov@domain.by" },
                    new User { Id = 3, Login = "cidorov", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 2, PersonalNum="+375-29-624-3392", PersonalNum1="+375-29-624-3382", Surname = "Сидоров", Name = "Игорь", Patronymic = "Витальевич", StrucDivId = 2, PostId = 3, Email = "cidorov@domain.by" },
                    new User { Id = 4, Login = "sokolova", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 2, PersonalNum="+375-29-547-1633", PersonalNum1="+375-29-547-1637", Surname = "Соколова", Name = "Ирина", Patronymic = "Петровна", StrucDivId = 3, PostId = 4, Email = "sokolova@domain.by" },
                    new User { Id = 5, Login = "pushkina", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 2, PersonalNum="+375-29-277-4532", PersonalNum1="+375-29-277-4533", Surname = "Пушкина", Name = "Оксана", Patronymic = "Николаевна", StrucDivId = 4, PostId = 5, Email = "pushkina@domain.by" },
                    new User { Id = 6, Login = "gnomova", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 2, PersonalNum="+375-29-277-4543", PersonalNum1="+375-29-277-4544", Surname = "Гномова", Name = "Татьяна", Patronymic = "Дмитриевна", StrucDivId = 4, PostId = 6, Email = "gnomova@domain.by" },
                    new User { Id = 7, Login = "ivanova", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 2, PersonalNum="+375-29-735-8895", PersonalNum1="+375-29-735-8896", Surname = "Иванова", Name = "Ольга", Patronymic = "Сергеевна", StrucDivId = 5, PostId = 7, Email = "ivanova@domain.by" },
                    new User { Id = 8, Login = "topal", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 2, PersonalNum="+375-29-735-8892", PersonalNum1="+375-29-735-8893", Surname = "Топалева", Name = "Ирина", Patronymic = "Владимировна", StrucDivId = 5, PostId = 8, Email = "topal@domain.by" },
                    new User { Id = 9, Login = "kirilov", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 2, PersonalNum="+375-29-611-2453", PersonalNum1="+375-29-611-2454", Surname = "Кирилов", Name = "Сергей", Patronymic = "Петрович", StrucDivId = 6, PostId = 9, Email = "kirilov@domain.by" },
                    new User { Id = 10, Login = "budnik", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 2, PersonalNum="+375-29-611-2454", PersonalNum1="+375-29-611-2455", Surname = "Будник", Name = "Павел", Patronymic = "Семенов", StrucDivId = 7, PostId = 10, Email = "budnik@domain.by" },
                    new User { Id = 11, Login = "solncev", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 2, PersonalNum="+375-29-332-1117", PersonalNum1="+375-29-332-1118", Surname = "Солнцев", Name = "Анатолий", Patronymic = "Владимирович", StrucDivId = 7, PostId = 11, Email = "solncev@domain.by" },
                    new User { Id = 12, Login = "dubov", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 2, PersonalNum="+375-29-332-1118", PersonalNum1="+375-29-332-1119", Surname = "Дубов", Name = "Артем", Patronymic = "Дмитриевич", StrucDivId = 8, PostId = 12, Email = "dubov@domain.by" },
                    new User { Id = 13, Login = "kisilev", Password = "e10adc3949ba59abbe56e057f20f883e", RoleId = 2, PersonalNum="+375-29-728-1163", PersonalNum1="+375-29-332-1165", Surname = "Кисилев", Name = "Сергей", Patronymic = "Анатольевич", StrucDivId = 8, PostId = 13, Email = "kisilev@domain.by" }
             });            
        }
    }
}
