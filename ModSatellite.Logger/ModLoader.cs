using System.Diagnostics;
using System.Threading.Tasks;

namespace ModSatellite.Logger
{
    public class ModLoader : TraceListener
    {
        public ModLoader()
        {

        }


        public override void Write(string message)
        {

        }

        public override void WriteLine(string message)
        {
            return;
        }

    }
}
