namespace VideoRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'46b985d7-0e42-493f-9783-316eca494e32', N'guest@sample.com', 0, N'APmGsNJt8DXC2Gn5pUaf4xZAnxGxlegVwB3iymT7jcYjJX+ilIRvB55Z/vFCIYIrww==', N'937887ee-c5b0-4807-b0c9-2a2cd164f6ef', NULL, 0, 0, NULL, 1, 0, N'guest@sample.com');
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'61f59e04-2e87-42a6-912d-022de7802659', N'admin@sample.com', 0, N'AC4MX4uy30U8UMAKsddB44FmHca772D0SqkRKiWLJxbSJgOgfyOIKbLTeQ2OOY1mgA==', N'69a37a01-917b-46e3-bfac-9d31807c7990', NULL, 0, 0, NULL, 1, 0, N'admin@sample.com');
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'9e5b00b7-c1e4-48f3-8d5a-e63ab3db39e0', N'CanManageMovies');
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'61f59e04-2e87-42a6-912d-022de7802659', N'9e5b00b7-c1e4-48f3-8d5a-e63ab3db39e0');
            ");
        }
        
        public override void Down()
        {
        }
    }
}
