using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
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
            m_buffer = new byte[width * height * 3];
        }

        public Pixel GetPixel(uint x, uint y)
        {
            Pixel result = new Pixel();
            uint index = (y * m_width) + x;
            index *= 3;
            result.R = m_buffer[index];
            result.G = m_buffer[index + 1];
            result.B = m_buffer[index + 2];
            return result;
        }

        public void SetPixel(uint x, uint y, ref Pixel p)
        {

            uint index = (y * m_width) + x;
            index *= 3;
            m_buffer[index] = p.R;
            m_buffer[index + 1] = p.G;
            m_buffer[index + 2] = p.B;
        }

        public BitmapSource Source()
        {
           
            int stride = 3 * (int)Width;
            BitmapSource BitImg = BitmapSource.Create((int)Width, (int)Height, 96, 96, PixelFormats.Rgb24, null, m_buffer, stride);
            List<Color> c = BitImg.Palette.Colors.ToList();
            return BitImg;
        }
    }
}
