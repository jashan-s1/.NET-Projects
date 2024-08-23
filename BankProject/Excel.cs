using ClosedXML.Excel;
using Microsoft.Data.SqlClient;

public class ReportGenerator
{

    public void GenerateExcelReport(string filePath)
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Accounts");

            // Add headers
            worksheet.Cell(1, 1).Value = "Account Number";
            worksheet.Cell(1, 2).Value = "First Name";
            worksheet.Cell(1, 3).Value = "Last Name";
            worksheet.Cell(1, 4).Value = "City";
            worksheet.Cell(1, 5).Value = "State";
            worksheet.Cell(1, 6).Value = "Amount";
            worksheet.Cell(1, 7).Value = "Account Type";
            worksheet.Cell(1, 8).Value = "Status";

            using (SqlConnection conn = new SqlConnection("Server=localhost;data source= LAPTOP-5MDKM4LG\\SQLEXPRESS;Database=BANK;Integrated Security=True; TrustServerCertificate=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT AccountNumber, FirstName, LastName, City, State, Amount, AccountType, Status FROM Accounts", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                int row = 2;
                while (reader.Read())
                {
                    worksheet.Cell(row, 1).Value = reader["AccountNumber"] != DBNull.Value ? (int)reader["AccountNumber"] : 0;
                    worksheet.Cell(row, 2).Value = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : string.Empty;
                    worksheet.Cell(row, 3).Value = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : string.Empty;
                    worksheet.Cell(row, 4).Value = reader["City"] != DBNull.Value ? (string)reader["City"] : string.Empty;
                    worksheet.Cell(row, 5).Value = reader["State"] != DBNull.Value ? (string)reader["State"] : string.Empty;
                    worksheet.Cell(row, 6).Value = reader["Amount"] != DBNull.Value ? (decimal)reader["Amount"] : 0m;
                    worksheet.Cell(row, 7).Value = reader["AccountType"] != DBNull.Value ? (string)reader["AccountType"] : string.Empty;
                    worksheet.Cell(row, 8).Value = reader["Status"] != DBNull.Value ? (string)reader["Status"] : string.Empty;
                    row++;
                }
            }

            workbook.SaveAs(filePath);
        }
    }
}

