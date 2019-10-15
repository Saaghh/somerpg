using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace somerpg_uwp
{
    public static class Settings
    {
        public static bool DrawLevels { get; set; } = false;
        public static bool DrawStandart { get; set; } = true;
        public static bool DrawInnerTriangles { get; set; } = false;
        public static bool DrawPolygons { get; set; } = false;
        public static bool DrawHighlightedPolygon { get; set; } = true;
    }
}
