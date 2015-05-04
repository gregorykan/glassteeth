namespace glassteeth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "glassteeth.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "glassteeth.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("glassteeth.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("glassteeth.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "glassteeth.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "glassteeth.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("glassteeth.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "glassteeth.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("glassteeth.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("glassteeth.AspNetUserRoles", "UserId", "glassteeth.AspNetUsers");
            DropForeignKey("glassteeth.AspNetUserLogins", "UserId", "glassteeth.AspNetUsers");
            DropForeignKey("glassteeth.AspNetUserClaims", "UserId", "glassteeth.AspNetUsers");
            DropForeignKey("glassteeth.AspNetUserRoles", "RoleId", "glassteeth.AspNetRoles");
            DropIndex("glassteeth.AspNetUserLogins", new[] { "UserId" });
            DropIndex("glassteeth.AspNetUserClaims", new[] { "UserId" });
            DropIndex("glassteeth.AspNetUsers", "UserNameIndex");
            DropIndex("glassteeth.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("glassteeth.AspNetUserRoles", new[] { "UserId" });
            DropIndex("glassteeth.AspNetRoles", "RoleNameIndex");
            DropTable("glassteeth.AspNetUserLogins");
            DropTable("glassteeth.AspNetUserClaims");
            DropTable("glassteeth.AspNetUsers");
            DropTable("glassteeth.AspNetUserRoles");
            DropTable("glassteeth.AspNetRoles");
        }
    }
}
