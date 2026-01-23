using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class RefactorFreelanceSkillsConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_category_project_categories_categories_id",
                table: "category_project");

            migrationBuilder.DropForeignKey(
                name: "fk_category_project_projects_project_id",
                table: "category_project");

            migrationBuilder.DropForeignKey(
                name: "fk_freelancer_language_freelancers_freelancer_id",
                table: "freelancer_language");

            migrationBuilder.DropForeignKey(
                name: "fk_freelancer_language_language_languages_id",
                table: "freelancer_language");

            migrationBuilder.DropForeignKey(
                name: "fk_freelancers_skills_skill_skill_id",
                table: "freelancers_skills");

            migrationBuilder.DropPrimaryKey(
                name: "pk_freelancers_skills",
                table: "freelancers_skills");

            migrationBuilder.DropIndex(
                name: "ix_freelancers_skills_freelancer_id",
                table: "freelancers_skills");

            migrationBuilder.DropPrimaryKey(
                name: "pk_freelancer_language",
                table: "freelancer_language");

            migrationBuilder.DropPrimaryKey(
                name: "pk_category_project",
                table: "category_project");

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 249);

            migrationBuilder.DropColumn(
                name: "id",
                table: "freelancers_skills");

            migrationBuilder.RenameTable(
                name: "freelancer_language",
                newName: "freelancers_languages");

            migrationBuilder.RenameTable(
                name: "category_project",
                newName: "projects_categories");

            migrationBuilder.RenameColumn(
                name: "skill_id",
                table: "freelancers_skills",
                newName: "skills_id");

            migrationBuilder.RenameIndex(
                name: "ix_freelancers_skills_skill_id",
                table: "freelancers_skills",
                newName: "ix_freelancers_skills_skills_id");

            migrationBuilder.RenameIndex(
                name: "ix_freelancer_language_languages_id",
                table: "freelancers_languages",
                newName: "ix_freelancers_languages_languages_id");

            migrationBuilder.RenameIndex(
                name: "ix_category_project_project_id",
                table: "projects_categories",
                newName: "ix_projects_categories_project_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_freelancers_skills",
                table: "freelancers_skills",
                columns: new[] { "freelancer_id", "skills_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_freelancers_languages",
                table: "freelancers_languages",
                columns: new[] { "freelancer_id", "languages_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_projects_categories",
                table: "projects_categories",
                columns: new[] { "categories_id", "project_id" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 7,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 8,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 9,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 10,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 11,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 12,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 13,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 14,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 15,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 16,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 17,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 18,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 19,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 20,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 21,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 22,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 23,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 24,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 25,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 26,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 27,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 28,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 29,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 30,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 31,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 32,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 33,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 34,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 35,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 36,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 37,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 38,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 39,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 40,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 41,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 42,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 43,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 44,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 45,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 46,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 47,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 48,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 49,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 50,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 51,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 52,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 53,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 54,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 55,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 56,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 57,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 58,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 59,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 60,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 61,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 62,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 63,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 64,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 65,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 66,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 67,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 68,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 69,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 70,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 71,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 72,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 73,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 74,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 75,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 76,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 77,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 78,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 79,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 80,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 81,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 82,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 83,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 84,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 85,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 86,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 87,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 88,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 89,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 90,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 91,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 92,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 93,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 94,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 95,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 96,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 97,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 98,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 99,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 100,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 101,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 102,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 103,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 104,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 105,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 106,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 107,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 108,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 109,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 110,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 111,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 112,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 113,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 114,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 115,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 116,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 117,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 118,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 119,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 120,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 121,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 122,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 123,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 124,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 125,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 126,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 127,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 128,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 129,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 130,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 131,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 132,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 133,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 134,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 135,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 136,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 137,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 138,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 139,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 140,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 141,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 142,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 143,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 144,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 145,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 146,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 147,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 148,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 149,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 150,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 151,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 152,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 153,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 154,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 155,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 156,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 157,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 158,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 159,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 160,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 161,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 162,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 163,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 164,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 165,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 166,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 167,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 168,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 169,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 170,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 171,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 172,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 173,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 174,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 175,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 176,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 177,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 178,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 179,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 180,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 181,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 182,
                columns: new[] { "code", "name" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 1, 23, 15, 36, 5, 301, DateTimeKind.Utc).AddTicks(4749), new DateTime(2026, 1, 23, 15, 36, 5, 301, DateTimeKind.Utc).AddTicks(4764), "81BF3CA445D4EEA4C8334D94415E24C559EFB914EE5D42B5DA4068B69A890A1B-9745F9395C238F5F2A826D5FDF268AE4" });

            migrationBuilder.AddForeignKey(
                name: "fk_freelancers_languages_freelancers_freelancer_id",
                table: "freelancers_languages",
                column: "freelancer_id",
                principalTable: "freelancers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freelancers_languages_language_languages_id",
                table: "freelancers_languages",
                column: "languages_id",
                principalTable: "languages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freelancers_skills_skill_skills_id",
                table: "freelancers_skills",
                column: "skills_id",
                principalTable: "skills",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_projects_categories_categories_categories_id",
                table: "projects_categories",
                column: "categories_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_projects_categories_projects_project_id",
                table: "projects_categories",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_freelancers_languages_freelancers_freelancer_id",
                table: "freelancers_languages");

            migrationBuilder.DropForeignKey(
                name: "fk_freelancers_languages_language_languages_id",
                table: "freelancers_languages");

            migrationBuilder.DropForeignKey(
                name: "fk_freelancers_skills_skill_skills_id",
                table: "freelancers_skills");

            migrationBuilder.DropForeignKey(
                name: "fk_projects_categories_categories_categories_id",
                table: "projects_categories");

            migrationBuilder.DropForeignKey(
                name: "fk_projects_categories_projects_project_id",
                table: "projects_categories");

            migrationBuilder.DropPrimaryKey(
                name: "pk_freelancers_skills",
                table: "freelancers_skills");

            migrationBuilder.DropPrimaryKey(
                name: "pk_projects_categories",
                table: "projects_categories");

            migrationBuilder.DropPrimaryKey(
                name: "pk_freelancers_languages",
                table: "freelancers_languages");

            migrationBuilder.RenameTable(
                name: "projects_categories",
                newName: "category_project");

            migrationBuilder.RenameTable(
                name: "freelancers_languages",
                newName: "freelancer_language");

            migrationBuilder.RenameColumn(
                name: "skills_id",
                table: "freelancers_skills",
                newName: "skill_id");

            migrationBuilder.RenameIndex(
                name: "ix_freelancers_skills_skills_id",
                table: "freelancers_skills",
                newName: "ix_freelancers_skills_skill_id");

            migrationBuilder.RenameIndex(
                name: "ix_projects_categories_project_id",
                table: "category_project",
                newName: "ix_category_project_project_id");

            migrationBuilder.RenameIndex(
                name: "ix_freelancers_languages_languages_id",
                table: "freelancer_language",
                newName: "ix_freelancer_language_languages_id");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "freelancers_skills",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "pk_freelancers_skills",
                table: "freelancers_skills",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_category_project",
                table: "category_project",
                columns: new[] { "categories_id", "project_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_freelancer_language",
                table: "freelancer_language",
                columns: new[] { "freelancer_id", "languages_id" });

            migrationBuilder.InsertData(
                table: "countries",
                columns: new[] { "id", "alpha2code", "alpha3code", "name" },
                values: new object[,]
                {
                    { 1, "AF", "AFG", "Afghanistan" },
                    { 2, "AL", "ALB", "Albania" },
                    { 3, "DZ", "DZA", "Algeria" },
                    { 4, "AS", "ASM", "American Samoa" },
                    { 5, "AD", "AND", "Andorra" },
                    { 6, "AO", "AGO", "Angola" },
                    { 7, "AI", "AIA", "Anguilla" },
                    { 8, "AQ", "ATA", "Antarctica" },
                    { 9, "AG", "ATG", "Antigua and Barbuda" },
                    { 10, "AR", "ARG", "Argentina" },
                    { 11, "AM", "ARM", "Armenia" },
                    { 12, "AW", "ABW", "Aruba" },
                    { 13, "AU", "AUS", "Australia" },
                    { 14, "AT", "AUT", "Austria" },
                    { 15, "AZ", "AZE", "Azerbaijan" },
                    { 16, "BS", "BHS", "Bahamas" },
                    { 17, "BH", "BHR", "Bahrain" },
                    { 18, "BD", "BGD", "Bangladesh" },
                    { 19, "BB", "BRB", "Barbados" },
                    { 20, "BY", "BLR", "Belarus" },
                    { 21, "BE", "BEL", "Belgium" },
                    { 22, "BZ", "BLZ", "Belize" },
                    { 23, "BJ", "BEN", "Benin" },
                    { 24, "BM", "BMU", "Bermuda" },
                    { 25, "BT", "BTN", "Bhutan" },
                    { 26, "BO", "BOL", "Bolivia" },
                    { 27, "BQ", "BES", "Bonaire, Sint Eustatius and Saba" },
                    { 28, "BA", "BIH", "Bosnia and Herzegovina" },
                    { 29, "BW", "BWA", "Botswana" },
                    { 30, "BV", "BVT", "Bouvet Island" },
                    { 31, "BR", "BRA", "Brazil" },
                    { 32, "IO", "IOT", "British Indian Ocean Territory" },
                    { 33, "BN", "BRN", "Brunei Darussalam" },
                    { 34, "BG", "BGR", "Bulgaria" },
                    { 35, "BF", "BFA", "Burkina Faso" },
                    { 36, "BI", "BDI", "Burundi" },
                    { 37, "KH", "KHM", "Cambodia" },
                    { 38, "CM", "CMR", "Cameroon" },
                    { 39, "CA", "CAN", "Canada" },
                    { 40, "CV", "CPV", "Cape Verde" },
                    { 41, "KY", "CYM", "Cayman Islands" },
                    { 42, "CF", "CAF", "Central African Republic" },
                    { 43, "TD", "TCD", "Chad" },
                    { 44, "CL", "CHL", "Chile" },
                    { 45, "CN", "CHN", "China" },
                    { 46, "CX", "CXR", "Christmas Island" },
                    { 47, "CC", "CCK", "Cocos (Keeling) Islands" },
                    { 48, "CO", "COL", "Colombia" },
                    { 49, "KM", "COM", "Comoros" },
                    { 50, "CD", "COD", "Congo" },
                    { 51, "CG", "COG", "Congo" },
                    { 52, "CK", "COK", "Cook Islands" },
                    { 53, "CR", "CRI", "Costa Rica" },
                    { 54, "HR", "HRV", "Croatia" },
                    { 55, "CU", "CUB", "Cuba" },
                    { 56, "CW", "CUW", "Curaçao" },
                    { 57, "CY", "CYP", "Cyprus" },
                    { 58, "CZ", "CZE", "Czech Republic" },
                    { 59, "CI", "CIV", "Côte D'Ivoire" },
                    { 60, "DK", "DNK", "Denmark" },
                    { 61, "DJ", "DJI", "Djibouti" },
                    { 62, "DM", "DMA", "Dominica" },
                    { 63, "DO", "DOM", "Dominican Republic" },
                    { 64, "EC", "ECU", "Ecuador" },
                    { 65, "EG", "EGY", "Egypt" },
                    { 66, "SV", "SLV", "El Salvador" },
                    { 67, "GQ", "GNQ", "Equatorial Guinea" },
                    { 68, "ER", "ERI", "Eritrea" },
                    { 69, "EE", "EST", "Estonia" },
                    { 70, "ET", "ETH", "Ethiopia" },
                    { 71, "FK", "FLK", "Falkland Islands (Malvinas)" },
                    { 72, "FO", "FRO", "Faroe Islands" },
                    { 73, "FJ", "FJI", "Fiji" },
                    { 74, "FI", "FIN", "Finland" },
                    { 75, "FR", "FRA", "France" },
                    { 76, "GF", "GUF", "French Guiana" },
                    { 77, "PF", "PYF", "French Polynesia" },
                    { 78, "TF", "ATF", "French Southern Territories" },
                    { 79, "GA", "GAB", "Gabon" },
                    { 80, "GM", "GMB", "Gambia" },
                    { 81, "GE", "GEO", "Georgia" },
                    { 82, "DE", "DEU", "Germany" },
                    { 83, "GH", "GHA", "Ghana" },
                    { 84, "GI", "GIB", "Gibraltar" },
                    { 85, "GR", "GRC", "Greece" },
                    { 86, "GL", "GRL", "Greenland" },
                    { 87, "GD", "GRD", "Grenada" },
                    { 88, "GP", "GLP", "Guadeloupe" },
                    { 89, "GU", "GUM", "Guam" },
                    { 90, "GT", "GTM", "Guatemala" },
                    { 91, "GG", "GGY", "Guernsey" },
                    { 92, "GN", "GIN", "Guinea" },
                    { 93, "GW", "GNB", "Guinea-Bissau" },
                    { 94, "GY", "GUY", "Guyana" },
                    { 95, "HT", "HTI", "Haiti" },
                    { 96, "HM", "HMD", "Heard Island and Mcdonald Islands" },
                    { 97, "HN", "HND", "Honduras" },
                    { 98, "HK", "HKG", "Hong Kong" },
                    { 99, "HU", "HUN", "Hungary" },
                    { 100, "IS", "ISL", "Iceland" },
                    { 101, "IN", "IND", "India" },
                    { 102, "ID", "IDN", "Indonesia" },
                    { 103, "IR", "IRN", "Iran" },
                    { 104, "IQ", "IRQ", "Iraq" },
                    { 105, "IE", "IRL", "Ireland" },
                    { 106, "IM", "IMN", "Isle of Man" },
                    { 107, "IL", "ISR", "Israel" },
                    { 108, "IT", "ITA", "Italy" },
                    { 109, "JM", "JAM", "Jamaica" },
                    { 110, "JP", "JPN", "Japan" },
                    { 111, "JE", "JEY", "Jersey" },
                    { 112, "JO", "JOR", "Jordan" },
                    { 113, "KZ", "KAZ", "Kazakhstan" },
                    { 114, "KE", "KEN", "Kenya" },
                    { 115, "KI", "KIR", "Kiribati" },
                    { 116, "KW", "KWT", "Kuwait" },
                    { 117, "KG", "KGZ", "Kyrgyzstan" },
                    { 118, "LA", "LAO", "Lao People's Democratic Republic" },
                    { 119, "LV", "LVA", "Latvia" },
                    { 120, "LB", "LBN", "Lebanon" },
                    { 121, "LS", "LSO", "Lesotho" },
                    { 122, "LR", "LBR", "Liberia" },
                    { 123, "LY", "LBY", "Libya" },
                    { 124, "LI", "LIE", "Liechtenstein" },
                    { 125, "LT", "LTU", "Lithuania" },
                    { 126, "LU", "LUX", "Luxembourg" },
                    { 127, "MO", "MAC", "Macao" },
                    { 128, "MK", "MKD", "Macedonia" },
                    { 129, "MG", "MDG", "Madagascar" },
                    { 130, "MW", "MWI", "Malawi" },
                    { 131, "MY", "MYS", "Malaysia" },
                    { 132, "MV", "MDV", "Maldives" },
                    { 133, "ML", "MLI", "Mali" },
                    { 134, "MT", "MLT", "Malta" },
                    { 135, "MH", "MHL", "Marshall Islands" },
                    { 136, "MQ", "MTQ", "Martinique" },
                    { 137, "MR", "MRT", "Mauritania" },
                    { 138, "MU", "MUS", "Mauritius" },
                    { 139, "YT", "MYT", "Mayotte" },
                    { 140, "MX", "MEX", "Mexico" },
                    { 141, "FM", "FSM", "Micronesia" },
                    { 142, "MD", "MDA", "Moldova" },
                    { 143, "MC", "MCO", "Monaco" },
                    { 144, "MN", "MNG", "Mongolia" },
                    { 145, "ME", "MNE", "Montenegro" },
                    { 146, "MS", "MSR", "Montserrat" },
                    { 147, "MA", "MAR", "Morocco" },
                    { 148, "MZ", "MOZ", "Mozambique" },
                    { 149, "MM", "MMR", "Myanmar" },
                    { 150, "NA", "NAM", "Namibia" },
                    { 151, "NR", "NRU", "Nauru" },
                    { 152, "NP", "NPL", "Nepal" },
                    { 153, "NL", "NLD", "Netherlands" },
                    { 154, "NC", "NCL", "New Caledonia" },
                    { 155, "NZ", "NZL", "New Zealand" },
                    { 156, "NI", "NIC", "Nicaragua" },
                    { 157, "NE", "NER", "Niger" },
                    { 158, "NG", "NGA", "Nigeria" },
                    { 159, "NU", "NIU", "Niue" },
                    { 160, "NF", "NFK", "Norfolk Island" },
                    { 161, "KP", "PRK", "North Korea" },
                    { 162, "MP", "MNP", "Northern Mariana Islands" },
                    { 163, "NO", "NOR", "Norway" },
                    { 164, "OM", "OMN", "Oman" },
                    { 165, "PK", "PAK", "Pakistan" },
                    { 166, "PW", "PLW", "Palau" },
                    { 167, "PS", "PSE", "Palestinian Territory" },
                    { 168, "PA", "PAN", "Panama" },
                    { 169, "PG", "PNG", "Papua New Guinea" },
                    { 170, "PY", "PRY", "Paraguay" },
                    { 171, "PE", "PER", "Peru" },
                    { 172, "PH", "PHL", "Philippines" },
                    { 173, "PN", "PCN", "Pitcairn" },
                    { 174, "PL", "POL", "Poland" },
                    { 175, "PT", "PRT", "Portugal" },
                    { 176, "PR", "PRI", "Puerto Rico" },
                    { 177, "QA", "QAT", "Qatar" },
                    { 178, "RO", "ROU", "Romania" },
                    { 179, "RU", "RUS", "Russia" },
                    { 180, "RW", "RWA", "Rwanda" },
                    { 181, "RE", "REU", "Réunion" },
                    { 182, "BL", "BLM", "Saint Barthélemy" },
                    { 183, "SH", "SHN", "Saint Helena, Ascension and Tristan Da Cunha" },
                    { 184, "KN", "KNA", "Saint Kitts and Nevis" },
                    { 185, "LC", "LCA", "Saint Lucia" },
                    { 186, "MF", "MAF", "Saint Martin (French Part)" },
                    { 187, "PM", "SPM", "Saint Pierre and Miquelon" },
                    { 188, "VC", "VCT", "Saint Vincent and The Grenadines" },
                    { 189, "WS", "WSM", "Samoa" },
                    { 190, "SM", "SMR", "San Marino" },
                    { 191, "ST", "STP", "Sao Tome and Principe" },
                    { 192, "SA", "SAU", "Saudi Arabia" },
                    { 193, "SN", "SEN", "Senegal" },
                    { 194, "RS", "SRB", "Serbia" },
                    { 195, "SC", "SYC", "Seychelles" },
                    { 196, "SL", "SLE", "Sierra Leone" },
                    { 197, "SG", "SGP", "Singapore" },
                    { 198, "SX", "SXM", "Sint Maarten (Dutch Part)" },
                    { 199, "SK", "SVK", "Slovakia" },
                    { 200, "SI", "SVN", "Slovenia" },
                    { 201, "SB", "SLB", "Solomon Islands" },
                    { 202, "SO", "SOM", "Somalia" },
                    { 203, "ZA", "ZAF", "South Africa" },
                    { 204, "GS", "SGS", "South Georgia" },
                    { 205, "KR", "KOR", "South Korea" },
                    { 206, "SS", "SSD", "South Sudan" },
                    { 207, "ES", "ESP", "Spain" },
                    { 208, "LK", "LKA", "Sri Lanka" },
                    { 209, "SD", "SDN", "Sudan" },
                    { 210, "SR", "SUR", "Suriname" },
                    { 211, "SJ", "SJM", "Svalbard and Jan Mayen" },
                    { 212, "SZ", "SWZ", "Swaziland" },
                    { 213, "SE", "SWE", "Sweden" },
                    { 214, "CH", "CHE", "Switzerland" },
                    { 215, "SY", "SYR", "Syrian Arab Republic" },
                    { 216, "TW", "TWN", "Taiwan" },
                    { 217, "TJ", "TJK", "Tajikistan" },
                    { 218, "TZ", "TZA", "Tanzania" },
                    { 219, "TH", "THA", "Thailand" },
                    { 220, "TL", "TLS", "Timor-Leste" },
                    { 221, "TG", "TGO", "Togo" },
                    { 222, "TK", "TKL", "Tokelau" },
                    { 223, "TO", "TON", "Tonga" },
                    { 224, "TT", "TTO", "Trinidad and Tobago" },
                    { 225, "TN", "TUN", "Tunisia" },
                    { 226, "TR", "TUR", "Turkey" },
                    { 227, "TM", "TKM", "Turkmenistan" },
                    { 228, "TC", "TCA", "Turks and Caicos Islands" },
                    { 229, "TV", "TUV", "Tuvalu" },
                    { 230, "UG", "UGA", "Uganda" },
                    { 231, "UA", "UKR", "Ukraine" },
                    { 232, "AE", "ARE", "United Arab Emirates" },
                    { 233, "GB", "GBR", "United Kingdom" },
                    { 234, "US", "USA", "United States" },
                    { 235, "UM", "UMI", "United States Minor Outlying Islands" },
                    { 236, "UY", "URY", "Uruguay" },
                    { 237, "UZ", "UZB", "Uzbekistan" },
                    { 238, "VU", "VUT", "Vanuatu" },
                    { 239, "VA", "VAT", "Vatican City" },
                    { 240, "VE", "VEN", "Venezuela" },
                    { 241, "VN", "VNM", "Viet Nam" },
                    { 242, "VG", "VGB", "Virgin Islands, British" },
                    { 243, "VI", "VIR", "Virgin Islands, U.S." },
                    { 244, "WF", "WLF", "Wallis and Futuna" },
                    { 245, "EH", "ESH", "Western Sahara" },
                    { 246, "YE", "YEM", "Yemen" },
                    { 247, "ZM", "ZMB", "Zambia" },
                    { 248, "ZW", "ZWE", "Zimbabwe" },
                    { 249, "AX", "ALA", "Åland Islands" }
                });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "code", "name" },
                values: new object[] { "aa", "Afar" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "code", "name" },
                values: new object[] { "ab", "Abkhazian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "code", "name" },
                values: new object[] { "ae", "Avestan" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "code", "name" },
                values: new object[] { "af", "Afrikaans" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "code", "name" },
                values: new object[] { "ak", "Akan" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "code", "name" },
                values: new object[] { "am", "Amharic" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 7,
                columns: new[] { "code", "name" },
                values: new object[] { "an", "Aragonese" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 8,
                columns: new[] { "code", "name" },
                values: new object[] { "ar", "Arabic" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 9,
                columns: new[] { "code", "name" },
                values: new object[] { "as", "Assamese" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 10,
                columns: new[] { "code", "name" },
                values: new object[] { "av", "Avaric" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 11,
                columns: new[] { "code", "name" },
                values: new object[] { "ay", "Aymara" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 12,
                columns: new[] { "code", "name" },
                values: new object[] { "az", "Azerbaijani" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 13,
                columns: new[] { "code", "name" },
                values: new object[] { "ba", "Bashkir" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 14,
                columns: new[] { "code", "name" },
                values: new object[] { "be", "Belarusian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 15,
                columns: new[] { "code", "name" },
                values: new object[] { "bg", "Bulgarian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 16,
                columns: new[] { "code", "name" },
                values: new object[] { "bh", "Bihari languages" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 17,
                columns: new[] { "code", "name" },
                values: new object[] { "bi", "Bislama" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 18,
                columns: new[] { "code", "name" },
                values: new object[] { "bm", "Bambara" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 19,
                columns: new[] { "code", "name" },
                values: new object[] { "bn", "Bengali" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 20,
                columns: new[] { "code", "name" },
                values: new object[] { "bo", "Tibetan" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 21,
                columns: new[] { "code", "name" },
                values: new object[] { "br", "Breton" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 22,
                columns: new[] { "code", "name" },
                values: new object[] { "bs", "Bosnian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 23,
                columns: new[] { "code", "name" },
                values: new object[] { "ca", "Catalan; Valencian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 24,
                columns: new[] { "code", "name" },
                values: new object[] { "ce", "Chechen" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 25,
                columns: new[] { "code", "name" },
                values: new object[] { "ch", "Chamorro" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 26,
                columns: new[] { "code", "name" },
                values: new object[] { "co", "Corsican" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 27,
                columns: new[] { "code", "name" },
                values: new object[] { "cr", "Cree" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 28,
                columns: new[] { "code", "name" },
                values: new object[] { "cs", "Czech" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 29,
                columns: new[] { "code", "name" },
                values: new object[] { "cv", "Chuvash" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 30,
                columns: new[] { "code", "name" },
                values: new object[] { "cy", "Welsh" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 31,
                columns: new[] { "code", "name" },
                values: new object[] { "da", "Danish" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 32,
                columns: new[] { "code", "name" },
                values: new object[] { "de", "German" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 33,
                columns: new[] { "code", "name" },
                values: new object[] { "dv", "Divehi; Dhivehi; Maldivian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 34,
                columns: new[] { "code", "name" },
                values: new object[] { "dz", "Dzongkha" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 35,
                columns: new[] { "code", "name" },
                values: new object[] { "ee", "Ewe" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 36,
                columns: new[] { "code", "name" },
                values: new object[] { "el", "Greek, Modern (1453-)" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 37,
                columns: new[] { "code", "name" },
                values: new object[] { "en", "English" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 38,
                columns: new[] { "code", "name" },
                values: new object[] { "eo", "Esperanto" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 39,
                columns: new[] { "code", "name" },
                values: new object[] { "es", "Spanish; Castilian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 40,
                columns: new[] { "code", "name" },
                values: new object[] { "et", "Estonian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 41,
                columns: new[] { "code", "name" },
                values: new object[] { "eu", "Basque" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 42,
                columns: new[] { "code", "name" },
                values: new object[] { "fa", "Persian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 43,
                columns: new[] { "code", "name" },
                values: new object[] { "ff", "Fulah" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 44,
                columns: new[] { "code", "name" },
                values: new object[] { "fi", "Finnish" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 45,
                columns: new[] { "code", "name" },
                values: new object[] { "fj", "Fijian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 46,
                columns: new[] { "code", "name" },
                values: new object[] { "fo", "Faroese" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 47,
                columns: new[] { "code", "name" },
                values: new object[] { "fr", "French" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 48,
                columns: new[] { "code", "name" },
                values: new object[] { "fy", "Western Frisian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 49,
                columns: new[] { "code", "name" },
                values: new object[] { "ga", "Irish" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 50,
                columns: new[] { "code", "name" },
                values: new object[] { "gd", "Gaelic; Scomttish Gaelic" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 51,
                columns: new[] { "code", "name" },
                values: new object[] { "gl", "Galician" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 52,
                columns: new[] { "code", "name" },
                values: new object[] { "gn", "Guarani" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 53,
                columns: new[] { "code", "name" },
                values: new object[] { "gu", "Gujarati" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 54,
                columns: new[] { "code", "name" },
                values: new object[] { "gv", "Manx" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 55,
                columns: new[] { "code", "name" },
                values: new object[] { "ha", "Hausa" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 56,
                columns: new[] { "code", "name" },
                values: new object[] { "he", "Hebrew" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 57,
                columns: new[] { "code", "name" },
                values: new object[] { "hi", "Hindi" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 58,
                columns: new[] { "code", "name" },
                values: new object[] { "ho", "Hiri Motu" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 59,
                columns: new[] { "code", "name" },
                values: new object[] { "hr", "Croatian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 60,
                columns: new[] { "code", "name" },
                values: new object[] { "ht", "Haitian; Haitian Creole" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 61,
                columns: new[] { "code", "name" },
                values: new object[] { "hu", "Hungarian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 62,
                columns: new[] { "code", "name" },
                values: new object[] { "hy", "Armenian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 63,
                columns: new[] { "code", "name" },
                values: new object[] { "hz", "Herero" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 64,
                columns: new[] { "code", "name" },
                values: new object[] { "id", "Indonesian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 65,
                columns: new[] { "code", "name" },
                values: new object[] { "ie", "Interlingue; Occidental" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 66,
                columns: new[] { "code", "name" },
                values: new object[] { "ig", "Igbo" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 67,
                columns: new[] { "code", "name" },
                values: new object[] { "ii", "Sichuan Yi; Nuosu" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 68,
                columns: new[] { "code", "name" },
                values: new object[] { "ik", "Inupiaq" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 69,
                columns: new[] { "code", "name" },
                values: new object[] { "io", "Ido" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 70,
                columns: new[] { "code", "name" },
                values: new object[] { "is", "Icelandic" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 71,
                columns: new[] { "code", "name" },
                values: new object[] { "it", "Italian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 72,
                columns: new[] { "code", "name" },
                values: new object[] { "iu", "Inuktitut" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 73,
                columns: new[] { "code", "name" },
                values: new object[] { "ja", "Japanese" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 74,
                columns: new[] { "code", "name" },
                values: new object[] { "jv", "Javanese" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 75,
                columns: new[] { "code", "name" },
                values: new object[] { "ka", "Georgian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 76,
                columns: new[] { "code", "name" },
                values: new object[] { "kg", "Kongo" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 77,
                columns: new[] { "code", "name" },
                values: new object[] { "ki", "Kikuyu; Gikuyu" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 78,
                columns: new[] { "code", "name" },
                values: new object[] { "kj", "Kuanyama; Kwanyama" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 79,
                columns: new[] { "code", "name" },
                values: new object[] { "kk", "Kazakh" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 80,
                columns: new[] { "code", "name" },
                values: new object[] { "kl", "Kalaallisut; Greenlandic" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 81,
                columns: new[] { "code", "name" },
                values: new object[] { "km", "Central Khmer" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 82,
                columns: new[] { "code", "name" },
                values: new object[] { "kn", "Kannada" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 83,
                columns: new[] { "code", "name" },
                values: new object[] { "ko", "Korean" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 84,
                columns: new[] { "code", "name" },
                values: new object[] { "kr", "Kanuri" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 85,
                columns: new[] { "code", "name" },
                values: new object[] { "ks", "Kashmiri" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 86,
                columns: new[] { "code", "name" },
                values: new object[] { "ku", "Kurdish" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 87,
                columns: new[] { "code", "name" },
                values: new object[] { "kv", "Komi" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 88,
                columns: new[] { "code", "name" },
                values: new object[] { "kw", "Cornish" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 89,
                columns: new[] { "code", "name" },
                values: new object[] { "ky", "Kirghiz; Kyrgyz" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 90,
                columns: new[] { "code", "name" },
                values: new object[] { "la", "Latin" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 91,
                columns: new[] { "code", "name" },
                values: new object[] { "lb", "Luxembourgish; Letzeburgesch" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 92,
                columns: new[] { "code", "name" },
                values: new object[] { "lg", "Ganda" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 93,
                columns: new[] { "code", "name" },
                values: new object[] { "li", "Limburgan; Limburger; Limburgish" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 94,
                columns: new[] { "code", "name" },
                values: new object[] { "ln", "Lingala" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 95,
                columns: new[] { "code", "name" },
                values: new object[] { "lo", "Lao" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 96,
                columns: new[] { "code", "name" },
                values: new object[] { "lt", "Lithuanian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 97,
                columns: new[] { "code", "name" },
                values: new object[] { "lu", "Luba-Katanga" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 98,
                columns: new[] { "code", "name" },
                values: new object[] { "lv", "Latvian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 99,
                columns: new[] { "code", "name" },
                values: new object[] { "mg", "Malagasy" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 100,
                columns: new[] { "code", "name" },
                values: new object[] { "mh", "Marshallese" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 101,
                columns: new[] { "code", "name" },
                values: new object[] { "mi", "Maori" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 102,
                columns: new[] { "code", "name" },
                values: new object[] { "mk", "Macedonian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 103,
                columns: new[] { "code", "name" },
                values: new object[] { "ml", "Malayalam" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 104,
                columns: new[] { "code", "name" },
                values: new object[] { "mn", "Mongolian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 105,
                columns: new[] { "code", "name" },
                values: new object[] { "mr", "Marathi" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 106,
                columns: new[] { "code", "name" },
                values: new object[] { "ms", "Malay" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 107,
                columns: new[] { "code", "name" },
                values: new object[] { "mt", "Maltese" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 108,
                columns: new[] { "code", "name" },
                values: new object[] { "my", "Burmese" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 109,
                columns: new[] { "code", "name" },
                values: new object[] { "na", "Nauru" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 110,
                columns: new[] { "code", "name" },
                values: new object[] { "nb", "Bokmål, Norwegian; Norwegian Bokmål" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 111,
                columns: new[] { "code", "name" },
                values: new object[] { "nd", "Ndebele, North; North Ndebele" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 112,
                columns: new[] { "code", "name" },
                values: new object[] { "ne", "Nepali" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 113,
                columns: new[] { "code", "name" },
                values: new object[] { "ng", "Ndonga" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 114,
                columns: new[] { "code", "name" },
                values: new object[] { "nl", "Dutch; Flemish" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 115,
                columns: new[] { "code", "name" },
                values: new object[] { "nn", "Norwegian Nynorsk; Nynorsk, Norwegian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 116,
                columns: new[] { "code", "name" },
                values: new object[] { "no", "Norwegian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 117,
                columns: new[] { "code", "name" },
                values: new object[] { "nr", "Ndebele, South; South Ndebele" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 118,
                columns: new[] { "code", "name" },
                values: new object[] { "nv", "Navajo; Navaho" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 119,
                columns: new[] { "code", "name" },
                values: new object[] { "ny", "Chichewa; Chewa; Nyanja" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 120,
                columns: new[] { "code", "name" },
                values: new object[] { "oc", "Occitan (post 1500)" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 121,
                columns: new[] { "code", "name" },
                values: new object[] { "oj", "Ojibwa" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 122,
                columns: new[] { "code", "name" },
                values: new object[] { "om", "Oromo" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 123,
                columns: new[] { "code", "name" },
                values: new object[] { "or", "Oriya" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 124,
                columns: new[] { "code", "name" },
                values: new object[] { "os", "Ossetian; Ossetic" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 125,
                columns: new[] { "code", "name" },
                values: new object[] { "pa", "Panjabi; Punjabi" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 126,
                columns: new[] { "code", "name" },
                values: new object[] { "pi", "Pali" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 127,
                columns: new[] { "code", "name" },
                values: new object[] { "pl", "Polish" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 128,
                columns: new[] { "code", "name" },
                values: new object[] { "ps", "Pushto; Pashto" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 129,
                columns: new[] { "code", "name" },
                values: new object[] { "pt", "Portuguese" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 130,
                columns: new[] { "code", "name" },
                values: new object[] { "qu", "Quechua" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 131,
                columns: new[] { "code", "name" },
                values: new object[] { "rm", "Romansh" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 132,
                columns: new[] { "code", "name" },
                values: new object[] { "rn", "Rundi" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 133,
                columns: new[] { "code", "name" },
                values: new object[] { "ro", "Romanian; Moldavian; Moldovan" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 134,
                columns: new[] { "code", "name" },
                values: new object[] { "ru", "Russian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 135,
                columns: new[] { "code", "name" },
                values: new object[] { "rw", "Kinyarwanda" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 136,
                columns: new[] { "code", "name" },
                values: new object[] { "sa", "Sanskrit" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 137,
                columns: new[] { "code", "name" },
                values: new object[] { "sc", "Sardinian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 138,
                columns: new[] { "code", "name" },
                values: new object[] { "sd", "Sindhi" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 139,
                columns: new[] { "code", "name" },
                values: new object[] { "se", "Northern Sami" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 140,
                columns: new[] { "code", "name" },
                values: new object[] { "sg", "Sango" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 141,
                columns: new[] { "code", "name" },
                values: new object[] { "si", "Sinhala; Sinhalese" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 142,
                columns: new[] { "code", "name" },
                values: new object[] { "sk", "Slovak" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 143,
                columns: new[] { "code", "name" },
                values: new object[] { "sl", "Slovenian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 144,
                columns: new[] { "code", "name" },
                values: new object[] { "sm", "Samoan" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 145,
                columns: new[] { "code", "name" },
                values: new object[] { "sn", "Shona" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 146,
                columns: new[] { "code", "name" },
                values: new object[] { "so", "Somali" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 147,
                columns: new[] { "code", "name" },
                values: new object[] { "sq", "Albanian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 148,
                columns: new[] { "code", "name" },
                values: new object[] { "sr", "Serbian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 149,
                columns: new[] { "code", "name" },
                values: new object[] { "ss", "Swati" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 150,
                columns: new[] { "code", "name" },
                values: new object[] { "st", "Sotho, Southern" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 151,
                columns: new[] { "code", "name" },
                values: new object[] { "su", "Sundanese" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 152,
                columns: new[] { "code", "name" },
                values: new object[] { "sv", "Swedish" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 153,
                columns: new[] { "code", "name" },
                values: new object[] { "sw", "Swahili" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 154,
                columns: new[] { "code", "name" },
                values: new object[] { "ta", "Tamil" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 155,
                columns: new[] { "code", "name" },
                values: new object[] { "te", "Telugu" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 156,
                columns: new[] { "code", "name" },
                values: new object[] { "tg", "Tajik" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 157,
                columns: new[] { "code", "name" },
                values: new object[] { "th", "Thai" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 158,
                columns: new[] { "code", "name" },
                values: new object[] { "ti", "Tigrinya" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 159,
                columns: new[] { "code", "name" },
                values: new object[] { "tk", "Turkmen" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 160,
                columns: new[] { "code", "name" },
                values: new object[] { "tl", "Tagalog" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 161,
                columns: new[] { "code", "name" },
                values: new object[] { "tn", "Tswana" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 162,
                columns: new[] { "code", "name" },
                values: new object[] { "to", "Tonga (Tonga Islands)" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 163,
                columns: new[] { "code", "name" },
                values: new object[] { "tr", "Turkish" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 164,
                columns: new[] { "code", "name" },
                values: new object[] { "ts", "Tsonga" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 165,
                columns: new[] { "code", "name" },
                values: new object[] { "tt", "Tatar" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 166,
                columns: new[] { "code", "name" },
                values: new object[] { "tw", "Twi" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 167,
                columns: new[] { "code", "name" },
                values: new object[] { "ty", "Tahitian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 168,
                columns: new[] { "code", "name" },
                values: new object[] { "ug", "Uighur; Uyghur" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 169,
                columns: new[] { "code", "name" },
                values: new object[] { "uk", "Ukrainian" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 170,
                columns: new[] { "code", "name" },
                values: new object[] { "ur", "Urdu" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 171,
                columns: new[] { "code", "name" },
                values: new object[] { "uz", "Uzbek" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 172,
                columns: new[] { "code", "name" },
                values: new object[] { "ve", "Venda" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 173,
                columns: new[] { "code", "name" },
                values: new object[] { "vi", "Vietnamese" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 174,
                columns: new[] { "code", "name" },
                values: new object[] { "vo", "Volapük" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 175,
                columns: new[] { "code", "name" },
                values: new object[] { "wa", "Walloon" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 176,
                columns: new[] { "code", "name" },
                values: new object[] { "wo", "Wolof" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 177,
                columns: new[] { "code", "name" },
                values: new object[] { "xh", "Xhosa" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 178,
                columns: new[] { "code", "name" },
                values: new object[] { "yi", "Yiddish" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 179,
                columns: new[] { "code", "name" },
                values: new object[] { "yo", "Yoruba" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 180,
                columns: new[] { "code", "name" },
                values: new object[] { "za", "Zhuang; Chuang" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 181,
                columns: new[] { "code", "name" },
                values: new object[] { "zh", "Chinese" });

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "id",
                keyValue: 182,
                columns: new[] { "code", "name" },
                values: new object[] { "zu", "Zulu" });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "modified_at", "password_hash" },
                values: new object[] { new DateTime(2026, 1, 20, 12, 0, 51, 958, DateTimeKind.Utc).AddTicks(3577), new DateTime(2026, 1, 20, 12, 0, 51, 958, DateTimeKind.Utc).AddTicks(3586), "648F71DB31C58420563B6F1022799EACEC677B0767FE2DDC8093D23E55AD5C82-62FEA1406A384136AB2560C3E60A767B" });

            migrationBuilder.CreateIndex(
                name: "ix_freelancers_skills_freelancer_id",
                table: "freelancers_skills",
                column: "freelancer_id");

            migrationBuilder.AddForeignKey(
                name: "fk_category_project_categories_categories_id",
                table: "category_project",
                column: "categories_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_category_project_projects_project_id",
                table: "category_project",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freelancer_language_freelancers_freelancer_id",
                table: "freelancer_language",
                column: "freelancer_id",
                principalTable: "freelancers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freelancer_language_language_languages_id",
                table: "freelancer_language",
                column: "languages_id",
                principalTable: "languages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_freelancers_skills_skill_skill_id",
                table: "freelancers_skills",
                column: "skill_id",
                principalTable: "skills",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
