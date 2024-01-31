using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Language
    {
        public int Id { get; set; }

        public int AppId { get; set; }

        public string LanguageName { get; set; }

        public bool? CanRead { get; set; }

        public bool? CanWrite { get; set; }

        public bool? CanSpeak { get; set; }

    }
}
