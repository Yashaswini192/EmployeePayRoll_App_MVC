using Microsoft.Extensions.Configuration;
using ModelLayer.Model;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepoLayer.Service
{
    public class EmpRepo : IEmpRepo
    {
        private readonly IConfiguration configuration;

        public EmpRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AddEmployee(EmployeeModel employeeModel)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(this.configuration.GetConnectionString("EmpPayRollDataBase")))
                {
                    connect.Open();
                    SqlCommand sqlCommand = new SqlCommand("AddEmployeeDetails", connect);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Name", employeeModel.Name);
                    sqlCommand.Parameters.AddWithValue("@ProfileImage", employeeModel.ProfileImage);
                    sqlCommand.Parameters.AddWithValue("@Gender", employeeModel.Gender);
                    sqlCommand.Parameters.AddWithValue("@department", employeeModel.Department);
                    sqlCommand.Parameters.AddWithValue("@Salary", employeeModel.Salary);
                    sqlCommand.Parameters.AddWithValue("@StartDate", employeeModel.StartDate);
                    sqlCommand.Parameters.AddWithValue("@Notes", employeeModel.Notes);

                    int value = sqlCommand.ExecuteNonQuery();
                    string result = value.ToString();
                    connect.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            try
            {
                List<EmployeeModel> employees = new List<EmployeeModel>();
                using (SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("EmpPayRollDataBase")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetAllEmployees", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            EmployeeModel employeeModel = new EmployeeModel()
                            {
                                EmpId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                ProfileImage = reader.GetString(2),
                                Gender = reader.GetString(3),
                                Department = reader.GetString(4),
                                Salary = reader.GetDecimal(5),
                                StartDate = reader.GetDateTime(6),
                                Notes = reader.GetString(7)
                            };
                            employees.Add(employeeModel);

                        }
                    }
                    con.Close();
                    reader.Close();
                    return employees;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
