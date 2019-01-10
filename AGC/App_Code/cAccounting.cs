using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AGC
{
    public class cAccounting : cBase
    {
        public cAccounting()
        {

        }

        #region "GET LIST"
        public DataTable GET_PAYABLE_ACCOUNT_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT("[ACCOUNTING].[spGET_PAYABLE_ACCOUNT_LIST]");
            return dt;
        }


        #endregion


        #region "TRANSACTION"


        #endregion



    }
}

