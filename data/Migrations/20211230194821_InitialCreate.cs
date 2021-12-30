using Microsoft.EntityFrameworkCore.Migrations;

namespace data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    ChildPrice = table.Column<double>(nullable: true),
                    AdultPrice = table.Column<double>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    ArrivalDate = table.Column<string>(nullable: true),
                    DepartureDate = table.Column<string>(nullable: true),
                    Room = table.Column<string>(nullable: true),
                    NumberOfPeople = table.Column<int>(nullable: true),
                    NumberOfChildren = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
