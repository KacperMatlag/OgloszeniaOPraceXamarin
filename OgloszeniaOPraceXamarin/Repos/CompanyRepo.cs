﻿using OgloszeniaOPraceXamarin.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OgloszeniaOPraceXamarin.Repos {
    public static class CompanyRepo {
        private static SQLiteAsyncConnection database;

        static CompanyRepo() {
            database = new SQLiteAsyncConnection(App.dbPath);
            database.CreateTableAsync<Company>().Wait();
            if (database.Table<Company>().CountAsync().Result == 0) {
                SeedAsync();
            }

        }

        // Create operation
        public static async Task AddAsync(Company company) {
            await database.InsertAsync(company);
        }

        // Read operations
        public static async Task<Company> GetAsync(int id) {
            return await database.Table<Company>().FirstOrDefaultAsync(c => c.ID == id);
        }

        public static async Task<List<Company>> GetAllAsync() {
            return await database.Table<Company>().ToListAsync();
        }

        // Update operation
        public static async Task UpdateAsync(Company company) {
            await database.UpdateAsync(company);
        }

        // Delete operation
        public static async Task DeleteAsync(int id) {
            await database.DeleteAsync<Company>(id);
        }

        public static async void SeedAsync() {
            List<Company> companies = new List<Company> {
                new Company { ID=1,Name = "Tech Solutions Inc", Description = "Information Technology", NIP = 123456789, Location = "City A", ImageLink = "https://www.gotowelogo.pl/wp-content/uploads/2019/12/Gotowelogo_579.png" },
                new Company { ID=2,Name = "ABC Corp", Description = "Software Development", NIP = 987654321, Location = "City B", ImageLink = "https://projektowane.pl/site_media/uploads/artykuly/starbucks-logo.png" },
                new Company { ID=3,Name = "XYZ Ltd", Description = "Manufacturing", NIP = 456789123, Location = "City C", ImageLink = "https://e-reklamowe.com/wp-content/uploads/2018/01/logo-ochrona-bezpieczenstwo-1.jpg" },
                new Company {ID=4, Name = "123 Industries", Description = "Consulting", NIP = 789123456, Location = "City D", ImageLink = "https://www.grupapns.pl/wp-content/uploads/2020/04/projektowanie-logo-dla-firm-1.png" },
                new Company {ID = 5,  Name = "Global Services Co", Description = "Financial Services", NIP = 321654987, Location = "City E", ImageLink = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ17HeDZW7y_S35hBu8tDotTIw-TSP5Wp913A&usqp=CAU" }
            };

            foreach (Company company in companies)
                await AddAsync(company);
        }
    }
}
