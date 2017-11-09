using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrolinx.Sdk.Sidebar.Util
{
    public class GraphicUtil
    {
        public static float GetScaling()
        {
            float dpiX = 96F;
            using (var lbl = new System.Windows.Forms.Label())
            using (var g = lbl.CreateGraphics())
            {
                dpiX = g.DpiX / dpiX;
            }

            return dpiX;
        }
    }
}
