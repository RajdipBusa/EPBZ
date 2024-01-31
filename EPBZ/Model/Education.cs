using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Education
    {
        public int Id { get; set; }

        public int AppId { get; set; }
        public string Board { get; set; }

        public int? Year { get; set; }

        public decimal? CGPA { get; set; }
    }
}
