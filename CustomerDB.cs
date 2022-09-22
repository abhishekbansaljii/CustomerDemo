using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace CustomerDemo
{
    public class CustomerDB
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        public CustomerDB()
        {
            string conStr = "server=.;database=SoleraEmployee;integrated security=true;user id = sa;pwd=sa";
            con = new SqlConnection(conStr);
        }

        public int GenerateID()
        {
            string cmdStr = "select max(cid) from Customer";
            cmd = new SqlCommand(cmdStr, con);
            int genId =0;
            try
            {
                con.Open();
                object data = cmd.ExecuteScalar();
                if (data.ToString().Equals(""))
                {
                    genId = 1;
                }
                else
                {
                    genId = Convert.ToInt32(data)+1;
                }
            }catch(SqlException e)
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
           
           return genId;
        }

        public void AddCustomer(Customer c)
        {
            string insStr = $"insert into Customer values ({c.CID},'{c.CNAME}','{c.CGENDER}','{c.ADDRESS}','{c.MOBILE}')";
            cmd = new SqlCommand(insStr, con);
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
        public bool UpdateCustomer(int cid, Customer c)
        {
            string updtStr = $"update Customer set cname = '{c.CNAME}',cgender = '{c.CGENDER}',caddress = '{c.ADDRESS}',cmobile = '{c.MOBILE}' where cid = {c.CID}";
            cmd = new SqlCommand(updtStr, con);
            try
            {
                con.Open();
                int rEffected = cmd.ExecuteNonQuery();
                if (rEffected == 0)
                {
                    return false;
                }
                else
                {
                    return true;

                    Console.WriteLine("Customer Updated Successfully in database...............");
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
        }

            
        public bool DeleteCustomer(int cid)
        {
            string dltStr = $"delete from Customer  where cid = {cid}"; 
            cmd = new SqlCommand(dltStr, con);
            try
            {
                con.Open();
                int rEffected = cmd.ExecuteNonQuery();
                if (rEffected == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                    Console.WriteLine("Customer Deleted Successfully...");
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

        }
        public Customer FindCustomer(int cid)
        {
            string findtStr = $"select * from Customer  where cid = {cid}";
            cmd = new SqlCommand(findtStr, con);
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
                    return null;
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
        }
        public List<Customer> GetCustomers()
        {
            List<Customer> lcst = new List<Customer> ();
            string custStr = $"select * from Customer";
            cmd = new SqlCommand(custStr, con);
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
