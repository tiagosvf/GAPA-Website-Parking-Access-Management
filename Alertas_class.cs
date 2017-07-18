using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GAPA
{
    public class Alertas_class
    {
        public int id { get; set; }
        public TimeSpan hora_inicial {get;set;}
        public TimeSpan hora_final { get; set; }
        public string Tipo_evento { get; set; }
    }
}