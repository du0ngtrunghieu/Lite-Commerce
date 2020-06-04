using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LiteCommerce.DataLayers.SQLServer
{
    public class ProductDAL : IProductDAL
    {
        private string connectionString;
        public ProductDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Add(Product product)
        {
            throw new NotImplementedException();
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
	                                from Products
	                                where @searchValue = N'' or ProductName like @searchValue
                                    ";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return count;
        }

        public int Delete(int[] ProductIDs)
        {
            throw new NotImplementedException();
        }

        public Product Get(int ProductID)
        {
            throw new NotImplementedException();
        }

        public List<Product> List(int page, int pagesize, string searchValue)
        {
            List<Product> data = new List<Product>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"
                                select * 
                                from (
	                                select ROW_NUMBER() over(order by ProductName) as RowNumber,Products.* 
	                                from Products
	                                where (@searchValue = N'') or (ProductName like @searchValue)
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
                        data.Add(new Product()
                        {

                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            ProductName = Convert.ToString(dbReader["ProductName"]),
                            Descriptions = Convert.ToString(dbReader["Descriptions"]),
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            PhotoPath = Convert.ToString(dbReader["PhotoPath"]),
                            QuantityPerUnit = Convert.ToString(dbReader["QuantityPerUnit"]),
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            UnitPrice = Convert.ToDecimal(dbReader["UnitPrice"])


                        });
                    }
                }
                connection.Close();
            }
            return data;
        }

        public bool Update(Product data)
        {
            throw new NotImplementedException();
        }
    }
}
