using SQLFundamentals.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SQLFundamentals.DataAccess.Controllers
{
    public class PetController
    {
        public static int CreatePet(string petName, string species, int birthYear, string food, string veterinarian, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            int petId = 0;

            string insertSqlCommand = @"INSERT INTO PETS
                                                   (PETNAME
                                                    ,SPECIES
                                                    ,BIRTHYEAR
                                                    ,FOOD
                                                    ,VETERINARIAN)
                                             OUTPUT INSERTED.PETID
                                             VALUES
                                                   (@PETNAME,
                                                    @SPECIES,
                                                    @BIRTHYEAR,
                                                    @FOOD,
                                                    @VETERINARIAN)";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(insertSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@PETNAME", petName));
                    sqlCommand.Parameters.Add(new SqlParameter("@SPECIES", species));
                    sqlCommand.Parameters.Add(new SqlParameter("@BIRTHYEAR", birthYear));
                    sqlCommand.Parameters.Add(new SqlParameter("@FOOD", food));
                    sqlCommand.Parameters.Add(new SqlParameter("@VETERINARIAN", veterinarian));

                    sqlCommand.Connection.Open();
                    petId = (int)sqlCommand.ExecuteScalar();
                    sqlCommand.Connection.Close();
                }
            }
            return petId;
        }

        public static int UpdatePet(int petId, string petName, string species, int birthYear, string food, string veterinarian, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            string updateSqlCommand = @"UPDATE PETS
                                        SET PETNAME         = @PETNAME,
                                            SPECIES         = @SPECIES,
                                            BIRTHYEAR       = @BIRTHYEAR,
                                            FOOD            = @FOOD,
                                            VETERINARIAN    = @VETERINARIAN
                                        WHERE PETID      = @PETID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(updateSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@PETNAME", petName));
                    sqlCommand.Parameters.Add(new SqlParameter("@SPECIES", species));
                    sqlCommand.Parameters.Add(new SqlParameter("@BIRTHYEAR", birthYear));
                    sqlCommand.Parameters.Add(new SqlParameter("@FOOD", food));
                    sqlCommand.Parameters.Add(new SqlParameter("@VETERINARIAN", veterinarian));
                    sqlCommand.Parameters.Add(new SqlParameter("@PETID", petId));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return petId;
        }

        public static bool DeletePet(int petId, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            string deleteSqlCommand = @"DELETE FROM PETS WHERE PETID = @PETID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(deleteSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@PETID", petId));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return true;
        }

        public static IEnumerable<PetModel>? GetAllPets(ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            List<PetModel> petModels = new();

            string querySql = "SELECT * FROM PETS ORDER BY PETID DESC";

            using (SqlConnection sqlConnection = new(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new(querySql, sqlConnection))
                {
                    using (SqlDataAdapter sqlDataAdapter = new(sqlCommand))
                    {
                        using (DataTable dataTable = new())
                        {
                            sqlDataAdapter.Fill(dataTable);

                            PetModel petModel;

                            foreach (DataRow dataRow in dataTable.Rows)
                            {
                                petModel = new();

                                petModel.PetID = Convert.ToInt32(dataRow["PETID"]);
                                petModel.PetName = dataRow["PETNAME"].ToString() ?? "";
                                petModel.Species = dataRow["SPECIES"].ToString() ?? "";
                                petModel.BirthYear = Convert.ToInt32(dataRow["BIRTHYEAR"]);
                                petModel.Food = dataRow["FOOD"].ToString() ?? "";
                                petModel.Veterinarian = dataRow["VETERINARIAN"].ToString() ?? "";

                                petModels.Add(petModel);
                            }
                        }
                    }
                }
            }

            return petModels;
        }

        public static PetModel? GetPetByID(int petId, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            PetModel petModel = new();

            string querySql = "SELECT * FROM PETS WHERE PETID = @PETID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(querySql, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@PETID", petId));

                    sqlConnection.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            petModel.PetID = Convert.ToInt32(reader["PETID"]);
                            petModel.PetName = reader["PETNAME"].ToString() ?? "";
                            petModel.Species = reader["SPECIES"].ToString() ?? "";
                            petModel.BirthYear = Convert.ToInt32(reader["BIRTHYEAR"]);
                            petModel.Food = reader["FOOD"].ToString() ?? "";
                            petModel.Veterinarian = reader["VETERINARIAN"].ToString() ?? "";
                        }
                        else
                        {
                            throw new Exception("No rows found.");
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return petModel;
        }
    }
}