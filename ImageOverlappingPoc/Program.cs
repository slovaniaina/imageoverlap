using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;

namespace ImageOverlappingPoc
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var imageCombiner = new ImageCombiner();
                Console.WriteLine("start image overlap");
                var resultPath =
                    $"C:\\Users\\aandrian\\Documents\\MB\\Image_Overlapping_POC\\test-{Guid.NewGuid()}.pdf";

                #region test with MB code
                //var background = new MagickImage("C:\\Users\\aandrian\\Documents\\MB\\Image_Overlapping_POC\\svg-background.svg");
                //var background = new MagickImage("C:\\Users\\aandrian\\Documents\\MB\\Image_Overlapping_POC\\frame-compressed.svg");
                var background = new MagickImage("C:\\Users\\aandrian\\Documents\\MB\\Image_Overlapping_POC\\image-background-EPSfile.eps");
                //var background = new MagickImage("C:\\Users\\aandrian\\Documents\\MB\\Image_Overlapping_POC\\PNGBackGroung.png");
                var userImage = new MagickImage("C:\\Users\\aandrian\\Documents\\MB\\Image_Overlapping_POC\\UGC.jpeg");
                //var userImage = new MagickImage("C:\\Users\\aandrian\\Documents\\MB\\Image_Overlapping_POC\\Small-mario.png");
                var textImage = new MagickImage("C:\\Users\\aandrian\\Documents\\MB\\Image_Overlapping_POC\\UserText.png");

                imageCombiner.MergeImage(userImage,background,textImage,"12345", resultPath);
                #endregion

                #region test with eps image
                //imageCombiner.ConvertEpsImage("C:\\Users\\aandrian\\Documents\\MB\\Image_Overlapping_POC\\image-background-EPSfile.eps", resultPath);
                #endregion

                Console.WriteLine("end image overlap");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message} ");
                Console.ReadLine();
            }
        }
    }
}
