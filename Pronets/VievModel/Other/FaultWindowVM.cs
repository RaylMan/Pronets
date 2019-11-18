using Pronets.Data;
using Pronets.EntityRequests.Repairs_f;
using Pronets.Viev.MainWindows.Pages;
using Pronets.Viev.Other;
using Pronets.Viev.Repairs_f;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Other
{
    class FaultWindowVM : VievModelBase
    {
        #region Properties
        private ObservableCollection<Defects> defects = new ObservableCollection<Defects>();
        public ObservableCollection<Defects> Defects
        {
            get { return defects; }
            set
            {
                defects = value;
                RaisedPropertyChanged("Defects");
            }
        }
        private Defects selectedDefect;
        public Defects SelectedDefect
        {
            get { return selectedDefect; }
            set
            {
                selectedDefect = value;
                FillTextBoxes();
                RaisedPropertyChanged("SelectedDefect");
            }
        }
        private int selectedDefectIndex;
        public int SelectedDefectIndex
        {
            get { return selectedDefectIndex; }
            set
            {
                selectedDefectIndex = value;
                RaisedPropertyChanged("SelectedDefectIndex");
            }
        }
        private string defect;
        public string Defect
        {
            get { return defect; }
            set
            {
                defect = value;
                RaisedPropertyChanged("Defect");
            }
        }
        private string work;
        public string Work
        {
            get { return work; }
            set
            {
                work = value;
                RaisedPropertyChanged("Work");
            }
        }
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                RaisedPropertyChanged("IsSelected");
            }
        }

        RepairsPage baseRepairPage;
        RepairsTableEngineer baseRepairTablePage;
        #endregion

        public FaultWindowVM(RepairsPage page)
        {
            baseRepairPage = page;
            GetDeffects();
        }
        public FaultWindowVM(RepairsTableEngineer page)
        {
            baseRepairTablePage = page;
            GetDeffects();
        }

        private void GetDeffects()
        {
            defects.Clear();
            Defects = DefectsRequests.FillList();
            selectedDefectIndex = -1;

        }
        /// <summary>
        /// Автозаполнение текстбоксов для изменения элементов из выбранного элемента
        /// </summary>
        private void FillTextBoxes()
        {
            if (selectedDefect != null)
            {
                Defect = selectedDefect.Defect;
                Work = selectedDefect.Work;
            }
        }
        #region Send command
        private ICommand sendCommand;
        public ICommand SendCommand
        {
            get
            {
                if (sendCommand == null)
                {
                    sendCommand = new RelayCommand(new Action<object>(Send));
                }
                return sendCommand;
            }
            set
            {
                sendCommand = value;
                RaisedPropertyChanged("SendCommand");
            }
        }
        /// <summary>
        /// Обновляет данные на странице ремонта
        /// </summary>
        /// <param name="parametr"></param>
        public void Send(object parametr)
        {
            if (selectedDefect != null)
            {
                if (baseRepairPage != null)
                {
                    baseRepairPage.tbxDefect.Text = selectedDefect.Defect;
                    baseRepairPage.tbxWork.Text = selectedDefect.Work;
                }
                else if (baseRepairTablePage != null)
                {
                    baseRepairTablePage.tbxDefect.Text = selectedDefect.Defect;
                    baseRepairTablePage.tbxWork.Text = selectedDefect.Work;
                }

                CloseFaultWindow();
            }
        }
        /// <summary>
        /// Возращает экземпляр открытого окна FaultWindow
        /// </summary>
        /// <returns></returns>
        private void CloseFaultWindow()
        {
            FaultWindow faultWindow = null;
            foreach (var win in Application.Current.Windows)
            {
                if (win is FaultWindow)
                {
                    faultWindow = (FaultWindow)win;
                }
            }
            if (faultWindow != null)
                faultWindow.Close();
        }
        #endregion

        #region Send Multiple
        private ICommand sendMultipleCommand;
        public ICommand SendMultipleCommand
        {
            get
            {
                if (sendMultipleCommand == null)
                {
                    sendMultipleCommand = new RelayCommand(new Action<object>(SendMultiple));
                }
                return sendMultipleCommand;
            }
            set
            {
                sendMultipleCommand = value;
                RaisedPropertyChanged("SendMultipleCommand");
            }
        }
        /// <summary>
        /// Обновляет данные на странице ремонта
        /// </summary>
        /// <param name="parametr"></param>
        public void SendMultiple(object parametr)
        {
            string defectTemp = null;
            string workTemp = null;
            int defectsCount = 0;
            foreach (var item in Defects)
            {
                if (item.IsSelected)
                {
                    if (defectTemp != null && workTemp != null)
                    {
                        defectTemp += $", {item.Defect}";
                        workTemp += $", {item.Work}";
                        defectsCount++;
                    }
                    else
                    {
                        defectTemp += item.Defect;
                        workTemp += item.Work;
                    }
                }
            }
            if (defectsCount > 0)
            {
                defectTemp.Remove(defectTemp.Length - 1);
                workTemp.Remove(workTemp.Length - 1);
            }

            if (baseRepairPage != null)
            {
                baseRepairPage.tbxDefect.Text = defectTemp;
                baseRepairPage.tbxWork.Text = workTemp;
            }
            else if (baseRepairTablePage != null)
            {
                baseRepairTablePage.tbxDefect.Text = defectTemp;
                baseRepairTablePage.tbxWork.Text = workTemp;
            }

            CloseFaultWindow();
        }
        #endregion

        #region Add command
        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(new Action<object>(AddToBase));
                }
                return addCommand;
            }
            set
            {
                addCommand = value;
                RaisedPropertyChanged("AddCommand");
            }
        }
        /// <summary>
        /// Обновляет данные на странице ремонта
        /// </summary>
        /// <param name="parametr"></param>
        public void AddToBase(object parametr)
        {
            if (!string.IsNullOrWhiteSpace(Defect) && !string.IsNullOrWhiteSpace(Work))
            {
                Defects defect = new Defects { Defect = Defect, Work = Work };
                DefectsRequests.AddToBase(defect);
                Defects.Add(defect);
                selectedDefect = null;
                Defect = string.Empty;
                Work = string.Empty;
                GetDeffects();
            }
            else
                MessageBox.Show("Введите неисправность и проделанную работу", "Ошибка");

        }
        #endregion

        #region Edit command
        private ICommand editCommand;
        public ICommand EditCommand
        {
            get
            {
                if (editCommand == null)
                {
                    editCommand = new RelayCommand(new Action<object>(Edit));
                }
                return editCommand;
            }
            set
            {
                editCommand = value;
                RaisedPropertyChanged("EditCommand");
            }
        }
        /// <summary>
        /// Обновляет данные на странице ремонта
        /// </summary>
        /// <param name="parametr"></param>
        public void Edit(object parametr)
        {
            if (selectedDefect != null)
            {
                if (!string.IsNullOrWhiteSpace(Defect) && !string.IsNullOrWhiteSpace(Work))
                {
                    selectedDefect.Defect = Defect;
                    selectedDefect.Work = Work;

                    DefectsRequests.EditItem(selectedDefect);
                    GetDeffects();
                    MessageBox.Show("Успешное изменение!");
                }
                else
                    MessageBox.Show("Введите неисправность и проделанную работу", "Ошибка");
            }
        }
        #endregion

        #region Delete command
        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(new Action<object>(Delete));
                }
                return deleteCommand;
            }
            set
            {
                deleteCommand = value;
                RaisedPropertyChanged("DeleteCommand");
            }
        }
        /// <summary>
        /// Обновляет данные на странице ремонта
        /// </summary>
        /// <param name="parametr"></param>
        public void Delete(object parametr)
        {
            if (selectedDefect != null)
            {
                DefectsRequests.RemoveFromBase(selectedDefect, out bool ex);
                if (ex)
                {
                    Defects.RemoveAt(SelectedDefectIndex);
                    Defect = string.Empty;
                    Work = string.Empty;
                    selectedDefect = null;
                }
            }
        }
        #endregion
    }
}
