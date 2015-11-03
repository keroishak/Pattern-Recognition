using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
namespace Pattern_Recognition.Model
{
     struct Pixel
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
    }
    class Image
    {
        private byte[] m_buffer;
        private uint m_width, m_height;
        private uint m_components;
        bool m_needFlush;
        
        public uint Width
        {
            get { return m_width; }
        }

        public uint Height
        {
            get { return m_height; }
        }

        public byte[] Buffer
        {
            get { return m_buffer; }
        }
        public Image(Bitmap bmp)
        {
            //load code
            Pixel np=new Pixel();
            m_width = (uint)bmp.Width;
            m_height = (uint)bmp.Height;
            m_needFlush = false;
           
            if (bmp.PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
            {
                m_components = 3;
            }
            else if (bmp.PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed ||
                     bmp.PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed ||
                     bmp.PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
            {
                m_components = 4;
            }
            else if (bmp.PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
            {
                m_components = 1;
            }
            
            ImageConverter imageConverter = new ImageConverter();
             m_buffer = (byte[])imageConverter.ConvertTo(bmp, typeof(byte[]));
        }
        public Image(ref byte[] arr,int w,int h)
        {
            m_buffer = arr;
            m_width = (uint)w;
            m_height = (uint)h;
            m_components = 3;
        }
        public byte this[uint key]
        {
            get { return m_buffer[key]; }
            set
            {
                m_buffer[key] = value;
            }
        }

        public Image(uint width, uint height)
        {
            m_width = width;
            m_height = height;
            m_components = 3;
            m_buffer = new byte[width * height * 3];
        }

        public Pixel GetPixel(uint x, uint y)
        {
            Pixel result = new Pixel();
            uint index = (y * m_width) + x;
            index *= m_components;
            result.R = m_buffer[index];
            result.G = m_buffer[index + 1];
            result.B = m_buffer[index + 2];
            return result;
        }

        public void SetPixel(uint x, uint y, ref Pixel p)
        {

            uint index = (y * m_width) + x;
            index *= m_components;
            m_buffer[index] = p.R;
            m_buffer[index + 1] = p.G;
            m_buffer[index + 2] = p.B;
        }

        public BitmapSource Source()
        {
           
            int stride = (int)m_components * (int)Width;
            BitmapSource BitImg = BitmapSource.Create((int)Width, (int)Height, 96, 96, PixelFormats.Rgb24, null, m_buffer, stride);
            return BitImg;
        }
    }
}
