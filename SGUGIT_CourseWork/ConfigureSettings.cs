using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;


namespace SGUGIT_CourseWork
{
    public static class ConfigureSettings
    {
        //
        // Цветовая Схема
        //
        #region
        public static Color Colors(int Number)
        {
            // От яркого к самому темному
            switch (Number)
            {
                case 1:
                    return Color.FromArgb(237, 243, 255);

                case 2:
                    return Color.FromArgb(228, 236, 252);

                case 3:
                    return Color.FromArgb(209, 221, 248);

                case 4:
                    return Color.FromArgb(191, 202, 226);

                case 5:
                    return Color.FromArgb(174, 186, 210);

                case 6:
                    return Color.FromArgb(135, 146, 174);

                case 7:
                    return Color.Black;

                default:
                    return Colors(3);

            }
        }
        #endregion

        //
        // Font Settings
        //
        public static string FontName = "Golos Text";
        public static int FontSize(int Number = 1)
        {
            // От самого большого к самому маленькому
            switch (Number)
            {
                case 1:
                    return 18;

                case 2:
                    return 16;

                case 3:
                    return 14;

                default:
                    return 16;
            }
        }



        //
        // Voids
        //
        public static string pathForConfigurate = "";

        public static void MainConfigureSettings_Read()
        {
            if (File.Exists(pathForConfigurate))
            {

            }
            else
            {

            }
        }

        public static void MainConfigureSettings_Save()
        {

        }

    }
}
