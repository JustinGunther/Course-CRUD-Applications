using SQLFundamentals.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SQLFundamentals.DataAccess.Controllers
{
    public class OrderController
    {
        public static int CreateOrder(string orderStatus, string orderQuantity, string orderAmount, string orderTotal, string orderDatePlaced, string orderDateShipped, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            int orderId = 0;
            string insertSqlCommand = @"INSERT INTO ORDERS
                                                   (ORDERSTATUS,
                                                    ORDERQUANTITY,
                                                    ORDERAMOUNT,
                                                    ORDERTOTAL,
                                                    ORDERDATEPLACED,
                                                    ORDERDATESHIPPED)
                                             OUTPUT INSERTED.ORDERID
                                             VALUES
                                                   (@ORDERSTATUS,
                                                    @ORDERQUANTITY,
                                                    @ORDERAMOUNT,
                                                    @ORDERTOTAL,
                                                    @ORDERDATEPLACED,
                                                    @ORDERDATESHIPPED)";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(insertSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@ORDERSTATUS", orderStatus));
                    sqlCommand.Parameters.Add(new SqlParameter("@ORDERQUANTITY", orderQuantity));
                    sqlCommand.Parameters.Add(new SqlParameter("@ORDERAMOUNT", orderAmount));
                    sqlCommand.Parameters.Add(new SqlParameter("@ORDERTOTAL", orderTotal));
                    sqlCommand.Parameters.Add(new SqlParameter("@ORDERDATEPLACED", orderDatePlaced));
                    sqlCommand.Parameters.Add(new SqlParameter("@ORDERDATESHIPPED", orderDateShipped));

                    sqlCommand.Connection.Open();
                    orderId = (int)sqlCommand.ExecuteScalar();
                    sqlCommand.Connection.Close();
                }
            }
            return orderId;
        }

        public static int UpdateOrder(int orderId, string orderStatus, string orderQuantity, string orderAmount, string orderTotal, string orderDatePlaced, string orderDateShipped, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            string updateSqlCommand = @"UPDATE ORDERS
                                               SET ORDERSTATUS      = @ORDERSTATUS,
                                                   ORDERQUANTITY    = @ORDERQUANTITY,
                                                   ORDERAMOUNT      = @ORDERAMOUNT,
                                                   ORDERTOTAL       = @ORDERTOTAL,
                                                   ORDERDATEPLACED  = @ORDERDATEPLACED,
                                                   ORDERDATESHIPPED = @ORDERDATESHIPPED
                                               WHERE ORDERID        = @ORDERID";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(updateSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@ORDERSTATUS", orderStatus));
                    sqlCommand.Parameters.Add(new SqlParameter("@ORDERQUANTITY", orderQuantity));
                    sqlCommand.Parameters.Add(new SqlParameter("@ORDERAMOUNT", orderAmount));
                    sqlCommand.Parameters.Add(new SqlParameter("@ORDERTOTAL", orderTotal));
                    sqlCommand.Parameters.Add(new SqlParameter("@ORDERDATEPLACED", orderDatePlaced));
                    sqlCommand.Parameters.Add(new SqlParameter("@ORDERDATESHIPPED", orderDateShipped));
                    sqlCommand.Parameters.Add(new SqlParameter("@ORDERID", orderId));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return orderId;
        }

        public static bool DeleteOrder(int orderID, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            string deleteSqlCommand = @"DELETE FROM ORDERS WHERE ORDERID = @ORDERID";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(deleteSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@ORDERID", orderID));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return true;
        }

        public static IEnumerable<OrderModel>? GetAllOrders(ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            List<OrderModel> ordersList = new();

            string querySql = "SELECT * FROM ORDERS ORDER BY ORDERID DESC";
            using (SqlConnection sqlConnection = new(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new(querySql, sqlConnection))
                {
                    using (SqlDataAdapter sqlDataAdapter = new(sqlCommand))
                    {
                        using (DataTable dataTable = new())
                        {
                            sqlDataAdapter.Fill(dataTable);
                            OrderModel orderModel;
                            foreach (DataRow dataRow in dataTable.Rows)
                            {
                                orderModel = new();

                                orderModel.OrderID = Convert.ToInt32(dataRow["ORDERID"]);
                                orderModel.OrderStatus = dataRow["ORDERSTATUS"].ToString() ?? "";
                                orderModel.OrderQuantity = Convert.ToInt32(dataRow["ORDERQUANTITY"]);
                                orderModel.OrderAmount = (decimal)dataRow["ORDERAMOUNT"];
                                orderModel.OrderTotal = (decimal)dataRow["ORDERTOTAL"];
                                orderModel.OrderDatePlaced = (DateTime)dataRow["ORDERDATEPLACED"];

                                if (dataRow["ORDERDATESHIPPED"] != DBNull.Value)
                                {
                                    orderModel.OrderDateShipped = (DateTime)dataRow["ORDERDATESHIPPED"];
                                }

                                ordersList.Add(orderModel);
                            }
                        }
                    }
                }
            }

            return ordersList;
        }

        public static OrderModel? GetOrderByID(int orderID, ISQLFundamentalsConfigManager configManager)
        {
            string sqlConnectionString = configManager.SQLFundamentalsConnection;
            OrderModel orderModel = new();

            string querySql = "SELECT * FROM ORDERS WHERE ORDERID = @ORDERID";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(querySql, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@ORDERID", orderID));
                    sqlConnection.Open();
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            orderModel.OrderID = Convert.ToInt32(reader["ORDERID"]);
                            orderModel.OrderStatus = reader["ORDERSTATUS"]?.ToString() ?? "";
                            orderModel.OrderQuantity = Convert.ToInt32(reader["ORDERQUANTITY"]);
                            orderModel.OrderAmount = (decimal)reader["ORDERAMOUNT"];
                            orderModel.OrderTotal = (decimal)reader["ORDERTOTAL"];
                            orderModel.OrderDatePlaced = (DateTime)reader["ORDERDATEPLACED"];
                            if (reader["ORDERDATESHIPPED"] != DBNull.Value)
                            {
                                orderModel.OrderDateShipped = (DateTime)reader["ORDERDATESHIPPED"];
                            }
                        }
                        else
                        {
                            throw new Exception("No rows found.");
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return orderModel;
        }
    }
}