using SQLFundamentals.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SQLFundamentals.DataAccess.Controllers
{
    public class TeamRecordsController
    {
        public static int CreateTeamRecords(string teamName, string location, string season2017, string season2018, string season2019, string season2020, string season2021, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            int teamRecordId = 0;

            string insertSqlCommand = @"INSERT INTO TEAMRECORDS
                                                   (TEAMNAME,
                                                    LOCATION,
                                                    [2017SEASON],
                                                    [2018SEASON],
                                                    [2019SEASON],
                                                    [2020SEASON],
                                                    [2021SEASON])
                                             OUTPUT INSERTED.TEAMID
                                             VALUES
                                                   (@TEAMNAME,
                                                    @LOCATION,
                                                    @SEASON2017,
                                                    @SEASON2018,
                                                    @SEASON2019,
                                                    @SEASON2020,
                                                    @SEASON2021)";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(insertSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@TEAMNAME", teamName));
                    sqlCommand.Parameters.Add(new SqlParameter("@LOCATION", location));
                    sqlCommand.Parameters.Add(new SqlParameter("@SEASON2017", season2017));
                    sqlCommand.Parameters.Add(new SqlParameter("@SEASON2018", season2018));
                    sqlCommand.Parameters.Add(new SqlParameter("@SEASON2019", season2019));
                    sqlCommand.Parameters.Add(new SqlParameter("@SEASON2020", season2020));
                    sqlCommand.Parameters.Add(new SqlParameter("@SEASON2021", season2021));

                    sqlCommand.Connection.Open();
                    teamRecordId = (int)sqlCommand.ExecuteScalar();
                    sqlCommand.Connection.Close();
                }
            }
            return teamRecordId;
        }

        public static int UpdateTeamRecords(int teamRecordId, string teamName, string location, string season2017, string season2018, string season2019, string season2020, string season2021, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            string updateSqlCommand = @"UPDATE TEAMRECORDS
                                        SET TEAMNAME      = @TEAMNAME,
                                            LOCATION      = @LOCATION,
                                            [2017SEASON]    = @SEASON2017,
                                            [2018SEASON]    = @SEASON2018,
                                            [2019SEASON]    = @SEASON2019,
                                            [2020SEASON]    = @SEASON2020,
                                            [2021SEASON]    = @SEASON2021
                                        WHERE TEAMID      = @TEAMID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(updateSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@TEAMNAME", teamName));
                    sqlCommand.Parameters.Add(new SqlParameter("@LOCATION", location));
                    sqlCommand.Parameters.Add(new SqlParameter("@SEASON2017", season2017));
                    sqlCommand.Parameters.Add(new SqlParameter("@SEASON2018", season2018));
                    sqlCommand.Parameters.Add(new SqlParameter("@SEASON2019", season2019));
                    sqlCommand.Parameters.Add(new SqlParameter("@SEASON2020", season2020));
                    sqlCommand.Parameters.Add(new SqlParameter("@SEASON2021", season2021));
                    sqlCommand.Parameters.Add(new SqlParameter("@TEAMID", teamRecordId));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return teamRecordId;
        }

        public static bool DeleteTeamRecords(int teamRecordId, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            string deleteSqlCommand = @"DELETE FROM TEAMRECORDS WHERE TEAMID = @TEAMID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(deleteSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@TEAMID", teamRecordId));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return true;
        }

        public static IEnumerable<TeamRecordsModel>? GetAllTeamRecords(ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            List<TeamRecordsModel> teamRecordModels = new();

            string querySql = "SELECT * FROM TEAMRECORDS ORDER BY TEAMID DESC";

            using (SqlConnection sqlConnection = new(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new(querySql, sqlConnection))
                {
                    using (SqlDataAdapter sqlDataAdapter = new(sqlCommand))
                    {
                        using (DataTable dataTable = new())
                        {
                            sqlDataAdapter.Fill(dataTable);

                            TeamRecordsModel teamRecordModel;

                            foreach (DataRow dataRow in dataTable.Rows)
                            {
                                teamRecordModel = new();

                                teamRecordModel.TeamRecordsID = Convert.ToInt32(dataRow["TEAMID"]);
                                teamRecordModel.TeamName = dataRow["TEAMNAME"].ToString() ?? "";
                                teamRecordModel.Location = dataRow["LOCATION"].ToString() ?? "";
                                teamRecordModel.Season2017 = dataRow["2017SEASON"].ToString() ?? "";
                                teamRecordModel.Season2018 = dataRow["2018SEASON"].ToString() ?? "";
                                teamRecordModel.Season2019 = dataRow["2019SEASON"].ToString() ?? "";
                                teamRecordModel.Season2020 = dataRow["2020SEASON"].ToString() ?? "";
                                teamRecordModel.Season2021 = dataRow["2021SEASON"].ToString() ?? "";

                                teamRecordModels.Add(teamRecordModel);
                            }
                        }
                    }
                }
            }

            return teamRecordModels;
        }

        public static TeamRecordsModel? GetTeamRecordsByID(int teamRecordId, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            TeamRecordsModel teamRecordModel = new();

            string querySql = "SELECT * FROM TEAMRECORDS WHERE TEAMID = @TEAMID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(querySql, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@TEAMID", teamRecordId));

                    sqlConnection.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            teamRecordModel.TeamRecordsID = Convert.ToInt32(reader["TEAMID"]);
                            teamRecordModel.TeamName = reader["TEAMNAME"].ToString() ?? "";
                            teamRecordModel.Location = reader["LOCATION"].ToString() ?? "";
                            teamRecordModel.Season2017 = reader["2017SEASON"].ToString() ?? "";
                            teamRecordModel.Season2018 = reader["2018SEASON"].ToString() ?? "";
                            teamRecordModel.Season2019 = reader["2019SEASON"].ToString() ?? "";
                            teamRecordModel.Season2020 = reader["2020SEASON"].ToString() ?? "";
                            teamRecordModel.Season2021 = reader["2021SEASON"].ToString() ?? "";
                        }
                        else
                        {
                            throw new Exception("No rows found.");
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return teamRecordModel;
        }
    }
}