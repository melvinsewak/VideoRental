namespace VideoRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DomainUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "Name", c => c.String(nullable: false, maxLength: 255));
            Sql("INSERT INTO Movies(Name,ReleaseDate,AdditionDate,GenreId,NumberInStock) Values('Jon Wick 3','20190312','20190313',1,5)");
            Sql("INSERT INTO Movies(Name,ReleaseDate,AdditionDate,GenreId,NumberInStock) Values('Titanic','20120412','20120413',3,5)");
            Sql("INSERT INTO Movies(Name,ReleaseDate,AdditionDate,GenreId,NumberInStock) Values('Shrek','20170812','20180813',2,5)");
            Sql("INSERT INTO Movies(Name,ReleaseDate,AdditionDate,GenreId,NumberInStock) Values('Conjuring','20160412','20160413',4,3)");
            Sql("INSERT INTO Movies(Name,ReleaseDate,AdditionDate,GenreId,NumberInStock) Values('Murder Mystery','20190512','20190513',6,5)");
            Sql("INSERT INTO Movies(Name,ReleaseDate,AdditionDate,GenreId,NumberInStock) Values('Alladin','20190412','20190413',6,3)");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Name", c => c.String());
        }
    }
}
