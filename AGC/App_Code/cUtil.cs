using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AGC
{
    public class cUtil : cBase
    {
        public cUtil()
        {

        }

        //BRANCH AREA LOCATION
        public DataTable GET_BRANCH_AREA()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT_StoredProc("[UTIL].[spGET_BRANCH_AREA]");
            return dt;
        }

        public DataTable GET_SUPERVISOR_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT_StoredProc("[UTIL].[spGET_SUPERVISOR_LIST]");
            return dt;
        }

        public DataTable GET_BRANCH_INCHARGE_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT_StoredProc("[UTIL].[spGET_BRANCH_INCHARGE_LIST]");
            return dt;
        }

        public DataTable GET_MODE_PAYMENT_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT_StoredProc("[UTIL].[spGET_MODE_PAYMENT]");
            return dt;
        }

        public DataTable GET_STATUS_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT_StoredProc("[UTIL].[spGET_STATUS_LIST]");
            return dt;
        }


        public DataTable GET_MACHINE_EQUIPMENT_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT("[UTIL].[spGET_MACHINE_EQUIPMENT_LIST]");
            return dt;
        }

        public DataTable GET_DAYS_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT("[UTIL].[spGET_DAYS_LIST]");
            return dt;
        }


        public DataTable GET_BRANCH_SCHEDULE_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT("[Util].[spGET_BRANCH_SCHEDULE_LIST]");
            return dt;
        }

        public DataTable GET_ITEM_UOM_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT("[Util].[spGET_ITEM_UOM_LIST]");
            return dt;
        }

        public DataTable GET_ITEM_CATEGORY_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT("[Util].[spGET_ITEM_CATEGORY_LIST]");
            return dt;

        }

        public DataTable GET_STOCK_ADJUSTMENT_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT("[UTIL].[spGET_STOCK_ADJUSTMENT_LIST]");
            return dt;

        }


        //PARTNER PRICE
        public DataTable GET_PARTNER_PRICE(string _partnerCode)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[UTIL].[spGET_PARTNER_PRICE]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PARTNERCODE", _partnerCode);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;

        }


        //BRANCH PRICE
        public DataTable GET_BRANCH_PRICE(string _branchCode)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[UTIL].[spGET_BRANCH_PRICE]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BRANCHCODE", _branchCode);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;

        }


        #region "TRANS DATA"

        //UPDATE FOR DELIVERY BRANCH SCHEDULE
        public void UPDATE_DELIVERY_BRANCH_SCHEDULE(int _schedID, bool _M, bool _T, bool _W, bool _TH, bool _F, bool _SA, bool _S, string _userCode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[UTIL].[spUPDATE_BRANCH_DELIVERY_SCHEDULE]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SCHEDID", _schedID );
                    cmd.Parameters.AddWithValue("@M", _M);
                    cmd.Parameters.AddWithValue("@T", _T);
                    cmd.Parameters.AddWithValue("@W", _W);
                    cmd.Parameters.AddWithValue("@TH", _TH);
                    cmd.Parameters.AddWithValue("@F", _F);
                    cmd.Parameters.AddWithValue("@SA", _SA);
                    cmd.Parameters.AddWithValue("@S", _S);
                    cmd.Parameters.AddWithValue("@USERCODE", _userCode);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void UPDATE_PARTNER_PRICE(string _partnerCode, string _itemCode, double _partnerPrice, double _sellingPrice)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[UTIL].[spUPDATE_PARTNER_PRICE]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PARTNERCODE", _partnerCode);
                    cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);
                    cmd.Parameters.AddWithValue("@PARTNERPRICE", _partnerPrice);
                    cmd.Parameters.AddWithValue("@SELLINGPRICE", _sellingPrice);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UPDATE_BRANCH_PRICE(string _branchCode, string _itemCode, double _branchPrice, double _sellingPrice)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[UTIL].[spUPDATE_BRANCH_PRICE]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BRANCHCODE", _branchCode);
                    cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);
                    cmd.Parameters.AddWithValue("@BRANCHPRICE", _branchPrice);
                    cmd.Parameters.AddWithValue("@SELLINGPRICE", _sellingPrice);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion


    }
    }

