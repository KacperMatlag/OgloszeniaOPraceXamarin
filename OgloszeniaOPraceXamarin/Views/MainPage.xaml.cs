using OgloszeniaOPraceXamarin.Models;
using OgloszeniaOPraceXamarin.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OgloszeniaOPraceXamarin.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();
            Setup();
        }
        async void Setup() {
            await Task.Delay(3000);
            AnnouncementList.ItemsSource=await AnnouncementRepository.GetAllAsync();
        }
        protected override async void OnAppearing() {
            base.OnAppearing();
            AnnouncementList.ItemsSource = await AnnouncementRepository.GetAllAsync();
        }

        private async void SwipeItem_Invoked(object sender, EventArgs e) {
            AnnouncementModel announcement = (sender as SwipeItem).CommandParameter as AnnouncementModel;
            await Navigation.PushAsync(new AnnouncementView(announcement));
        }
    }
}