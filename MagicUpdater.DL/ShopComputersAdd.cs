//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MagicUpdater.DL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShopComputersAdd
    {
        public int ComputerId { get; set; }
        public Nullable<System.DateTime> AvgPerformanceCounterValuesDateTimeUtc { get; set; }
        public Nullable<double> AvgCpuTime { get; set; }
        public Nullable<double> AvgRamAvailableMBytes { get; set; }
        public Nullable<double> AvgDiskQueueLength { get; set; }
    
        public virtual ShopComputer ShopComputer { get; set; }
    }
}