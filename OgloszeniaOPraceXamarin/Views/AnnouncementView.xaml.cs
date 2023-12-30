using OgloszeniaOPraceXamarin.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OgloszeniaOPraceXamarin.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnnouncementView : ContentPage {
        public AnnouncementView(AnnouncementModel announcement) {
            InitializeComponent();
            Title = announcement.Title;
            BindingContext = announcement;
        }
    }
}