using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Penna.Core.Utilities.Constants;
using Penna.Data.EntityFramework.Contexts;
using Penna.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Penna.Data.Seeds
{
    public static class SeedData
    {
        private static string FirstUserId = string.Empty;
        public async static Task Seed(AppDbContext db,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            try
            {
                if (db.Database.GetPendingMigrations().Count() > 0)
                {
                    db.Database.Migrate();
                }
            }
            catch (Exception)
            {
            }
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
            await SeedCountry(db);
            await SeedCity(db);
            await SeedTenant(db);
        }
        private async static Task SeedUsers(UserManager<AppUser> userManager)
        {
            if (await userManager.FindByEmailAsync("admin@admin.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    EmailConfirmed = true,
                    FullName = "Ahmet Dokur",
                };
                var result = await userManager.CreateAsync(user, "P@ssword1");
                if (result.Succeeded)
                {
                    FirstUserId = user.Id;
                    await userManager.AddToRoleAsync(user, SD.ROLE_SuperAdmin);
                }
            }
            if (await userManager.FindByEmailAsync("gurhan@innocube.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "gurhan@innocube.com",
                    Email = "gurhan@innocube.com",
                    EmailConfirmed = true,
                    FullName = "Gürhan Kozan",
                };
                var result = await userManager.CreateAsync(user, "P@ssword1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, SD.ROLE_SuperAdmin);
                }
            }
            if (await userManager.FindByEmailAsync("alkan@innocube.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "alkan@innocube.com",
                    Email = "alkan@innocube.com",
                    EmailConfirmed = true,
                    FullName = "Alkan Toykan",
                };
                var result = await userManager.CreateAsync(user, "P@ssword1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, SD.ROLE_SuperAdmin);
                }
            }
            if (await userManager.FindByEmailAsync("admin1@demo.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "admin1@demo.com",
                    Email = "admin1@demo.com",
                    EmailConfirmed = true,
                    FullName = "Burak Atlı",
                    TenantId = 1
                };
                var result = await userManager.CreateAsync(user, "P@ssword1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, SD.ROLE_Admin);
                }
            }
            if (await userManager.FindByEmailAsync("manager1@demo.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "manager1@demo.com",
                    Email = "manager1@demo.com",
                    EmailConfirmed = true,
                    FullName = "Burak Atlı",
                    TenantId = 1
                };
                var result = await userManager.CreateAsync(user, "P@ssword1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, SD.ROLE_ProjectManager);
                }
            }
            if (await userManager.FindByEmailAsync("customer1@demo.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "customer1@demo.com",
                    Email = "customer1@demo.com",
                    EmailConfirmed = true,
                    FullName = "Mert Budak",
                    TenantId = 1
                };
                var result = await userManager.CreateAsync(user, "P@ssword1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, SD.ROLE_Customer);
                }
            }
            if (await userManager.FindByEmailAsync("customer2@demo.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "customer2@demo.com",
                    Email = "customer2@demo.com",
                    EmailConfirmed = true,
                    FullName = "Kemal Türker",
                    TenantId = 1
                };
                var result = await userManager.CreateAsync(user, "P@ssword1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, SD.ROLE_Customer);
                }
            }
            if (await userManager.FindByEmailAsync("taseron@demo.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "taseron@demo.com",
                    Email = "taseron@demo.com",
                    EmailConfirmed = true,
                    FullName = "Niyazi Atagil",
                    TenantId = 1
                };
                var result = await userManager.CreateAsync(user, "P@ssword1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, SD.ROLE_Taseron);
                }
            }
            if (await userManager.FindByEmailAsync("tedarikci@demo.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "tedarikci@demo.com",
                    Email = "tedarikci@demo.com",
                    EmailConfirmed = true,
                    FullName = "Mustafa Arabgir",
                    TenantId = 1
                };
                var result = await userManager.CreateAsync(user, "P@ssword1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, SD.ROLE_Supplier);
                }
            }
        }

        private async static Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(SD.ROLE_SuperAdmin))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = SD.ROLE_SuperAdmin });
            }
            if (!await roleManager.RoleExistsAsync(SD.ROLE_Admin))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = SD.ROLE_Admin });
            }
            if (!await roleManager.RoleExistsAsync(SD.ROLE_ProjectManager))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = SD.ROLE_ProjectManager });
            }
            if (!await roleManager.RoleExistsAsync(SD.ROLE_Taseron))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = SD.ROLE_Taseron });
            }
            if (!await roleManager.RoleExistsAsync(SD.ROLE_Supplier))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = SD.ROLE_Supplier });
            }
            if (!await roleManager.RoleExistsAsync(SD.ROLE_Customer))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = SD.ROLE_Customer });
            }
        }

        private async static Task SeedCountry(AppDbContext db)
        {
            if (!db.Countries.Any())
            {
                IEnumerable<Country> countries = new[] {
                    new Country { Id = 1,   Name = "Afghanistan", DialCode = "93", Code = "AF", IsActive = true },
                    new Country { Id = 2,   Name = "Albania", DialCode = "355", Code = "AL", IsActive = true },
                    new Country { Id = 3,   Name = "Algeria", DialCode = "213", Code = "DZ", IsActive = true },
                    new Country { Id = 4,   Name = "AmericanSamoa", DialCode = "1 684", Code = "AS", IsActive = true },
                    new Country { Id = 5,   Name = "Andorra", DialCode = "376", Code = "AD", IsActive = true },
                    new Country { Id = 6,   Name = "Anla", DialCode = "244", Code = "AO", IsActive = true },
                    new Country { Id = 7,   Name = "Anguilla", DialCode = "1 264", Code = "AI", IsActive = true },
                    new Country { Id = 8,   Name = "Antarctica", DialCode = "672", Code = "AQ", IsActive = true },
                    new Country { Id = 9,   Name = "Antigua and Barbuda", DialCode = "1268", Code = "AG", IsActive = true },
                    new Country { Id = 10,  Name = "Argentina", DialCode = "54", Code = "AR", IsActive = true },
                    new Country { Id = 11,  Name = "Armenia", DialCode = "374", Code = "AM", IsActive = true },
                    new Country { Id = 12,  Name = "Aruba", DialCode = "297", Code = "AW", IsActive = true },
                    new Country { Id = 13,  Name = "Australia", DialCode = "61", Code = "AU", IsActive = true },
                    new Country { Id = 14,  Name = "Austria", DialCode = "43", Code = "AT", IsActive = true },
                    new Country { Id = 15,  Name = "Azerbaijan", DialCode = "994", Code = "AZ", IsActive = true },
                    new Country { Id = 16,  Name = "Bahamas", DialCode = "1 242", Code = "BS", IsActive = true },
                    new Country { Id = 17,  Name = "Bahrain", DialCode = "973", Code = "BH", IsActive = true },
                    new Country { Id = 18,  Name = "Bangladesh", DialCode = "880", Code = "BD", IsActive = true },
                    new Country { Id = 19,  Name = "Barbados", DialCode = "1 246", Code = "BB", IsActive = true },
                    new Country { Id = 20,  Name = "Belarus", DialCode = "375", Code = "BY", IsActive = true },
                    new Country { Id = 21,  Name = "Belgium", DialCode = "32", Code = "BE", IsActive = true },
                    new Country { Id = 22,  Name = "Belize", DialCode = "501", Code = "BZ", IsActive = true },
                    new Country { Id = 23,  Name = "Benin", DialCode = "229", Code = "BJ", IsActive = true },
                    new Country { Id = 24,  Name = "Bermuda", DialCode = "1 441", Code = "BM", IsActive = true },
                    new Country { Id = 25,  Name = "Bhutan", DialCode = "975", Code = "BT", IsActive = true },
                    new Country { Id = 26,  Name = "Bolivia, Plurinational State of Bolivia", DialCode = "591", Code = "BO", IsActive = true },
                    new Country { Id = 27,  Name = "Bosnia and Herzevina", DialCode = "387", Code = "BA", IsActive = true },
                    new Country { Id = 28,  Name = "Botswana", DialCode = "267", Code = "BW", IsActive = true },
                    new Country { Id = 29,  Name = "Bouvet Island", DialCode = "55", Code = "BV", IsActive = true },
                    new Country { Id = 30,  Name = "Brazil", DialCode = "55", Code = "BR", IsActive = true },
                    new Country { Id = 31,  Name = "British Indian Ocean Territory", DialCode = "246", Code = "IO", IsActive = true },
                    new Country { Id = 32,  Name = "Brunei Darussalam", DialCode = "673", Code = "BN", IsActive = true },
                    new Country { Id = 33,  Name = "Bulgaria", DialCode = "359", Code = "BG", IsActive = true },
                    new Country { Id = 34,  Name = "Burkina Faso", DialCode = "226", Code = "BF", IsActive = true },
                    new Country { Id = 35,  Name = "Burundi", DialCode = "257", Code = "BI", IsActive = true },
                    new Country { Id = 36,  Name = "Cambodia", DialCode = "855", Code = "KH", IsActive = true },
                    new Country { Id = 37,  Name = "Cameroon", DialCode = "237", Code = "CM", IsActive = true },
                    new Country { Id = 38,  Name = "Canada", DialCode = "1", Code = "CA", IsActive = true },
                    new Country { Id = 39,  Name = "Cape Verde", DialCode = "238", Code = "CV", IsActive = true },
                    new Country { Id = 40,  Name = "Cayman Islands", DialCode = "1345", Code = "KY", IsActive = true },
                    new Country { Id = 41,  Name = "Central African Republic", DialCode = "236", Code = "CF", IsActive = true },
                    new Country { Id = 42,  Name = "Chad", DialCode = "235", Code = "TD", IsActive = true },
                    new Country { Id = 43,  Name = "Chile", DialCode = "56", Code = "CL", IsActive = true },
                    new Country { Id = 44,  Name = "China", DialCode = "86", Code = "CN", IsActive = true },
                    new Country { Id = 45,  Name = "Christmas Island", DialCode = "61", Code = "CX", IsActive = true },
                    new Country { Id = 46,  Name = "Cocos (Keeling) Islands", DialCode = "61", Code = "CC", IsActive = true },
                    new Country { Id = 47,  Name = "Colombia", DialCode = "57", Code = "CO", IsActive = true },
                    new Country { Id = 48,  Name = "Comoros", DialCode = "269", Code = "KM", IsActive = true },
                    new Country { Id = 49,  Name = "Con", DialCode = "242", Code = "CG", IsActive = true },
                    new Country { Id = 50,  Name = "Con, The Democratic Republic of the", DialCode = "243", Code = "CD", IsActive = true },
                    new Country { Id = 51,  Name = "Cook Islands", DialCode = "682", Code = "CK", IsActive = true },
                    new Country { Id = 52,  Name = "Costa Rica", DialCode = "506", Code = "CR", IsActive = true },
                    new Country { Id = 53,  Name = "Cote d''Ivoire", DialCode = "225", Code = "CI", IsActive = true },
                    new Country { Id = 54,  Name = "Croatia", DialCode = "385", Code = "HR", IsActive = true },
                    new Country { Id = 55,  Name = "Cuba", DialCode = "53", Code = "CU", IsActive = true },
                    new Country { Id = 56,  Name = "Cyprus", DialCode = "357", Code = "CY", IsActive = true },
                    new Country { Id = 57,  Name = "Czech Republic", DialCode = "420", Code = "CZ", IsActive = true },
                    new Country { Id = 58,  Name = "Denmark", DialCode = "45", Code = "DK", IsActive = true },
                    new Country { Id = 59,  Name = "Djibouti", DialCode = "253", Code = "DJ", IsActive = true },
                    new Country { Id = 60,  Name = "Dominica", DialCode = "1 767", Code = "DM", IsActive = true },
                    new Country { Id = 61,  Name = "Dominican Republic", DialCode = "1 849", Code = "DO", IsActive = true },
                    new Country { Id = 62,  Name = "Ecuador", DialCode = "593", Code = "EC", IsActive = true },
                    new Country { Id = 63,  Name = "Egypt", DialCode = "20", Code = "EG", IsActive = true },
                    new Country { Id = 64,  Name = "El Salvador", DialCode = "503", Code = "SV", IsActive = true },
                    new Country { Id = 65,  Name = "Equatorial Guinea", DialCode = "240", Code = "GQ", IsActive = true },
                    new Country { Id = 66,  Name = "Eritrea", DialCode = "291", Code = "ER", IsActive = true },
                    new Country { Id = 67,  Name = "Estonia", DialCode = "372", Code = "EE", IsActive = true },
                    new Country { Id = 68,  Name = "Ethiopia", DialCode = "251", Code = "ET", IsActive = true },
                    new Country { Id = 69,  Name = "Falkland Islands (Malvinas)", DialCode = "500", Code = "FK", IsActive = true },
                    new Country { Id = 70,  Name = "Faroe Islands", DialCode = "298", Code = "FO", IsActive = true },
                    new Country { Id = 71,  Name = "Fiji", DialCode = "679", Code = "FJ", IsActive = true },
                    new Country { Id = 72,  Name = "Finland", DialCode = "358", Code = "FI", IsActive = true },
                    new Country { Id = 73,  Name = "France", DialCode = "33", Code = "FR", IsActive = true },
                    new Country { Id = 74,  Name = "French Guiana", DialCode = "594", Code = "GF", IsActive = true },
                    new Country { Id = 75,  Name = "French Polynesia", DialCode = "689", Code = "PF", IsActive = true },
                    new Country { Id = 76,  Name = "French Southern and Antarctic Lands", DialCode = "262", Code = "TF", IsActive = true },
                    new Country { Id = 77,  Name = "Gabon", DialCode = "241", Code = "GA", IsActive = true },
                    new Country { Id = 78,  Name = "Gambia", DialCode = "220", Code = "GM", IsActive = true },
                    new Country { Id = 79,  Name = "Georgia", DialCode = "995", Code = "GE", IsActive = true },
                    new Country { Id = 80,  Name = "Germany", DialCode = "49", Code = "DE", IsActive = true },
                    new Country { Id = 81,  Name = "Ghana", DialCode = "233", Code = "GH", IsActive = true },
                    new Country { Id = 82,  Name = "Gibraltar", DialCode = "350", Code = "GI", IsActive = true },
                    new Country { Id = 83,  Name = "Greece", DialCode = "30", Code = "GR", IsActive = true },
                    new Country { Id = 84,  Name = "Greenland", DialCode = "299", Code = "GL", IsActive = true },
                    new Country { Id = 85,  Name = "Grenada", DialCode = "1 473", Code = "GD", IsActive = true },
                    new Country { Id = 86,  Name = "Guadeloupe", DialCode = "590", Code = "GP", IsActive = true },
                    new Country { Id = 87,  Name = "Guam", DialCode = "1 671", Code = "GU", IsActive = true },
                    new Country { Id = 88,  Name = "Guatemala", DialCode = "502", Code = "GT", IsActive = true },
                    new Country { Id = 89,  Name = "Guernsey", DialCode = "44", Code = "GG", IsActive = true },
                    new Country { Id = 90,  Name = "Guinea", DialCode = "224", Code = "GN", IsActive = true },
                    new Country { Id = 91,  Name = "Guinea-Bissau", DialCode = "245", Code = "GW", IsActive = true },
                    new Country { Id = 92,  Name = "Guyana", DialCode = "592", Code = "GY", IsActive = true },
                    new Country { Id = 93,  Name = "Haiti", DialCode = "509", Code = "HT", IsActive = true },
                    new Country { Id = 94,  Name = "Heard Island and McDonald Islands", DialCode = "672", Code = "HM", IsActive = true },
                    new Country { Id = 95,  Name = "Holy See (Vatican City State)", DialCode = "379", Code = "VA", IsActive = true },
                    new Country { Id = 96,  Name = "Honduras", DialCode = "504", Code = "HN", IsActive = true },
                    new Country { Id = 97,  Name = "Hong Kong", DialCode = "852", Code = "HK", IsActive = true },
                    new Country { Id = 98,  Name = "Hungary", DialCode = "36", Code = "HU", IsActive = true },
                    new Country { Id = 99,  Name = "Iceland", DialCode = "354", Code = "IS", IsActive = true },
                    new Country { Id = 100, Name = "India", DialCode = "91", Code = "IN", IsActive = true },
                    new Country { Id = 101, Name = "Indonesia", DialCode = "62", Code = "ID", IsActive = true },
                    new Country { Id = 102, Name = "Iran, Islamic Republic of", DialCode = "98", Code = "IR", IsActive = true },
                    new Country { Id = 103, Name = "Iraq", DialCode = "964", Code = "IQ", IsActive = true },
                    new Country { Id = 104, Name = "Ireland", DialCode = "353", Code = "IE", IsActive = true },
                    new Country { Id = 105, Name = "Isle of Man", DialCode = "44", Code = "IM", IsActive = true },
                    new Country { Id = 106, Name = "Israel", DialCode = "972", Code = "IL", IsActive = true },
                    new Country { Id = 107, Name = "Italy", DialCode = "39", Code = "IT", IsActive = true },
                    new Country { Id = 108, Name = "Jamaica", DialCode = "1 876", Code = "JM", IsActive = true },
                    new Country { Id = 109, Name = "Japan", DialCode = "81", Code = "JP", IsActive = true },
                    new Country { Id = 110, Name = "Jersey", DialCode = "44", Code = "JE", IsActive = true },
                    new Country { Id = 111, Name = "Jordan", DialCode = "962", Code = "JO", IsActive = true },
                    new Country { Id = 112, Name = "Kazakhstan", DialCode = "7", Code = "KZ", IsActive = true },
                    new Country { Id = 113, Name = "Kenya", DialCode = "254", Code = "KE", IsActive = true },
                    new Country { Id = 114, Name = "Kiribati", DialCode = "686", Code = "KI", IsActive = true },
                    new Country { Id = 115, Name = "Korea, Democratic People''s Republic of", DialCode = "850", Code = "KP", IsActive = true },
                    new Country { Id = 116, Name = "Korea, Republic of", DialCode = "82", Code = "KR", IsActive = true },
                    new Country { Id = 117, Name = "Kuwait", DialCode = "965", Code = "KW", IsActive = true },
                    new Country { Id = 118, Name = "Kyrgyzstan", DialCode = "996", Code = "KG", IsActive = true },
                    new Country { Id = 119, Name = "Lao People''s Democratic Republic", DialCode = "856", Code = "LA", IsActive = true },
                    new Country { Id = 120, Name = "Latvia", DialCode = "371", Code = "LV", IsActive = true },
                    new Country { Id = 121, Name = "Lebanon", DialCode = "961", Code = "LB", IsActive = true },
                    new Country { Id = 122, Name = "Lesotho", DialCode = "266", Code = "LS", IsActive = true },
                    new Country { Id = 123, Name = "Liberia", DialCode = "231", Code = "LR", IsActive = true },
                    new Country { Id = 124, Name = "Libyan Arab Jamahiriya", DialCode = "218", Code = "LY", IsActive = true },
                    new Country { Id = 125, Name = "Liechtenstein", DialCode = "423", Code = "LI", IsActive = true },
                    new Country { Id = 126, Name = "Lithuania", DialCode = "370", Code = "LT", IsActive = true },
                    new Country { Id = 127, Name = "Luxembourg", DialCode = "352", Code = "LU", IsActive = true },
                    new Country { Id = 128, Name = "Macao", DialCode = "853", Code = "MO", IsActive = true },
                    new Country { Id = 129, Name = "Macedonia, The Former Yuslav Republic of", DialCode = "389", Code = "MK", IsActive = true },
                    new Country { Id = 130, Name = "Madagascar", DialCode = "261", Code = "MG", IsActive = true },
                    new Country { Id = 131, Name = "Malawi", DialCode = "265", Code = "MW", IsActive = true },
                    new Country { Id = 132, Name = "Malaysia", DialCode = "60", Code = "MY", IsActive = true },
                    new Country { Id = 133, Name = "Maldives", DialCode = "960", Code = "MV", IsActive = true },
                    new Country { Id = 134, Name = "Mali", DialCode = "223", Code = "ML", IsActive = true },
                    new Country { Id = 135, Name = "Malta", DialCode = "356", Code = "MT", IsActive = true },
                    new Country { Id = 136, Name = "Marshall Islands", DialCode = "692", Code = "MH", IsActive = true },
                    new Country { Id = 137, Name = "Martinique", DialCode = "596", Code = "MQ", IsActive = true },
                    new Country { Id = 138, Name = "Mauritania", DialCode = "222", Code = "MR", IsActive = true },
                    new Country { Id = 139, Name = "Mauritius", DialCode = "230", Code = "MU", IsActive = true },
                    new Country { Id = 140, Name = "Mayotte", DialCode = "262", Code = "YT", IsActive = true },
                    new Country { Id = 141, Name = "Mexico", DialCode = "52", Code = "MX", IsActive = true },
                    new Country { Id = 142, Name = "Micronesia, Federated States of", DialCode = "691", Code = "FM", IsActive = true },
                    new Country { Id = 143, Name = "Moldova, Republic of", DialCode = "373", Code = "MD", IsActive = true },
                    new Country { Id = 144, Name = "Monaco", DialCode = "377", Code = "MC", IsActive = true },
                    new Country { Id = 145, Name = "Monlia", DialCode = "976", Code = "MN", IsActive = true },
                    new Country { Id = 146, Name = "Montenegro", DialCode = "382", Code = "ME", IsActive = true },
                    new Country { Id = 147, Name = "Montserrat", DialCode = "1664", Code = "MS", IsActive = true },
                    new Country { Id = 148, Name = "Morocco", DialCode = "212", Code = "MA", IsActive = true },
                    new Country { Id = 149, Name = "Mozambique", DialCode = "258", Code = "MZ", IsActive = true },
                    new Country { Id = 150, Name = "Myanmar", DialCode = "95", Code = "MM", IsActive = true },
                    new Country { Id = 151, Name = "Namibia", DialCode = "264", Code = "NA", IsActive = true },
                    new Country { Id = 152, Name = "Nauru", DialCode = "674", Code = "NR", IsActive = true },
                    new Country { Id = 153, Name = "Nepal", DialCode = "977", Code = "NP", IsActive = true },
                    new Country { Id = 154, Name = "Netherlands", DialCode = "31", Code = "NL", IsActive = true },
                    new Country { Id = 155, Name = "Netherlands Antilles", DialCode = "599", Code = "AN", IsActive = true },
                    new Country { Id = 156, Name = "New Caledonia", DialCode = "687", Code = "NC", IsActive = true },
                    new Country { Id = 157, Name = "New Zealand", DialCode = "64", Code = "NZ", IsActive = true },
                    new Country { Id = 158, Name = "Nicaragua", DialCode = "505", Code = "NI", IsActive = true },
                    new Country { Id = 159, Name = "Niger", DialCode = "227", Code = "NE", IsActive = true },
                    new Country { Id = 160, Name = "Nigeria", DialCode = "234", Code = "NG", IsActive = true },
                    new Country { Id = 161, Name = "Niue", DialCode = "683", Code = "NU", IsActive = true },
                    new Country { Id = 162, Name = "Norfolk Island", DialCode = "672", Code = "NF", IsActive = true },
                    new Country { Id = 163, Name = "Northern Mariana Islands", DialCode = "1 670", Code = "MP", IsActive = true },
                    new Country { Id = 164, Name = "Norway", DialCode = "47", Code = "NO", IsActive = true },
                    new Country { Id = 165, Name = "Oman", DialCode = "968", Code = "OM", IsActive = true },
                    new Country { Id = 166, Name = "Pakistan", DialCode = "92", Code = "PK", IsActive = true },
                    new Country { Id = 167, Name = "Palau", DialCode = "680", Code = "PW", IsActive = true },
                    new Country { Id = 168, Name = "Palestinian Territory, Occupied", DialCode = "970", Code = "PS", IsActive = true },
                    new Country { Id = 169, Name = "Panama", DialCode = "507", Code = "PA", IsActive = true },
                    new Country { Id = 170, Name = "Papua New Guinea", DialCode = "675", Code = "PG", IsActive = true },
                    new Country { Id = 171, Name = "Paraguay", DialCode = "595", Code = "PY", IsActive = true },
                    new Country { Id = 172, Name = "Peru", DialCode = "51", Code = "PE", IsActive = true },
                    new Country { Id = 173, Name = "Philippines", DialCode = "63", Code = "PH", IsActive = true },
                    new Country { Id = 174, Name = "Pitcairn", DialCode = "870", Code = "PN", IsActive = true },
                    new Country { Id = 175, Name = "Poland", DialCode = "48", Code = "PL", IsActive = true },
                    new Country { Id = 176, Name = "Portugal", DialCode = "351", Code = "PT", IsActive = true },
                    new Country { Id = 177, Name = "Puerto Rico", DialCode = "1 939", Code = "PR", IsActive = true },
                    new Country { Id = 178, Name = "Qatar", DialCode = "974", Code = "QA", IsActive = true },
                    new Country { Id = 179, Name = "Réunion", DialCode = "262", Code = "RE", IsActive = true },
                    new Country { Id = 180, Name = "Romania", DialCode = "40", Code = "RO", IsActive = true },
                    new Country { Id = 181, Name = "Russia", DialCode = "7", Code = "RU", IsActive = true },
                    new Country { Id = 182, Name = "Rwanda", DialCode = "250", Code = "RW", IsActive = true },
                    new Country { Id = 183, Name = "Saint Helena, Ascension and Tristan Da Cunha", DialCode = "290", Code = "SH", IsActive = true },
                    new Country { Id = 184, Name = "Saint Kitts and Nevis", DialCode = "1 869", Code = "KN", IsActive = true },
                    new Country { Id = 185, Name = "Saint Lucia", DialCode = "1 758", Code = "LC", IsActive = true },
                    new Country { Id = 186, Name = "Saint Pierre and Miquelon", DialCode = "508", Code = "PM", IsActive = true },
                    new Country { Id = 187, Name = "Saint Vincent and the Grenadines", DialCode = "1 784", Code = "VC", IsActive = true },
                    new Country { Id = 188, Name = "Samoa", DialCode = "685", Code = "WS", IsActive = true },
                    new Country { Id = 189, Name = "San Marino", DialCode = "378", Code = "SM", IsActive = true },
                    new Country { Id = 190, Name = "Sao Tome and Principe", DialCode = "239", Code = "ST", IsActive = true },
                    new Country { Id = 191, Name = "Saudi Arabia", DialCode = "966", Code = "SA", IsActive = true },
                    new Country { Id = 192, Name = "Senegal", DialCode = "221", Code = "SN", IsActive = true },
                    new Country { Id = 193, Name = "Serbia", DialCode = "381", Code = "RS", IsActive = true },
                    new Country { Id = 194, Name = "Seychelles", DialCode = "248", Code = "SC", IsActive = true },
                    new Country { Id = 195, Name = "Sierra Leone", DialCode = "232", Code = "SL", IsActive = true },
                    new Country { Id = 196, Name = "Singapore", DialCode = "65", Code = "SG", IsActive = true },
                    new Country { Id = 197, Name = "Slovakia", DialCode = "421", Code = "SK", IsActive = true },
                    new Country { Id = 198, Name = "Slovenia", DialCode = "386", Code = "SI", IsActive = true },
                    new Country { Id = 199, Name = "Solomon Islands", DialCode = "677", Code = "SB", IsActive = true },
                    new Country { Id = 200, Name = "Somalia", DialCode = "252", Code = "SO", IsActive = true },
                    new Country { Id = 201, Name = "South Africa", DialCode = "27", Code = "ZA", IsActive = true },
                    new Country { Id = 202, Name = "South Georgia and the South Sandwich Islands", DialCode = "500", Code = "GS", IsActive = true },
                    new Country { Id = 203, Name = "Spain", DialCode = "34", Code = "ES", IsActive = true },
                    new Country { Id = 204, Name = "Sri Lanka", DialCode = "94", Code = "LK", IsActive = true },
                    new Country { Id = 205, Name = "Sudan", DialCode = "249", Code = "SD", IsActive = true },
                    new Country { Id = 206, Name = "SuriName", DialCode = "597", Code = "SR", IsActive = true },
                    new Country { Id = 207, Name = "Svalbard and Jan Mayen", DialCode = "47", Code = "SJ", IsActive = true },
                    new Country { Id = 208, Name = "Swaziland", DialCode = "268", Code = "SZ", IsActive = true },
                    new Country { Id = 209, Name = "Sweden", DialCode = "46", Code = "SE", IsActive = true },
                    new Country { Id = 210, Name = "Switzerland", DialCode = "41", Code = "CH", IsActive = true },
                    new Country { Id = 211, Name = "Syrian Arab Republic", DialCode = "963", Code = "SY", IsActive = true },
                    new Country { Id = 212, Name = "Taiwan", DialCode = "886", Code = "TW", IsActive = true },
                    new Country { Id = 213, Name = "Tajikistan", DialCode = "992", Code = "TJ", IsActive = true },
                    new Country { Id = 214, Name = "Tanzania, United Republic of", DialCode = "255", Code = "TZ", IsActive = true },
                    new Country { Id = 215, Name = "Thailand", DialCode = "66", Code = "TH", IsActive = true },
                    new Country { Id = 216, Name = "Timor-Leste", DialCode = "670", Code = "TL", IsActive = true },
                    new Country { Id = 217, Name = "To", DialCode = "228", Code = "TG", IsActive = true },
                    new Country { Id = 218, Name = "Tokelau", DialCode = "690", Code = "TK", IsActive = true },
                    new Country { Id = 219, Name = "Tonga", DialCode = "676", Code = "TO", IsActive = true },
                    new Country { Id = 220, Name = "Trinidad and Toba", DialCode = "1 868", Code = "TT", IsActive = true },
                    new Country { Id = 221, Name = "Tunisia", DialCode = "216", Code = "TN", IsActive = true },
                    new Country { Id = 222, Name = "Türkiye", DialCode = "90", Code = "TR", IsActive = true },
                    new Country { Id = 223, Name = "Turkmenistan", DialCode = "993", Code = "TM", IsActive = true },
                    new Country { Id = 224, Name = "Turks and Caicos Islands", DialCode = "1 649", Code = "TC", IsActive = true },
                    new Country { Id = 225, Name = "Tuvalu", DialCode = "688", Code = "TV", IsActive = true },
                    new Country { Id = 226, Name = "Uganda", DialCode = "256", Code = "UG", IsActive = true },
                    new Country { Id = 227, Name = "Ukraine", DialCode = "380", Code = "UA", IsActive = true },
                    new Country { Id = 228, Name = "United Arab Emirates", DialCode = "971", Code = "AE", IsActive = true },
                    new Country { Id = 229, Name = "United Kingdom", DialCode = "44", Code = "GB", IsActive = true },
                    new Country { Id = 230, Name = "United States", DialCode = "1", Code = "US", IsActive = true },
                    new Country { Id = 231, Name = "United States Minor Outlying Islands", DialCode = "1581", Code = "UM", IsActive = true },
                    new Country { Id = 232, Name = "Uruguay", DialCode = "598", Code = "UY", IsActive = true },
                    new Country { Id = 233, Name = "Uzbekistan", DialCode = "998", Code = "UZ", IsActive = true },
                    new Country { Id = 234, Name = "Vanuatu", DialCode = "678", Code = "VU", IsActive = true },
                    new Country { Id = 235, Name = "Venezuela, Bolivarian Republic of", DialCode = "58", Code = "VE", IsActive = true },
                    new Country { Id = 236, Name = "Viet Nam", DialCode = "84", Code = "VN", IsActive = true },
                    new Country { Id = 237, Name = "Virgin Islands, British", DialCode = "1 284", Code = "VG", IsActive = true },
                    new Country { Id = 238, Name = "Virgin Islands, U.S.", DialCode = "1 340", Code = "VI", IsActive = true },
                    new Country { Id = 239, Name = "Wallis and Futuna", DialCode = "681", Code = "WF", IsActive = true },
                    new Country { Id = 240, Name = "Western Sahara", DialCode = "732", Code = "EH", IsActive = true },
                    new Country { Id = 241, Name = "Yemen", DialCode = "967", Code = "YE", IsActive = true },
                    new Country { Id = 242, Name = "Zambia", DialCode = "260", Code = "ZM", IsActive = true },
                    new Country { Id = 243, Name = "Zimbabwe", DialCode = "263", Code = "ZW", IsActive = true }};
                await db.Countries.AddRangeAsync(countries);
                await db.Database.OpenConnectionAsync();
                try
                {
                    await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Countries ON");
                    await db.SaveChangesAsync();
                    await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Countries OFF");
                }
                finally
                {
                    await db.Database.CloseConnectionAsync();
                }
            }
        }

        private async static Task SeedCity(AppDbContext db)
        {
            if (!db.Cities.Any())
            {
                IEnumerable<City> cities = new[] {
                new City { Id = 1, Name = "ADANA", CountryId =222 },
                new City { Id = 2, Name = "ADIYAMAN", CountryId =222 },
                new City { Id = 3, Name = "AFYONKARAHISAR", CountryId =222 },
                new City { Id = 4, Name = "AGRI", CountryId =222 },
                new City { Id = 5, Name = "AKSARAY", CountryId =222 },
                new City { Id = 6, Name = "AMASYA", CountryId =222 },
                new City { Id = 7, Name = "ANKARA", CountryId =222 },
                new City { Id = 8, Name = "ANTALYA", CountryId =222 },
                new City { Id = 9, Name = "ARDAHAN", CountryId =222 },
                new City { Id = 10, Name = "ARTVIN", CountryId =222 },
                new City { Id = 11, Name = "AYDIN", CountryId =222 },
                new City { Id = 12, Name = "BALIKESIR", CountryId =222 },
                new City { Id = 13, Name = "BARTIN", CountryId =222 },
                new City { Id = 14, Name = "BATMAN", CountryId =222 },
                new City { Id = 15, Name = "BAYBURT", CountryId =222 },
                new City { Id = 16, Name = "BILECIK", CountryId =222 },
                new City { Id = 17, Name = "BINGÖL", CountryId =222 },
                new City { Id = 18, Name = "BITLIS", CountryId =222 },
                new City { Id = 19, Name = "BOLU", CountryId =222 },
                new City { Id = 20, Name = "BURDUR", CountryId =222 },
                new City { Id = 21, Name = "BURSA", CountryId =222 },
                new City { Id = 22, Name = "ÇANAKKALE", CountryId =222 },
                new City { Id = 23, Name = "ÇANKIRI", CountryId =222 },
                new City { Id = 24, Name = "ÇORUM", CountryId =222 },
                new City { Id = 25, Name = "DENIZLI", CountryId =222 },
                new City { Id = 26, Name = "DIYARBAKIR", CountryId =222 },
                new City { Id = 27, Name = "DÜZCE", CountryId =222 },
                new City { Id = 28, Name = "EDIRNE", CountryId =222 },
                new City { Id = 29, Name = "ELAZIG", CountryId =222 },
                new City { Id = 30, Name = "ERZINCAN", CountryId =222 },
                new City { Id = 31, Name = "ERZURUM", CountryId =222 },
                new City { Id = 32, Name = "ESKISEHIR", CountryId =222 },
                new City { Id = 33, Name = "GAZIANTEP", CountryId =222 },
                new City { Id = 34, Name = "GIRESUN", CountryId =222 },
                new City { Id = 35, Name = "GÜMÜSHANE", CountryId =222 },
                new City { Id = 36, Name = "HAKKARI", CountryId =222 },
                new City { Id = 37, Name = "HATAY", CountryId =222 },
                new City { Id = 38, Name = "IGDIR", CountryId =222 },
                new City { Id = 39, Name = "ISPARTA", CountryId =222 },
                new City { Id = 40, Name = "ISTANBUL", CountryId =222 },
                new City { Id = 41, Name = "IZMIR", CountryId =222 },
                new City { Id = 42, Name = "KAHRAMANMARAS", CountryId =222 },
                new City { Id = 43, Name = "KARABÜK", CountryId =222 },
                new City { Id = 44, Name = "KARAMAN", CountryId =222 },
                new City { Id = 45, Name = "KARS", CountryId =222 },
                new City { Id = 46, Name = "KASTAMONU", CountryId =222 },
                new City { Id = 47, Name = "KAYSERI", CountryId =222 },
                new City { Id = 48, Name = "KIRIKKALE", CountryId =222 },
                new City { Id = 49, Name = "KIRKLARELI", CountryId =222 },
                new City { Id = 50, Name = "KIRSEHIR", CountryId =222 },
                new City { Id = 51, Name = "KILIS", CountryId =222 },
                new City { Id = 52, Name = "KOCAELI", CountryId =222 },
                new City { Id = 53, Name = "KONYA", CountryId =222 },
                new City { Id = 54, Name = "KÜTAHYA", CountryId =222 },
                new City { Id = 55, Name = "MALATYA", CountryId =222 },
                new City { Id = 56, Name = "MANISA", CountryId =222 },
                new City { Id = 57, Name = "MARDIN", CountryId =222 },
                new City { Id = 58, Name = "MERSIN", CountryId =222 },
                new City { Id = 59, Name = "MUGLA", CountryId =222 },
                new City { Id = 60, Name = "MUS", CountryId =222 },
                new City { Id = 61, Name = "NEVSEHIR", CountryId =222 },
                new City { Id = 62, Name = "NIGDE", CountryId =222 },
                new City { Id = 63, Name = "ORDU", CountryId =222 },
                new City { Id = 64, Name = "OSMANIYE", CountryId =222 },
                new City { Id = 65, Name = "RIZE", CountryId =222 },
                new City { Id = 66, Name = "SAKARYA", CountryId =222 },
                new City { Id = 67, Name = "SAMSUN", CountryId =222 },
                new City { Id = 68, Name = "SIIRT", CountryId =222 },
                new City { Id = 69, Name = "SINOP", CountryId =222 },
                new City { Id = 70, Name = "SIVAS", CountryId =222 },
                new City { Id = 71, Name = "SANLIURFA", CountryId =222 },
                new City { Id = 72, Name = "SIRNAK", CountryId =222 },
                new City { Id = 73, Name = "TEKIRDAG", CountryId =222 },
                new City { Id = 74, Name = "TOKAT", CountryId =222 },
                new City { Id = 75, Name = "TRABZON", CountryId =222 },
                new City { Id = 76, Name = "TUNCELI", CountryId =222 },
                new City { Id = 77, Name = "USAK", CountryId =222 },
                new City { Id = 78, Name = "VAN", CountryId =222 },
                new City { Id = 79, Name = "YALOVA", CountryId =222 },
                new City { Id = 80, Name = "YOZGAT", CountryId =222 },
                new City { Id = 81, Name = "ZONGULDAK", CountryId =222 }};

                await db.Cities.AddRangeAsync(cities);
                await db.Database.OpenConnectionAsync();
                try
                {
                    await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Cities ON");
                    await db.SaveChangesAsync();
                    await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Cities OFF");
                }
                finally
                {
                    await db.Database.CloseConnectionAsync();
                }
            }
        }

        private async static Task SeedTenant(AppDbContext db)
        {
            if (!db.Tenants.Any())
            {
                Tenant tenant = new Tenant
                {
                    Id = 1,
                    Title = "Demo İnşaat Taahhüt A.Ş.",
                    FullName = "Alihan Kurt",
                    Email = "alihankurt@demoinsaat.com",
                    CountryDialCode = "90",
                    Phone = "555 4528965",
                    Address = "Yeşilyurt Cad. Manolya Sokak No:35",
                    CityId = 40,
                    CountryId = 222,
                    CreatedBy = FirstUserId,
                    CreatedDate = DateTime.Now,
                    TaxId = "5127456823",
                    TaxOffice = "Bakırköy",
                    IsActive = true,
                    IsLocked = false
                };
                await db.Tenants.AddAsync(tenant);
                await db.Database.OpenConnectionAsync();
                try
                {
                    await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Tenants ON");
                    await db.SaveChangesAsync();
                    await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Tenants OFF");
                }
                finally
                {
                    await db.Database.CloseConnectionAsync();
                }
            }
        }
    }
}
