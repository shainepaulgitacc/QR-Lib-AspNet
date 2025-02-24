﻿using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Model.Domain
{
    public class Employee:BaseModel
    {
        [Key]
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public Suffix? Suffix { get; set; }
        public Gender Gender { get; set; }
        public string? ProfilePicture { get; set; }
        public string Designation { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public virtual ICollection<EmployeeAttendance> _empAttendance { get; set; }
    }
}
