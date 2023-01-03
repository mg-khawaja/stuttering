
using OTPControl;
using Stuttering.Models;
using Stuttering.Resx;
using Stuttering.SQL;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stuttering.Views.Assessment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Evaluation : ContentView
    {
        string q1Rating;
        string q2Rating;
        string q3Rating;
        string q4Rating;
        string q5Rating;
        string q6Rating;
        string q7Rating;
        Style BtnStyle;
        Style BtnSelectedStyle;
        public Evaluation()
        {
            InitializeComponent();
            BtnSelectedStyle = Q1Btn1.Style;
            BtnStyle = Q1Btn2.Style;
            Q1Btn1.Style = BtnStyle;
        }

        private void Q1Btn_Clicked(object sender, System.EventArgs e)
        {
            var btn = sender as Button;
            var value = btn.CommandParameter as string;
            switch (q1Rating)
            {
                case "1":
                    Q1Btn1.Style = BtnStyle;
                    break;
                case "2":
                    Q1Btn2.Style = BtnStyle;
                    break;
                case "3":
                    Q1Btn3.Style = BtnStyle;
                    break;
                case "4":
                    Q1Btn4.Style = BtnStyle;
                    break;
                default:
                    break;
            }
            switch (value)
            {
                case "1":
                    Q1Btn1.Style = BtnSelectedStyle;
                    break;
                case "2":
                    Q1Btn2.Style = BtnSelectedStyle;
                    break;
                case "3":
                    Q1Btn3.Style = BtnSelectedStyle;
                    break;
                case "4":
                    Q1Btn4.Style = BtnSelectedStyle;
                    break;
                default:
                    break;
            }
            q1Rating = value;
        }
        private void Q2Btn_Clicked(object sender, System.EventArgs e)
        {
            var btn = sender as Button;
            var value = btn.CommandParameter as string;
            switch (q2Rating)
            {
                case "1":
                    Q2Btn1.Style = BtnStyle;
                    break;
                case "2":
                    Q2Btn2.Style = BtnStyle;
                    break;
                case "3":
                    Q2Btn3.Style = BtnStyle;
                    break;
                case "4":
                    Q2Btn4.Style = BtnStyle;
                    break;
                default:
                    break;
            }
            switch (value)
            {
                case "1":
                    Q2Btn1.Style = BtnSelectedStyle;
                    break;
                case "2":
                    Q2Btn2.Style = BtnSelectedStyle;
                    break;
                case "3":
                    Q2Btn3.Style = BtnSelectedStyle;
                    break;
                case "4":
                    Q2Btn4.Style = BtnSelectedStyle;
                    break;
                default:
                    break;
            }
            q2Rating = value;
        }
        private void Q3Btn_Clicked(object sender, System.EventArgs e)
        {
            var btn = sender as Button;
            var value = btn.CommandParameter as string;
            switch (q3Rating)
            {
                case "1":
                    Q3Btn1.Style = BtnStyle;
                    break;
                case "2":
                    Q3Btn2.Style = BtnStyle;
                    break;
                case "3":
                    Q3Btn3.Style = BtnStyle;
                    break;
                case "4":
                    Q3Btn4.Style = BtnStyle;
                    break;
                default:
                    break;
            }
            switch (value)
            {
                case "1":
                    Q3Btn1.Style = BtnSelectedStyle;
                    break;
                case "2":
                    Q3Btn2.Style = BtnSelectedStyle;
                    break;
                case "3":
                    Q3Btn3.Style = BtnSelectedStyle;
                    break;
                case "4":
                    Q3Btn4.Style = BtnSelectedStyle;
                    break;
                default:
                    break;
            }
            q3Rating = value;
        }
        private void Q4Btn_Clicked(object sender, System.EventArgs e)
        {
            var btn = sender as Button;
            var value = btn.CommandParameter as string;
            switch (q4Rating)
            {
                case "1":
                    Q4Btn1.Style = BtnStyle;
                    break;
                case "2":
                    Q4Btn2.Style = BtnStyle;
                    break;
                case "3":
                    Q4Btn3.Style = BtnStyle;
                    break;
                case "4":
                    Q4Btn4.Style = BtnStyle;
                    break;
                default:
                    break;
            }
            switch (value)
            {
                case "1":
                    Q4Btn1.Style = BtnSelectedStyle;
                    break;
                case "2":
                    Q4Btn2.Style = BtnSelectedStyle;
                    break;
                case "3":
                    Q4Btn3.Style = BtnSelectedStyle;
                    break;
                case "4":
                    Q4Btn4.Style = BtnSelectedStyle;
                    break;
                default:
                    break;
            }
            q4Rating = value;
        }
        private void Q5Btn_Clicked(object sender, System.EventArgs e)
        {
            var btn = sender as Button;
            var value = btn.CommandParameter as string;
            switch (q5Rating)
            {
                case "1":
                    Q5Btn1.Style = BtnStyle;
                    break;
                case "2":
                    Q5Btn2.Style = BtnStyle;
                    break;
                case "3":
                    Q5Btn3.Style = BtnStyle;
                    break;
                case "4":
                    Q5Btn4.Style = BtnStyle;
                    break;
                default:
                    break;
            }
            switch (value)
            {
                case "1":
                    Q5Btn1.Style = BtnSelectedStyle;
                    break;
                case "2":
                    Q5Btn2.Style = BtnSelectedStyle;
                    break;
                case "3":
                    Q5Btn3.Style = BtnSelectedStyle;
                    break;
                case "4":
                    Q5Btn4.Style = BtnSelectedStyle;
                    break;
                default:
                    break;
            }
            q5Rating = value;
        }
        private void Q6Btn_Clicked(object sender, System.EventArgs e)
        {
            var btn = sender as Button;
            var value = btn.CommandParameter as string;
            switch (q6Rating)
            {
                case "1":
                    Q6Btn1.Style = BtnStyle;
                    break;
                case "2":
                    Q6Btn2.Style = BtnStyle;
                    break;
                case "3":
                    Q6Btn3.Style = BtnStyle;
                    break;
                case "4":
                    Q6Btn4.Style = BtnStyle;
                    break;
                default:
                    break;
            }
            switch (value)
            {
                case "1":
                    Q6Btn1.Style = BtnSelectedStyle;
                    break;
                case "2":
                    Q6Btn2.Style = BtnSelectedStyle;
                    break;
                case "3":
                    Q6Btn3.Style = BtnSelectedStyle;
                    break;
                case "4":
                    Q6Btn4.Style = BtnSelectedStyle;
                    break;
                default:
                    break;
            }
            q6Rating = value;
        }
        private void Q7Btn_Clicked(object sender, System.EventArgs e)
        {
            var btn = sender as Button;
            var value = btn.CommandParameter as string;
            switch (q7Rating)
            {
                case "1":
                    Q7Btn1.Style = BtnStyle;
                    break;
                case "2":
                    Q7Btn2.Style = BtnStyle;
                    break;
                case "3":
                    Q7Btn3.Style = BtnStyle;
                    break;
                case "4":
                    Q7Btn4.Style = BtnStyle;
                    break;
                default:
                    break;
            }
            switch (value)
            {
                case "1":
                    Q7Btn1.Style = BtnSelectedStyle;
                    break;
                case "2":
                    Q7Btn2.Style = BtnSelectedStyle;
                    break;
                case "3":
                    Q7Btn3.Style = BtnSelectedStyle;
                    break;
                case "4":
                    Q7Btn4.Style = BtnSelectedStyle;
                    break;
                default:
                    break;
            }
            q7Rating = value;
        }

        private async void SaveButton_Clicked(object sender, System.EventArgs e)
        {
            if(q1Rating == null || q2Rating == null || q3Rating == null || q4Rating == null ||
                q5Rating == null || q6Rating == null || q7Rating == null)
            {
                await (Application.Current as App).MainPage.DisplayAlert(StutteringResources.error, StutteringResources.attempt_all, StutteringResources.ok);
            }
            else
            {
                SQLiteDbManager.SaveEvaluation(new Models.Evaluation()
                {
                    Date = DateTime.Now.ToString("dd MMM"),
                    Q1Rate = Convert.ToInt32(q1Rating),
                    Q2Rate = Convert.ToInt32(q2Rating),
                    Q3Rate = Convert.ToInt32(q3Rating),
                    Q4Rate = Convert.ToInt32(q4Rating),
                    Q5Rate = Convert.ToInt32(q5Rating),
                    Q6Rate = Convert.ToInt32(q6Rating),
                    Q7Rate = Convert.ToInt32(q7Rating)
                });
                var meta = SQLiteDbManager.GetMetadata();
                if (meta != null)
                {
                    meta.SelfEvalDateTime = DateTime.Now;
                    SQLiteDbManager.SaveMetadata(meta);
                }
                (Application.Current as App).MainPage = new NavigationPage(new Views.MasterDetailPage());
            }
        }
    }
}