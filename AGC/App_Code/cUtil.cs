using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AGC
{
    public class cUtil:cBase
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
    }
}