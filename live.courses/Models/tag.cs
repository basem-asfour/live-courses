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
    
    public partial class tag
    {
        public tag()
        {
            this.course_tags = new HashSet<course_tags>();
            this.work_group_tags = new HashSet<work_group_tags>();
        }
    
        public int id { get; set; }
        public string tag1 { get; set; }
    
        public virtual ICollection<course_tags> course_tags { get; set; }
        public virtual ICollection<work_group_tags> work_group_tags { get; set; }
    }
}