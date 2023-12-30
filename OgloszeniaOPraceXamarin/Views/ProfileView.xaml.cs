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
    public partial class ProfileView : ContentPage {
        public ProfileView() {
            InitializeComponent();
            Setup();
        }
        private async void Setup() {
            Login.Text = App.user.Login;
            Name.Text = App.user.Profile.Name;
            Surname.Text = App.user.Profile.Surname;
            Email.Text = App.user.Profile.Email;

            UserAnnouncementList.ItemsSource = await AnnouncementRepository.GetAllByUserID(App.user.ID);
        }
    }
}