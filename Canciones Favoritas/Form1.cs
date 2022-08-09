using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Canciones_Favoritas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Hago focus al primer textbox y cargo la lista actual de canciones
            txtTitle.Focus();
            LoadDataIntoGrid();

            //Quito el id de la vista del usuario
            dgvList.Columns[0].Visible = false;
        }

        private void LoadDataIntoGrid()
        {
            MySqlConnection connectionDB = Conexion.conexion();
            connectionDB.Open();

            MySqlCommand command = connectionDB.CreateCommand();
            command.CommandText = "SELECT * FROM `informacion`";

            MySqlDataReader sdr = command.ExecuteReader();

            DataTable dtRecords = new DataTable();
            dtRecords.Load(sdr);
            
            dgvList.DataSource = dtRecords;
            connectionDB.Close();
        }

        private void clearFields()
        {
            txtTitle.Clear();
            txtGroup.Clear();
            nudYear.Value = 0;
            txtGenre.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text;
            string group = txtGroup.Text;
            int year = Convert.ToInt32(nudYear.Value);
            string genre = txtGenre.Text;

            string sql = "";

            if (year.ToString() == "")
            {
                year = 0;
            }            

            if (title != "")
            {
                sql = "INSERT INTO `informacion` (`title`, `group`, `year`, `genre`) VALUES ('" + title + "', '" + group + "', '" + year + "', '" + genre + "')";                

                MySqlConnection connectionDB = Conexion.conexion();
                connectionDB.Open();
                
                MySqlCommand command = new MySqlCommand(sql, connectionDB);
                command.ExecuteNonQuery();
                MessageBox.Show("¡Canción agregada correctamente!");

                connectionDB.Close();

                LoadDataIntoGrid();
                clearFields();
                txtTitle.Focus();

            }
            else
            {
                MessageBox.Show("Su canción debe tener al menos un titulo");
            }

            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
                        
            int id = Convert.ToInt32(dgvList.CurrentRow.Cells[0].Value);
            string sql;

            if (txtDeleteTitle.Text != "")
            {
                sql = $"DELETE FROM `informacion` WHERE `id`={id}";

                MySqlConnection connectionDB = Conexion.conexion();
                connectionDB.Open();
                
                MySqlCommand command = new MySqlCommand(sql, connectionDB);
                command.ExecuteNonQuery();
                MessageBox.Show("¡Canción eliminada correctamente!");
                
                //Limpiar campos
                txtDeleteTitle.Clear();
                txtDeleteGroup.Clear();

                //Cerrar la conexión y recargar la gridview
                connectionDB.Close();
                LoadDataIntoGrid();
                
            }
            else
            {
                MessageBox.Show("Haga doble click sobre una fila");
            }

        }

        private void dgvList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(dgvList.CurrentRow.Cells[3].Value.ToString() != "")
            {
                txtDeleteTitle.Text = dgvList.CurrentRow.Cells[1].Value.ToString();
                txtDeleteGroup.Text = dgvList.CurrentRow.Cells[2].Value.ToString();
            }
            else
            {
                MessageBox.Show("Seleccione una fila que no esté vacía");
            }
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
