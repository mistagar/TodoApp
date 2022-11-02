//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace WhatTodo.Pages
//{
//	public class IndexModel : PageModel
//	{
//		private readonly ILogger<IndexModel> _logger;

//		public IndexModel(ILogger<IndexModel> logger)
//		{
//			_logger = logger;
//		}

//		public void OnGet()
//		{

//		}
//	}
//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WhatTodo.Pages.Tasks
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {

            try
            {
                string connectionString = "Data Source=GARCODE;Initial Catalog=Tasks;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Tasks";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.title = reader.GetString(1);
                                clientInfo.details = reader.GetString(2);
                                clientInfo.status = reader.GetString(3);
                                clientInfo.date = reader.GetDateTime(4).ToString();

                                listClients.Add(clientInfo);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }

    public class ClientInfo
    {
        public string id;
        public string title;
        public string details;
        public string status;
        public string date;

    }
}
