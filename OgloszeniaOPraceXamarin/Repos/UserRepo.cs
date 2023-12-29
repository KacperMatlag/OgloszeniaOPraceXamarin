using OgloszeniaOPraceXamarin.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using OgloszeniaOPraceXamarin.Supports;

namespace OgloszeniaOPraceXamarin.Repos {
    public static class UserRepo {
        private static SQLiteAsyncConnection database;

        static UserRepo() {
            database = new SQLiteAsyncConnection(App.dbPath);
            database.CreateTableAsync<UserModel>().Wait();
            if (database.Table<UserModel>().CountAsync().Result == 0) {
                SeedAsync();
            }
        }

        public static async Task AddAsync(UserModel user) {
            await database.InsertAsync(user);

            if (user.Profile != null) {
                await ProfileRepo.AddAsync(user.Profile);
            }

            if (user.Company != null) {
                await CompanyRepo.AddAsync(user.Company);
            }
        }

        public static async Task<UserModel> GetAsync(int id) {
            var user = await database.Table<UserModel>().FirstOrDefaultAsync(u => u.ID == id);

            if (user != null) {
                user.Profile = await ProfileRepo.GetAsync(user.ProfileID.GetValueOrDefault());
                user.Company = await CompanyRepo.GetAsync(user.CompanyID.GetValueOrDefault());
            }

            return user;
        }

        public static async Task<List<UserModel>> GetAllAsync() {
            var users = await database.Table<UserModel>().ToListAsync();

            foreach (var user in users) {
                user.Profile = await ProfileRepo.GetAsync(user.ProfileID.GetValueOrDefault());
                user.Company = await CompanyRepo.GetAsync(user.CompanyID.GetValueOrDefault());
            }

            return users;
        }

        public static async Task UpdateAsync(UserModel user) {
            await database.UpdateAsync(user);

            if (user.Profile != null) {
                await ProfileRepo.UpdateAsync(user.Profile);
            }

            if (user.Company != null) {
                await CompanyRepo.UpdateAsync(user.Company);
            }
        }

        public static async Task DeleteAsync(int id) {
            var user = await GetAsync(id);

            if (user != null) {
                await ProfileRepo.DeleteAsync(user.ProfileID.GetValueOrDefault());
                await CompanyRepo.DeleteAsync(user.CompanyID.GetValueOrDefault());
                await database.DeleteAsync<UserModel>(id);
            }
        }

        public static async void SeedAsync() {
            List<UserModel> userModels = new List<UserModel>() {
                new UserModel {
                    ID = 1,
                    Login="Login",
                    Password=PasswordHandling.HashPassword("Password"),
                    CompanyID=1,
                    Permission=2,
                    ProfileID=1,
                },
                new UserModel {
                    ID = 2,
                    Login="Login1",
                    Password=PasswordHandling.HashPassword("Password1"),
                    CompanyID=1,
                    Permission=1,
                    ProfileID=1,
                }
            };

            foreach (UserModel userModel in userModels)
                await AddAsync(userModel);
        }
    }
}
