using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_14fi_WPF_masodik
{
    public class Dataa
    {
        public static ObservableCollection<JsonResponse> users = new ObservableCollection<JsonResponse>()   // mindenhonnan elérhető, mert public és static
            { new JsonResponse(), new JsonResponse()};                      // amikor a bal oldalon jelentkezem be, akkor a bal oldaliba jelentkezem be, amikor a jobb oldaliba jelntkezem be, akkor a jobb oldaliba fogja rögzíteni.
    }
}
