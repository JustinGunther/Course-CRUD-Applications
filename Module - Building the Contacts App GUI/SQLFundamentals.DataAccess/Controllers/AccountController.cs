using SQLFundamentals.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SQLFundamentals.DataAccess.Controllers
{
    public class AccountController
    {
        public static int CreateAccount(string bankName, string accountNumber, string accountBalance, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            int accountId = 0;

            string insertSqlCommand = @"INSERT INTO BANKING
                                                   (BANKNAME,
                                                    ACCOUNTNUMBER,
                                                    ACCOUNTBALANCE)
                                             OUTPUT INSERTED.ACCOUNTID
                                             VALUES
                                                   (@BANKNAME,
                                                    @ACCOUNTNUMBER,
                                                    @ACCOUNTBALANCE)";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(insertSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@BANKNAME", bankName));
                    sqlCommand.Parameters.Add(new SqlParameter("@ACCOUNTNUMBER", accountNumber));
                    sqlCommand.Parameters.Add(new SqlParameter("@ACCOUNTBALANCE", accountBalance));

                    sqlCommand.Connection.Open();
                    accountId = (int)sqlCommand.ExecuteScalar();
                    sqlCommand.Connection.Close();
                }
            }
            return accountId;
        }

        public static int UpdateAccount(int accountId, string bankName, string accountNumber, string accountBalance, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            string updateSqlCommand = @"UPDATE BANKING
                                        SET BANKNAME           = @BANKNAME,
                                            ACCOUNTNUMBER      = @ACCOUNTNUMBER,
                                            ACCOUNTBALANCE     = @ACCOUNTBALANCE
                                        WHERE ACCOUNTID        = @ACCOUNTID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(updateSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@BANKNAME", bankName));
                    sqlCommand.Parameters.Add(new SqlParameter("@ACCOUNTNUMBER", accountNumber));
                    sqlCommand.Parameters.Add(new SqlParameter("@ACCOUNTBALANCE", accountBalance));
                    sqlCommand.Parameters.Add(new SqlParameter("@ACCOUNTID", accountId));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return accountId;
        }

        public static bool DeleteAccount(int accountId, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            string deleteSqlCommand = @"DELETE FROM BANKING WHERE ACCOUNTID = @ACCOUNTID";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(deleteSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@ACCOUNTID", accountId));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return true;
        }

        public static List<AccountModel>? GetAllAccounts(ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            List<AccountModel> accountsList = new();

            string querySql = "SELECT * FROM BANKING ORDER BY ACCOUNTID DESC";
            using (SqlConnection sqlConnection = new(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new(querySql, sqlConnection))
                {
                    using (SqlDataAdapter sqlDataAdapter = new(sqlCommand))
                    {
                        using (DataTable dataTable = new())
                        {
                            sqlDataAdapter.Fill(dataTable);

                            AccountModel accountModel;

                            foreach (DataRow dataRow in dataTable.Rows)
                            {
                                accountModel = new();

                                accountModel.AccountID = Convert.ToInt32(dataRow["ACCOUNTID"]);
                                accountModel.BankName = dataRow["BANKNAME"].ToString() ?? "";
                                accountModel.AccountNumber = Convert.ToInt32(dataRow["ACCOUNTNUMBER"]);
                                accountModel.AccountBalance = Convert.ToDecimal(dataRow["ACCOUNTBALANCE"]);

                                accountsList.Add(accountModel);
                            }
                        }
                    }
                }
            }

            return accountsList;
        }

        public static AccountModel? GetAccountByID(int accountId, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            AccountModel accountModel = new();

            string querySql = "SELECT * FROM BANKING WHERE ACCOUNTID = @ACCOUNTID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(querySql, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@ACCOUNTID", accountId));

                    sqlConnection.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            accountModel.AccountID = Convert.ToInt32(reader["ACCOUNTID"]);
                            accountModel.BankName = reader["BANKNAME"].ToString() ?? "";
                            accountModel.AccountNumber = Convert.ToInt32(reader["ACCOUNTNUMBER"]);
                            accountModel.AccountBalance = Convert.ToDecimal(reader["ACCOUNTBALANCE"]);
                        }
                        else
                        {
                            throw new Exception("No rows found.");
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return accountModel;
        }
    }
}