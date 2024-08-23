using System;
using System.Data;
using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace BankProject
{
    public class Accounts
    {
        public int AccountNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public DateTime DateOfOpening { get; set; }
        public decimal Amount { get; set; }
        public string? AccountType { get; set; }
        public string? Status { get; set; }
    }

    class AccountCrud
    {

        public void GetAllAccounts()
        {
            using (SqlConnection conn = new SqlConnection("Server=localhost;data source= LAPTOP-5MDKM4LG\\SQLEXPRESS;Database=BANK;Integrated Security=True; TrustServerCertificate=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetAllBankAccounts", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine("AccountNumber\tFirstName\tLastName\tCity\tState\tDateOfOpening\tAmount\tAccountType\tStatus");

                while (reader.Read())
                {
                    Console.WriteLine($"{reader["AccountNumber"]}\t{reader["FirstName"]}\t{reader["LastName"]}\t{reader["City"]}\t{reader["State"]}\t{reader["DateOfOpening"]}\t{reader["Amount"]}\t{reader["AccountType"]}\t{reader["Status"]}");
                }
            }
        }

        public string UpdateAccount(int accountNumber, decimal newAmount)
        {
            using (SqlConnection conn = new SqlConnection("Server=localhost;data source= LAPTOP-5MDKM4LG\\SQLEXPRESS;Database=BANK;Integrated Security=True; TrustServerCertificate=True"))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("UpdateAccount", conn);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);
                cmd.Parameters.AddWithValue("@Amount", newAmount);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0 ? "Account updated successfully." : "Account not found.";
            }
        }

        public string DeleteAccount(int accountNumber)
        {
            using (SqlConnection conn = new SqlConnection("Server=localhost;data source= LAPTOP-5MDKM4LG\\SQLEXPRESS;Database=BANK;Integrated Security=True; TrustServerCertificate=True"))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("DeleteBankAccount", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0 ? "Account deleted successfully." : "Account not found.";
            }
        }

        public string WithdrawMoney(int account, decimal WithdrawAmount)
        {
            using (SqlConnection conn = new SqlConnection("Server=localhost;data source= LAPTOP-5MDKM4LG\\SQLEXPRESS;Database=BANK;Integrated Security=True; TrustServerCertificate=True"))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("WithdrawAmount", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AccountNumber", account);
                cmd.Parameters.AddWithValue("@withdrawAmount", WithdrawAmount);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0 ? "Amount Withdrawn successfully." : "Insufficient Funds.";
            }
        }

        public int GenerateAccountNumber()
        {

            SqlConnection conn = new SqlConnection("Server=localhost;data source= LAPTOP-5MDKM4LG\\SQLEXPRESS;Database=BANK;Integrated Security=True; TrustServerCertificate=True");
            conn.Open();


            SqlCommand cmd = new SqlCommand("GenerateAccount", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            int accno;
            accno = dr.GetInt32(0);
            return accno;
        }


        public string CreateAccount(Accounts account)
        {
            SqlConnection conn = new SqlConnection("Server=localhost;data source= LAPTOP-5MDKM4LG\\SQLEXPRESS;Database=BANK;Integrated Security=True; TrustServerCertificate=True");
            conn.Open();

            SqlCommand cmd = new SqlCommand("CreateAccount", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
            cmd.Parameters.AddWithValue("@FirstName", account.FirstName);
            cmd.Parameters.AddWithValue("@LastName", account.LastName);
            cmd.Parameters.AddWithValue("@City", account.City);
            cmd.Parameters.AddWithValue("@State", account.State);
            cmd.Parameters.AddWithValue("@DateOfOpening", account.DateOfOpening);
            cmd.Parameters.AddWithValue("@Amount", account.Amount);
            cmd.Parameters.AddWithValue("@AccountType", account.AccountType);
            cmd.Parameters.AddWithValue("@Status", account.Status);

            cmd.ExecuteNonQuery();
            return "Account Created......\n.......................";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Dotnet Bank Project...");
            AccountCrud obj = new AccountCrud();
            bool valid=true;
            while (valid)
            {
                Console.WriteLine("\nWelcome to Dotnet Bank Project...");
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. See all accounts");
                Console.WriteLine("2. Create an account");
                Console.WriteLine("3. Update an account");
                Console.WriteLine("4. Delete an account");
                Console.WriteLine("5. Withdraw money");
                Console.WriteLine("6. Exit");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("\n..................\nAll Accounts:");
                        obj.GetAllAccounts();
                        break;

                    case 2:
                        Console.WriteLine("\n..................\nCreate a New Account:");
                        int accno = obj.GenerateAccountNumber();
                        Accounts accounts = new Accounts();
                        accounts.AccountNumber = accno;
                        Console.WriteLine("Enter FirstName ");
                        accounts.FirstName = Console.ReadLine();
                        int temp = 0;
                        while (temp == 0)
                        {
                            int tem = 0;
                            foreach (char c in accounts.FirstName)
                            {
                                if (!((c >= 65 && c <= 90) || (c >= 97 && c <= 122)))
                                {
                                    Console.WriteLine("Invalid Name....");
                                    tem = 1;
                                    break;
                                }
                            }
                            if (tem == 1)
                            {
                                Console.WriteLine("Enter FirstName Again : ");
                                accounts.FirstName = Console.ReadLine();
                            }
                            else
                            {
                                temp = 1;
                            }
                        }
                        Console.WriteLine("Enter LastName");
                        accounts.LastName = Console.ReadLine();
                        bool isValid = false;

                        while (!isValid)
                        {
                            bool hasInvalidChar = false;

                            foreach (char c in accounts.LastName)
                            {
                                if (!char.IsLetter(c))
                                {
                                    Console.WriteLine("Invalid Name. Please enter a valid name.");
                                    hasInvalidChar = true;
                                    break;
                                }
                            }

                            if (hasInvalidChar)
                            {
                                Console.WriteLine("Enter FirstName Again: ");
                                accounts.LastName = Console.ReadLine();
                            }
                            else
                            {
                                isValid = true;
                            }
                        }
                        Console.WriteLine("Enter City ");
                        accounts.City = Console.ReadLine();
                        Console.WriteLine("Enter State ");
                        accounts.State = Console.ReadLine();
                        accounts.DateOfOpening = DateTime.Today;
                        Console.WriteLine("Enter Amount ");
                        accounts.Amount = Convert.ToInt32(Console.ReadLine());
                        while (true)
                        {
                            if (accounts.Amount < 0)
                            {
                                Console.WriteLine("Enter the valid Amount : ");
                                accounts.Amount = Convert.ToInt32(Console.ReadLine());
                            }
                            else break;
                        }
                        while(true){
                        Console.WriteLine("Enter Account Type  : \n1.Savings\n2.Current\n3.FixedDeposit\n4.RecurringDeposit");
                        int choices = Convert.ToInt32(Console.ReadLine());
                            int checktype=0;
                            switch (choices)
                        {
                            case 1:
                                accounts.AccountType = "Savings";
                                Console.WriteLine("Your account type is : " + accounts.AccountType);
                                break;
                            case 2:
                                accounts.AccountType = "Current";
                                Console.WriteLine("Your account type is : " + accounts.AccountType);
                                break;
                            case 3:
                                accounts.AccountType = "FixedDeposit";
                                Console.WriteLine("Your account type is : " + accounts.AccountType);
                                break;
                            case 4:
                                accounts.AccountType = "RecurringDeposit";
                                Console.WriteLine("Your account type is : " + accounts.AccountType);
                                break;
                            default : Console.WriteLine("Not a Valid Account Type...Please Try again");
                                      checktype=1;
                                      break;

                        }
                        if(checktype==0){
                            break;
                        }
                        }
                        while(true){
                        Console.WriteLine("Enter Status  : \n1.Active\n2.Inactive\n3.Suspended\n4.Closed");
                        int choices = Convert.ToInt32(Console.ReadLine());
                            int checktype=0;
                            switch (choices)
                        {
                            case 1:
                                accounts.Status = "Active";
                                Console.WriteLine("Your Status is : " + accounts.Status);
                                break;
                            case 2:
                                accounts.Status = "Inactive";
                                Console.WriteLine("Your Status is : " + accounts.Status);
                                break;
                            case 3:
                                accounts.Status = "Suspended";
                                Console.WriteLine("Your Status is : " + accounts.Status);
                                break;
                            case 4:
                                accounts.Status = "Closed";
                                Console.WriteLine("Your Status is : " + accounts.Status);
                                break;
                            default : Console.WriteLine("Not a Valid Account Type...Please Try again");
                                      checktype=1;
                                      break;

                        }
                        if(checktype==0){
                            break;
                        }
                        }
                        Console.WriteLine(obj.CreateAccount(accounts));
                        break;

                    case 3:
                        Console.WriteLine("\n.................\nUpdate an Account:");
                        Console.WriteLine("Enter the Account Number : ");
                        int number = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the Amount : ");
                        decimal newAmmount;
                        while (!decimal.TryParse(Console.ReadLine(), out newAmmount))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid decimal amount.");
                        }


                        Console.WriteLine(obj.UpdateAccount(number, newAmmount));
                        break;

                    case 4:
                        Console.WriteLine("\nDelete an Account:");
                        Console.WriteLine("Enter the Account Number you want to delete : ");
                        int nummber = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(obj.DeleteAccount(nummber));
                        break;

                    case 5:
                        Console.WriteLine("\nWithdraw Money:");
                        Console.WriteLine("Enter the account Number : ");
                        int nummberr = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the ammount to be withdrawn : ");
                        decimal newAAmmount;
                        while (!decimal.TryParse(Console.ReadLine(), out newAAmmount))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid decimal amount.");
                        }
                        Console.WriteLine(obj.WithdrawMoney(nummberr, newAAmmount));
                        break;
                    case 6:
                        Console.WriteLine("Exiting...");
                        valid=false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
                
            }
            Console.WriteLine("Want to Generate an Excel Report ? Yes or NO : ");
            string ask=Console.ReadLine();
            if(ask.ToLower()=="yes"){
                ReportGenerator reportGenerator = new ReportGenerator();
                reportGenerator.GenerateExcelReport(@"D:\Excels\report.xlsx");
                Console.WriteLine("Excel report generated successfully."); 
            }
            
        }
    }


}
