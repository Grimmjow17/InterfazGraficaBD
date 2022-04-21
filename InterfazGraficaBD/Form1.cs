using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace InterfazGraficaBD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection("" +
        "Server=localhost; user id=root; password=;"
        + "database=Cuentas;sslMode=none");

        MySqlCommand cmd;
        MySqlDataAdapter da;
        DataTable dt;
        string sql;
        string UsuarioID;
        int resulta;

        private void LoadData()
        {
            try
            {
                sql = "Select UsuarioID, Matricula, Correo" +
                    "from BD_InterfazG";
                conn.Open();
                cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                da = new MySqlDataAdapter(cmd);
                da.SelectCommand = cmd;
                dt = new DataTable();
                dgvCuentas.DataSource = dt;

                txtMatricula.Clear();
                txtCorreo.Clear();

                btnEliminar.Enabled = false;
                btnActualizar.Enabled = false;
                btnGuardar.Enabled = true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                conn.Close();
                da.Dispose();
            }
        }

        private void guardarDatos()

        {

            try
            {
                sql = "Insert into BD_InterfazG (Matricula,Correo" +
                    ") values ('" + txtMatricula.Text + "','" + txtCorreo.Text + "')";

                conn.Open();
                cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                resulta = cmd.ExecuteNonQuery();

                if (resulta > 0)
                {
                    MessageBox.Show("Ha guardado correctamente en el registro!", "Guardado");
                }

                else
                {
                    MessageBox.Show("Se produjo un error al guardar en el registro", "Error");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                conn.Close();
            }

        }

        private void actualizarDatos()

        {

            try
            {
                sql = "Update BD_InterfazG set Matricula ='" + txtMatricula.Text
                    + "', Apellidos = '" + txtCorreo.Text + "'Where" +
                    "UsuarioID=" + UsuarioID;

                conn.Open();
                cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                resulta = cmd.ExecuteNonQuery();

                if (resulta > 0)
                {
                    MessageBox.Show("Usted ha actualizado correctamente el registro!", "Actualizar");
                }

                else
                {
                    MessageBox.Show("Se produjo un error al actualizar el registro.", "Error");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                conn.Close();
            }
        }

        private void eliminarDatos()

        {

            try
            {
                sql = "Delete from BD_InterfazG Where  UsuarioID=" + UsuarioID;

                conn.Open();
                cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                resulta = cmd.ExecuteNonQuery();

                if (resulta > 0)
                {
                    MessageBox.Show("Usted ha eliminado exitosamente del registro!", "Eliminar");
                }

                else
                {
                    MessageBox.Show("Se produjo un error al eliminar en el registro.", "Error");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                conn.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            guardarDatos();
            LoadData();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizarDatos();
            LoadData();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminarDatos();
            LoadData();
        }

        private void dgvCuentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UsuarioID = dgvCuentas.CurrentRow.Cells[0].Value.ToString();
            txtMatricula.Text = dgvCuentas.CurrentRow.Cells[1].Value.ToString();
            txtCorreo.Text = dgvCuentas.CurrentRow.Cells[2].Value.ToString();

            btnEliminar.Enabled = false;
            btnActualizar.Enabled = false;
            btnGuardar.Enabled = true;

        }
    }   
}