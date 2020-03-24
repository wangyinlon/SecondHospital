using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriageClient
{
    public class PatientInfo : INotifyPropertyChanged
    {
        public string PatientID { get; set; }
        public string PatientName { get; set; }
        public string PatientTime { get; set; }
    


        private string patientState;
        public string PatientState
        {
            get
            {
                return patientState;
            }
            set
            {
                patientState = value;
                OnPropertyChanged("PatientState");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
