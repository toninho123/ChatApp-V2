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
    public class GrupoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public GrupoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            string query = @"SELECT Grupo.Id, Grupo.Nome as nomeGrupo, Grupo.Estado, Grupo.Administrador ,Grupo.Id_Sala, Grupo.Id_Utilizador,
                             Sala.Nome as salaNome, Sala.isAtiva
                             FROM (Grupo
                             INNER JOIN Sala ON Grupo.Id_Sala = Sala.Id)
                             WHERE Grupo.Id_Utilizador = " + id;

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
        public JsonResult Post(Grupo grupo)
        {
            string query = @"insert into Grupo values (@Id, @Nome, @Estado, @Administrador, @Id_Sala, @Id_Utilizador)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", grupo.Id);
                    myCommand.Parameters.AddWithValue("@Nome", grupo.Nome);
                    myCommand.Parameters.AddWithValue("@Estado", grupo.Estado);
                    myCommand.Parameters.AddWithValue("@Administrador", grupo.Administrador);
                    myCommand.Parameters.AddWithValue("@Id_Sala", grupo.Id_Sala);
                    myCommand.Parameters.AddWithValue("@Id_Utilizador", grupo.Id_Utilizador);
                    MySqlDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Grupo Adicionado!");
        }


        [HttpPut]
        public JsonResult Put(Grupo grupo)
        {
            string query = @"update Grupo set Estado= @Estado where Id= @Id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", grupo.Id);
                    myCommand.Parameters.AddWithValue("@Estado", grupo.Estado);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Grupo Atualizado!");
        }



        [HttpDelete("{id}")]
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
        }
    }
}
