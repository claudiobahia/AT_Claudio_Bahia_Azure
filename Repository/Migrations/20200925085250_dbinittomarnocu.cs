﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class dbinittomarnocu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Foto = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Foto = table.Column<string>(nullable: false),
                    PaisId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estado_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Amigo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Foto = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Sobrenome = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Telefone = table.Column<string>(nullable: false),
                    Dataaniversario = table.Column<string>(nullable: false),
                    PaisId = table.Column<int>(nullable: false),
                    EstadoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amigo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amigo_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estado",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Amigo_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Amizade",
                columns: table => new
                {
                    PessoaId = table.Column<int>(nullable: false),
                    AmigoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amizade", x => new { x.PessoaId, x.AmigoId });
                    table.ForeignKey(
                        name: "FK_Amizade_Amigo_AmigoId",
                        column: x => x.AmigoId,
                        principalTable: "Amigo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amigo_EstadoId",
                table: "Amigo",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Amigo_PaisId",
                table: "Amigo",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Amizade_AmigoId",
                table: "Amizade",
                column: "AmigoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estado_PaisId",
                table: "Estado",
                column: "PaisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amizade");

            migrationBuilder.DropTable(
                name: "Amigo");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "Pais");
        }
    }
}
