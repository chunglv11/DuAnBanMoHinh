using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BanMoHinh.API.Migrations
{
    public partial class adddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1f6d8939-ae0c-4fdb-bf66-c6d017fcae4a"), "b5dd0b66-09b4-4eae-8b0e-74280d6d06f7", "User", "USER" },
                    { new Guid("a6381a7e-bc6e-4fc7-9e71-74fe84ffc8e8"), "43dc792c-8a8b-482b-a216-a6122e01ff1f", "Admin", "ADMIN" },
                    { new Guid("f0d825a9-60e6-4e76-90e1-e58185f37d88"), "095110d0-127d-4ad8-a4ed-47162958da4d", "Guest", "GUEST" }
                });

            migrationBuilder.InsertData(
                table: "Brand",
                columns: new[] { "Id", "BrandName" },
                values: new object[,]
                {
                    { new Guid("5a94ea4a-46fe-4db2-babc-7c009a736c67"), "Brand 3" },
                    { new Guid("5aa8f30e-4822-4a6d-aff5-a66cae01b2a5"), "Brand 5" },
                    { new Guid("67f26e63-00ef-46c0-b736-b769b1b3b890"), "Brand 2" },
                    { new Guid("8c0b64d4-8176-4bc7-9795-f40e9bde946d"), "Brand 4" },
                    { new Guid("f180bdeb-4587-41ae-af0d-3f87922f644c"), "Brand 1" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("578942ac-8c63-46a1-8364-336f5c50d4dd"), "Category 2" },
                    { new Guid("7a8f965e-9fce-4291-96e0-8038a8e6b290"), "Category 4" },
                    { new Guid("9d66eaac-20fa-45e1-a4d8-66f92f8308f8"), "Category 1" },
                    { new Guid("c0749423-e4e6-4407-ac49-523e83c14e7c"), "Category 3" },
                    { new Guid("ed08c89b-eeeb-42a9-a11d-9ad3f76132eb"), "Category 5" }
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "ColorId", "ColorCode", "ColorName" },
                values: new object[,]
                {
                    { new Guid("017d4c19-c47c-42c0-9028-9974553734e4"), "#FFC0CB", "Hồng" },
                    { new Guid("29748565-2c9b-4851-9c6b-853dda79a928"), "#00FF00", "Xanh lá cây" },
                    { new Guid("2b43c23b-a74c-4e80-9ceb-47a7d82a1153"), "#C0C0C0", "Bạc" },
                    { new Guid("3821073e-a4f9-4f05-b802-395a61da25d3"), "#000080", "Xanh lam" },
                    { new Guid("46a7e77e-358a-47a9-89e6-f633f48511b2"), "#A52A2A", "Nâu" },
                    { new Guid("4c22a2fd-ac8b-4fba-b103-1cb37013979a"), "#00BFFF", "Xanh da trời" },
                    { new Guid("523030ae-49fc-4f3c-acef-8b8f1bbeee44"), "#808080", "Xám" },
                    { new Guid("769585ff-6d9e-4070-aebd-256248ed4146"), "#000000", "Đen" },
                    { new Guid("7c0b62c7-a930-4a49-ae94-0e946bf2b105"), "#FFFFFF", "Trắng" },
                    { new Guid("89296303-7daf-4601-8c14-18b16b7ed3f0"), "#FFA500", "Cam" },
                    { new Guid("9efe3f79-2523-403d-bc92-fd3e597f8168"), "#800080", "Tím" },
                    { new Guid("a926b8ca-4074-4f5c-a8b8-5cc1f9765e9c"), "#C0C0C0", "Xám tro" },
                    { new Guid("cce6949d-a2b9-4b68-8c9d-915cf6428891"), "#FF0000", "Đỏ" },
                    { new Guid("cdea07a3-3606-4dbd-96f0-cc9903107410"), "#FFDAB9", "Hồng phấn" },
                    { new Guid("d9cbf0b2-6760-458d-bce6-80b9d56855c9"), "#0000FF", "Xanh dương" },
                    { new Guid("f9f92f5c-1d1b-40a6-853b-7b6116a05752"), "#FFFF00", "Vàng" }
                });

            migrationBuilder.InsertData(
                table: "Material",
                columns: new[] { "Id", "MaterialName" },
                values: new object[,]
                {
                    { new Guid("55cb8a7a-3fd0-49a4-8c84-6e3ba01830fc"), "Gỗ" },
                    { new Guid("5f206baf-74fe-4753-bee2-d5358ac87df2"), "Sắt" },
                    { new Guid("d634d377-f008-4516-8ffe-ae3254dff219"), "Nhựa pvc" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatus",
                columns: new[] { "Id", "OrderStatusName" },
                values: new object[,]
                {
                    { new Guid("00bb5049-ced0-4441-8331-74798d8c911b"), "Giao hàng thành công" },
                    { new Guid("07f2ba73-1eaa-47ab-8e9e-5f0e53c153e6"), "Yêu cầu trả hàng" },
                    { new Guid("256f86a3-eb15-46ea-b48d-a253cce723be"), "Chờ lấy hàng" },
                    { new Guid("597c062b-cba0-47b8-9f83-d6b61721f1ed"), "Chấp nhận trả hàng" },
                    { new Guid("64ed3ee4-b18c-4d0e-8bfc-4d3074bd2580"), "Đang được xử lý" },
                    { new Guid("6f7dee08-5951-44f5-98ee-6541242c3ea4"), "Đang giao hàng" },
                    { new Guid("b60b7d66-df4d-4a34-9801-e841a47f1cd3"), "Hủy đơn" },
                    { new Guid("d77f0a42-f384-4092-ac86-389ef24c703a"), "Giao hàng không thành công" }
                });

            migrationBuilder.InsertData(
                table: "Payment",
                columns: new[] { "Id", "PaymentName" },
                values: new object[,]
                {
                    { new Guid("3fc07bc0-159b-47c5-80f5-205c862e8ce7"), "Thanh toán Online" },
                    { new Guid("b2583742-33a8-44b6-9350-487f20261f6c"), "Thanh toán khi nhận hàng" }
                });

            migrationBuilder.InsertData(
                table: "Rank",
                columns: new[] { "Id", "Description", "Name", "PoinsMax", "PointsMin" },
                values: new object[,]
                {
                    { new Guid("548ee949-c5a0-47bb-bfaf-1aa65466385e"), null, "Kim Cương", 10000000, 3000001 },
                    { new Guid("b986ed6b-2011-4e30-89de-f845bf20489c"), null, "Bạc", 1000000, 0 },
                    { new Guid("db0c69c2-8fcb-41ad-9de0-892ca48ae5a5"), null, "Vàng", 3000000, 1000001 }
                });

            migrationBuilder.InsertData(
                table: "VoucherStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5f5bf4ff-e128-47e9-914b-e60f85d325c4"), "Used" },
                    { new Guid("af945a6b-a675-45ab-a065-2aa93e7a8be7"), "Expired" },
                    { new Guid("d1a75855-8aa3-4e46-be7c-58c26314fdac"), "Active" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1f6d8939-ae0c-4fdb-bf66-c6d017fcae4a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6381a7e-bc6e-4fc7-9e71-74fe84ffc8e8"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f0d825a9-60e6-4e76-90e1-e58185f37d88"));

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: new Guid("5a94ea4a-46fe-4db2-babc-7c009a736c67"));

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: new Guid("5aa8f30e-4822-4a6d-aff5-a66cae01b2a5"));

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: new Guid("67f26e63-00ef-46c0-b736-b769b1b3b890"));

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: new Guid("8c0b64d4-8176-4bc7-9795-f40e9bde946d"));

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: new Guid("f180bdeb-4587-41ae-af0d-3f87922f644c"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("578942ac-8c63-46a1-8364-336f5c50d4dd"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("7a8f965e-9fce-4291-96e0-8038a8e6b290"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("9d66eaac-20fa-45e1-a4d8-66f92f8308f8"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("c0749423-e4e6-4407-ac49-523e83c14e7c"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("ed08c89b-eeeb-42a9-a11d-9ad3f76132eb"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("017d4c19-c47c-42c0-9028-9974553734e4"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("29748565-2c9b-4851-9c6b-853dda79a928"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("2b43c23b-a74c-4e80-9ceb-47a7d82a1153"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("3821073e-a4f9-4f05-b802-395a61da25d3"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("46a7e77e-358a-47a9-89e6-f633f48511b2"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("4c22a2fd-ac8b-4fba-b103-1cb37013979a"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("523030ae-49fc-4f3c-acef-8b8f1bbeee44"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("769585ff-6d9e-4070-aebd-256248ed4146"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("7c0b62c7-a930-4a49-ae94-0e946bf2b105"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("89296303-7daf-4601-8c14-18b16b7ed3f0"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("9efe3f79-2523-403d-bc92-fd3e597f8168"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("a926b8ca-4074-4f5c-a8b8-5cc1f9765e9c"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("cce6949d-a2b9-4b68-8c9d-915cf6428891"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("cdea07a3-3606-4dbd-96f0-cc9903107410"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("d9cbf0b2-6760-458d-bce6-80b9d56855c9"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("f9f92f5c-1d1b-40a6-853b-7b6116a05752"));

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "Id",
                keyValue: new Guid("55cb8a7a-3fd0-49a4-8c84-6e3ba01830fc"));

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "Id",
                keyValue: new Guid("5f206baf-74fe-4753-bee2-d5358ac87df2"));

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "Id",
                keyValue: new Guid("d634d377-f008-4516-8ffe-ae3254dff219"));

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("00bb5049-ced0-4441-8331-74798d8c911b"));

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("07f2ba73-1eaa-47ab-8e9e-5f0e53c153e6"));

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("256f86a3-eb15-46ea-b48d-a253cce723be"));

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("597c062b-cba0-47b8-9f83-d6b61721f1ed"));

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("64ed3ee4-b18c-4d0e-8bfc-4d3074bd2580"));

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("6f7dee08-5951-44f5-98ee-6541242c3ea4"));

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("b60b7d66-df4d-4a34-9801-e841a47f1cd3"));

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("d77f0a42-f384-4092-ac86-389ef24c703a"));

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: new Guid("3fc07bc0-159b-47c5-80f5-205c862e8ce7"));

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: new Guid("b2583742-33a8-44b6-9350-487f20261f6c"));

            migrationBuilder.DeleteData(
                table: "Rank",
                keyColumn: "Id",
                keyValue: new Guid("548ee949-c5a0-47bb-bfaf-1aa65466385e"));

            migrationBuilder.DeleteData(
                table: "Rank",
                keyColumn: "Id",
                keyValue: new Guid("b986ed6b-2011-4e30-89de-f845bf20489c"));

            migrationBuilder.DeleteData(
                table: "Rank",
                keyColumn: "Id",
                keyValue: new Guid("db0c69c2-8fcb-41ad-9de0-892ca48ae5a5"));

            migrationBuilder.DeleteData(
                table: "VoucherStatus",
                keyColumn: "Id",
                keyValue: new Guid("5f5bf4ff-e128-47e9-914b-e60f85d325c4"));

            migrationBuilder.DeleteData(
                table: "VoucherStatus",
                keyColumn: "Id",
                keyValue: new Guid("af945a6b-a675-45ab-a065-2aa93e7a8be7"));

            migrationBuilder.DeleteData(
                table: "VoucherStatus",
                keyColumn: "Id",
                keyValue: new Guid("d1a75855-8aa3-4e46-be7c-58c26314fdac"));
        }
    }
}
