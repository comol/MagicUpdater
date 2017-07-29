using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepTreeControl
{
    [Serializable]
    public class StepsProperties
    {
        public string OperationName { get; set; }
        public int OperationId { get; set; }
        public bool IsPositive { get; set; }
        public int WaitingForSuccessInterval { get; set; }
        public bool IsOperErrorEqualRuntimeError { get; set; }
        public bool Is1cErrorEqualRuntimeError { get; set; }
        public int RepeatTimesOnLackOf1cResponse { get; set; }
        public int RepeatingIntervalOn1cWaiting { get; set; }
        public int Id { get; set; }
        public int ParentId { get; set; }
    }
}
