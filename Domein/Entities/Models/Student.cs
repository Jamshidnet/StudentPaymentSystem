using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domein.Models
{
    public class Student : BaseAuditableEntity
    {
        public string? Name { get; set; }

        public string? Email { get; set; }
        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public bool EnrollmentStatus { get; set; }
    }
}
