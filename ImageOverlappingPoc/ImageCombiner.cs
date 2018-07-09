﻿using System;
using ImageMagick;
using System.Configuration;

namespace ImageOverlappingPoc
{
    public class ImageCombiner
    {
        public bool KeepEpsFormat { get; set; }

        public ImageCombiner()
        {
            if (ConfigurationManager.AppSettings["KeepEpsFormat"] != null)
            {
                KeepEpsFormat = bool.Parse(ConfigurationManager.AppSettings["KeepEpsFormat"]);
            }    
        }

        public void CombineImage(string firstImagePath, string secondImagePath, string resultPath)
        {
            using (var images = new MagickImageCollection())
            {
                // Add the first image
                var first = new MagickImage(firstImagePath);
                images.Add(first);

                // Add the second image
                var second = new MagickImage(secondImagePath);
                images.Add(second);

                // Create a mosaic from both images
                using (var result = images.Mosaic())
                {
                    // Save the result
                    result.Write(resultPath);
                }
            }
        }

        public void ConvertEpsImage(string epsImagePath, string resultImagePath)
        {
            MagickNET.SetGhostscriptDirectory(@"C:\Program Files\gs\gs9.23\bin");
            using (var image = new MagickImage(epsImagePath))
            {
                image.Write(resultImagePath);
            }
        }

        #region MB code snippet

        public void MergeImage(MagickImage userImage, MagickImage backgroundImage, MagickImage textImage, string orderNumber, string resultPath)
        {
            #region use ghostscript
            MagickNET.SetGhostscriptDirectory(@"C:\Program Files\gs\gs9.23\bin");
            #endregion

            using (var backgroundImageClone = new MagickImage(backgroundImage.Clone()))
            {
                using (var mergedImage = new MagickImageCollection())
                {
                    //var packPersonalizationSettings = this.GetPackPersoSettings;
                    var imageXPosition = Constants.PackPersonalizationPdfSettings.UserImage.UserImageXPosition;
                    var imageYPosition = Constants.PackPersonalizationPdfSettings.UserImage.UserImageYPosition;
                    var imageRotation = Constants.PackPersonalizationPdfSettings.UserImage.Rotation;
                    userImage.Page = new MagickGeometry(imageXPosition, imageYPosition, 0, 0);
                    userImage.Rotate(imageRotation);
                    mergedImage.Add(userImage);

                    if (KeepEpsFormat)
                    {
                        #region keep the EPS format
                        Console.WriteLine(
                            $"background before : width {backgroundImageClone.Width} height {backgroundImageClone.Height}");
                        backgroundImageClone.Page = new MagickGeometry(imageXPosition, imageYPosition, 0, 0);
                        backgroundImageClone.Resize(2480, 3508);
                        Console.WriteLine(
                            $"background after : width {backgroundImageClone.Width} height {backgroundImageClone.Height}");
                        mergedImage.Add(backgroundImageClone);
                        #endregion
                    }
                    else
                    {
                        #region convert the backgroundimage to PNG
                        Console.WriteLine("Convert the EPS image to PNG");
                        backgroundImageClone.Write("C:\\Users\\aandrian\\Documents\\MB\\Image_Overlapping_POC\\image-background-PNGfile.png");
                        var backgroundImageClonePng = new MagickImage("C:\\Users\\aandrian\\Documents\\MB\\Image_Overlapping_POC\\image-background-PNGfile.png");
                        Console.WriteLine(
                            $"background before : width {backgroundImageClonePng.Width} height {backgroundImageClonePng.Height}");
                        backgroundImageClonePng.Page = new MagickGeometry(imageXPosition, imageYPosition, 0, 0);
                        backgroundImageClonePng.Resize(2480, 3508);
                        Console.WriteLine(
                            $"background after : width {backgroundImageClonePng.Width} height {backgroundImageClonePng.Height}");
                        mergedImage.Add(backgroundImageClonePng);
                        #endregion
                    }

                    textImage.Resize(Constants.PackPersonalizationPdfSettings.UserText.UserTextResizedWidth,
                        Constants.PackPersonalizationPdfSettings.UserText.UserTextResizedHeight);
                    textImage.Page =
                        new MagickGeometry(Constants.PackPersonalizationPdfSettings.UserText.UserTextXPosition,
                            Constants.PackPersonalizationPdfSettings.UserText.UserTextYPosition, 0, 0);
                    textImage.Rotate(Constants.PackPersonalizationPdfSettings.UserText.Rotation);
                    mergedImage.Add(textImage);

                    MagickImage resultImage = (MagickImage)mergedImage.Mosaic();

                    resultImage.Format = MagickFormat.Pdf;
                    resultImage.Quality = Constants.PackPersonalizationPdfSettings.Quality;
                    resultImage.Depth = Constants.PackPersonalizationPdfSettings.Depth;
                    resultImage.Density = new Density(Constants.PackPersonalizationPdfSettings.Density);
                    AddOrderNumber(ref resultImage, orderNumber);

                    resultImage.Write(resultPath);
                }
            }
        }

        private static void AddOrderNumber(ref MagickImage image, string text)
        {
            new Drawables().FontPointSize(Constants.PackPersonalizationPdfSettings.OrderNumber.FontSize)
                .Font("Arial")
                .TextAntialias(true)
                .StrokeWidth(4)
                .StrokeColor(new MagickColor(Constants.PackPersonalizationPdfSettings.OrderNumber.OrderNumberTextColor))
                .FillColor(new MagickColor(Constants.PackPersonalizationPdfSettings.OrderNumber.OrderNumberTextColor))
                .TextAlignment(TextAlignment.Left)
                .Rotation(Constants.PackPersonalizationPdfSettings.OrderNumber.Rotation)
                .Text(Constants.PackPersonalizationPdfSettings.OrderNumber.OrderNumberXPosition,
                    Constants.PackPersonalizationPdfSettings.OrderNumber.OrderNumberYPosition, text).Draw(image);
        }

        #endregion
    }
}
