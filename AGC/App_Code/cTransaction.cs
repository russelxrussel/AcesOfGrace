﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AGC
{
    public class cTransaction: cBase
    {
        public cTransaction()
        {

        }


        #region  "GET"
        public DataTable GET_BRANCH_STOCK(string _branchCode)
        {
            DataTable dt = new DataTable();
            
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[TRANSACTION].[spGET_BRANCH_STOCK]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BRANCHCODE", _branchCode);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;

        }


        #region "SALES SECTION"
        public DataTable GET_BRANCH_SALES_BY_DATE(DateTime _salesDate)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[TRANSACTION].[spGET_SALES_BY_DATE]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SALESDATE", _salesDate);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public void INSERT_BRANCH_SALES(string _branchCode, string _salesNum, DateTime _salesDate, string _remarks,
                                            string _itemCode, int _quantity)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[TRANSACTION].[spINSERT_BRANCH_SALES]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@BRANCHCODE", _branchCode);
                    cmd.Parameters.AddWithValue("@SALESNUM", _salesNum);
                    cmd.Parameters.AddWithValue("@SALESDATE", _salesDate);
                    cmd.Parameters.AddWithValue("@REMARKS", _remarks);
                    cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);
                    cmd.Parameters.AddWithValue("@QUANTITY", _quantity);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }




        #endregion
        /*BRANCH SALES SECTION
        **
        */


        public bool CHECK_EXIST_DELIVERY_ENTRY(DateTime _dateDelivery, string _branchCode) 
        {
            bool x;

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[TRANSACTION].[CHECK_EXIST_BRANCH_DELIVERY_ENTRY]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DELIVERYDATE", _dateDelivery);
                    cmd.Parameters.AddWithValue("@BRANCHCODE", _branchCode);

                    cn.Open();

                    x = (bool)cmd.ExecuteScalar();
                    
                }
                return x;
            }

          
        }

        public DataTable GET_BRANCH_DELIVERY(DateTime _deliveryDate)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[TRANSACTION].[spGET_BRANCH_DELIVERY]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DELIVERYDATE", _deliveryDate);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public DataTable GET_DELIVERY_NOT_YET_POSTED()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT_StoredProc("[TRANSACTION].[spGET_DELIVERY_NOT_YET_POSTED]");
            return dt;
        }

        public DataTable GET_DELIVERY_FOR_POSTING()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT_StoredProc("[TRANSACTION].[spGET_DELIVERY_FOR_POSTING]");
            return dt;
        }


        #endregion

        public void INSERT_BRANCH_DELIVERY(string _branchCode, string _deliveryNum, DateTime _deliveryDate, string _remarks,
                                             string _itemCode, int _quantity)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[TRANSACTION].[spINSERT_BRANCH_DELIVERY]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@BRANCH_CODE", _branchCode);
                    cmd.Parameters.AddWithValue("@DELIVERY_NUM", _deliveryNum);
                    cmd.Parameters.AddWithValue("@DELIVERY_DATE", _deliveryDate);
                    cmd.Parameters.AddWithValue("@REMARKS", _remarks);
                    cmd.Parameters.AddWithValue("@ITEM_CODE", _itemCode);
                    cmd.Parameters.AddWithValue("@QUANTITY", _quantity);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }



        public void CANCEL_BRANCH_DELIVERY(string _deliveryNum)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[TRANSACTION].[spCANCEL_BRANCH_DELIVERY]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@DELIVERYNUM", _deliveryNum);
                   
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }


        public void UPDATE_BRANCH_DELIVERY_POSTING(string _deliveryNum)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[TRANSACTION].[spUPDATE_BRANCH_DELIVERY_POSTED]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DELIVERYNUM", _deliveryNum);
                   

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        //UPDATE LIST OF ITEM IN PARTICULAR DELIVERY RECIEPT THAT DOESN'T POSTED YET.
        public void UPDATE_DELIVERY_ADJUSTMENT(string _deliveryNum, string _itemCode, int _quantity)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[TRANSACTION].[spUPDATE_BRANCH_DELIVERY_ADJUSTMENT]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
          
                    cmd.Parameters.AddWithValue("@DELIVERYNUM", _deliveryNum);
                    cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);
                    cmd.Parameters.AddWithValue("@QUANTITY", _quantity);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void INSERT_BRANCH_RETURN_ITEM(string _branchCode, string _delReturnNum, DateTime _returnDate, string _remarks,
                                          string _itemCode, int _quantity)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[TRANSACTION].[spINSERT_BRANCH_DELIVERY_RETURN]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@BRANCHCODE", _branchCode);
                    cmd.Parameters.AddWithValue("@DELRETURNNUM", _delReturnNum);
                    cmd.Parameters.AddWithValue("@RETURNDATE", _returnDate);
                    cmd.Parameters.AddWithValue("@REMARKS", _remarks);
                    cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);
                    cmd.Parameters.AddWithValue("@QUANTITY", _quantity);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }



     


     
        #region "INVENTORY SECTION


        /*USAGE CONDIMENTS
        **
            */
        public DataTable GET_STOCK_USAGE_BY_DATE(DateTime _usageDate)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[INVENTORY].[spGET_STOCK_USAGE_BY_DATE]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USAGEDATE", _usageDate);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }


        public void INSERT_CONSUME_BRANCH_ITEM(string _branchCode, string _consumeNum, DateTime _consumeDate, string _remarks,
                                            string _itemCode, int _quantity)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[INVENTORY].[spINSERT_STOCK_CONSUME]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@BRANCHCODE", _branchCode);
                    cmd.Parameters.AddWithValue("@CONSUMENUM", _consumeNum);
                    cmd.Parameters.AddWithValue("@CONSUMEDATE", _consumeDate);
                    cmd.Parameters.AddWithValue("@REMARKS", _remarks);
                    cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);
                    cmd.Parameters.AddWithValue("@QUANTITY", _quantity);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }


        #endregion


    }
}