using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Skills
    {
        public int Id { get; set; }

        public int AppId { get; set; }

        public string SkillName { get; set; }

        public int SkillRate { get; set; }
    }
}
