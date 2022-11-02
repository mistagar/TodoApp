using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WhatTodo.Pages.Tasks
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientinfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientinfo.title = Request.Form["title"];
            clientinfo.details = Request.Form["details"];
            clientinfo.status = Request.Form["status"];

            if (clientinfo.title.Length == 0 || clientinfo.details.Length == 0 || clientinfo.status.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            // Save to the database.
            try
            {
                string connectionString = "Data Source=GARCODE;Initial Catalog=Tasks;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Tasks" + "(Title, Detail, Status) VALUES " +
                        "(@title, @details, @status);";

                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@title", clientinfo.title);
                        command.Parameters.AddWithValue("@details", clientinfo.details);
                        command.Parameters.AddWithValue("@status", clientinfo.status);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            clientinfo.title = "";
            clientinfo.details = "";
            clientinfo.status = "";
            successMessage = "New Client Added Correctly";

            Response.Redirect("/Index");
        }
    }
}
