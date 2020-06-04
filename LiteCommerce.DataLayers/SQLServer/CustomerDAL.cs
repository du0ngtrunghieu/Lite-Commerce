using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;
using System.Data.SqlClient;
using System.Data;
namespace LiteCommerce.DataLayers.SQLServer
{
    public class CustomerDAL : ICustomerDAL
    {
        private string connectionString;
        public CustomerDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Add(Customer data)
        {
            int customerId = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Customers
                                          (
	                                          CustomerID,
	                                          CompanyName,
	                                          ContactName,
	                                          ContactTitle,
	                                          Address,
	                                          Country,
	                                          Phone,
	                                          Fax,
                                              City
                                          )
                                          VALUES
                                          (
	                                          @CustomerID,
	                                          @CompanyName,
	                                          @ContactName,
	                                          @ContactTitle,
	                                          @Address,
	                                          @Country,
	                                          @Phone,
	                                          @Fax,
                                              @City
	                                          
                                          );
                                          SELECT @@IDENTITY;";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                cmd.Parameters.AddWithValue("@CompanyName", data.CompanyName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@ContactTitle", data.ContactTitle);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                cmd.Parameters.AddWithValue("@Fax", data.Fax);
                cmd.Parameters.AddWithValue("@City", data.City);
                
                customerId = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return customerId;
        }

        public int Count(string searchValue)
        {
            int count = 0;
            List<Customer> data = new List<Customer>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"
                                select count(*)
	                                from Customers
	                                where @searchValue = N'' or CompanyName like @searchValue
                                    ";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);


                count = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return count;
        }

        public int Delete(string[] CustomerIDs)
        {
            int countDeleted = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Customers
                                            WHERE(CustomerID = @customerId)
                                              AND(CustomerID NOT IN(SELECT CustomerID FROM Orders))";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@CustomerID", SqlDbType.NVarChar);
                //cmd.Parameters.Add("@customerId",sqlDbType.);
                foreach (string customerId in CustomerIDs)
                {
                    cmd.Parameters["@customerId"].Value = customerId;
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        countDeleted += 1;
                }

                connection.Close();
            }
            return countDeleted;
        }

        public Customer Get(string customerID)
        {
            Customer data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Customers WHERE CustomerID = @customerID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@customerID", customerID);

                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Customer()
                        {
                            CustomerID = Convert.ToString(dbReader["CustomerID"]),
                            CompanyName = Convert.ToString(dbReader["CompanyName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            ContactTitle = Convert.ToString(dbReader["ContactTitle"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Phone = Convert.ToString(dbReader["Phone"]),
                            Fax = Convert.ToString(dbReader["Fax"]),
                            City = Convert.ToString(dbReader["City"])
                        };
                    }
                }

                connection.Close();
            }
            return data;
        }

        public List<Customer> List(int page, int pagesize, string searchValue)
        {
            List<Customer> data = new List<Customer>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"
                                select * 
                                from (
	                                select ROW_NUMBER() over(order by CompanyName) as RowNumber,Customers.* 
	                                from Customers 
	                                where (@searchValue = N'') or (CompanyName like @searchValue)
	                                ) as t
                                where t.RowNumber between (@page -1) * @pageSize +1 and (@page*@pageSize)
                                order by t.RowNumber
                                    ";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pagesize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Customer()
                        {

                            CustomerID = Convert.ToString(dbReader["CustomerID"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            CompanyName = Convert.ToString(dbReader["CompanyName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            ContactTitle = Convert.ToString(dbReader["ContactTitle"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Fax = Convert.ToString(dbReader["Fax"]),
                            Phone = Convert.ToString(dbReader["Phone"]),
                            

                        });
                    }
                }
                connection.Close();
            }
            return data;
        }

        public bool Update(Customer data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Customers
                                           SET
                                              CompanyName = @CompanyName 
                                              ,ContactName = @ContactName
                                              ,ContactTitle = @ContactTitle
                                              ,Address = @Address
                                              ,City = @City
                                              ,Country = @Country
                                              ,Phone = @Phone
                                              ,Fax = @Fax
                                              
                                          WHERE CustomerID = @CustomerID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                cmd.Parameters.AddWithValue("@CompanyName", data.CompanyName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@ContactTitle", data.ContactTitle);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                cmd.Parameters.AddWithValue("@Fax", data.Fax);
               

                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }

            return rowsAffected > 0;
        }
    }
}
