using SQLFundamentals.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SQLFundamentals.DataAccess.Controllers
{
    public class MovieController
    {
        public static int CreateMovie(string movieTitle, int year, string director, string leadActor, int myRating, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            int movieId = 0;

            string insertSqlCommand = @"INSERT INTO MOVIES
                                                   (MOVIETITLE
                                                    ,YEAR
                                                    ,DIRECTOR
                                                    ,LEADACTOR
                                                    ,MYRATING)
                                             OUTPUT INSERTED.MOVIEID
                                             VALUES
                                                   (@MOVIETITLE,
                                                    @YEAR,
                                                    @DIRECTOR,
                                                    @LEADACTOR,
                                                    @MYRATING)";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(insertSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@MOVIETITLE", movieTitle));
                    sqlCommand.Parameters.Add(new SqlParameter("@YEAR", year));
                    sqlCommand.Parameters.Add(new SqlParameter("@DIRECTOR", director));
                    sqlCommand.Parameters.Add(new SqlParameter("@LEADACTOR", leadActor));
                    sqlCommand.Parameters.Add(new SqlParameter("@MYRATING", myRating));

                    sqlCommand.Connection.Open();
                    movieId = (int)sqlCommand.ExecuteScalar();
                    sqlCommand.Connection.Close();
                }
            }
            return movieId;
        }

        public static int UpdateMovie(int movieId, string movieTitle, int year, string director, string leadActor, int myRating, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            string updateSqlCommand = @"UPDATE MOVIES
                                        SET MOVIETITLE   = @MOVIETITLE,
                                            YEAR         = @YEAR,
                                            DIRECTOR     = @DIRECTOR,
                                            LEADACTOR    = @LEADACTOR,
                                            MYRATING     = @MYRATING
                                        WHERE MOVIEID    = @MOVIEID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(updateSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@MOVIETITLE", movieTitle));
                    sqlCommand.Parameters.Add(new SqlParameter("@YEAR", year));
                    sqlCommand.Parameters.Add(new SqlParameter("@DIRECTOR", director));
                    sqlCommand.Parameters.Add(new SqlParameter("@LEADACTOR", leadActor));
                    sqlCommand.Parameters.Add(new SqlParameter("@MYRATING", myRating));
                    sqlCommand.Parameters.Add(new SqlParameter("@MOVIEID", movieId));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return movieId;
        }

        public static bool DeleteMovie(int movieId, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            string deleteSqlCommand = @"DELETE FROM MOVIES WHERE MOVIEID = @MOVIEID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(deleteSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@MOVIEID", movieId));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return true;
        }

        public static IEnumerable<MovieModel>? GetAllMovies(ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            List<MovieModel> movieList = new();

            string querySql = "SELECT * FROM MOVIES ORDER BY MOVIEID DESC";

            using (SqlConnection sqlConnection = new(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new(querySql, sqlConnection))
                {
                    using (SqlDataAdapter sqlDataAdapter = new(sqlCommand))
                    {
                        using (DataTable dataTable = new())
                        {
                            sqlDataAdapter.Fill(dataTable);

                            MovieModel movieModel;

                            foreach (DataRow dataRow in dataTable.Rows)
                            {
                                movieModel = new();

                                movieModel.MovieID = Convert.ToInt32(dataRow["MOVIEID"]);
                                movieModel.MovieTitle = dataRow["MOVIETITLE"].ToString() ?? "";
                                movieModel.Year = Convert.ToInt32(dataRow["YEAR"]);
                                movieModel.Director = dataRow["DIRECTOR"].ToString() ?? "";
                                movieModel.LeadActor = dataRow["LEADACTOR"].ToString() ?? "";
                                movieModel.MyRating = Convert.ToInt32(dataRow["MYRATING"]);

                                movieList.Add(movieModel);
                            }
                        }
                    }
                }
            }

            return movieList;
        }

        public static MovieModel? GetMovieByID(int movieId, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            MovieModel movieModel = new();

            string querySql = "SELECT * FROM MOVIES WHERE MOVIEID = @MOVIEID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(querySql, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@MOVIEID", movieId));

                    sqlConnection.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            movieModel.MovieID = Convert.ToInt32(reader["MOVIEID"]);
                            movieModel.MovieTitle = reader["MOVIETITLE"].ToString() ?? "";
                            movieModel.Year = Convert.ToInt32(reader["YEAR"]);
                            movieModel.Director = reader["DIRECTOR"].ToString() ?? "";
                            movieModel.LeadActor = reader["LEADACTOR"].ToString() ?? "";
                            movieModel.MyRating = Convert.ToInt32(reader["MYRATING"]);
                        }
                        else
                        {
                            throw new Exception("No rows found.");
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return movieModel;
        }
    }
}