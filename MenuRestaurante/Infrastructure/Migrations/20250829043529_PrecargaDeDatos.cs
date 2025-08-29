using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PrecargaDeDatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description", "Name", "Order" },
                values: new object[,]
                {
                    { 1, "Pequeñas porciones para abrir el apetito antes del plato principal.", "Entradas", 1 },
                    { 2, "Opciones frescas y livianas, ideales como acompañamiento o plato principal.", "Ensaladas", 2 },
                    { 3, "Platos rápidos y clásicos de bodegón: milanesas, tortillas, revueltos.", "Minutas", 3 },
                    { 4, "Variedad de pastas caseras y salsas tradicionales.", "Pastas", 4 },
                    { 5, "Cortes de carne asados a la parrilla, servidos con guarniciones.", "Parrilla", 5 },
                    { 6, "Pizzas artesanales con masa casera y variedad de ingredientes.", "Pizzas", 6 },
                    { 7, "Sandwiches y lomitos completos preparados al momento.", "Sandwiches", 7 },
                    { 8, "Gaseosas, jugos, aguas y opciones sin alcohol.", "Bebidas", 8 },
                    { 9, "Cervezas de producción artesanal, rubias, rojas y negras.", "Cerveza Artesanal", 9 },
                    { 10, "Clásicos dulces caseros para cerrar la comida.", "Postres", 10 }
                });

            migrationBuilder.InsertData(
                table: "DeliveryType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Delivery" },
                    { 2, "Take away" },
                    { 3, "Dine in" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "In progress" },
                    { 3, "Ready" },
                    { 4, "Delivery" },
                    { 5, "Closed" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "DeliveryType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DeliveryType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DeliveryType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
