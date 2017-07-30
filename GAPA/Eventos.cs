using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GAPA
{
    public class Eventos
    {
        public int id { get; set; }
        public string tipo { get; set; }
        public int id_utilizador { get; set; }
        public DateTime data_hora { get; set; }
        public string descricao { get; set; }
        public int portao { get; set; }
        public string nome_utilizador { get; set;}
        public string nome_portao { get; set; }
    }
}