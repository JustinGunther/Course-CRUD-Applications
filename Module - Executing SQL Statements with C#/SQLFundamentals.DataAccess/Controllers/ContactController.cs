using SQLFundamentals.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFundamentals.DataAccess.Controllers
{
    public class ContactController
    {
        private static string sqlConnectionString = "Server=localhost;Database=SQLFundamentals;Trusted_Connection=True;";

        #region Contacts Commands
        public static bool CreateContact(string firstName, string lastName, string phoneNumber, string eMailAddress)
        {
            try
            {
                string insertSqlCommand = @"INSERT INTO Contacts
                                                   (FirstName
                                                   ,LastName
                                                   ,PhoneNumber
                                                   ,EMailAddress
                                                   ,DateInserted)
                                             VALUES
                                                   (@FirstName
                                                   ,@LastName
                                                   ,@PhoneNumber
                                                   ,@EMailAddress
                                                   ,GETDATE())";
                using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(insertSqlCommand, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("@FirstName", firstName));
                        sqlCommand.Parameters.Add(new SqlParameter("@LastName", lastName));
                        sqlCommand.Parameters.Add(new SqlParameter("@PhoneNumber", phoneNumber));
                        sqlCommand.Parameters.Add(new SqlParameter("@EMailAddress", eMailAddress));

                        sqlCommand.Connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlCommand.Connection.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public static bool UpdateContact(int contactID, string firstName, string lastName, string phoneNumber, string eMailAddress)
        {
            try
            {
                string updateSqlCommand = @"UPDATE Contacts
                                               SET FirstName =    @FirstName
                                                  ,LastName =     @LastName
                                                  ,PhoneNumber =  @PhoneNumber
                                                  ,EMailAddress = @EMailAddress
                                             WHERE ContactID =    @ContactID";
                using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(updateSqlCommand, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("@FirstName", firstName));
                        sqlCommand.Parameters.Add(new SqlParameter("@LastName", lastName));
                        sqlCommand.Parameters.Add(new SqlParameter("@PhoneNumber", phoneNumber));
                        sqlCommand.Parameters.Add(new SqlParameter("@EMailAddress", eMailAddress));
                        sqlCommand.Parameters.Add(new SqlParameter("@ContactID", contactID));

                        sqlCommand.Connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlCommand.Connection.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public static bool DeleteContact(int contactID)
        {
            try
            {
                string deleteSqlCommand = @"DELETE FROM Contacts WHERE ContactID = @ContactID";
                using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(deleteSqlCommand, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("@ContactID", contactID));

                        sqlCommand.Connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlCommand.Connection.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #region Contacts Queries

        public static List<ContactModel>? GetAllContacts()
        {
            try
            {
                List<ContactModel> contactsList = new List<ContactModel>();

                string querySql = "SELECT * FROM Contacts ORDER BY DateInserted DESC";
                using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(querySql, sqlConnection))
                    {
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                        {
                            using (DataTable dataTable = new DataTable())
                            {
                                sqlDataAdapter.Fill(dataTable);

                                contactsList = dataTable.AsEnumerable().Select(dataRow => new ContactModel
                                {
                                    ContactID = Convert.ToInt32(dataRow["ContactID"]),
                                    FirstName = dataRow["FirstName"]?.ToString() ?? "",
                                    LastName = dataRow["LastName"]?.ToString() ?? "",
                                    EMailAddress = dataRow["EMailAddress"]?.ToString() ?? "",
                                    PhoneNumber = dataRow["PhoneNumber"]?.ToString() ?? "",
                                    DateInserted = Convert.ToDateTime(dataRow["DateInserted"])
                                }).ToList();
                            }
                        }
                    }
                }

                return contactsList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static ContactModel? GetContactByID(int contactID)
        {
            try
            {
                ContactModel contact = new ContactModel();

                string querySql = "SELECT * FROM Contacts WHERE ContactID = @ContactID";
                using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(querySql, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("@ContactID", contactID));
                        sqlConnection.Open();
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();

                                contact.ContactID = Convert.ToInt32(reader["ContactID"]);
                                contact.FirstName = reader["FirstName"]?.ToString() ?? "";
                                contact.LastName = reader["LastName"]?.ToString() ?? "";
                                contact.EMailAddress = reader["EMailAddress"]?.ToString() ?? "";
                                contact.PhoneNumber = reader["PhoneNumber"]?.ToString() ?? "";
                                contact.DateInserted = Convert.ToDateTime(reader["DateInserted"]);
                            }
                            else
                            {
                                throw new Exception("No rows found.");
                            }
                        }

                        sqlConnection.Close();
                    }
                }
                return contact;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        #endregion
    }
}
