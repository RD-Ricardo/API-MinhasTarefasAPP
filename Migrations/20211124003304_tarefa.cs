using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTaskApp_Api.Migrations
{
    public partial class tarefa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tarefas",
                table: "Tarefas");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tarefas",
                newName: "IdTarefaApp");

            migrationBuilder.AlterColumn<int>(
                name: "IdTarefaApp",
                table: "Tarefas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "IdTarefaApi",
                table: "Tarefas",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Tarefas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tarefas",
                table: "Tarefas",
                column: "IdTarefaApi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tarefas",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "IdTarefaApi",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Tarefas");

            migrationBuilder.RenameColumn(
                name: "IdTarefaApp",
                table: "Tarefas",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Tarefas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tarefas",
                table: "Tarefas",
                column: "Id");
        }
    }
}
