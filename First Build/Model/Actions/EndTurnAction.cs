using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Build
{

    public class EndTurnAction : Action
    {
        public override bool IsAvaliable => true;

        public EndTurnAction() : base()
        {
            header = "Закончить ход";
            text = "Закончить ход";
        }
    }
}
