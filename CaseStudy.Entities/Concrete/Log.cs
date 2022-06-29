using CaseStudy.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Entities.Concrete
{
    [Table("WEB959_LOG")]
    public class Log:IEntity
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(200)]
        [Column("METHOD")]
        public string Method { get; set; }
        [StringLength(500)]
        [Column("LOGDETAIL")]
        public string LogDetail { get; set; }
        [Column("STATUSCODE")]
        public int StatusCode { get; set; }
        [Column("CREATEDDATE")]
        public DateTime CreatedDate { get; set; }

    }
}
