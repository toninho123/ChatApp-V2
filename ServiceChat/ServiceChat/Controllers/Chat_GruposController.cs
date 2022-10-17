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
    public class Chat_GruposController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public Chat_GruposController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from Sala";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
     
        [HttpPost]
        public JsonResult Post(Chat_Grupos sala)
        {
            long lastId = 0;
            string query = @"insert into sala (Nome, Id_Curso, Id_Entidade, Capa, Dt_Criado, Ativo, Tipo, Updated_At) 
                            values (@Nome, @Id_Curso, @Id_Entidade, @Capa, @Dt_Criado, @Ativo, @Tipo, @Updated_At)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Nome", sala.Nome);
                    myCommand.Parameters.AddWithValue("@Id_Curso", sala.Id_Curso);
                    myCommand.Parameters.AddWithValue("@Id_Entidade", sala.Id_Entidade);
                    myCommand.Parameters.AddWithValue("@Capa", sala.Capa);
                    myCommand.Parameters.AddWithValue("@Dt_Criado", DateTime.Now);
                    myCommand.Parameters.AddWithValue("@Ativo", sala.Ativo);
                    myCommand.Parameters.AddWithValue("@Tipo", sala.Tipo);
                    myCommand.Parameters.AddWithValue("@Updated_At", DateTime.Now);
                    MySqlDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    lastId = myCommand.LastInsertedId;
                    myCon.Close();
                }
            }
            return new JsonResult(lastId);
        }


        [HttpPut]
        public JsonResult Put(Chat_Grupos sala)
        {
            string query = @"update Sala set Ativo= @Ativo where Id= @Id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", sala.Id);
                    myCommand.Parameters.AddWithValue("@Ativo", sala.Ativo);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Sala Atualizada!");
        }



        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from Sala where Id= @Id";

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
            return new JsonResult("Sala Apagada!");
        }
    }
}
