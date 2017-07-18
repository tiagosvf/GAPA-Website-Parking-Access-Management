using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GAPA
{
    public class Utilizadores
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string nome { get; set; }
        public string password { get; set; }
        public string tipo { get; set; }
        public bool ativo { get; set; }
        public int perfil { get; set; }
        public string nome_perfil { get; set; }
        public string cod_seguranca { get; set; }
        public string cod_sessao { get; set; }
    }
}