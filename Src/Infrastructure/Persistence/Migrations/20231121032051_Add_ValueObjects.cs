using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Northwind.Infrastructure.Identity.Persistence
{
    /// <inheritdoc />
    public partial class Add_ValueObjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_PostalCode",
                table: "Suppliers",
                newName: "Address_PostalCode_Number");

            migrationBuilder.RenameColumn(
                name: "Address_Country",
                table: "Suppliers",
                newName: "Address_Country_Name");

            migrationBuilder.RenameColumn(
                name: "ShipAddress_PostalCode",
                table: "Orders",
                newName: "ShipAddress_PostalCode_Number");

            migrationBuilder.RenameColumn(
                name: "ShipAddress_Country",
                table: "Orders",
                newName: "ShipAddress_Country_Name");

            migrationBuilder.RenameColumn(
                name: "Address_PostalCode",
                table: "Employees",
                newName: "Address_PostalCode_Number");

            migrationBuilder.RenameColumn(
                name: "Address_Country",
                table: "Employees",
                newName: "Address_Country_Name");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Customers",
                newName: "Phone_Number");

            migrationBuilder.RenameColumn(
                name: "Fax",
                table: "Customers",
                newName: "Fax_Number");

            migrationBuilder.RenameColumn(
                name: "Address_PostalCode",
                table: "Customers",
                newName: "Address_PostalCode_Number");

            migrationBuilder.RenameColumn(
                name: "Address_Country",
                table: "Customers",
                newName: "Address_Country_Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_PostalCode_Number",
                table: "Suppliers",
                newName: "Address_PostalCode");

            migrationBuilder.RenameColumn(
                name: "Address_Country_Name",
                table: "Suppliers",
                newName: "Address_Country");

            migrationBuilder.RenameColumn(
                name: "ShipAddress_PostalCode_Number",
                table: "Orders",
                newName: "ShipAddress_PostalCode");

            migrationBuilder.RenameColumn(
                name: "ShipAddress_Country_Name",
                table: "Orders",
                newName: "ShipAddress_Country");

            migrationBuilder.RenameColumn(
                name: "Address_PostalCode_Number",
                table: "Employees",
                newName: "Address_PostalCode");

            migrationBuilder.RenameColumn(
                name: "Address_Country_Name",
                table: "Employees",
                newName: "Address_Country");

            migrationBuilder.RenameColumn(
                name: "Phone_Number",
                table: "Customers",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Fax_Number",
                table: "Customers",
                newName: "Fax");

            migrationBuilder.RenameColumn(
                name: "Address_PostalCode_Number",
                table: "Customers",
                newName: "Address_PostalCode");

            migrationBuilder.RenameColumn(
                name: "Address_Country_Name",
                table: "Customers",
                newName: "Address_Country");
        }
    }
}
