//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PIDHub.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FlightControllerHardware
    {
        public FlightControllerHardware()
        {
            this.Quads = new HashSet<Quad>();
        }
    
        public int ID { get; set; }
        public string FCHardwareName { get; set; }
    
        public virtual ICollection<Quad> Quads { get; set; }
    }
}
