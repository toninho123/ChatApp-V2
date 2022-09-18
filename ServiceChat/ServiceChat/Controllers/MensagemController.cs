using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ServiceChat.Model;
using System;
using System.Data;


namespace ServiceChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensagemController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MensagemController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            string query = @"SELECT mensagem.Id, mensagem.Texto, mensagem.Ficheiro, mensagem.Data_Mensagem, mensagem.Id_Sala, mensagem.Id_Utilizador,
                             Utilizador.Id, Utilizador.Numero_Aluno, Utilizador.Nome, Utilizador.Funcao, Utilizador.Estado
                             FROM (Mensagem INNER JOIN Utilizador ON Mensagem.Id_Utilizador = Utilizador.Id)
                             WHERE Mensagem.Id_Sala = " + id + " order by Data_Mensagem";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }


        [HttpPost]
        public JsonResult Post(Mensagem mensagem)
        {
            string query = @"insert into Mensagem (Texto, Ficheiro, Data_Mensagem ,Id_Sala, Id_Utilizador) values (@Texto, @Ficheiro, @Data_Mensagem, @Id_Sala, @Id_Utilizador)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new(query, myCon))
                {
                   //myCommand.Parameters.AddWithValue("@Id", mensagem.Id);
                    myCommand.Parameters.AddWithValue("@Texto", mensagem.Texto);
                    myCommand.Parameters.AddWithValue("@Ficheiro", mensagem.Ficheiro);
                    myCommand.Parameters.AddWithValue("@Data_Mensagem", mensagem.Data_Mensagem);
                    myCommand.Parameters.AddWithValue("@Id_Sala", mensagem.Id_Sala);
                    myCommand.Parameters.AddWithValue("@Id_Utilizador", mensagem.Id_Utilizador);
                    MySqlDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Mensagem Enviada!");
        }


       /* [HttpPut]
        public JsonResult Put(Mensagem mensagem, int id)
        {
            string query = @"update Mensagem set Texto= @Texto where Id="+id;

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", mensagem.Id);
                    myCommand.Parameters.AddWithValue("@Texto", mensagem.Texto);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Grupo Atualizado!");
        }*/



        /*[HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from Grupo where Id= @Id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Grupo Apagado!");
        }*/
    }
}
