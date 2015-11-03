using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Recognition.Model
{
    class ImageLoader
    {

        private static string deduceEXT(string file)
        {
            var array = file.Split('.');
            if (array.Length > 1)
                return array[array.Length - 1];
            else
                return null;
        }

        public static Bitmap CreateNonIndexedImage(System.Drawing.Image src)
        {
            Bitmap newBmp = new Bitmap(src.Width, src.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics gfx = Graphics.FromImage(newBmp))
            {
                gfx.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
                gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                gfx.DrawImage(src, new Rectangle(0, 0, src.Width, src.Height));
            }

            return newBmp;
        }

        public static Image load_PPM(string file)
        {
            Image result = null;
            BinaryReader rd = new BinaryReader(new FileStream(file,FileMode.Open));
            int count = 0;
            int width = 0, height = 0;
            string type = "";
            try
            {
                while (count < 4)
                {
                    char c = (char) rd.PeekChar();
                    if (c == '#')
                    {
                        while (rd.ReadChar() != '\n') ;
                    }
                    else if (Char.IsWhiteSpace(c))
                    {
                        rd.ReadChar();
                    }
                    else
                    {
                        if (count == 0)
                        {
                            type += rd.ReadChar().ToString() + rd.ReadChar().ToString();
                            count++;
                        }
                        else if (count == 1)
                        {
                            width = ReadInt(rd);
                            count++;
                        }
                        else if (count == 2)
                        {
                            height = ReadInt(rd);
                            count++;
                        }
                        else if (count == 3)
                        {
                            //this is always 255
                            ReadInt(rd);
                            count++;
                        }
                        else
                        {
                            throw new Exception("can't parse file");
                        }
                    }
                }

                PixelFormat p;
                int components;
                if (type == "P3")
                {
                    p = PixelFormat.Format24bppRgb;
                    components = 3;
                }
                else if (type == "P6")
                {
                    p = PixelFormat.Format24bppRgb;
                    components = 3;
                }
                else
                {
                    throw new Exception("Can't identify type");
                }

                result = new Image((uint) width, (uint) height, (uint) components);

                if (type == "P3")
                {
                    int chars = (int) (rd.BaseStream.Length - rd.BaseStream.Position);
                    char[] data = rd.ReadChars(chars);

                    string val = "";
                    uint index = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (Char.IsWhiteSpace(data[i]))
                        {
                            if (val != "")
                            {
                                result[index] = (byte) int.Parse(val);
                                val = "";
                                index++;
                            }
                        }
                        else
                        {
                            val += data[i];
                        }
                    }
                }
                else
                {
                    int bytes = (int)(rd.BaseStream.Length - rd.BaseStream.Position);
                    byte[] data = rd.ReadBytes(bytes);
                    for (int i = 0; i < bytes; i++)
                        result[(uint)i] = data[i];
                }

                //it's Beeeeeeep BGR NOT RGB DAMN IT
                for (uint i = 0; i < result.Buffer.Length; i += 3)
                {
                    byte tmp = result[i];
                    result[i] = result[i + 2];
                    result[i + 2] = tmp;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                rd.Close();
            }

            result.flush();
            return result;
        }

        private static int ReadInt(BinaryReader rd)
        {
            string val = "";
            while (!Char.IsWhiteSpace((char)rd.PeekChar()))
            {
                val += rd.ReadChar().ToString();
            }
            rd.ReadByte();
            return int.Parse(val);
        }

        private static void save_P3(string file, Image img)
        {
            StreamWriter rw = new StreamWriter(new FileStream(file, FileMode.Create));

            try
            {
                rw.Write("P3\n");
                rw.Write("#saved by our software\n");

                rw.Write(img.Width);
                rw.Write(" ");
                rw.Write(img.Height);
                rw.Write("\n");

                rw.Write("255\n");

                for (uint i = 0; i < img.Buffer.Length; i+=3)
                {
                    rw.Write(img[i+2].ToString()+" ");
                    rw.Write(img[i + 1].ToString()+" ");
                    rw.Write(img[i].ToString());

                    if(i+3 != img.Buffer.Length)
                        rw.Write(" ");
                }
            }
            finally
            {
                rw.Close();
            }
        }

        private static void save_P6(string file, Image img)
        {
            StreamWriter rw = new StreamWriter(new FileStream(file, FileMode.Create));

            try
            {
                rw.Write("P6\n");
                rw.Write("#saved by our software\n");

                rw.Write(img.Width);
                rw.Write(" ");
                rw.Write(img.Height);
                rw.Write("\n");

                rw.Write("255\n");
            }
            finally
            {
                rw.Close();
            }

            BinaryWriter bw = new BinaryWriter(new FileStream(file,FileMode.Append));

            try
            {
                for (int i = 0; i < img.Buffer.Length; i += 3)
                {
                    bw.Write(img.Buffer[i+2]);
                    bw.Write(img.Buffer[i + 1]);
                    bw.Write(img.Buffer[i]);
                }
            }
            finally
            {
                bw.Close();
            }
        }

        public static Image LoadImage(string filePath)
        {
            string ext = deduceEXT(filePath);

            if (ext.ToLower() == "ppm")
            {
                //custom loader
                //return loadPPM(filePath);
                return load_PPM(filePath);
            }
            else
            {
                Bitmap bitmap = new Bitmap(filePath);
                return new Image(bitmap);
            }
        }

        public static void SaveImage(string filePath, Image img)
        {
            string ext = deduceEXT(filePath);

            if (ext.ToLower() == "p3")
            {
                //save P3
                filePath = filePath.Replace("."+ext, ".ppm");
                save_P3(filePath,img);
            }else if (ext.ToLower() == "p6")
            {
                //save P6
                filePath = filePath.Replace("." + ext, ".ppm");
                save_P6(filePath, img);
            }
            else
            {
                if(ext.ToLower() == "jpg")
                    img.bitmap.Save(filePath,ImageFormat.Jpeg);
                else if(ext.ToLower() == "bmp")
                    img.bitmap.Save(filePath, ImageFormat.Bmp);
                else if(ext.ToLower() == "png")
                    img.bitmap.Save(filePath, ImageFormat.Png);
                else
                    throw new Exception("unidentified image type");
            }
        }
    }
}
