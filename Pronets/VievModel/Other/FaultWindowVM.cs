using Pronets.Data;
using Pronets.EntityRequests.Repairs_f;
using Pronets.Viev.MainWindows.Pages;
using Pronets.Viev.Other;
using Pronets.Viev.Repairs_f;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Other
{
    public class FaultWindowVM : VievModelBase
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
        private ObservableCollection<Repair_Categories> repair_Categories = new ObservableCollection<Repair_Categories>();
        public ObservableCollection<Repair_Categories> Repair_Categories
        {
            get { return repair_Categories; }
            set
            {
                repair_Categories = value;
                RaisedPropertyChanged("Repair_Categories");
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
                FillCategoryComboBox();
                RaisedPropertyChanged("SelectedDefect");
            }
        }
        private Repair_Categories selectedCategory;
        public Repair_Categories SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                RaisedPropertyChanged("SelectedCategory");
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
            Repair_Categories = RepairCategoriesRequests.FillList();
        }
        public FaultWindowVM(RepairsTableEngineer page)
        {
            baseRepairTablePage = page;
            GetDeffects();
            Repair_Categories = RepairCategoriesRequests.FillList();
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
        private void FillCategoryComboBox()
        {
            if (selectedDefect != null)
            {
                Repair_Categories category = Repair_Categories.FirstOrDefault(c => c.Category == selectedDefect.Repair_Category);
                if (category != null)
                {
                    SelectedCategory = category;
                }
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
            if (selectedDefect != null && SelectedCategory != null)
            {
                if (baseRepairPage != null)
                {
                    baseRepairPage.tbxDefect.Text = selectedDefect.Defect;
                    baseRepairPage.tbxWork.Text = selectedDefect.Work;
                    SetCategoryAtBaseWindow();
                }
                else if (baseRepairTablePage != null)
                {
                    baseRepairTablePage.tbxDefect.Text = selectedDefect.Defect;
                    baseRepairTablePage.tbxWork.Text = selectedDefect.Work;
                    SetCategoryAtBaseWindow();
                }

                CloseFaultWindow();
            }
        }
        /// <summary>
        /// Устновка значения списка с категорией ремонта в окне ремонта или таблицы ремонтов
        /// </summary>
        private void SetCategoryAtBaseWindow()
        {
            if (baseRepairPage != null && baseRepairPage.cbxCategories.Items != null)
            {
                foreach (Repair_Categories item in baseRepairPage.cbxCategories.Items)
                {
                    if (item.Category == selectedCategory.Category)
                    {
                        baseRepairPage.cbxCategories.SelectedItem = item;
                    }
                }
            }
            else if (baseRepairTablePage != null && baseRepairTablePage.cbxCategories.Items != null)
            {
                foreach (Repair_Categories item in baseRepairTablePage.cbxCategories.Items)
                {
                    if (item.Category == selectedCategory.Category)
                    {
                        baseRepairTablePage.cbxCategories.SelectedItem = item;
                    }
                }
            }
        }
        /// <summary>
        /// Закрывает окно FaultWindow
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
            // удаляет последние запятые, если несколько неисправностей
            if (defectsCount > 0)
            {
                defectTemp.Remove(defectTemp.Length - 1);
                workTemp.Remove(workTemp.Length - 1);
            }

            if (baseRepairPage != null && SelectedCategory != null)
            {
                baseRepairPage.tbxDefect.Text = defectTemp;
                baseRepairPage.tbxWork.Text = workTemp;
                SendHardRepairCategory();

            }
            else if (baseRepairTablePage != null && SelectedCategory != null)
            {
                baseRepairTablePage.tbxDefect.Text = defectTemp;
                baseRepairTablePage.tbxWork.Text = workTemp;
                SendHardRepairCategory();
            }

            CloseFaultWindow();
        }
        /// <summary>
        /// Устновка значения  списка с категорией ремонта в окне ремонта или таблицы ремонтов на "Сложный ремонт"
        /// </summary>
        /// <returns></returns>
        private void SendHardRepairCategory()
        {
            Repair_Categories category = Repair_Categories.FirstOrDefault(c => c.Category == "Сложный ремонт");
            if(!IsSomeFaults())
            {
                var defect = Defects.FirstOrDefault(d => d.IsSelected == true);
                category = Repair_Categories.FirstOrDefault(c => c.Category == defect.Repair_Category);
            }

            if (baseRepairPage != null && baseRepairPage.cbxCategories.Items != null)
            {
                foreach (Repair_Categories item in baseRepairPage.cbxCategories.Items)
                {
                    if (item.Category == category.Category)
                    {
                        baseRepairPage.cbxCategories.SelectedItem = item;
                    }
                }
            }
            else if (baseRepairTablePage != null && baseRepairTablePage.cbxCategories.Items != null)
            {
                foreach (Repair_Categories item in baseRepairTablePage.cbxCategories.Items)
                {
                    if (item.Category == category.Category)
                    {
                        baseRepairTablePage.cbxCategories.SelectedItem = item;
                    }
                }
            }
        }
        /// <summary>
        /// Возвращает true если несколько выбранных неисправностей
        /// </summary>
        /// <returns></returns>
        private bool IsSomeFaults()
        {
            var query = Defects.Where(d => d.IsSelected == true).ToList();
            if(query != null)
            {
                if (query.Count > 1)
                    return true;
            }
            return false;
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
            if (!string.IsNullOrWhiteSpace(Defect) && !string.IsNullOrWhiteSpace(Work) && SelectedCategory != null)
            {
                Defects defect = new Defects { Defect = Defect, Work = Work, Repair_Category = SelectedCategory.Category };
                DefectsRequests.AddToBase(defect);
                Defects.Add(defect);
                selectedDefect = null;
                Defect = string.Empty;
                Work = string.Empty;
                GetDeffects();
            }
            else
                MessageBox.Show("Введите неисправность, проделанную работу и выберете категорию ремонта", "Ошибка");

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
                if (!string.IsNullOrWhiteSpace(Defect) && !string.IsNullOrWhiteSpace(Work) && SelectedCategory != null)
                {
                    selectedDefect.Defect = Defect;
                    selectedDefect.Work = Work;
                    selectedDefect.Repair_Category = SelectedCategory.Category;
                    DefectsRequests.EditItem(selectedDefect);
                    GetDeffects();
                    MessageBox.Show("Успешное изменение!");
                }
                else
                    MessageBox.Show("Введите неисправность, проделанную работу и выберете категорию ремонта", "Ошибка");
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

        #region Test search
        private string _searchString;

        public string SearchString
        {
            get { return _searchString; }
            set
            {
                if (SetProperty(ref _searchString, value))
                {

                    PropertyInfo prop = typeof(Defects).GetProperty("Defect");
                    if (prop != null)
                    {
                        if (
                            Defects.Any(
                                p =>
                                    prop.GetValue(p)
                                        .ToString()
                                        .ToLower()
                                        .Contains(_searchString.ToLower())))
                        {
                            SelectedDefect =
                                Defects.First(
                                    p =>
                                        prop.GetValue(p)
                                            .ToString()
                                            .ToLower()
                                            .Contains(_searchString.ToLower()));
                        }
                    }
                }
            }
        }
        #endregion
    }
}
