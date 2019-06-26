namespace VideoRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenre : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres(Id,Name) Values(1,'Action')");
            Sql("INSERT INTO Genres(Id,Name) Values(2,'Comedy')");
            Sql("INSERT INTO Genres(Id,Name) Values(3,'Romantic')");
            Sql("INSERT INTO Genres(Id,Name) Values(4,'Horror')");
            Sql("INSERT INTO Genres(Id,Name) Values(5,'Sci-fi')");
            Sql("INSERT INTO Genres(Id,Name) Values(6,'Thriller')");
            Sql("INSERT INTO Genres(Id,Name) Values(7,'Romedy')");
        }
        
        public override void Down()
        {
        }
    }
}
