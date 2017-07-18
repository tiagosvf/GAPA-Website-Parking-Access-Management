using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GAPA
{
    public class Permissoes
    {
        public int id { get; set; }
        public int id_perfil { get; set; }
        public int portao { get; set; }
        public string nome_utilizador { get; set; }
    }
}