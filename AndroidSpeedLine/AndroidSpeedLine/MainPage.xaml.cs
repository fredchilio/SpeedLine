using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AndroidSpeedLine
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            TimeNow.Time = DateTime.Now.TimeOfDay;
            TimeNow.IsEnabled = false;
        }
        void OnToggled(object sender, ToggledEventArgs e)
        {
            if (TimeSelect.IsToggled)
            {

                LabelTimeSelect.Text = "Time Now";
                TimeNow.Time = DateTime.Now.TimeOfDay;
                LabelTimeSelect.TextColor = Color.Red;
                LabelTimeSelect.FontAttributes = FontAttributes.Bold;
                TimeNow.IsEnabled = false;

            }
            else
            {
                LabelTimeSelect.Text = "Selected";
                LabelTimeSelect.TextColor = Color.Black;
                LabelTimeSelect.FontAttributes = FontAttributes.Bold;
                TimeNow.IsEnabled = true;
            }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HowManyForTime.Text)
               ^ Convert.ToDouble(HowManyForTime.Text) < 0)
                HowManyForTime.Text = "4";

            if (string.IsNullOrEmpty(LineSpeed.Text)
                ^ Convert.ToDouble(LineSpeed.Text) < 0)
                LineSpeed.Text = "165";

            if (string.IsNullOrEmpty(TimeLineWork.Text) ^
                Convert.ToDouble(TimeLineWork.Text) > 100 ^
                Convert.ToDouble(TimeLineWork.Text) < 0)
                TimeLineWork.Text = "12";
            

            if (TimeSelect.IsToggled)
            {
                TimeNow.Time = DateTime.Now.TimeOfDay;
            }

            TimeFinalLess.IsVisible = true;
            TimeFinalMore.IsVisible = true;




            try
            {

                double diferenceTime = (TimeLastOne.Time.Subtract(TimeNow.Time).TotalSeconds);

                if(diferenceTime<0)
                {
                    TimeSpan tempLocalTimeLast = TimeLastOne.Time;

                    TimeSpan lessHalfDay = new TimeSpan(12, 0, 0);
                    
                    TimeSpan tempLocalTimeNow = TimeNow.Time;

                    TimeSpan TempSubtraction = (TimeLastOne.Time + lessHalfDay) - (TimeNow.Time - lessHalfDay);

                    diferenceTime = TempSubtraction.TotalSeconds;
                }
                //double hourDifModeule = TimeNow.Time.TotalMilliseconds;


                double totalSecondsLineWork = Convert.ToDouble(LineSpeed.Text) + Convert.ToDouble(TimeLineWork.Text);

                double tempAnswareModule = (diferenceTime % totalSecondsLineWork);

                int diferenceLessModule = Convert.ToInt32(diferenceTime - tempAnswareModule);
                DateTime tempTimeNow = new DateTime();



                DateTime hourDifModeuleLess = tempTimeNow.AddSeconds(diferenceLessModule + TimeNow.Time.TotalSeconds);
                DateTime hourDifModeuleMore = hourDifModeuleLess.AddSeconds(totalSecondsLineWork);

                TimeFinalLess.Time = hourDifModeuleLess.TimeOfDay;
                TimeFinalMore.Time = hourDifModeuleMore.TimeOfDay;

                if ((diferenceLessModule / Convert.ToInt32(totalSecondsLineWork) * Convert.ToInt32(HowManyForTime.Text)) > 0)
                {
                    LabelQtMoreLess.Text = (diferenceLessModule / Convert.ToInt32(totalSecondsLineWork) * Convert.ToInt32(HowManyForTime.Text)).ToString();
                    LabelQtMoreMore.Text = (Convert.ToInt32(LabelQtMoreLess.Text) + 4).ToString();
                }
                else
                {
                    //LabelQtMoreLess.Text = "Type time";
                    //LabelQtMoreMore.Text = "corretly!!!";
                    LabelQtMoreLess.Text = (diferenceLessModule / Convert.ToInt32(totalSecondsLineWork) * Convert.ToInt32(HowManyForTime.Text)).ToString();
                    LabelQtMoreMore.Text = (Convert.ToInt32(LabelQtMoreLess.Text) + 4).ToString();

                }
            }
            catch (Exception)
            {

            }

        }
    }
}
