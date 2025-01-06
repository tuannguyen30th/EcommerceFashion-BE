using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntityV5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantAttributeValueDatas_ProductVariants_ProductVariantId",
                table: "ProductVariantAttributeValueDatas");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantAttributeValueDatas_ProductVariants_ProductVariantId",
                table: "ProductVariantAttributeValueDatas",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantAttributeValueDatas_ProductVariants_ProductVariantId",
                table: "ProductVariantAttributeValueDatas");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantAttributeValueDatas_ProductVariants_ProductVariantId",
                table: "ProductVariantAttributeValueDatas",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
