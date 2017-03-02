using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CrmHub.Infra.Data.Migrations
{
    public partial class AddLogApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeEntity");

            migrationBuilder.DropTable(
                name: "FieldMappingValue");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.CreateTable(
                name: "LogApi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Crm = table.Column<string>(nullable: false),
                    Entity = table.Column<string>(nullable: false),
                    Method = table.Column<string>(nullable: false),
                    Parameters = table.Column<string>(nullable: true),
                    Response = table.Column<string>(nullable: true),
                    Send = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogApi", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogApi");

            migrationBuilder.CreateTable(
                name: "AttributeEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Attribute = table.Column<string>(type: "varchar(150)", nullable: true),
                    CompanyId = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Entity = table.Column<string>(type: "varchar(150)", nullable: true),
                    Label = table.Column<string>(type: "varchar(150)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeEntity_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CNPJ = table.Column<string>(type: "varchar(14)", nullable: false),
                    Code = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CRMId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "varchar(250)", nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", nullable: false),
                    Password = table.Column<string>(maxLength: 65, nullable: false),
                    Token = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User = table.Column<string>(type: "varchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendor_Crm_CRMId",
                        column: x => x.CRMId,
                        principalTable: "Crm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vendor_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldMappingValue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(nullable: true),
                    CompanyId = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntityCrm = table.Column<string>(type: "varchar(150)", nullable: true),
                    EntityHub = table.Column<string>(type: "varchar(150)", nullable: true),
                    LabelCrm = table.Column<string>(type: "varchar(150)", nullable: true),
                    LabelHub = table.Column<string>(type: "varchar(150)", nullable: true),
                    NameCrm = table.Column<string>(type: "varchar(150)", nullable: true),
                    NameHub = table.Column<string>(type: "varchar(150)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldMappingValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldMappingValue_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FieldMappingValue_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeEntity_CompanyId",
                table: "AttributeEntity",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_CompanyId",
                table: "Client",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldMappingValue_ClientId",
                table: "FieldMappingValue",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldMappingValue_CompanyId",
                table: "FieldMappingValue",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_CRMId",
                table: "Vendor",
                column: "CRMId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_CompanyId",
                table: "Vendor",
                column: "CompanyId");
        }
    }
}
