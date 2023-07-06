﻿using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Repository
{
    public class InvoiceRepo
    {
        public DataSet GetBLDetails(string connstring, string BL_NO, string PORT, string ORG_CODE)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_BL_DETAILS" },
                new SqlParameter("@BL_NO", SqlDbType.VarChar, 100) { Value = BL_NO },
                new SqlParameter("@PORT", SqlDbType.VarChar, 100) { Value = PORT },
                new SqlParameter("@ORG_CODE", SqlDbType.VarChar, 50) { Value = ORG_CODE },
            };

            return SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_INVOICE", parameters);
        }

        public static T GetSingleDataFromDataSet<T>(DataTable dataTable) where T : new()
        {
            return SqlHelper.CreateItemFromRow<T>(dataTable.Rows[0]);
        }

        public static List<T> GetListFromDataSet<T>(DataTable dataTable) where T : new()
        {
            return SqlHelper.CreateListFromTable<T>(dataTable);
        }

        public void InsertInvoice(string connstring, INVOICE_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION",    SqlDbType.VarChar,50) { Value = "INSERT_INVOICES" },
                  new SqlParameter("@INVOICE_NO",   SqlDbType.VarChar,100) { Value = master.INVOICE_NO},
                  new SqlParameter("@INVOICE_ID",   SqlDbType.Int) { Value = master.INVOICE_ID},
                  new SqlParameter("@INVOICE_TYPE", SqlDbType.VarChar, 100) { Value = master.INVOICE_TYPE },
                  new SqlParameter("@BILL_TO",      SqlDbType.VarChar, 50) { Value = master.BILL_TO },
                  new SqlParameter("@BILL_FROM",    SqlDbType.VarChar, 50) { Value = master.BILL_FROM },
                  new SqlParameter("@SHIPPER_NAME", SqlDbType.NVarChar, 50) { Value = master.SHIPPER_NAME },
                  new SqlParameter("@PAYMENT_TERM", SqlDbType.VarChar, 50) { Value = master.PAYMENT_TERM },
                  new SqlParameter("@ADDRESS", SqlDbType.VarChar, 50) { Value = master.ADDRESS },
                  new SqlParameter("@BRANCH_ID",     SqlDbType.Int ) { Value = master.BRANCH_ID },
                  new SqlParameter("@INVOICE_DATE",  SqlDbType.DateTime ) { Value = master.INVOICE_DATE },
                  new SqlParameter("@BL_NO",        SqlDbType.VarChar, 50) { Value = master.BL_NO },
                  new SqlParameter("@AGENT_NAME",   SqlDbType.VarChar, 50) { Value = master.AGENT_NAME},
                  new SqlParameter("@AGENT_CODE",   SqlDbType.VarChar, 50) { Value = master.AGENT_CODE},
                  new SqlParameter("@CREATED_BY",   SqlDbType.VarChar, 50) { Value = master.CREATED_BY},
                  new SqlParameter("@UPDATED_BY",   SqlDbType.VarChar, 50) { Value = master.UPDATED_BY},
                  new SqlParameter("@STATUS",        SqlDbType.VarChar, 50) { Value = master.STATUS},
                  new SqlParameter("@CONTAINERS",    SqlDbType.VarChar) { Value = master.CONTAINERS},
                  new SqlParameter("@SHIPPER_REF",    SqlDbType.VarChar,100) { Value = master.SHIPPER_REF},
                  new SqlParameter("@REMARKS",    SqlDbType.VarChar,255) { Value = master.REMARKS},
                };

                var ID = SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_INVOICE", parameters);

                string[] columns = new string[18];
                columns[0] = "INVOICE_NO";
                columns[1] = "CHARGE_NAME";
                columns[2] = "EXCHANGE_RATE";
                columns[3] = "QUANTITY";
                columns[4] = "AMOUNT";
                columns[5] = "HSN_CODE";
                columns[6] = "APPROVED_RATE";
                columns[7] = "CURRENCY";
                columns[8] = "IS_SRRCHARGE";
                columns[9] = "INVOICE_ID";
                columns[10] = "ID";
                columns[11] = "TAXABLE_AMOUNT";
                columns[12] = "RATE_PER";
                columns[13] = "SGST";
                columns[14] = "CGST";
                columns[15] = "IGST";
                columns[16] = "TAX_AMOUNT";
                columns[17] = "TOTAL_AMOUNT";

                if (ID != "NULL")
                {
                    DataTable tbl = new DataTable();

                    tbl.Columns.Add(new DataColumn("INVOICE_NO", typeof(string)));
                    tbl.Columns.Add(new DataColumn("CHARGE_NAME", typeof(string)));
                    tbl.Columns.Add(new DataColumn("QUANTITY", typeof(int)));
                    tbl.Columns.Add(new DataColumn("HSN_CODE", typeof(string)));
                    tbl.Columns.Add(new DataColumn("CURRENCY", typeof(string)));
                    tbl.Columns.Add(new DataColumn("IS_SRRCHARGE", typeof(string)));
                    tbl.Columns.Add(new DataColumn("INVOICE_ID", typeof(int)));
                    tbl.Columns.Add(new DataColumn("ID", typeof(int)));
                    tbl.Columns.Add(new DataColumn("APPROVED_RATE", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("AMOUNT", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("EXCHANGE_RATE", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("TAXABLE_AMOUNT", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("RATE_PER", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("SGST", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("CGST", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("IGST", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("TAX_AMOUNT", typeof(decimal)));
                    tbl.Columns.Add(new DataColumn("TOTAL_AMOUNT", typeof(decimal)));


                    foreach (var i in master.BL_LIST)
                    {
                        DataRow dr = tbl.NewRow();

                        dr["INVOICE_NO"] = master.INVOICE_NO;
                        dr["INVOICE_ID"] = Convert.ToInt32(ID);
                        dr["CHARGE_NAME"] = i.CHARGE_NAME;
                        dr["QUANTITY"] = i.QUANTITY;
                        dr["HSN_CODE"] = i.HSN_CODE;
                        dr["CURRENCY"] = i.CURRENCY;
                        dr["IS_SRRCHARGE"] = i.IS_SRRCHARGE;
                        dr["ID"] = i.ID;
                        dr["APPROVED_RATE"] = i.APPROVED_RATE;
                        dr["AMOUNT"] = i.AMOUNT;
                        dr["EXCHANGE_RATE"] = i.EXCHANGE_RATE;
                        dr["TAXABLE_AMOUNT"] = i.TAXABLE_AMOUNT;
                        dr["RATE_PER"] = i.RATE_PER;
                        dr["SGST"] = i.SGST;
                        dr["CGST"] = i.CGST;
                        dr["IGST"] = i.IGST;
                        dr["TAX_AMOUNT"] = i.TAX_AMOUNT;
                        dr["TOTAL_AMOUNT"] = i.TOTAL_AMOUNT;

                        tbl.Rows.Add(dr);
                    }                    

                    SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl, "INVOICE_CHARGES", columns);
                }
                else
                {
                    SqlHelper.UpdateInvoiceCharges<INVOICE_CHARGES>(master.BL_LIST, "INVOICE_CHARGES", connstring, columns);
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void FinalizeInvoice(string connstring, INVOICE_FINALIZE master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION",    SqlDbType.VarChar,50) { Value = "FINALIZE_INVOICE" },
                  new SqlParameter("@INVOICE_NO",   SqlDbType.VarChar,100) { Value = master.INVOICE_NO},
                  new SqlParameter("@INVOICE_ID", SqlDbType.Int) { Value = master.INVOICE_ID },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_INVOICE", parameters);               
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetInvoiceDetails(string connstring, int INVOICE_ID,string INVOICE_NO, string PORT, string ORG_CODE)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_INVOICE_DETAILS" },
                new SqlParameter("@INVOICE_NO", SqlDbType.VarChar, 100) { Value = INVOICE_NO },
                new SqlParameter("@PORT", SqlDbType.VarChar, 100) { Value = PORT },
                new SqlParameter("@ORG_CODE", SqlDbType.VarChar, 50) { Value = ORG_CODE },
                new SqlParameter("@INVOICE_ID", SqlDbType.Int) { Value = INVOICE_ID },
            };

            return SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_CRUD_INVOICE", parameters);
        }

        public List<INVOICE_MASTER> GetInvoiceList(string connstring, string FROM_DATE, string TO_DATE, string ORG_CODE, string PORT, string BL_NO)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_INVOICE_LIST" },
                  new SqlParameter("@FROMDATE", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(FROM_DATE) ? null : Convert.ToDateTime(FROM_DATE) },
                  new SqlParameter("@TODATE", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(TO_DATE) ? null : Convert.ToDateTime(TO_DATE) },
                  new SqlParameter("@ORG_CODE", SqlDbType.VarChar, 50) { Value = ORG_CODE },
                  new SqlParameter("@PORT", SqlDbType.VarChar, 100) { Value = PORT },
                  new SqlParameter("@BL_NO", SqlDbType.VarChar, 100) { Value = BL_NO },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_INVOICE", parameters);
                List<INVOICE_MASTER> invoiceList = SqlHelper.CreateListFromTable<INVOICE_MASTER>(dataTable);

                return invoiceList;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
