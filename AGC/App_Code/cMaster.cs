using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace AGC
{
    public class cMaster:cBase
    {
        public cMaster()
        {

        }


        #region "DISPLAY / GET"
        public DataTable GET_BRANCH_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT_StoredProc("[MASTER].[spGET_BRANCH_LIST]");
            return dt;
        }

        public DataTable GET_BRANCH_MACHINE_EQUIPMENT_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT("[MASTER].[spGET_BRANCH_MACHINE_EQUIPMENT_LIST]");
            return dt;
        }

        public DataTable GET_PARTNERS_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT_StoredProc("[MASTER].[spGET_PARTNERS_LIST]");
            return dt;
        }


        #endregion

        #region "CREATE - UPDATE"

        /*
        INSERT - UPDATE ITEM
            */

        public void INSERT_UPDATE_ITEM(string _itemCode, string _itemName, string _itemCategoryCode, string _itemInventoryCode,
                                       string _statusCode, string _payAccCode, DateTime _dateUpdate)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[MASTER].[spINSERT_UPDATE_ITEM]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);
                    cmd.Parameters.AddWithValue("@ITEMNAME", _itemName);
                    cmd.Parameters.AddWithValue("@ITEMCATEGORYCODE", _itemCategoryCode);
                    cmd.Parameters.AddWithValue("@ITEMUOMINVENTORY", _itemInventoryCode);
                    cmd.Parameters.AddWithValue("@STATUSCODE", _statusCode);
                    cmd.Parameters.AddWithValue("@PAYACCCODE", _payAccCode);
                    cmd.Parameters.AddWithValue("@DU", _dateUpdate);
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }


        /*
         INSERT - UPDATE OF NEW BRANCH INFORMATION
            */
        public void INSERT_BRANCH(string _branchCode, string _branchName, string _branchAddress,string _branchContactNumbers, 
                                  int _branchInchargeID, int _supervisorID, string _partnerCode, DateTime _openingDate, int _areaId, 
                                  string _lessorName, string _modePaymentCode, int _paymentDay, string _remarks)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[MASTER].[spINSERT_BRANCH]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@BRANCH_CODE", _branchCode);
                    cmd.Parameters.AddWithValue("@BRANCH_NAME", _branchName);
                    cmd.Parameters.AddWithValue("@BRANCH_ADDRESS", _branchAddress);
                    cmd.Parameters.AddWithValue("@BRANCH_CONTACT_NUMBER", _branchContactNumbers);
                    cmd.Parameters.AddWithValue("@BRANCH_INCHARGE_ID", _branchInchargeID);
                    cmd.Parameters.AddWithValue("@SUPERVISOR_ID", _supervisorID);
                    cmd.Parameters.AddWithValue("@PARTNER_CODE", _partnerCode);
                    cmd.Parameters.AddWithValue("@OPENING_DATE", _openingDate);
                    cmd.Parameters.AddWithValue("@AREAID", _areaId);
                    cmd.Parameters.AddWithValue("@LESSOR_NAME", _lessorName);
                    cmd.Parameters.AddWithValue("@MODE_PAYMENT_CODE", _modePaymentCode);
                    cmd.Parameters.AddWithValue("@PAYMENT_DAY", _paymentDay);
                    cmd.Parameters.AddWithValue("@REMARKS", _remarks);
                    
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void UPDATE_BRANCH(string _branchCode, string _branchName, string _branchAddress, string _branchContactNumbers,
                                  int _branchInchargeID, int _supervisorID, string _partnerCode, DateTime _openingDate, int _areaId,
                                  string _lessorName, string _modePaymentCode, int _paymentDay, string _remarks, bool _isActive)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[MASTER].[spUPDATE_BRANCH]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@BRANCH_CODE", _branchCode);
                    cmd.Parameters.AddWithValue("@BRANCH_NAME", _branchName);
                    cmd.Parameters.AddWithValue("@BRANCH_ADDRESS", _branchAddress);
                    cmd.Parameters.AddWithValue("@BRANCH_CONTACT_NUMBER", _branchContactNumbers);
                    cmd.Parameters.AddWithValue("@BRANCH_INCHARGE_ID", _branchInchargeID);
                    cmd.Parameters.AddWithValue("@SUPERVISOR_ID", _supervisorID);
                    cmd.Parameters.AddWithValue("@PARTNER_CODE", _partnerCode);
                    cmd.Parameters.AddWithValue("@OPENING_DATE", _openingDate);
                    cmd.Parameters.AddWithValue("@AREAID", _areaId);
                    cmd.Parameters.AddWithValue("@LESSOR_NAME", _lessorName);
                    cmd.Parameters.AddWithValue("@MODE_PAYMENT_CODE", _modePaymentCode);
                    cmd.Parameters.AddWithValue("@PAYMENT_DAY", _paymentDay);
                    cmd.Parameters.AddWithValue("@REMARKS", _remarks);
                    cmd.Parameters.AddWithValue("@IS_ACTIVE", _isActive);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        /*
        INSERT - UPDATE OF BRANCH MACHINE AND EQUIPMENT
        */

        public void INSERT_BRANCH_MACHINE_EQUIPMENT(string _branchCode, string _machEquipCode, string _addtDescription, string _serial,
                                                    DateTime _dateIssue)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[MASTER].[spINSERT_BRANCH_MACHINE_EQUIPMENT]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@BRANCH_CODE", _branchCode);
                    cmd.Parameters.AddWithValue("@MACHEQUIP_CODE",_machEquipCode);
                    cmd.Parameters.AddWithValue("@ADDITIONAL_DESCRIPTION",_addtDescription);
                    cmd.Parameters.AddWithValue("@SERIAL", _serial);
                    cmd.Parameters.AddWithValue("@DATE_ISSUE",_dateIssue);


                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void UPDATE_BRANCH_MACHINE_EQUIPMENT(int _id, string _machEquipCode, string _addtDescription, string _serial,
                                                  DateTime _dateIssue)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[MASTER].[spUPDATE_BRANCH_MACHINE_EQUIPMENT]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@ID", _id);
                    cmd.Parameters.AddWithValue("@MACHEQUIP_CODE", _machEquipCode);
                    cmd.Parameters.AddWithValue("@ADDITIONAL_DESCRIPTION", _addtDescription);
                    cmd.Parameters.AddWithValue("@SERIAL", _serial);
                    cmd.Parameters.AddWithValue("@DATE_ISSUE", _dateIssue);


                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }


        public void DELETE_BRANCH_MACHINE_EQUIPMENT(int _id, string _deletedRemarks)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[MASTER].[spDELETE_BRANCH_MACHINE_EQUIPMENT]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@ID", _id);
                    cmd.Parameters.AddWithValue("@DELETED_REMARKS", _deletedRemarks);


                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }
        #endregion


        #region

        /*ITEMS SECTION*/

        //GET LIST OF ITEM
        public DataTable GET_ITEMS_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT_StoredProc("[MASTER].[spGET_ITEMS_LIST]");
            return dt;
        }

        #endregion

    }
}