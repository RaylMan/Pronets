using Pronets.Data;
using Pronets.VievModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.Model
{
    class RepairsModel : VievModelBase
    {
        protected Repairs repair = new Data.Repairs();
        protected ObservableCollection<Repairs> repairs = new ObservableCollection<Repairs>();
        public ObservableCollection<Repairs> Repairs
        {
            get { return repairs; }

            set
            {
                repairs = value;
                RaisedPropertyChanged("Repairs");
            }
        }
        //protected v_Repairs v_Repair;
        protected ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
        public ObservableCollection<v_Repairs> V_Repairs
        {
            get { return v_Repairs; }

            set
            {
                v_Repairs = value;
                RaisedPropertyChanged("V_Repairs");
            }
        }
        protected ObservableCollection<v_Repairs> v_RepairsCopy = new ObservableCollection<v_Repairs>();
        public ObservableCollection<v_Repairs> V_RepairsCopy
        {
            get { return v_RepairsCopy; }

            set
            {
                v_RepairsCopy = value;
                RaisedPropertyChanged("V_RepairsCopy");
            }
        }
        protected int? repairId;
        public int? RepairId
        {
            get { return repairId; }
            set
            {
                repairId = value;
                RaisedPropertyChanged("RepairId");
            }
        }
        protected int? documentId;
        public int? DocumentId
        {
            get { return documentId; }
            set
            {
                documentId = value;
                RaisedPropertyChanged("DocumentId");
            }
        }
        protected string nomenclature;
        public string Nomenclature
        {
            get { return nomenclature; }
            set
            {
                nomenclature = value;
                RaisedPropertyChanged("Nomenclature");
            }
        }
        protected string serial_Number;
        public string Serial_Number
        {
            get { return serial_Number; }
            set
            {
                serial_Number = value;
                RaisedPropertyChanged("Serial_Number");
            }
        }
        protected string claimed_Malfunction;
        public string Claimed_Malfunction
        {
            get { return claimed_Malfunction; }
            set
            {
                claimed_Malfunction = value;
                RaisedPropertyChanged("Claimed_Malfunction");
            }
        }
        protected int? client;
        public int? Client
        {
            get { return client; }
            set
            {
                client = value;
                RaisedPropertyChanged("Client");
            }
        }
        protected int client_Id;
        public int Client_Id
        {
            get { return client_Id; }
            set
            {
                client_Id = value;
                RaisedPropertyChanged("Client_Id");
            }
        }
        protected string client_Name;
        public string Client_Name
        {
            get { return client_Name; }
            set
            {
                client_Name = value;
                RaisedPropertyChanged("Client_Name");
            }
        }


        protected DateTime? date_Of_Receipt;
        public DateTime? Date_Of_Receipt
        {
            get { return date_Of_Receipt; }
            set
            {
                date_Of_Receipt = value;
                RaisedPropertyChanged("Date_Of_Receipt");
            }
        }
        protected DateTime departure_Date;
        public DateTime Departure_Date
        {
            get { return departure_Date; }
            set
            {
                departure_Date = value;
                RaisedPropertyChanged("Departure_Date");
            }
        }
        //protected int? inspector;
        //public int? Inspector
        //{
        //    get { return inspector; }
        //    set
        //    {
        //        inspector = value;
        //        RaisedPropertyChanged("Inspector");
        //    }
        //}
        protected int inspectorId;
        public int InspectorId
        {
            get { return inspectorId; }
            set
            {
                inspectorId = value;
                RaisedPropertyChanged("InspectorId");
            }
        }
        protected string inspector;
        public string Inspector
        {
            get { return inspector; }
            set
            {
                inspector = value;
                RaisedPropertyChanged("Inspector");
            }
        }
        protected string warranty;
        public string Warranty
        {
            get { return warranty; }
            set
            {
                warranty = value;
                RaisedPropertyChanged("Warranty");
            }
        }
        protected string identifie_Fault;
        public string Identifie_Fault
        {
            get { return identifie_Fault; }
            set
            {
                identifie_Fault = value;
                RaisedPropertyChanged("Identifie_Fault");
            }
        }
        protected string work_Done;
        public string Work_Done
        {
            get { return work_Done; }
            set
            {
                work_Done = value;
                RaisedPropertyChanged("Work_Done");
            }
        }
        protected string repair_Category;
        public string Repair_Category
        {
            get { return repair_Category; }
            set
            {
                repair_Category = value;
                RaisedPropertyChanged("Repair_Category");
            }
        }
        protected int engineerId;
        public int EngineerId
        {
            get { return engineerId; }
            set
            {
                engineerId = value;
                RaisedPropertyChanged("EngineerId");
            }
        }
        protected string engineer;
        public string Engineer
        {
            get { return engineer; }
            set
            {
                engineer = value;
                RaisedPropertyChanged("Engineer");
            }
        }

        protected DateTime? repair_Date;
        public DateTime? Repair_Date
        {
            get { return repair_Date; }
            set
            {
                repair_Date = value;
                RaisedPropertyChanged("Repair_Date");
            }
        }
        protected string status;
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                RaisedPropertyChanged("Status");
            }
        }
        protected string note;
        public string Note
        {
            get { return note; }
            set
            {
                note = value;
                RaisedPropertyChanged("Note");
            }
        }
        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                if (value == isChecked) return;
                isChecked = value;
                RaisedPropertyChanged();
            }
        }
    }
}
