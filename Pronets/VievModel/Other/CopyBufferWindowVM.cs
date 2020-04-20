using Pronets.Data;
using Pronets.EntityRequests.Users_f;
using Pronets.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Other
{
    public class CopyBufferWindowVM : VievModelBase
    {
        #region Properties
        Users user;
        private string copyBuffer;
        public string CopyBuffer
        {
            get { return copyBuffer; }
            set
            {
                copyBuffer = value;
                RaisedPropertyChanged("CopyBuffer");
            }
        }
        private string bufferSerial;
        public string BufferSerial
        {
            get { return bufferSerial; }
            set
            {
                bufferSerial = value;
                RaisedPropertyChanged("BufferSerial");
            }
        }
        private string bufferPonMac;
        public string BufferPonMac
        {
            get { return bufferPonMac; }
            set
            {
                bufferPonMac = value;
                RaisedPropertyChanged("BufferPonMac");
            }
        }
        private string bufferMac;
        public string BufferMac
        {
            get { return bufferMac; }
            set
            {
                bufferMac = value;
                RaisedPropertyChanged("BufferMac");
            }
        }
        #endregion
        public CopyBufferWindowVM()
        {
            GetCopyBuffer();
        }
        private void GetCopyBuffer()
        {
            try
            {
                user = UsersRequest.GetDefauldUser();
                CopyBuffer = user.CopyBuffer;
                BufferSerial = user.BufferSerial;
                BufferPonMac = user.BufferPonMac;
                BufferMac = user.BufferMac;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"Ошибка");
            }
            
        }

        #region SaveCopyBufferCommand
        private ICommand saveCopyBufferCommand;
        public ICommand SaveCopyBufferCommand
        {
            get
            {
                if (saveCopyBufferCommand == null)
                {
                    saveCopyBufferCommand = new RelayCommand(new Action<object>(SaveCopyBuffer));
                }
                return saveCopyBufferCommand;
            }
            set
            {
                saveCopyBufferCommand = value;
                RaisedPropertyChanged("SaveCopyBufferCommand");
            }
        }
        public void SaveCopyBuffer(object Parameter)
        {
            if (user != null)
            {
                try
                {
                    BufferView buffer = new BufferView();
                    buffer.CopyBuffer = CopyBuffer.Replace(" ", "");
                    buffer.BufferSerial = BufferSerial.Replace(" ", "");
                    buffer.BufferPonMac = BufferPonMac.Replace(" ", "");
                    buffer.BufferMac = BufferMac;

                    UsersRequest.SaveBuffer(user.UserId, buffer);
                    MessageBox.Show("Сохранено");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            } 
        }
        #endregion
        #region RemoveCopyBufferCommand
        private ICommand removeCopyBufferCommand;
        public ICommand RemoveCopyBufferCommand
        {
            get
            {
                if (removeCopyBufferCommand == null)
                {
                    removeCopyBufferCommand = new RelayCommand(new Action<object>(RemoveCopyBuffer));
                }
                return removeCopyBufferCommand;
            }
            set
            {
                removeCopyBufferCommand = value;
                RaisedPropertyChanged("RemoveCopyBufferCommand");
            }
        }
        public void RemoveCopyBuffer(object Parameter)
        {
            if (user != null)
            {
                try
                {
                    CopyBuffer = string.Empty;
                    BufferSerial = string.Empty;
                    BufferMac = string.Empty;
                    BufferPonMac = string.Empty;
                    UsersRequest.SaveBuffer(user.UserId, new BufferView());
                   // UsersRequest.SetCopyBufer(user.UserId, "");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
        }
        #endregion
    }
}
