using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicUpdaterSettings
{
    public static class Constants
    {
        public static string JsonConnectionSettingsFileName {get; private set;} = Path.Combine(Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location),
                "settings.json");
        public const string PIPE_NAME = "PipesOfPiece";
    }
}
