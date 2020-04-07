using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Pronets.Model.Barcode
{
    public abstract class AbstractDevice
    {
        private BarcodeLib.Barcode barcode = new BarcodeLib.Barcode();
        public string Manufacturer { get; protected set; }
        public string Model { get; private set; }
        public string SerialNumber { get; private set; }
        public string PonSerialNumber { get; private set; }
        public string WanMacAdress { get; private set; }
        public BitmapImage SerialNumberImage { get; private set; }
        public BitmapImage PonSerialNumberImage { get; private set; }
        public int SerialWidth { get; set; }
        public int SerialHeigth { get; set; }
        public int PonSerialWidth { get; set; }
        public int PonSerialHeigth { get; set; }
        public AbstractDevice(string model, string serialNumber, string ponSerialNumber, string wanMacAdress)
        {
            Model = model;
            SerialNumber = serialNumber;
            PonSerialNumber = ponSerialNumber;
            WanMacAdress = wanMacAdress;
            SerialWidth = 289;
            SerialHeigth = 49;
            PonSerialWidth = 425;
            PonSerialHeigth = 49;
            
            try
            {
                SerialNumberImage = BitmapToImageSource(GetBitmap(serialNumber, SerialWidth, SerialHeigth));
                PonSerialNumberImage = BitmapToImageSource(GetBitmap(ponSerialNumber, PonSerialWidth, PonSerialHeigth));
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public BitmapImage GenerateSerialNumberImage(double heigth)
        {
            double width = heigth * 5;
            return BitmapToImageSource(GetBitmap(SerialNumber, (int)width, (int)heigth));
        }
        public BitmapImage GeneratePonSerialNumberImage(double heigth)
        {
            double width = heigth * 8.67;
            return BitmapToImageSource(GetBitmap(SerialNumber, (int)width, (int)heigth));
        }

        protected BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
        private Bitmap GetBitmap(string text, int width, int heigth)
        {
           return (Bitmap)barcode.Encode(BarcodeLib.TYPE.CODE128, text, System.Drawing.Color.Black, System.Drawing.Color.White, width, heigth);
        }
    }
}
