//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace live.courses.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class room
    {
        public room()
        {
            this.Room_Members = new HashSet<Room_Members>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string about { get; set; }
        public string photo { get; set; }
        public string admin { get; set; }
    
        public virtual ICollection<Room_Members> Room_Members { get; set; }
    }
}
