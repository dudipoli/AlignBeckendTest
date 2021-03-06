using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlignBeckendApi.Dal.Entities
{
    public class MissionEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int MissionId { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public DateTime Date { get; set; }
        public string AgentAlias { get; set; }
    }
}
