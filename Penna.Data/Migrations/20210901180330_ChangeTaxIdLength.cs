using Microsoft.EntityFrameworkCore.Migrations;

namespace Penna.Data.Migrations
{
    public partial class ChangeTaxIdLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TaxId",
                table: "Tenants",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.Sql(InstallScript);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TaxId",
                table: "Tenants",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 11);

            migrationBuilder.Sql(UninstallScript);
        }

        public const string InstallScript = @"
            CREATE PROCEDURE [dbo].[GetUserInfoByTenantAndRole]
            @TenantId int,
            @RoleName nvarchar(256)
            AS
            BEGIN
	            SET NOCOUNT ON;
                select distinct u.Id, u.UserName, u.Email, u.PhoneNumber, u.LockoutEnd, u.AccessFailedCount, 
	            u.FullName, u.Address, u.CityId, c.Name as CityName, u.CountryId, s.Name as CountryName,
	            u.CountryDialCode, u.TCIdentityNo, u.Status, u.PictureUrl, u.PictureRealName, u.PictureContentType,
	            u.TenantId, t.Title as TenantTitle, t.FullName as TenantName,  r.Name as RoleName
	            from dbo.AspNetUsers u
	            inner join dbo.AspNetUserRoles ur on ur.UserId=u.Id
	            inner join dbo.AspNetRoles r on r.Id=ur.RoleId and r.Name=@RoleName
	            inner join dbo.Tenants t on t.Id=u.TenantId
	            left join dbo.Cities c on c.Id=u.CityId
	            left join dbo.Countries s on s.Id=u.CountryId
	            where u.TenantId=@TenantId and r.Name=@RoleName
            END;
        ";

        private const string UninstallScript = @"
            DROP PROCEDURE [dbo].[GetUserInfoByTenantAndRole];
        ";

    }
}
