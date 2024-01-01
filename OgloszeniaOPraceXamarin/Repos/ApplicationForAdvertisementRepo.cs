﻿using OgloszeniaOPraceXamarin.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OgloszeniaOPraceXamarin.Repos {
    public static class ApplicationForAdvertisementRepo {
        static SQLiteAsyncConnection _database;
        static ApplicationForAdvertisementRepo() {
            _database = new SQLiteAsyncConnection(App.dbPath);
            _database.CreateTableAsync<ApplicationForAdvertisement>().Wait();
        }
        public static async Task<ApplicationForAdvertisement> AddAsync(ApplicationForAdvertisement application) {
            await _database.InsertAsync(application);
            return application;
        }
        public static async Task DeleteAsync(int userID,int announcementID) {
           ApplicationForAdvertisement x= await _database.Table<ApplicationForAdvertisement>().Where(e=>e.UserID==userID&&e.AnnouncementID==announcementID).FirstOrDefaultAsync();
           await _database.DeleteAsync(x);
        }
        public static async Task<List<ApplicationForAdvertisement>> getAsyncByUserID(int userID) {
            return await _database.Table<ApplicationForAdvertisement>().Where(e => e.UserID == userID).ToListAsync();
        }
        public static async Task<int> getCount(int announcementID) {
            return await _database.Table<ApplicationForAdvertisement>().Where(e => e.AnnouncementID == announcementID).CountAsync();
        }
        public static async Task<bool> isApplicating(int userID, int announcementID) {
            if (await _database.Table<ApplicationForAdvertisement>().Where(e => e.AnnouncementID == announcementID && e.UserID == userID).CountAsync() > 0) {
                return true;
            }
            return false;
        }
    }
}
