namespace Rentorer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsersRoles : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a367ee47-f694-4562-9336-eb3f026ee634', N'CanManageAll')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'790570de-f1f4-4cf5-9631-5608e03fb370', N'CanManageMovies')

                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c3cc4b9b-2dd2-4e83-854c-1fbd840a0748', N'guest@domain.com', 0, N'APzpPd0ILREk0foWns3eaaT4hGP0C9DMYJfdLL8a+yRlItHNQFVPwjp86+3N0c5NTA==', N'f17c0c69-0900-4811-a007-66adc06d4ea2', NULL, 0, 0, NULL, 1, 0, N'guest@domain.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ca95cc3d-04d0-47d9-a5e9-fb95ed095317', N'movieManager@domain.com', 0, N'AEt7NtVoWODOaedsc/5UzHh09JUCpMbONxfCgHP07QrdPvPyEDPnLmwR+HxHOZVIgg==', N'6568d386-aef0-4881-8f01-a585a63c28a3', NULL, 0, 0, NULL, 1, 0, N'movieManager@domain.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ffea3f8d-e0c9-4d29-8084-3a4641b64cb0', N'admin@domain.com', 0, N'AFrRBAcCR45JxzFq17/vvs3EMCJG6YbnYnKObCXX6LH2M+XCkjrhPetjflMtE/bH9g==', N'419fb0e6-4d82-4d52-a64a-6526a938c442', NULL, 0, 0, NULL, 1, 0, N'admin@domain.com')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ca95cc3d-04d0-47d9-a5e9-fb95ed095317', N'790570de-f1f4-4cf5-9631-5608e03fb370')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ffea3f8d-e0c9-4d29-8084-3a4641b64cb0', N'a367ee47-f694-4562-9336-eb3f026ee634')

            ");
        }
        
        public override void Down()
        {
        }
    }
}
