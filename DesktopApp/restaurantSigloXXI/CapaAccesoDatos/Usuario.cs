using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class Usuario
    {
        private int _IDPERSONA;
        private string _USERID;
        private string _PASSWORD;

        public int IDPERSONA { get => _IDPERSONA; set => _IDPERSONA = value; }
        public string USERID { get => _USERID; set => _USERID = value; }
        public string PASSWORD { get => _PASSWORD; set => _PASSWORD = value; }


        public Usuario()
        {

        }




    }

   
}
