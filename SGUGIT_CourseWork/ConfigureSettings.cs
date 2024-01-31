using System.IO;
using System.Drawing;


namespace SGUGIT_CourseWork
{
    public static class ConfigureSettings
    {
        //
        // Цветовая Схема
        //
        public static Color Colors(int Number)
        {
            // От яркого к самому темному
            switch (Number)
            {
                case 1:
                    return Color.FromArgb(255, 255, 255);

                case 2:
                    return Color.FromArgb(231, 231, 231);

                case 3:
                    return Color.FromArgb(209, 209, 209);

                case 4:
                    return Color.FromArgb(182, 182, 182);

                case 5:
                    return Color.FromArgb(155, 155, 155);

                //case 6:
                //return Color.FromArgb(135, 146, 174);

                case 7:
                    return Color.Black;

                default:
                    return Colors(3);

            }
        }

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
                    return 16;

                case 2:
                    return 14;

                case 3:
                    return 12;

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
