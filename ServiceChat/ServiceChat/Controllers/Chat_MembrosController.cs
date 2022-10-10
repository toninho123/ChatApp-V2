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
    public class Chat_MembrosController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public Chat_MembrosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            string query = @"SELECT Grupo.Id, Grupo.Ativo, Grupo.Administrador ,Grupo.Id_Grupo, Grupo.Id_Utilizador,
                            Sala.Nome as salaNome, Sala.Ativo FROM (Grupo INNER JOIN Sala ON Grupo.Id_Grupo = Sala.Id)
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
        public JsonResult Post(Chat_Membros grupo)
        {
            string query = @"insert into Grupo (Ativo, Administrador, Dt_Criado, Id_Grupo, Id_Utilizador) values (@Ativo, @Administrador, @Dt_Criado, @Id_Grupo, @Id_Utilizador)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Ativo", grupo.Ativo);
                    myCommand.Parameters.AddWithValue("@Dt_Criado", grupo.Dt_Criado);
                    myCommand.Parameters.AddWithValue("@Administrador", grupo.Administrador);
                    myCommand.Parameters.AddWithValue("@Id_Grupo", grupo.Id_Grupo);
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
        public JsonResult Put(Chat_Membros grupo)
        {
            string query = @"update Grupo set Ativo= @Ativo where Id= @Id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", grupo.Id);
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
