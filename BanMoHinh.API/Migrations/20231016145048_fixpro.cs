using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BanMoHinh.API.Migrations
{
    public partial class fixpro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_VoucherStatus_VoucherStatusId",
                table: "Voucher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoucherStatus",
                table: "VoucherStatus");

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

            migrationBuilder.RenameTable(
                name: "VoucherStatus",
                newName: "voucherstatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_voucherstatus",
                table: "voucherstatus",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("5ac33c85-2750-4448-a853-13effc29f79d"), "3252562e-cb4d-4eff-8305-8f915ca77e53", "Admin", "ADMIN" },
                    { new Guid("88314876-bafe-4601-a479-0ba7f39a464b"), "49389960-c515-4cb4-a0df-c3661728b87a", "Guest", "GUEST" },
                    { new Guid("b9e9923c-3792-461b-8a25-5f268f6b1eeb"), "7f2afbf6-0726-4b51-b08a-0c87161048a1", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Brand",
                columns: new[] { "Id", "BrandName" },
                values: new object[,]
                {
                    { new Guid("7a9985bf-4ea5-4e97-8691-f603ed63161a"), "Brand 3" },
                    { new Guid("d935b78f-97e8-41a7-ab57-e1f8f9ed0ec5"), "Brand 5" },
                    { new Guid("d9cfb5bf-5d97-4096-8a62-aa19de46def1"), "Brand 1" },
                    { new Guid("eea176ac-2082-42e3-9b86-174825712d6b"), "Brand 4" },
                    { new Guid("f61644ab-b193-4fec-ba60-3a44367ab67c"), "Brand 2" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("09ad07e8-00bb-4b4c-8b7f-aa9d6b091564"), "Category 5" },
                    { new Guid("0f979cc1-d56e-4063-b91e-b86d7b36dcff"), "Category 2" },
                    { new Guid("a5866616-2061-42f0-ba4e-350e424b0c17"), "Category 1" },
                    { new Guid("b89b4b7c-a8dc-4d56-8b28-61737fe1bdb0"), "Category 3" },
                    { new Guid("fcece3ac-fefe-4da3-b990-2149aeb4e780"), "Category 4" }
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "ColorId", "ColorCode", "ColorName" },
                values: new object[,]
                {
                    { new Guid("1edd95a8-fd81-46c1-b3ff-8956b08d6db9"), "#FFA500", "Cam" },
                    { new Guid("25bb529a-45e7-4ef1-a4f6-98b893604d29"), "#0000FF", "Xanh dương" },
                    { new Guid("3254928c-2459-4f0e-ae14-91146a11982b"), "#000000", "Đen" },
                    { new Guid("3ede6672-5ac7-4d57-ac41-e9681cb00dcc"), "#FF0000", "Đỏ" },
                    { new Guid("5b238aba-9ed0-4c0d-9474-2373448995c6"), "#FFDAB9", "Hồng phấn" },
                    { new Guid("88b8f68f-ef8e-4cc0-b255-90875ec89c3f"), "#FFFFFF", "Trắng" },
                    { new Guid("8a3243ef-fb30-495d-ba87-a32093ce3fb3"), "#00BFFF", "Xanh da trời" },
                    { new Guid("8abb6cd4-8a42-472a-af3c-438d76e0cf90"), "#FFFF00", "Vàng" },
                    { new Guid("949d5035-2192-4d2a-99e3-00c60510b80b"), "#000080", "Xanh lam" },
                    { new Guid("ad54cb5d-d783-4120-9866-fe12c81b7e79"), "#00FF00", "Xanh lá cây" },
                    { new Guid("d0c92120-9311-4b60-8d69-bbe944621db6"), "#C0C0C0", "Xám tro" },
                    { new Guid("da2177cf-2cf2-462f-a1c5-be05ee6fab44"), "#808080", "Xám" },
                    { new Guid("e955fac9-973d-470b-9e74-f202fab80d45"), "#FFC0CB", "Hồng" },
                    { new Guid("eb9a005b-da5a-4271-82c4-07288ea057ae"), "#C0C0C0", "Bạc" },
                    { new Guid("ebaaa7ca-f0db-430d-bb2c-931b73b9040a"), "#800080", "Tím" },
                    { new Guid("f520ec07-516e-4ab4-be68-6d37dad900e0"), "#A52A2A", "Nâu" }
                });

            migrationBuilder.InsertData(
                table: "Material",
                columns: new[] { "Id", "MaterialName" },
                values: new object[,]
                {
                    { new Guid("1ba5bdd0-56db-41d4-a775-7d59b14b3d03"), "Sắt" },
                    { new Guid("9905dd34-d7b6-4240-bc1f-2ae80342f653"), "Nhựa pvc" },
                    { new Guid("a9d3abf6-6bbf-4331-86a1-32427b51b6ee"), "Gỗ" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatus",
                columns: new[] { "Id", "OrderStatusName" },
                values: new object[,]
                {
                    { new Guid("1cd0c3dd-9336-4954-ad05-41da797603b1"), "Giao hàng thành công" },
                    { new Guid("2958862d-cf75-47d9-a6bc-39d5515e1e04"), "Chờ lấy hàng" },
                    { new Guid("37c8362b-20f6-49a0-bd2f-61d86df89c6b"), "Giao hàng không thành công" },
                    { new Guid("46dfc364-d851-437a-9782-e8661531576b"), "Hủy đơn" },
                    { new Guid("55cac8f9-4097-49a6-90ff-7b1b319567e2"), "Chấp nhận trả hàng" },
                    { new Guid("7a50eeb3-a38b-4a90-be80-38a21e9aa6f9"), "Đang được xử lý" },
                    { new Guid("8b57033e-bf28-4637-80b9-1d93859c7913"), "Đang giao hàng" },
                    { new Guid("e923e42a-b6ba-4257-8276-7ce64e9faa7a"), "Yêu cầu trả hàng" }
                });

            migrationBuilder.InsertData(
                table: "Payment",
                columns: new[] { "Id", "PaymentName" },
                values: new object[,]
                {
                    { new Guid("23d62b80-bd99-4ca0-9f23-f2fa7b17dc40"), "Thanh toán khi nhận hàng" },
                    { new Guid("d99b4538-7ac9-4560-811f-92526f752179"), "Thanh toán Online" }
                });

            migrationBuilder.InsertData(
                table: "Rank",
                columns: new[] { "Id", "Description", "Name", "PoinsMax", "PointsMin" },
                values: new object[,]
                {
                    { new Guid("219bdb57-d0b2-4c61-8927-1bfcfd23a7ca"), null, "Vàng", 3000000, 1000001 },
                    { new Guid("be922356-07d1-4180-af7a-332bf8c1cdb0"), null, "Bạc", 1000000, 0 },
                    { new Guid("d5cff66d-bbb0-4f42-9809-25fb4e7a8f6f"), null, "Kim Cương", 10000000, 3000001 }
                });

            migrationBuilder.InsertData(
                table: "VoucherType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("152ff858-87c8-41fa-8326-0c5d308c6a45"), "Khánh hàng" },
                    { new Guid("b9d899a2-a0cf-4260-bff2-170f836fb228"), "Sản phẩm" }
                });

            migrationBuilder.InsertData(
                table: "voucherstatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("38a9932c-c96f-4fed-a79b-6f4013be449e"), "Active" },
                    { new Guid("8e496366-b277-49b5-a1f0-06eea038da01"), "Expired" },
                    { new Guid("eab5c8bb-296a-4ca2-bb2e-0e68478153ec"), "Used" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_voucherstatus_VoucherStatusId",
                table: "Voucher",
                column: "VoucherStatusId",
                principalTable: "voucherstatus",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_voucherstatus_VoucherStatusId",
                table: "Voucher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_voucherstatus",
                table: "voucherstatus");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5ac33c85-2750-4448-a853-13effc29f79d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("88314876-bafe-4601-a479-0ba7f39a464b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b9e9923c-3792-461b-8a25-5f268f6b1eeb"));

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: new Guid("7a9985bf-4ea5-4e97-8691-f603ed63161a"));

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: new Guid("d935b78f-97e8-41a7-ab57-e1f8f9ed0ec5"));

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: new Guid("d9cfb5bf-5d97-4096-8a62-aa19de46def1"));

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: new Guid("eea176ac-2082-42e3-9b86-174825712d6b"));

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: new Guid("f61644ab-b193-4fec-ba60-3a44367ab67c"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("09ad07e8-00bb-4b4c-8b7f-aa9d6b091564"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("0f979cc1-d56e-4063-b91e-b86d7b36dcff"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("a5866616-2061-42f0-ba4e-350e424b0c17"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("b89b4b7c-a8dc-4d56-8b28-61737fe1bdb0"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("fcece3ac-fefe-4da3-b990-2149aeb4e780"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("1edd95a8-fd81-46c1-b3ff-8956b08d6db9"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("25bb529a-45e7-4ef1-a4f6-98b893604d29"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("3254928c-2459-4f0e-ae14-91146a11982b"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("3ede6672-5ac7-4d57-ac41-e9681cb00dcc"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("5b238aba-9ed0-4c0d-9474-2373448995c6"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("88b8f68f-ef8e-4cc0-b255-90875ec89c3f"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("8a3243ef-fb30-495d-ba87-a32093ce3fb3"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("8abb6cd4-8a42-472a-af3c-438d76e0cf90"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("949d5035-2192-4d2a-99e3-00c60510b80b"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("ad54cb5d-d783-4120-9866-fe12c81b7e79"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("d0c92120-9311-4b60-8d69-bbe944621db6"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("da2177cf-2cf2-462f-a1c5-be05ee6fab44"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("e955fac9-973d-470b-9e74-f202fab80d45"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("eb9a005b-da5a-4271-82c4-07288ea057ae"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("ebaaa7ca-f0db-430d-bb2c-931b73b9040a"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("f520ec07-516e-4ab4-be68-6d37dad900e0"));

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "Id",
                keyValue: new Guid("1ba5bdd0-56db-41d4-a775-7d59b14b3d03"));

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "Id",
                keyValue: new Guid("9905dd34-d7b6-4240-bc1f-2ae80342f653"));

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "Id",
                keyValue: new Guid("a9d3abf6-6bbf-4331-86a1-32427b51b6ee"));

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("1cd0c3dd-9336-4954-ad05-41da797603b1"));

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("2958862d-cf75-47d9-a6bc-39d5515e1e04"));

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("37c8362b-20f6-49a0-bd2f-61d86df89c6b"));

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("46dfc364-d851-437a-9782-e8661531576b"));

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("55cac8f9-4097-49a6-90ff-7b1b319567e2"));

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("7a50eeb3-a38b-4a90-be80-38a21e9aa6f9"));

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("8b57033e-bf28-4637-80b9-1d93859c7913"));

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("e923e42a-b6ba-4257-8276-7ce64e9faa7a"));

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: new Guid("23d62b80-bd99-4ca0-9f23-f2fa7b17dc40"));

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: new Guid("d99b4538-7ac9-4560-811f-92526f752179"));

            migrationBuilder.DeleteData(
                table: "Rank",
                keyColumn: "Id",
                keyValue: new Guid("219bdb57-d0b2-4c61-8927-1bfcfd23a7ca"));

            migrationBuilder.DeleteData(
                table: "Rank",
                keyColumn: "Id",
                keyValue: new Guid("be922356-07d1-4180-af7a-332bf8c1cdb0"));

            migrationBuilder.DeleteData(
                table: "Rank",
                keyColumn: "Id",
                keyValue: new Guid("d5cff66d-bbb0-4f42-9809-25fb4e7a8f6f"));

            migrationBuilder.DeleteData(
                table: "VoucherType",
                keyColumn: "Id",
                keyValue: new Guid("152ff858-87c8-41fa-8326-0c5d308c6a45"));

            migrationBuilder.DeleteData(
                table: "VoucherType",
                keyColumn: "Id",
                keyValue: new Guid("b9d899a2-a0cf-4260-bff2-170f836fb228"));

            migrationBuilder.DeleteData(
                table: "voucherstatus",
                keyColumn: "Id",
                keyValue: new Guid("38a9932c-c96f-4fed-a79b-6f4013be449e"));

            migrationBuilder.DeleteData(
                table: "voucherstatus",
                keyColumn: "Id",
                keyValue: new Guid("8e496366-b277-49b5-a1f0-06eea038da01"));

            migrationBuilder.DeleteData(
                table: "voucherstatus",
                keyColumn: "Id",
                keyValue: new Guid("eab5c8bb-296a-4ca2-bb2e-0e68478153ec"));

            migrationBuilder.RenameTable(
                name: "voucherstatus",
                newName: "VoucherStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoucherStatus",
                table: "VoucherStatus",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_VoucherStatus_VoucherStatusId",
                table: "Voucher",
                column: "VoucherStatusId",
                principalTable: "VoucherStatus",
                principalColumn: "Id");
        }
    }
}
