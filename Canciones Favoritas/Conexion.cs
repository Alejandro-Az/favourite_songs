using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace Canciones_Favoritas
{
    class Conexion
    {
        public static MySqlConnection conexion()
        {
            string cadenaConexion = "server=localhost;port=3306;uid=root;pwd='';database=songs; ";

            MySqlConnection connectionDB = new MySqlConnection(cadenaConexion); //instancia un objeto que requiere la cadena de conexion
            return connectionDB;                                               
        }
    }
}
