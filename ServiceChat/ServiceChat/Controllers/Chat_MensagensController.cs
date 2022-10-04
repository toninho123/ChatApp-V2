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
    public class Chat_MensagensController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public Chat_MensagensController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            string query = @"SELECT mensagem.Id, mensagem.Texto, mensagem.Anexo, mensagem.Anexo_Nome, mensagem.Dt_Criado, mensagem.Id_Grupo, mensagem.Id_Utilizador,
                            Utilizador.Id, Utilizador.Numero_Identificacao, Utilizador.Nome, Utilizador.Ativo
                            FROM (Mensagem INNER JOIN Utilizador ON Mensagem.Id_Utilizador = Utilizador.Id)
                            WHERE Mensagem.Id_Grupo = " + id + " order by Dt_Criado;";

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
        public JsonResult Post(Chat_Mensagens mensagem)
        {
            string query = @"insert into Mensagem (Texto, Anexo, Anexo_Nome, Dt_Criado, Updated_At, Id_Grupo, Id_Utilizador) 
                            values (@Texto, @Anexo, @Anexo_Nome, @Dt_Criado, @Updated_At, @Id_Grupo, @Id_Utilizador)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Texto", mensagem.Texto);
                    myCommand.Parameters.AddWithValue("@Anexo", mensagem.Anexo);
                    myCommand.Parameters.AddWithValue("@Anexo_Nome", mensagem.Anexo_Nome);
                    myCommand.Parameters.AddWithValue("@Dt_Criado", DateTime.Now);
                    myCommand.Parameters.AddWithValue("@Updated_At", DateTime.Now);
                    myCommand.Parameters.AddWithValue("@Id_Grupo", mensagem.Id_Grupo);
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
