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
    public class Course : BaseAuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }


        public string Instructor { get; set; }


        public DateTime StartDate { get; set; }


        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }
    }
}
