using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TriageClient
{
    /// <summary>
    /// OnlyShowMessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class OnlyShowMessageBox : INotifyPropertyChanged
    {

        private bool iserror = false;
        public void Show(string messageBoxText, bool iserror = false)
        {
            this.iserror = iserror;
            this.tb.Text = messageBoxText;
            this.Show();
        }

        public OnlyShowMessageBox()
        {
            InitializeComponent();
            //this.DataContext = new model() { YOffSet = -300d };
            YOffSet = -300d;
            this.Loaded += (y, k) =>
            {
                this.Top = 41;
                this.Left = (SystemParameters.WorkArea.Width) / 2 - this.ActualWidth / 2;
                //if (iserror)
                //{
                //    this.grid1.Visibility = Visibility.Collapsed;
                //}
                //else { this.grid2.Visibility = Visibility.Collapsed; }
                (this.Resources["ShowSb"] as Storyboard).Begin();
            };
        }


        private void Storyboard_Completed(object sender, EventArgs e)
        {
            this.Close();
        }

            private double YOffset;

        public event PropertyChangedEventHandler PropertyChanged;

        public double YOffSet
            {
                get { return YOffset; }
                set
                {
                    YOffset = value;
                NotifyPropertyChanged("YOffSet");
                }
            }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
