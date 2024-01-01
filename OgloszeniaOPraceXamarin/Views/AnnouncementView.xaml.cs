using OgloszeniaOPraceXamarin.Models;
using OgloszeniaOPraceXamarin.Repos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OgloszeniaOPraceXamarin.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnnouncementView : ContentPage {
            AnnouncementModel _announcement;
        public AnnouncementView(AnnouncementModel announcement) {
            InitializeComponent();
            Title = announcement.Title;
            BindingContext = announcement;
            _announcement= announcement;
            if (App.user!=null&&App.user.ID == announcement.User.ID) {
                Toolbar.IsVisible = true;
            }
        }

        private async void Edit_Clicked(object sender, System.EventArgs e) {
           await Navigation.PushAsync(new AnnouncementCreate(_announcement));
        }

        private async void Delete_Clicked(object sender, System.EventArgs e) {
            bool result = await DisplayAlert("Ostrzezenie", "Czy na pewno chcesz usunac to ogloszenie?", "Tak", "Nie");
            if (result) {
                await AnnouncementRepository.DeleteAsync(_announcement);
                await Navigation.PopToRootAsync();
            }
        }
    }
}