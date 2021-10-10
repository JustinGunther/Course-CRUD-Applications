using SQLFundamentals.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFundamentals.DataAccess.Controllers
{
    public class ContactController
    {
        private static string sqlConnectionString = ConfigurationManager.ConnectionStrings["SQLFundamentals"].ConnectionString;

        public static int CreateContact(string firstName, string lastName, string phoneNumber, string emailAddress)
        {
            int ContactId = 0;
            string insertSqlCommand = @"INSERT INTO CONTACTS
                                                   (FIRSTNAME,
                                                    LASTNAME,
                                                    PHONENUMBER,
                                                    EMAILADDRESS)
                                             OUTPUT INSERTED.CONTACTID
                                             VALUES
                                                   (@FIRSTNAME,
                                                    @LASTNAME,
                                                    @PHONENUMBER,
                                                    @EMAILADDRESS)";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(insertSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@FIRSTNAME", firstName));
                    sqlCommand.Parameters.Add(new SqlParameter("@LASTNAME", lastName));
                    sqlCommand.Parameters.Add(new SqlParameter("@PHONENUMBER", phoneNumber));
                    sqlCommand.Parameters.Add(new SqlParameter("@EMAILADDRESS", emailAddress));

                    sqlCommand.Connection.Open();
                    ContactId = (int)sqlCommand.ExecuteScalar();
                    sqlCommand.Connection.Close();
                }
            }
            return ContactId;
        }
        public static int UpdateContact(int contactID, string firstName, string lastName, string phoneNumber, string emailAddress)
        {
            string updateSqlCommand = @"UPDATE CONTACTS
                                               SET FIRSTNAME =    @FIRSTNAME,
                                                   LASTNAME =     @LASTNAME,
                                                   PHONENUMBER =  @PHONENUMBER,
                                                   EMAILADDRESS = @EMAILADDRESS
                                             WHERE CONTACTID =    @CONTACTID";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(updateSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@FIRSTNAME", firstName));
                    sqlCommand.Parameters.Add(new SqlParameter("@LASTNAME", lastName));
                    sqlCommand.Parameters.Add(new SqlParameter("@PHONENUMBER", phoneNumber));
                    sqlCommand.Parameters.Add(new SqlParameter("@EMAILADDRESS", emailAddress));
                    sqlCommand.Parameters.Add(new SqlParameter("@CONTACTID", contactID));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return contactID;
        }
        public static bool DeleteContact(int contactID)
        {
            string deleteSqlCommand = @"DELETE FROM CONTACTS WHERE CONTACTID = @CONTACTID";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(deleteSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@CONTACTID", contactID));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return true;
        }


        public static List<ContactModel>? GetAllContacts()
        {
            List<ContactModel> contactsList = new List<ContactModel>();

            string querySql = "SELECT * FROM CONTACTS ORDER BY CONTACTID DESC";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(querySql, sqlConnection))
                {
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        using (DataTable dataTable = new DataTable())
                        {
                            sqlDataAdapter.Fill(dataTable);
                            
                            foreach (DataRow dataRow in dataTable.Rows)
                            {
                                ContactModel contactModel = new ContactModel();

                                contactModel.ContactID = Convert.ToInt32(dataRow["CONTACTID"]);
                                contactModel.FirstName = dataRow["FIRSTNAME"]?.ToString() ?? "";
                                contactModel.LastName = dataRow["LASTNAME"]?.ToString() ?? "";
                                contactModel.EMailAddress = dataRow["EMAILADDRESS"]?.ToString() ?? "";
                                contactModel.PhoneNumber = dataRow["PHONENUMBER"]?.ToString() ?? "";

                                contactsList.Add(contactModel);
                            }
                        }
                    }
                }
            }

            return contactsList;
        }

        public static ContactModel? GetContactByID(int contactID)
        {
            ContactModel contact = new ContactModel();

            string querySql = "SELECT * FROM CONTACTS WHERE CONTACTID = @CONTACTID";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(querySql, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@CONTACTID", contactID));
                    sqlConnection.Open();
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            contact.ContactID = Convert.ToInt32(reader["CONTACTID"]);
                            contact.FirstName = reader["FIRSTNAME"]?.ToString() ?? "";
                            contact.LastName = reader["LASTNAME"]?.ToString() ?? "";
                            contact.EMailAddress = reader["EMAILADDRESS"]?.ToString() ?? "";
                            contact.PhoneNumber = reader["PHONENUMBER"]?.ToString() ?? "";
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

    }
}
