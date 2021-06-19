using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Morocoto.Infraestructure.Migrations
{
    public partial class Changes1962021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessType = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTaxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tax = table.Column<decimal>(type: "decimal(13,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTaxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LogMessage = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    UserDevice = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestState = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecurityQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SecurityQuestion = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserType = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    FullName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UserPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OsPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdentificationDocument = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserPassword = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    PIN = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    SecurityAnswer = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    SecurityQuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "User_SecurityQuestion_FK",
                        column: x => x.SecurityQuestionId,
                        principalTable: "SecurityQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "User_UserType_FK",
                        column: x => x.UserTypeId,
                        principalTable: "UserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreditAvailable = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "Customers_UserId_FK",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                    table.ForeignKey(
                        name: "Partner_UserId_FK",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    Province = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    City = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    Street1 = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: false),
                    Street2 = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UserAddresses_pk", x => new { x.Id, x.UserId });
                    table.ForeignKey(
                        name: "UserAddresses_UserId_fk",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPhoneNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UserPhoneNumbers_pk", x => new { x.Id, x.UserId });
                    table.ForeignKey(
                        name: "UserPhoneNumbers_UserId_FK",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerSenderId = table.Column<int>(type: "int", nullable: false),
                    CustomerRecieverId = table.Column<int>(type: "int", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreditTransfered = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    ConfirmationNumber = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    CustomerTaxesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Transactions_pk", x => new { x.Id, x.CustomerSenderId, x.CustomerRecieverId });
                    table.ForeignKey(
                        name: "ConfirmationNumber",
                        column: x => x.CustomerTaxesId,
                        principalTable: "CustomerTaxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Transactions_CustomerRecieverId_fk",
                        column: x => x.CustomerRecieverId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Transactions_CustomerSenderId_fk",
                        column: x => x.CustomerSenderId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartnerId = table.Column<int>(type: "int", nullable: false),
                    BusinessTypeId = table.Column<int>(type: "int", nullable: false),
                    BusinessNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BusinessName = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false),
                    BusinessCreditAvailable = table.Column<decimal>(type: "decimal(13,2)", precision: 13, scale: 2, nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                    table.ForeignKey(
                        name: "Business_BusinessTypeId_fk",
                        column: x => x.BusinessTypeId,
                        principalTable: "BusinessTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Business_partnerId_fk",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusinessAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    Province = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    City = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    Street1 = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: false),
                    Street2 = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BusinessAddresses_pk", x => new { x.Id, x.BusinessId });
                    table.ForeignKey(
                        name: "BusinessAddresses_BusinessId_fk",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusinessBills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<int>(type: "int", nullable: false),
                    PathFile = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessBills", x => x.Id);
                    table.ForeignKey(
                        name: "BusinessBills_BusinessId_fk",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusinessPhoneNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BusinessPhoneNumbers_pk", x => new { x.Id, x.BusinessId });
                    table.ForeignKey(
                        name: "BusinessPhoneNumbers_BusinessId_fk",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BuyCredits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CreditBought = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    CreditBoughtDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TransactionNumber = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    CustomerTaxId = table.Column<int>(type: "int", nullable: false),
                    PartnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BuyCredits_Pk", x => new { x.Id, x.BusinessId, x.CustomerId });
                    table.ForeignKey(
                        name: "BuyCredits_Business_Fk",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "BuyCredits_Customers_Fk",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "BuyCredits_CustomerTaxesId",
                        column: x => x.CustomerTaxId,
                        principalTable: "CustomerTaxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BuyCredits_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    RequestedCredit = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    RequestStateId = table.Column<int>(type: "int", nullable: false),
                    BusinessId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "Requests_RequestStates_Fk",
                        column: x => x.RequestStateId,
                        principalTable: "RequestStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "RequestStates_BusinessId_fk",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessAddresses_BusinessId",
                table: "BusinessAddresses",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessBills_BusinessId",
                table: "BusinessBills",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "Business_BusinessNumber_idx",
                table: "Businesses",
                column: "BusinessNumber",
                unique: true,
                filter: "[BusinessNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessTypeId",
                table: "Businesses",
                column: "BusinessTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_PartnerId",
                table: "Businesses",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessPhoneNumbers_BusinessId",
                table: "BusinessPhoneNumbers",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "BusinessType_idx",
                table: "BusinessTypes",
                column: "BusinessType",
                unique: true,
                filter: "[BusinessType] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BuyCredits_BusinessId",
                table: "BuyCredits",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyCredits_CustomerId",
                table: "BuyCredits",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyCredits_CustomerTaxId",
                table: "BuyCredits",
                column: "CustomerTaxId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyCredits_PartnerId",
                table: "BuyCredits",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_BusinessId",
                table: "Requests",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestStateId",
                table: "Requests",
                column: "RequestStateId");

            migrationBuilder.CreateIndex(
                name: "SecurityQuestions_idx",
                table: "SecurityQuestions",
                column: "SecurityQuestion",
                unique: true,
                filter: "[SecurityQuestion] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerRecieverId",
                table: "Transactions",
                column: "CustomerRecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerSenderId",
                table: "Transactions",
                column: "CustomerSenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerTaxesId",
                table: "Transactions",
                column: "CustomerTaxesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPhoneNumbers_UserId",
                table: "UserPhoneNumbers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SecurityQuestionId",
                table: "Users",
                column: "SecurityQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeId",
                table: "Users",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "User_Email_idx",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "User_IdentificationDocument_idx",
                table: "Users",
                column: "IdentificationDocument",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserType_idx",
                table: "UserTypes",
                column: "UserType",
                unique: true,
                filter: "[UserType] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessAddresses");

            migrationBuilder.DropTable(
                name: "BusinessBills");

            migrationBuilder.DropTable(
                name: "BusinessPhoneNumbers");

            migrationBuilder.DropTable(
                name: "BuyCredits");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UserAddresses");

            migrationBuilder.DropTable(
                name: "UserPhoneNumbers");

            migrationBuilder.DropTable(
                name: "RequestStates");

            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropTable(
                name: "CustomerTaxes");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "BusinessTypes");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SecurityQuestions");

            migrationBuilder.DropTable(
                name: "UserTypes");
        }
    }
}
