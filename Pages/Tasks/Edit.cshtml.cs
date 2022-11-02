using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WhatTodo.Pages.Tasks
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";

        
        
        public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {
                string connectionString = "Data Source=GARCODE;Initial Catalog=Tasks;Integrated Security=True";
                using( SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Tasks WHERE Id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id); 
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.title = reader.GetString(1);
                                clientInfo.details = reader.GetString(2);
                                clientInfo.status = reader.GetString(3);


                                //clientInfo.title = reader.GetString(0);
                                //clientInfo.details = reader.GetString(1);
                                //clientInfo.status = reader.GetString(2);


                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {

            clientInfo.id = Request.Form["id"];
            clientInfo.title = Request.Form["title"];
            clientInfo.details = Request.Form["details"];
            clientInfo.status = Request.Form["status"];

            if (clientInfo.title.Length == 0 || clientInfo.details.Length == 0 || clientInfo.status.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                string connectionString = "Data Source=GARCODE;Initial Catalog=Tasks;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Tasks SET Title = @title, Detail= @details, Status = @status WHERE Id=@id";
                    
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", clientInfo.id);
                        command.Parameters.AddWithValue("@title", clientInfo.title);
                        command.Parameters.AddWithValue("@details", clientInfo.details);
                        command.Parameters.AddWithValue("@status", clientInfo.status);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Index");
        }
    }
}
