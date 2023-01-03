using Stuttering.Resx;
using Stuttering.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stuttering.Views.Assessment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Rating : ContentView
    {
        string rating;
        Style BtnStyle;
        Style BtnSelectedStyle;
        public Rating()
        {
            InitializeComponent();
            BtnSelectedStyle = Btn1.Style;
            BtnStyle = Btn2.Style;
            Btn1.Style = BtnStyle;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var value = btn.CommandParameter as string;
            switch (rating)
            {
                case "1":
                    Btn1.Style = BtnStyle;
                    break;
                case "2":
                    Btn2.Style = BtnStyle;
                    break;
                case "3":
                    Btn3.Style = BtnStyle;
                    break;
                case "4":
                    Btn4.Style = BtnStyle;
                    break;
                case "5":
                    Btn5.Style = BtnStyle;
                    break;
                case "6":
                    Btn6.Style = BtnStyle;
                    break;
                case "7":
                    Btn7.Style = BtnStyle;
                    break;
                case "8":
                    Btn8.Style = BtnStyle;
                    break;
                case "9":
                    Btn9.Style = BtnStyle;
                    break;
                case "10":
                    Btn10.Style = BtnStyle;
                    break;
                default:
                    break;
            }
            switch (value)
            {
                case "1":
                    Btn1.Style = BtnSelectedStyle;
                    break;
                case "2":
                    Btn2.Style = BtnSelectedStyle;
                    break;
                case "3":
                    Btn3.Style = BtnSelectedStyle;
                    break;
                case "4":
                    Btn4.Style = BtnSelectedStyle;
                    break;
                case "5":
                    Btn5.Style = BtnSelectedStyle;
                    break;
                case "6":
                    Btn6.Style = BtnSelectedStyle;
                    break;
                case "7":
                    Btn7.Style = BtnSelectedStyle;
                    break;
                case "8":
                    Btn8.Style = BtnSelectedStyle;
                    break;
                case "9":
                    Btn9.Style = BtnSelectedStyle;
                    break;
                case "10":
                    Btn10.Style = BtnSelectedStyle;
                    break;
                default:
                    break;
            }
            rating = value;
        }
        private async void SaveButton_Clicked(object sender, System.EventArgs e)
        {
            if (rating == null)
            {
                await (Application.Current as App).MainPage.DisplayAlert(StutteringResources.error, StutteringResources.attempt, StutteringResources.ok);
            }
            else
            {
                SQLiteDbManager.SaveRating(new Models.Rating()
                {
                    Date = DateTime.Now.ToString("dd MMM"),
                    Rate = Convert.ToInt32(rating)
                });
                var meta = SQLiteDbManager.GetMetadata();
                if (meta != null)
                {
                    meta.SelfRateDateTime = DateTime.Now;
                    SQLiteDbManager.SaveMetadata(meta);
                }
                (Application.Current as App).MainPage = new NavigationPage(new Views.MasterDetailPage());
            }
        }
    }
}