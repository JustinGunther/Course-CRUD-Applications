using SQLFundamentals.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SQLFundamentals.DataAccess.Controllers
{
    public class InventoryController
    {
        public static int CreateInventory(string item, string brand, int countOnHand, string location, decimal cost, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            int inventoryId = 0;

            string insertSqlCommand = @"INSERT INTO INVENTORY
                                                   (ITEM,
                                                    BRAND,
                                                    COUNTONHAND,
                                                    LOCATION,
                                                    COST)
                                             OUTPUT INSERTED.INVENTORYID
                                             VALUES
                                                   (@ITEM,
                                                    @BRAND,
                                                    @COUNTONHAND,
                                                    @LOCATION,
                                                    @COST)";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(insertSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@ITEM", item));
                    sqlCommand.Parameters.Add(new SqlParameter("@BRAND", brand));
                    sqlCommand.Parameters.Add(new SqlParameter("@COUNTONHAND", countOnHand));
                    sqlCommand.Parameters.Add(new SqlParameter("@LOCATION", location));
                    sqlCommand.Parameters.Add(new SqlParameter("@COST", cost));

                    sqlCommand.Connection.Open();
                    inventoryId = (int)sqlCommand.ExecuteScalar();
                    sqlCommand.Connection.Close();
                }
            }
            return inventoryId;
        }

        public static int UpdateInventory(int inventoryId, string item, string brand, int countOnHand, string location, decimal cost, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            string updateSqlCommand = @"UPDATE INVENTORY
                                        SET ITEM            = @ITEM,
                                            BRAND           = @BRAND,
                                            COUNTONHAND     = @COUNTONHAND,
                                            LOCATION        = @LOCATION,
                                            COST            = @COST
                                        WHERE INVENTORYID    = @INVENTORYID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(updateSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@ITEM", item));
                    sqlCommand.Parameters.Add(new SqlParameter("@BRAND", brand));
                    sqlCommand.Parameters.Add(new SqlParameter("@COUNTONHAND", countOnHand));
                    sqlCommand.Parameters.Add(new SqlParameter("@LOCATION", location));
                    sqlCommand.Parameters.Add(new SqlParameter("@COST", cost));
                    sqlCommand.Parameters.Add(new SqlParameter("@INVENTORYID", inventoryId));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return inventoryId;
        }

        public static bool DeleteInventory(int inventoryId, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            string deleteSqlCommand = @"DELETE FROM INVENTORY WHERE INVENTORYID = @INVENTORYID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(deleteSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@INVENTORYID", inventoryId));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return true;
        }

        public static IEnumerable<InventoryModel>? GetAllInventories(ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            List<InventoryModel> inventoryList = new();

            string querySql = "SELECT * FROM INVENTORY ORDER BY INVENTORYID DESC";

            using (SqlConnection sqlConnection = new(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new(querySql, sqlConnection))
                {
                    using (SqlDataAdapter sqlDataAdapter = new(sqlCommand))
                    {
                        using (DataTable dataTable = new())
                        {
                            sqlDataAdapter.Fill(dataTable);

                            InventoryModel inventoryModel;

                            foreach (DataRow dataRow in dataTable.Rows)
                            {
                                inventoryModel = new();

                                inventoryModel.InventoryID = Convert.ToInt32(dataRow["INVENTORYID"]);
                                inventoryModel.Item = dataRow["ITEM"].ToString() ?? "";
                                inventoryModel.Brand = dataRow["BRAND"].ToString() ?? "";
                                inventoryModel.CountOnHand = Convert.ToInt32(dataRow["COUNTONHAND"]);
                                inventoryModel.Location = dataRow["LOCATION"].ToString() ?? "";
                                inventoryModel.Cost = Convert.ToDecimal(dataRow["COST"]);

                                inventoryList.Add(inventoryModel);
                            }
                        }
                    }
                }
            }

            return inventoryList;
        }

        public static InventoryModel? GetInventoryByID(int inventoryId, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            InventoryModel inventoryModel = new();

            string querySql = "SELECT * FROM INVENTORY WHERE INVENTORYID = @INVENTORYID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(querySql, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@INVENTORYID", inventoryId));

                    sqlConnection.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            inventoryModel.InventoryID = Convert.ToInt32(reader["INVENTORYID"]);
                            inventoryModel.Item = reader["ITEM"].ToString() ?? "";
                            inventoryModel.Brand = reader["BRAND"].ToString() ?? "";
                            inventoryModel.CountOnHand = Convert.ToInt32(reader["COUNTONHAND"]);
                            inventoryModel.Location = reader["LOCATION"].ToString() ?? "";
                            inventoryModel.Cost = Convert.ToDecimal(reader["COST"]);
                        }
                        else
                        {
                            throw new Exception("No rows found.");
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return inventoryModel;
        }
    }
}