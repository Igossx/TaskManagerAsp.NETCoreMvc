using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TaskMgr.CustomValidations;

namespace TaskMgr.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Nazwa")]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Opis")]
        [MaxLength(500)]
        public string Description { get; set; }

        [DefaultValue(false)]
        public bool Done { get; set; }

        [DataType(DataType.Date)]
        [DateLessThanOrEqualToToday]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Data")]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:t}", ApplyFormatInEditMode = true)]
        [DisplayName("Godzina")]
        public DateTime Time { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Data utworzenia")]
        public DateTime StartDate { get; set; }

    }
}
