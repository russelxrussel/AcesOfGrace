using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AGC
{ 
    public class cSystem : cBase
    {
        public cSystem()
        {
        }

        public DataSet GET_USER_MENU()
        {
            DataSet ds = new DataSet();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSys].[GET_MENU]", cn))

                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }

            return ds;
        }


        public DateTime GET_SERVER_DATE_TIME()
        {
            DateTime ServerDT;

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSys].[GET_SERVER_DATE_TIME]", cn))
                {

                    cn.Open();

                    ServerDT = (DateTime)cmd.ExecuteScalar();
                }

                return ServerDT;
            }
        }


        #region "SERIES NUMBER OF TRANSACTION SECTION"
        /*****  ************/
        public int SERIESNUMBER { get; set; }

        //For Transaction Process
        public string GENERATE_SERIES_NUMBER_TRANS(string _prefixCode)
        {
            SERIESNUMBER = 0;
            string PrefixCode = "";
            string AutoNumber = "";
            bool bIsNumberOnly = false;

            //try
            //{
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSys.GET_SERIES_NUMBER", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PREFIXCODE", _prefixCode);

                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            PrefixCode = dr["PrefixCode"].ToString();
                            bIsNumberOnly = (bool)dr["IsNumberOnly"];

                            if ((int)dr["Series"] > 0)
                            {

                                SERIESNUMBER = (int)dr["Series"] + 1;

                                /*Format Transaction AutoNumber
                                 * UP TO 999999999 AutoNumbers
                                 */

                                if (SERIESNUMBER > 99999999)
                                {
                                    if (bIsNumberOnly)
                                    {
                                        AutoNumber = SERIESNUMBER.ToString();
                                    }
                                    else
                                    {
                                        AutoNumber = PrefixCode + SERIESNUMBER;
                                    }
                                }
                               
                                else if (SERIESNUMBER > 9999999)
                                {
                                    if (bIsNumberOnly)
                                    {
                                        AutoNumber = "0" + SERIESNUMBER;
                                    }
                                    else
                                    {
                                        AutoNumber = PrefixCode + "0" + SERIESNUMBER;
                                    }
                                }
                                else if (SERIESNUMBER > 999999)
                                {
                                    if (bIsNumberOnly)
                                    {
                                        AutoNumber = "00" + SERIESNUMBER;
                                    }
                                    else
                                    {
                                        AutoNumber = PrefixCode + "00" + SERIESNUMBER;
                                    }

                                }

                                else if (SERIESNUMBER > 99999)
                                {
                                    if (bIsNumberOnly)
                                    {
                                        AutoNumber = "000" + SERIESNUMBER;
                                    }
                                    else
                                    {
                                        AutoNumber = PrefixCode + "000" + SERIESNUMBER;
                                    }

                                }

                                else if (SERIESNUMBER > 9999)
                                {
                                    if (bIsNumberOnly)
                                    {
                                        AutoNumber = "0000" + SERIESNUMBER;
                                    }
                                    else
                                    {
                                        AutoNumber = PrefixCode + "0000" + SERIESNUMBER;
                                    }
                                }

                                else if (SERIESNUMBER > 999)
                                {
                                    if (bIsNumberOnly)
                                    {
                                        AutoNumber = "00000" + SERIESNUMBER;
                                    }
                                    else
                                    {
                                        AutoNumber = PrefixCode + "00000" + SERIESNUMBER;
                                    }
                                }

                                else if (SERIESNUMBER > 99)
                                {
                                    if (bIsNumberOnly)
                                    {
                                        AutoNumber = "000000" + SERIESNUMBER;
                                    }
                                    else
                                    {
                                        AutoNumber = PrefixCode + "000000" + SERIESNUMBER;
                                    }
                                }

                                else if (SERIESNUMBER > 9)
                                {
                                    if (bIsNumberOnly)
                                    {
                                        AutoNumber = "0000000" + SERIESNUMBER;
                                    }
                                    else
                                    {
                                        AutoNumber = PrefixCode + "0000000" + SERIESNUMBER;
                                    }
                                }

                                else
                                {
                                    if (bIsNumberOnly)
                                    {
                                        AutoNumber = "00000000" + SERIESNUMBER;
                                    }
                                    else
                                    {
                                        AutoNumber = PrefixCode + "00000000" + SERIESNUMBER;
                                    }

                                }

                            }

                            else
                            {
                                SERIESNUMBER = SERIESNUMBER + 1;
                                if (bIsNumberOnly)
                                {
                                    AutoNumber = "00000000" + SERIESNUMBER;
                                }
                                else
                                {
                                    AutoNumber = PrefixCode + "00000000" + SERIESNUMBER;
                                }


                            }
                        }

                    }


                }
            }

            //}

            //catch { 

            //}


            return AutoNumber;

        }



        public void UPDATE_SERIES_NUMBER(string _prefixCode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSys.UPDATE_SERIES_NUMBER", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PREFIXCODE", _prefixCode);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }

        }

        #endregion



    }
}