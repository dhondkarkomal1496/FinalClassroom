//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalClassroom.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClassRoom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassRoom()
        {
            this.BookRooms = new HashSet<BookRoom>();
            this.ClassRoomDetails = new HashSet<ClassRoomDetail>();
        }
    
        public int ClassRoomId { get; set; }
        public string ClassRoomName { get; set; }
        public byte[] ClassroomImage { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookRoom> BookRooms { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassRoomDetail> ClassRoomDetails { get; set; }
    }
}