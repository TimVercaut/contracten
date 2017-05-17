using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ContractenOpvolging.Models.ContractenModels;

namespace ContractenOpvolging.Models
{
    public class ContractVerlengingViewModel
    {
        [Key]
        public int ContractID { get; set; }
        public string Consultant { get; set; }
        public string Klant { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Einddatum")]
        public DateTime EindDatum { get; set; }
        [MinimumDatum]
        [DataType(DataType.Date)]
        [Display(Name = "Nieuwe einddatum")]
        public DateTime NieuweEindDatum { get; set; }
        public Verlenging VerlengKleur { get; set; }
        public string NieuweKleur { get; set; }
    }

    public class MinimumDatumAttribute : RangeAttribute
    {
        public MinimumDatumAttribute()
          : base(typeof(DateTime),
                DateTime.Now.ToString(),
                DateTime.Now.AddYears(5).ToString())
        { }
    }
}
