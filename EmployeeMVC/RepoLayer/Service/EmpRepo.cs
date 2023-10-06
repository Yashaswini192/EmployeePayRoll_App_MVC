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

        public EmployeeModel GetEmployeeById(int EmpId)
        {
            try
            {
                string query = "SELECT * FROM EMPLOYEE WHERE EmpId=" + EmpId;
                
                using (SqlConnection connection = new SqlConnection(this.configuration.GetConnectionString("EmpPayRollDataBase")))
                {
                    EmployeeModel employee = new EmployeeModel();
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        EmployeeModel emp = new EmployeeModel()
                        {
                            EmpId = Convert.ToInt32(reader["EmpId"]),
                            Name = reader["Name"].ToString(),
                            ProfileImage = reader["ProfileImage"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            Department = reader["Department"].ToString(),
                            Salary = Convert.ToInt64(reader["Salary"]),
                            StartDate = reader.GetDateTime(6),
                            Notes = reader["Notes"].ToString()
                        };
                        return emp;
                    }
                    return employee;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EmployeeModel UpdateEmployeeById(EmployeeModel employeeModel)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(this.configuration.GetConnectionString("EmpPayRollDataBase")))
                {
                    connect.Open();
                    SqlCommand cmd = new SqlCommand("UpdateEmployeeDetails", connect);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //SqlDataReader reader = cmd.ExecuteReader();

                   
                        cmd.Parameters.AddWithValue("@EmpID", employeeModel.EmpId);
                        cmd.Parameters.AddWithValue("@Name", employeeModel.Name);
                        cmd.Parameters.AddWithValue("@ProfileImage", employeeModel.ProfileImage);
                        cmd.Parameters.AddWithValue("@Gender", employeeModel.Gender);
                        cmd.Parameters.AddWithValue("@department", employeeModel.Department);
                        cmd.Parameters.AddWithValue("@Salary", employeeModel.Salary);
                        cmd.Parameters.AddWithValue("@StartDate", employeeModel.StartDate);
                        cmd.Parameters.AddWithValue("@Notes", employeeModel.Notes);

                        cmd.ExecuteNonQuery();

                        connect.Close();
                        if (employeeModel != null)
                        {
                            return employeeModel;
                        }
                        return employeeModel;
                }
            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }     
    }
}
