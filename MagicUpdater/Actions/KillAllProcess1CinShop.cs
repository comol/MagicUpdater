using MagicUpdater.Abstract;
using MagicUpdater.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicUpdater.Actions
{
    class KillAllProcess1CinShop : OperAction
    {
          /// <summary>
          /// Если объявлено локальным сервером, то забирает с командного сервера список всех ip адресов этого магазина и отправляет по этим адресам команду на гашение 1С.
          /// Так же гасит 1С на этом компьютере.
          /// Если не объявлено локальным сервером, то только гасит 1С на этом компьютере.
          /// </summary>
        
        protected override void ActExecution()
        {
            
              
            if (ApplicationSettings.JsonSettings.IsLocatedServer == false)
            {
                new KillProcess1C().ActRun();
            }

            else
            {


               // Helpers.NetWork.SendActionStringToServer();
            }

        }
    
    }
}
