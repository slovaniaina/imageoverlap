using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageOverlappingPoc
{
    public static class Constants
    {
        public static class PackPersonalizationPdfSettings
        {

            public static class UserImage
            {
                //public static readonly int UserImageYPosition = 1760;

                //public static readonly int UserImageXPosition = 805;

                //public static readonly int Rotation = 270;

                public static readonly int UserImageYPosition = 820;

                public static readonly int UserImageXPosition = 805;

                public static readonly int Rotation = 270;
            }

            public static class UserText
            {
                public static readonly int UserTextXPosition = 2360;

                public static readonly int UserTextYPosition = 2900;

                public static readonly int UserTextResizedWidth = 1320;

                public static readonly int UserTextResizedHeight = 590;

                public static readonly int Rotation = 270;

            }

            public static class OrderNumber
            {
                public static readonly int OrderNumberXPosition = -3782;

                public static readonly int OrderNumberYPosition = -663;

                public static readonly int Rotation = 180;

                public static readonly string OrderNumberTextColor = "#CCFFFFFF";

                public static readonly int FontSize = 50;
            }

            public static readonly int Quality = 50;

            public static readonly int Depth = 300;

            public static readonly int Density = 300;
        }
    }
}
