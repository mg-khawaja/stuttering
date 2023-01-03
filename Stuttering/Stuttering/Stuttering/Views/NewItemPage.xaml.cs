using Stuttering.Models;
using Stuttering.ViewModels;
using Xamarin.Forms;

namespace Stuttering.Views
{
    public partial class NewItemPage : ContentPage
    {
        public StutterReadItem Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}