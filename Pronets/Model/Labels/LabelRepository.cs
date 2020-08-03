using Pronets.Model.Labels.LabelSamples;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.Model.Labels
{
    public static class LabelRepository
    {
        public static ObservableCollection<ILabel> GetLabels()
        {
            ObservableCollection<ILabel> labels = new ObservableCollection<ILabel>();

            labels.Add(new EltexONTLabelGpon());
            labels.Add(new EltexONTLabelGepon());
            labels.Add(new EltexNTE2Label());
            labels.Add(new EltexTAULabel());
            labels.Add(new EltexSMGLabel());
            labels.Add(new STBLabel());
            labels.Add(new SmlLabel());
            labels.Add(new HuaweiLabel());
            labels.Add(new MacAdressLabel());
            labels.Add(new DLinkServiceUserInfoLabel());
            labels.Add(new DLinkLoginPassLabel());
            labels.Add(new CanNotBeRestoredLabel());
            labels.Add(new CameraLabel());
            return labels;
        }
    }
}
