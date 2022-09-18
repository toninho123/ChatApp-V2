using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ServiceChat.Model;
using System.Data;

namespace ServiceChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SalaController(IConfiguration configuration)
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
        public JsonResult Post(Sala sala)
        {
            string query = @"insert into Sala values (@Id, @Nome, @isAtiva)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", sala.Id);
                    myCommand.Parameters.AddWithValue("@Nome", sala.Nome);
                    myCommand.Parameters.AddWithValue("@isAtiva", sala.isAtiva);
                    MySqlDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Sala Adicionada!");
        }


        [HttpPut]
        public JsonResult Put(Sala sala)
        {
            string query = @"update Sala set isAtiva= @isAtiva where Id= @Id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", sala.Id);
                    myCommand.Parameters.AddWithValue("@isAtiva", sala.isAtiva);
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
