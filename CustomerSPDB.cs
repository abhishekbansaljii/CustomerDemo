using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Model;
using System.Net;
using System.Reflection;
using System.Xml.Linq;

namespace CustomerDemo
{
    internal class CustomerSPDB
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        public CustomerSPDB()
        {
            string conStr = "server=.;database=SoleraEmployee;integrated security=true;user id = sa;pwd=sa";
            con = new SqlConnection(conStr);
        }
        public void AddCustomer(Customer c)
        {
            string insStr = $"AddCustomer";
            cmd = new SqlCommand(insStr, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cname", c.CNAME));
            cmd.Parameters.Add(new SqlParameter("@gender", c.CGENDER));
            cmd.Parameters.Add(new SqlParameter("@address", c.ADDRESS));
            cmd.Parameters.Add(new SqlParameter("@mobile", c.MOBILE));
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Customer Inserted Successfully in database...............");
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
        public string UpdateCustomer(int cid, Customer c)
        {
            string updStr = $"UpdateCustomer";
            cmd = new SqlCommand(updStr, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cid", c.CID));
            cmd.Parameters.Add(new SqlParameter("@cname", c.CNAME));
            cmd.Parameters.Add(new SqlParameter("@gender", c.CGENDER));
            cmd.Parameters.Add(new SqlParameter("@address", c.ADDRESS));
            cmd.Parameters.Add(new SqlParameter("@mobile", c.MOBILE));
            string returnData = "";
            SqlParameter sp = new SqlParameter("@sts", SqlDbType.VarChar, 100);
            sp.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(sp);
            try
            {
                con.Open();
                int rEffected = cmd.ExecuteNonQuery();
                returnData = cmd.Parameters[5].ToString();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return returnData;
        }


        public string DeleteCustomer(int cid)
        {
            string delStr = $"DeleteCustomer";
            cmd = new SqlCommand(delStr, con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cid", cid));
            SqlParameter sp = new SqlParameter("@sts", SqlDbType.VarChar, 100);
            sp.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(sp);
            string returnData = "";
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                returnData = cmd.Parameters[1].Value.ToString();

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return returnData;

        }
        public Customer FindCustomer(int cid)
        {
            string findtStr = $"FindCustomer";
            cmd = new SqlCommand(findtStr, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cid", cid));
            SqlDataReader dr = null;
            Customer cr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    cr = new Customer
                    {
                        CID = dr.GetInt32(0),
                        CNAME = dr.GetString(1),
                        CGENDER = dr.GetString(2),
                        ADDRESS = dr.GetString(3),
                        MOBILE = dr.GetString(4)
                    };
                    Console.WriteLine("Customer Found..");
                    return cr;
                }
                else
                {
                    cr = null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return cr;
        }
        public List<Customer> GetCustomers()
        {
            List<Customer> lcst = new List<Customer>();
            string custStr = $"CustomerSummary";
            cmd = new SqlCommand(custStr, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = null;
            Customer cr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    cr = new Customer
                    {
                        CID = dr.GetInt32(0),
                        CNAME = dr.GetString(1),
                        CGENDER = dr.GetString(2),
                        ADDRESS = dr.GetString(3),
                        MOBILE = dr.GetString(4)
                    };
                    lcst.Add(cr);
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return lcst;
        }
    }
}
