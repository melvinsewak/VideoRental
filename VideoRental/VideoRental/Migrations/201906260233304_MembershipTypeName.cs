namespace VideoRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MembershipTypeName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "Name", c => c.String());
            Sql("Update MembershipTypes set Name='Pay as You Go' where Id=1");
            Sql("Update MembershipTypes set Name='Monthly' where Id=2");
            Sql("Update MembershipTypes set Name='Quarterly' where Id=3");
            Sql("Update MembershipTypes set Name='Annual' where Id=4");
        }
        
        public override void Down()
        {
            DropColumn("dbo.MembershipTypes", "Name");
        }
    }
}
