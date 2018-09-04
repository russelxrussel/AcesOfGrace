using System;
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
        { }

        #region "CREATE - UPDATE"

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

        #endregion

    }
}