using SQLFundamentals.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SQLFundamentals.DataAccess.Controllers
{
    public class BookController
    {
        public static int CreateBook(string bookTitle, string author, int yearPublished, bool haveRead, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            int bookId = 0;

            string insertSqlCommand = @"INSERT INTO BOOKS
                                                   (BOOKTITLE,
                                                    AUTHOR,
                                                    YEARPUBLISHED,
                                                    HAVEREAD)
                                             OUTPUT INSERTED.BOOKID
                                             VALUES
                                                   (@BOOKTITLE,
                                                    @AUTHOR,
                                                    @YEARPUBLISHED,
                                                    @HAVEREAD)";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(insertSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@BOOKTITLE", bookTitle));
                    sqlCommand.Parameters.Add(new SqlParameter("@AUTHOR", author));
                    sqlCommand.Parameters.Add(new SqlParameter("@YEARPUBLISHED", yearPublished));
                    sqlCommand.Parameters.Add(new SqlParameter("@HAVEREAD", haveRead));

                    sqlCommand.Connection.Open();
                    bookId = (int)sqlCommand.ExecuteScalar();
                    sqlCommand.Connection.Close();
                }
            }
            return bookId;
        }

        public static int UpdateBook(int bookId, string bookTitle, string author, int yearPublished, bool haveRead, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            string updateSqlCommand = @"UPDATE BOOKS
                                        SET BOOKTITLE       = @BOOKTITLE,
                                            AUTHOR          = @AUTHOR,
                                            YEARPUBLISHED   = @YEARPUBLISHED,
                                            HAVEREAD        = @HAVEREAD
                                        WHERE BOOKID        = @BOOKID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(updateSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@BOOKTITLE", bookTitle));
                    sqlCommand.Parameters.Add(new SqlParameter("@AUTHOR", author));
                    sqlCommand.Parameters.Add(new SqlParameter("@YEARPUBLISHED", yearPublished));
                    sqlCommand.Parameters.Add(new SqlParameter("@HAVEREAD", haveRead));
                    sqlCommand.Parameters.Add(new SqlParameter("@BOOKID", bookId));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return bookId;
        }

        public static bool DeleteBook(int bookId, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            string deleteSqlCommand = @"DELETE FROM BOOKS WHERE BOOKID = @BOOKID";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(deleteSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@BOOKID", bookId));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return true;
        }

        public static IEnumerable<BookModel>? GetAllBooks(ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            List<BookModel> booksList = new();

            string querySql = "SELECT * FROM BOOKS ORDER BY BOOKID DESC";

            using (SqlConnection sqlConnection = new(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new(querySql, sqlConnection))
                {
                    using (SqlDataAdapter sqlDataAdapter = new(sqlCommand))
                    {
                        using (DataTable dataTable = new())
                        {
                            sqlDataAdapter.Fill(dataTable);

                            BookModel bookModel;

                            foreach (DataRow dataRow in dataTable.Rows)
                            {
                                bookModel = new();

                                bookModel.BookID = Convert.ToInt32(dataRow["BOOKID"]);
                                bookModel.BookTitle = dataRow["BOOKTITLE"].ToString() ?? "";
                                bookModel.Author = dataRow["AUTHOR"].ToString() ?? "";
                                bookModel.YearPublished = Convert.ToInt32(dataRow["YEARPUBLISHED"]);
                                bookModel.HaveRead = Convert.ToBoolean(dataRow["HAVEREAD"]);

                                booksList.Add(bookModel);
                            }
                        }
                    }
                }
            }

            return booksList;
        }

        public static BookModel? GetBookByID(int bookId, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            BookModel bookModel = new();

            string querySql = "SELECT * FROM BOOKS WHERE BOOKID = @BOOKID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(querySql, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@BOOKID", bookId));

                    sqlConnection.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            bookModel.BookID = Convert.ToInt32(reader["BOOKID"]);
                            bookModel.BookTitle = reader["BOOKTITLE"].ToString() ?? "";
                            bookModel.Author = reader["AUTHOR"].ToString() ?? "";
                            bookModel.YearPublished = Convert.ToInt32(reader["YEARPUBLISHED"]);
                            bookModel.HaveRead = Convert.ToBoolean(reader["HAVEREAD"]);
                        }
                        else
                        {
                            throw new Exception("No rows found.");
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return bookModel;
        }
    }
}