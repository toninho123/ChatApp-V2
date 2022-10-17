using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ServiceChat.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ServiceChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Criar_SalaController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public Criar_SalaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get(int id)
        {
            string query = @"SELECT Id as value, Nome as label from Utilizador where not Id = " + id;

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
                
                string query = @"INSERT INTO Grupo(Ativo, Administrador, Dt_Criado, Saiu, Lido, Updated_At, Id_Grupo, Id_Utilizador) VALUES 
                               (@Ativo, @Administrador, @Dt_Criado, @Saiu, @Lido, @Updated_At, @Id_Grupo, @Id_Utilizador)";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("Default");
                MySqlDataReader myReader;
                using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@Ativo", true);
                        myCommand.Parameters.AddWithValue("@Administrador", grupo.Administrador);
                        myCommand.Parameters.AddWithValue("@Dt_Criado", DateTime.Now);
                        myCommand.Parameters.AddWithValue("@Saiu", 0);
                        myCommand.Parameters.AddWithValue("@Lido", 0);
                        myCommand.Parameters.AddWithValue("@Updated_At", DateTime.Now);
                        myCommand.Parameters.AddWithValue("@Id_Grupo", grupo.Id_Grupo);
                        myCommand.Parameters.AddWithValue("@Id_Utilizador", grupo.Id_Utilizador);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }
            
            return new JsonResult("Utilizador Adicionado!");
        }
        
    }
}
