using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ServiceChat.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
namespace ServiceChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IDictionary<string, UserConnection> _connections;
        private readonly string _botUser;

        private readonly IConfiguration _configuration;

     
        public ChatHub(IDictionary<string, UserConnection> connections, IConfiguration configuration)
        {
            _botUser = "";
            _connections = connections;
            _configuration = configuration;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                _connections.Remove(Context.ConnectionId);
               Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", 
                   _botUser, new { type = "text", value = $"{userConnection.User} has left"});

                update(new Utilizador {Id = Convert.ToInt32(userConnection.Id), Ativo = false });

                SendUsersConnected(userConnection.Room);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public void update(Utilizador utilizador)
        {
            string query = @"update utilizador set Ativo = @Ativo where Id= @Id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", utilizador.Id);
                    myCommand.Parameters.AddWithValue("@Ativo", utilizador.Ativo);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
        }

        public async Task SendMessage(string message)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", new {id = userConnection.Id, nome = userConnection.User }, message);
            }
        }

        public async Task JoinRoom(UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);

            _connections[Context.ConnectionId] = userConnection;

            await Clients.Group(userConnection.Room)
                .SendAsync("ReceiveMessage", new { id = userConnection.Id, nome = userConnection.User, room = userConnection.Room }, new { type = "text", value = $"{userConnection.User} entrou {userConnection.Room}" });

            update(new Utilizador { Id = Convert.ToInt32(userConnection.Id), Ativo = true });
            

            await SendUsersConnected(userConnection.Room);
        }
        
        public Task SendUsersConnected(string room)
        {
            var users = _connections.Values
                .Where(c => c.Room == room)
                .Select(c => c.User);

            return Clients.Group(room).SendAsync("UsersInRoom", users);
        }


    }
}
