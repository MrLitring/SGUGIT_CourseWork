using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGUGIT_CourseWork.HelperCode
{
    public static class EventBus
    {
        public static Action onDataBaseChange;
        public static Action<int> onError;
    }
}
