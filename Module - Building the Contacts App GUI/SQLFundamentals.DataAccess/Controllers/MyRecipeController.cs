using SQLFundamentals.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SQLFundamentals.DataAccess.Controllers
{
    public class MyRecipeController
    {
        public static int CreateMyRecipe(string recipeName, string mealType, string mainIngredient, int prepTime, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            int myRecipeId = 0;

            string insertSqlCommand = @"INSERT INTO MYRECIPES
                                                   (RECIPENAME
                                                    ,MEALTYPE
                                                    ,MAININGREDIENT
                                                    ,PREPTIME)
                                             OUTPUT INSERTED.RECIPEID
                                             VALUES
                                                   (@RECIPENAME,
                                                    @MEALTYPE,
                                                    @MAININGREDIENT,
                                                    @PREPTIME)";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(insertSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@RECIPENAME", recipeName));
                    sqlCommand.Parameters.Add(new SqlParameter("@MEALTYPE", mealType));
                    sqlCommand.Parameters.Add(new SqlParameter("@MAININGREDIENT", mainIngredient));
                    sqlCommand.Parameters.Add(new SqlParameter("@PREPTIME", prepTime));

                    sqlCommand.Connection.Open();
                    myRecipeId = (int)sqlCommand.ExecuteScalar();
                    sqlCommand.Connection.Close();
                }
            }
            return myRecipeId;
        }

        public static int UpdateMyRecipe(int myRecipeId, string recipeName, string mealType, string mainIngredient, int prepTime, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            string updateSqlCommand = @"UPDATE MYRECIPES
                                        SET RECIPENAME      = @RECIPENAME,
                                            MEALTYPE        = @MEALTYPE,
                                            MAININGREDIENT  = @MAININGREDIENT,
                                            PREPTIME        = @PREPTIME
                                        WHERE RECIPEID       = @RECIPEID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(updateSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@RECIPENAME", recipeName));
                    sqlCommand.Parameters.Add(new SqlParameter("@MEALTYPE", mealType));
                    sqlCommand.Parameters.Add(new SqlParameter("@MAININGREDIENT", mainIngredient));
                    sqlCommand.Parameters.Add(new SqlParameter("@PREPTIME", prepTime));
                    sqlCommand.Parameters.Add(new SqlParameter("@RECIPEID", myRecipeId));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return myRecipeId;
        }

        public static bool DeleteMyRecipe(int myRecipeId, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            string deleteSqlCommand = @"DELETE FROM MYRECIPES WHERE RECIPEID = @RECIPEID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(deleteSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@RECIPEID", myRecipeId));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return true;
        }

        public static IEnumerable<MyRecipeModel>? GetAllMyRecipes(ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            List<MyRecipeModel> myRecipeModels = new();

            string querySql = "SELECT * FROM MYRECIPES ORDER BY RECIPEID DESC";

            using (SqlConnection sqlConnection = new(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new(querySql, sqlConnection))
                {
                    using (SqlDataAdapter sqlDataAdapter = new(sqlCommand))
                    {
                        using (DataTable dataTable = new())
                        {
                            sqlDataAdapter.Fill(dataTable);

                            MyRecipeModel myRecipeModel;

                            foreach (DataRow dataRow in dataTable.Rows)
                            {
                                myRecipeModel = new();

                                myRecipeModel.RecipeID = Convert.ToInt32(dataRow["RECIPEID"]);
                                myRecipeModel.RecipeName = dataRow["RECIPENAME"].ToString() ?? "";
                                myRecipeModel.MealType = dataRow["MEALTYPE"].ToString() ?? "";
                                myRecipeModel.MainIngredient = dataRow["MAININGREDIENT"].ToString() ?? "";
                                myRecipeModel.PrepTime = Convert.ToInt32(dataRow["PREPTIME"]);

                                myRecipeModels.Add(myRecipeModel);
                            }
                        }
                    }
                }
            }

            return myRecipeModels;
        }

        public static MyRecipeModel? GetMyRecipeByID(int myRecipeId, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            MyRecipeModel myRecipeModel = new();

            string querySql = "SELECT * FROM MYRECIPES WHERE RECIPEID = @RECIPEID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(querySql, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@RECIPEID", myRecipeId));

                    sqlConnection.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            myRecipeModel.RecipeID = Convert.ToInt32(reader["RECIPEID"]);
                            myRecipeModel.RecipeName = reader["RECIPENAME"].ToString() ?? "";
                            myRecipeModel.MealType = reader["MEALTYPE"].ToString() ?? "";
                            myRecipeModel.MainIngredient = reader["MAININGREDIENT"].ToString() ?? "";
                            myRecipeModel.PrepTime = Convert.ToInt32(reader["PREPTIME"]);
                        }
                        else
                        {
                            throw new Exception("No rows found.");
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return myRecipeModel;
        }
    }
}