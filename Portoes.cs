using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GAPA
{
    public class Portoes
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string ip { get; set; }
        public bool captura { get; set; } 
        public int camera { get; set; }
        public bool ativo { get; set; }
    }
}