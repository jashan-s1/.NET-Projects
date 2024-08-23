using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

public class LoginModel{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly string _connectionString = "Server=localhost;data source= LAPTOP-5MDKM4LG\\SQLEXPRESS;Database=SqlPractice;Integrated Security=True; TrustServerCertificate=True"; // Replace with your SQL Server connection string

    [HttpPost]
    public IActionResult Login([FromBody] LoginModel login)
    {
        if (ValidateUser(login.Username, login.Password))
        {
            return Ok(new { success = true });
        }
        else
        {
            return Unauthorized(new { success = false });
        }
    }

    private bool ValidateUser(string username, string password)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username AND Password = @Password"; // Ensure password is hashed in real applications

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password); // Use hashed password comparison

            connection.Open();
            int count = (int)command.ExecuteScalar();
            return count == 1;
        }
    }
}
