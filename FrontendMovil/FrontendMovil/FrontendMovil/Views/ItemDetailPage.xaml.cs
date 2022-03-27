using FrontendMovil.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace FrontendMovil.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}