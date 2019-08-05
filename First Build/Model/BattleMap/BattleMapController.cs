using First_Build.Model.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Build.Model.BattleMap
{
    public abstract class BattleMapController : BattleMapModel
    {
        public BattleMapController((int x, int y) measures) : base(measures) { }
    }
}
