using SQLFundamentals.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SQLFundamentals.DataAccess.Controllers
{
    public class DocumentController
    {
        public static int CreateDocument(string documentName, string description, string lastUpdateDate, string location, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            int documentId = 0;

            string insertSqlCommand = @"INSERT INTO DOCUMENTS
                                                   (DOCUMENTNAME,
                                                    DESCRIPTION,
                                                    LASTUPDATEDATE,
                                                    LOCATION)
                                             OUTPUT INSERTED.DOCUMENTID
                                             VALUES
                                                   (@DOCUMENTNAME,
                                                    @DESCRIPTION,
                                                    @LASTUPDATEDATE,
                                                    @LOCATION)";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(insertSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@DOCUMENTNAME", documentName));
                    sqlCommand.Parameters.Add(new SqlParameter("@DESCRIPTION", description));
                    sqlCommand.Parameters.Add(new SqlParameter("@LASTUPDATEDATE", lastUpdateDate));
                    sqlCommand.Parameters.Add(new SqlParameter("@LOCATION", location));

                    sqlCommand.Connection.Open();
                    documentId = (int)sqlCommand.ExecuteScalar();
                    sqlCommand.Connection.Close();
                }
            }
            return documentId;
        }

        public static int UpdateDocument(int documentId, string documentName, string description, string lastUpdateDate, string location, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            string updateSqlCommand = @"UPDATE DOCUMENTS
                                        SET DOCUMENTNAME     = @DOCUMENTNAME,
                                            DESCRIPTION      = @DESCRIPTION,
                                            LASTUPDATEDATE   = @LASTUPDATEDATE,
                                            LOCATION         = @LOCATION
                                        WHERE DOCUMENTID     = @DOCUMENTID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(updateSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@DOCUMENTNAME", documentName));
                    sqlCommand.Parameters.Add(new SqlParameter("@DESCRIPTION", description));
                    sqlCommand.Parameters.Add(new SqlParameter("@LASTUPDATEDATE", lastUpdateDate));
                    sqlCommand.Parameters.Add(new SqlParameter("@LOCATION", location));
                    sqlCommand.Parameters.Add(new SqlParameter("@DOCUMENTID", documentId));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return documentId;
        }

        public static bool DeleteDocument(int documentId, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            string deleteSqlCommand = @"DELETE FROM DOCUMENTS WHERE DOCUMENTID = @DOCUMENTID";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(deleteSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@DOCUMENTID", documentId));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return true;
        }

        public static IEnumerable<DocumentModel>? GetAllDocuments(ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            List<DocumentModel> documentsList = new();

            string querySql = "SELECT * FROM DOCUMENTS ORDER BY DOCUMENTID DESC";

            using (SqlConnection sqlConnection = new(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new(querySql, sqlConnection))
                {
                    using (SqlDataAdapter sqlDataAdapter = new(sqlCommand))
                    {
                        using (DataTable dataTable = new())
                        {
                            sqlDataAdapter.Fill(dataTable);

                            DocumentModel documentModel;

                            foreach (DataRow dataRow in dataTable.Rows)
                            {
                                documentModel = new();

                                documentModel.DocumentID = Convert.ToInt32(dataRow["DOCUMENTID"]);
                                documentModel.DocumentName = dataRow["DOCUMENTNAME"].ToString() ?? "";
                                documentModel.Description = dataRow["DESCRIPTION"].ToString() ?? "";
                                documentModel.LastUpdateDate = Convert.ToDateTime(dataRow["LASTUPDATEDATE"]);
                                documentModel.Location = dataRow["LOCATION"].ToString() ?? "";

                                documentsList.Add(documentModel);
                            }
                        }
                    }
                }
            }

            return documentsList;
        }

        public static DocumentModel? GetDocumentByID(int documentId, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            DocumentModel documentModel = new();

            string querySql = "SELECT * FROM DOCUMENTS WHERE DOCUMENTID = @DOCUMENTID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(querySql, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@DOCUMENTID", documentId));

                    sqlConnection.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            documentModel.DocumentID = Convert.ToInt32(reader["DOCUMENTID"]);
                            documentModel.DocumentName = reader["DOCUMENTNAME"].ToString() ?? "";
                            documentModel.Description = reader["DESCRIPTION"].ToString() ?? "";
                            documentModel.LastUpdateDate = Convert.ToDateTime(reader["LASTUPDATEDATE"]);
                            documentModel.Location = reader["LOCATION"].ToString() ?? "";
                        }
                        else
                        {
                            throw new Exception("No rows found.");
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return documentModel;
        }
    }
}