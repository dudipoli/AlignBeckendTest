using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AlignBeckendTest.Models
{
    public class Mission
    {
        [StringLength(3)]
        [Required]
        public string Agent { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
