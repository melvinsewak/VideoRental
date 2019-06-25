namespace VideoRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCustomers : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Customers(Name,IsSubscribedToNewsletter,MembershipTypeId) VALUES('John Smith','False',1)");
            Sql("INSERT INTO Customers(Name,IsSubscribedToNewsletter,MembershipTypeId) VALUES('Mary Williams','True',2)");
        }
        
        public override void Down()
        {
        }
    }
}
