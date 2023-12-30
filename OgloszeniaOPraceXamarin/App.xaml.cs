using System;
using Xamarin.Forms;
using System.IO;
using OgloszeniaOPraceXamarin.Repos;
using OgloszeniaOPraceXamarin.Models;
using System.Diagnostics;
using OgloszeniaOPraceXamarin.Views;

namespace OgloszeniaOPraceXamarin {
    public partial class App : Application {
        public static string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "baza1111111111111111111121111.db3");
        public static UserModel user=null;
        public App() {
            InitializeComponent();
            if(!File.Exists(dbPath)) 
                File.Create(dbPath);
            Setup();
            MainPage = new NavigationPage(new MainPage());
            ;
        }
        static async void Setup() {
            await CategoryRepo.GetAsync(0);
            await CompanyRepo.GetAsync(0);
            await ProfileRepo.GetAsync(0);
            await TypeOfWorkRepo.GetAsync(0);
            await UserRepo.GetAsync(0);
            await AnnouncementRepository.GetByIdAsync(0);
        }
    }
}
