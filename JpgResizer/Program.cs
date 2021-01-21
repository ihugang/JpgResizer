using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace JpgResizer
{
    class Program
    {
        static bool isOverwrite = false;
        static Int64 ratio = 75L;
        static bool isIncludeSubDirectories = false;
        static bool isBackup = false;

        static void Main(string[] args)
        {
            
            string filename = "*.jpg";

            if (args.Length == 0)
            {
                Console.WriteLine("============== JPG Compress tool ==================");
                Console.WriteLine("= Author : shrek");
                Console.WriteLine("= Contact: (QQ390652)");
                Console.WriteLine("= Date   : 2021.01.21");
                Console.WriteLine("= Hangzhou Codans Cyberinfo Company. ");
                Console.WriteLine("===================================================");
                Console.WriteLine("Please enter resize argument.");
                Console.WriteLine("Usage: JpgResizer <filename> -r 75 -o -b");
                Console.WriteLine("Usage: JpgResizer *.jpg -r 75 -o -b -s");
                Console.WriteLine(" -b  : backup the original file. use when -o is set.");
                Console.WriteLine(" -o  : overwrite the original file. ");
                Console.WriteLine(" -s  : search all jpg files in sub-directories. ignore filename.");
                Console.WriteLine(" -r  : compress level. default is 75, 100 is best.");

                return;
            }

            filename = args[0];
            if (!filename.Contains(".jpg") && !filename.Contains(".jpeg") && !filename.Contains("*"))
            {
                Console.WriteLine("   missing filename.");
                return;
            }

            var index = 1;
            foreach (var item in args.Skip(1))
            {
                if (item.ToLower() == "-o")
                {
                    isOverwrite = true;
                }
                if (item.ToLower() == "-b")
                {
                    isBackup = true;
                }
                else if (item.ToLower()== "-s")
                {
                    isIncludeSubDirectories = true;
                }
                else if (item.ToLower()=="-r")
                {
                    if (args.Length >= index+1)
                    {
                        ratio = Int64.Parse(args[index + 1]);
                    }
                    else
                    {
                        Console.WriteLine("  missing ratio paramenter.");
                        return;
                    }
                }
                index++;
            }

            if (isIncludeSubDirectories)
            {
                ListJpg(AppContext.BaseDirectory);
            }
            else { 
                VaryQualityLevel(filename, ratio, isOverwrite);
            }

        }

        private static void ListJpg(string dir)
        {
            DirectoryInfo d = new DirectoryInfo(dir);
            FileInfo[] files = d.GetFiles("*.jpg");//文件
            DirectoryInfo[] directs = d.GetDirectories();//文件夹
            foreach (FileInfo f in files)
            {
                VaryQualityLevel(f.FullName, ratio, isOverwrite);  
            }
            //获取子文件夹内的文件列表，递归遍历  
            foreach (DirectoryInfo dd in directs)
            {
                ListJpg(dd.FullName);
            }
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private static void VaryQualityLevel(string fileName,Int64 ratio,bool isOverwrite)
        {
            Console.WriteLine($"filename : {fileName}");
            // Get a bitmap. The using statement ensures objects  
            // are automatically disposed from memory after use.  
            var originalSize = new FileInfo(fileName).Length;
            var newSize = 0L;


            var newFileName = Path.GetFileNameWithoutExtension(fileName) + "_new" + Path.GetExtension(fileName);
            if (isOverwrite)
            {
                newFileName = fileName;
                if (isBackup)
                {
                    var backupFileName = Path.GetFileNameWithoutExtension(fileName) + "_original" + Path.GetExtension(fileName);
                    File.Copy(fileName, backupFileName, true);
                }
            }
            using (Bitmap bmp0 = new Bitmap(fileName))
            {
                var bmp1 = new Bitmap(bmp0);
                bmp0.Dispose();
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                // Create an Encoder object based on the GUID  
                // for the Quality parameter category.  
                System.Drawing.Imaging.Encoder myEncoder =
                    System.Drawing.Imaging.Encoder.Quality;

                // Create an EncoderParameters object.  
                // An EncoderParameters object has an array of EncoderParameter  
                // objects. In this case, there is only one  
                // EncoderParameter object in the array.  
                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, ratio);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp1.Save(newFileName, jpgEncoder, myEncoderParameters);
                newSize = new FileInfo(newFileName).Length;
                Console.WriteLine($"  resize ok. {originalSize} -> {newSize} { ((originalSize - newSize) * 100 / originalSize).ToString("0.#") }% compressed.");
            }
        }
    }
}
