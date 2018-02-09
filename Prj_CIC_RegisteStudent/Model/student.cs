using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_CIC_RegisteStudent.Model
{
    public class student
    {
        [Key]
        public int id { get; set; }
        public int year { get; set; }

        public string sch { get; set; }

        public string dept { get; set; }

        public int testno { get; set; }

        public string region { get; set; }

        public string name { get; set; }

        public string schname { get; set; }

        public string situ { get; set; }

        public string check { get; set; }
    }

    public class studentTestNo
    {
        public int testno{ get; set; }

        public int year { get; set; }
    }
}
