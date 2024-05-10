using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGUGIT_CourseWork.HelperCode
{
    /// <summary>
    /// Класс хранящий основные слова и предложения
    /// </summary>
    public static class GeneralTextData
    {
        public static char Alpha = '\u03B1';
        public static char Assure = '\u039C';
        public static char Micro = '\u03BC';



        public static string Error = "Owubka";
        public static string Error_MinimumTwoPoints = "Минимальное количество точек в системе должно быть не менее двух!";
        public static string Error_SystemCount = "Количество точек в каждом блоке должны совпадать!";
        public static string Error_FieldEmpty = "Необходимо заполнить обязательные поля!";



        public static string Warning = "Предупреждение";
        public static string Warning_BlockUnstable = "В данном блоке наблюдается нестабильность. Пожалуйста, перейдите к следующему уровню декомпозиции.";
    }
}
