using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectoFinal_Cinel_2024.Pages.BackendPages.Pages
{
    public partial class Cursos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_geraRef_Click(object sender, EventArgs e)
        {
            // Obter o tipo selecionado no DropDownList
            string tipo = ddl_tipo.SelectedItem.Text;

            // Encontrar a primeira sequência de duas ou três letras maiúsculas
            var matchTipo = System.Text.RegularExpressions.Regex.Match(tipo, "[A-Z]{2,3}");
            string tipoMaiusculas = matchTipo.Value;

            // Obter as iniciais maiúsculas do nome do curso
            string nomeCurso = tb_nomeCurso.Text;
            string nomeCursoIniciais = string.Concat(nomeCurso.Split(' ').Where(s => !string.IsNullOrEmpty(s) && char.IsUpper(s[0])).Select(s => s[0]));

            // Conectar ao banco de dados
            string connectionString = ConfigurationManager.ConnectionStrings["GestCinel2_DBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Consultar o banco de dados para obter o número da última ID de curso criada
                string query = "GetLastCursoId";
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                object result = command.ExecuteScalar();

                // Se a última ID de curso existir, incrementa ela em 1, caso contrário, começa com 01
                string idCurso;
                if (result != null && result != DBNull.Value)
                {
                    int lastId = Convert.ToInt32(result);
                    idCurso = (lastId + 1).ToString("00");
                }
                else
                {
                    idCurso = "01";
                }

                // Gerar a referência
                string referencia = tipoMaiusculas + "." + nomeCursoIniciais + "." + idCurso;

                // Mostrar a referência no Label
                lbl_ref.Text = referencia;
            }
        }

        protected void btn_gravar_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GestCinel2_DBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_AddCurso", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Nome_Curso", tb_nomeCurso.Text);
                    command.Parameters.AddWithValue("@id_Area", ddl_area.SelectedValue);
                    command.Parameters.AddWithValue("@id_Tipo", ddl_tipo.SelectedValue);
                    command.Parameters.AddWithValue("@id_Local", ddl_local.SelectedValue);
                    command.Parameters.AddWithValue("@data_Inicio", tb_dataInicio.Text);
                    command.Parameters.AddWithValue("@data_Fim", tb_dataFim.Text);
                    command.Parameters.AddWithValue("@horario", tb_horario.Text);
                    command.Parameters.AddWithValue("@referencia", lbl_ref.Text);
                    command.Parameters.AddWithValue("@valor_Custo", tb_valor.Text);
                    command.Parameters.AddWithValue("@id_Estado", ddl_estado.SelectedValue);

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}