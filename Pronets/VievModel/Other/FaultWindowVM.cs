using Pronets.Data;
using Pronets.EntityRequests.Repairs_f;
using Pronets.Viev.MainWindows.Pages;
using Pronets.Viev.Other;
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

        RepairsPage basePage;
        public FaultWindowVM(RepairsPage page)
        {
            basePage = page;
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
                basePage.tbxDefect.Text = selectedDefect.Defect;
                basePage.tbxWork.Text = selectedDefect.Work;
                var window = GetFauldWindow();
                if (window != null)
                    window.Close();
            }
        }

        /// <summary>
        /// Возращает экземпляр открытого окна FaultWindow
        /// </summary>
        /// <returns></returns>
        private FaultWindow GetFauldWindow()
        {
            FaultWindow faultWindow = null;
            foreach (var win in Application.Current.Windows)
            {
                if (win is FaultWindow)
                {
                    faultWindow = (FaultWindow)win;
                }  
            }
            return faultWindow;
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
            if(!string.IsNullOrWhiteSpace(Defect) && !string.IsNullOrWhiteSpace(Work))
            {
                Defects defect = new Defects { Defect = Defect, Work = Work };
                DefectsRequests.AddToBase(defect);
                Defects.Add(defect);
                selectedDefect = null;
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
                if(ex)
                {
                    Defects.RemoveAt(SelectedDefectIndex);
                    selectedDefect = null;
                }
            }
        }
        #endregion
    }
}
