namespace ProyectoAutos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB_AUTOS : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Pedidoes", "fecha");
            DropColumn("dbo.Pedidoes", "total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pedidoes", "total", c => c.Double(nullable: false));
            AddColumn("dbo.Pedidoes", "fecha", c => c.DateTime(nullable: false));
        }
    }
}
