using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicUpdater.DL.Models
{
    public class ViewShedulerStepsModelParentChild
    {
        public int ParentId { get; set; }
        public int? ChildId { get; set; }
        public ShedulerStep StepData { get; set; }
        public bool IsChildPositiveBranch { get; set; }
    }
}
