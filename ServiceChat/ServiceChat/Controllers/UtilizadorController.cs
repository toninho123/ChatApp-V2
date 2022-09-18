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
    public class UtilizadorController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public UtilizadorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
         
        [HttpGet]
        public JsonResult Get(int id)
        {
            string query = @"SELECT Grupo.Id as Id_Grupo, Utilizador.Id, Utilizador.Numero_Aluno, Utilizador.Nome as nomeUtilizador, Utilizador.Funcao, Utilizador.Estado
                            FROM (Grupo INNER JOIN Utilizador ON Grupo.Id_Utilizador = Utilizador.Id)
                            WHERE Grupo.Id_Sala = "+id;
             
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using(MySqlConnection myCon = new MySqlConnection(sqlDataSource))
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

        [HttpGet("{Numero_Aluno}")]
        public JsonResult GetUtilizador(int numero_aluno)
        {
            string query = @"select * from utilizador where Numero_Aluno = @Numero_Aluno";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Numero_Aluno", numero_aluno);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Utilizador utilizador)
        {
            string query = @"insert into Utilizador values (@Id, @Numero_Aluno, @Nome, @Funcao, @Estado)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", utilizador.Id);
                    myCommand.Parameters.AddWithValue("@Numero_Aluno", utilizador.Numero_Aluno);
                    myCommand.Parameters.AddWithValue("@Nome", utilizador.Nome);
                    myCommand.Parameters.AddWithValue("@Funcao", utilizador.Funcao);
                    myCommand.Parameters.AddWithValue("@Estado", utilizador.Estado);
                    MySqlDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Utilizador Adicionado!");
        }


        [HttpPut]
        public JsonResult Put(Utilizador utilizador)
        {
            string query = @"update utilizador set Estado= @Estado where Id= @Id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", utilizador.Id);
                    myCommand.Parameters.AddWithValue("@Estado", utilizador.Estado);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Utilizador Atualizado!");
        }



        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"DELETE Grupo FROM grupo INNER JOIN Utilizador
                            ON (Utilizador.Id = Grupo.Id_Utilizador) WHERE Grupo.Id_Utilizador = @Id";
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
            return new JsonResult("Utilizador Apagado!");
        }
    }
}
