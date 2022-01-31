using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private IConfiguration _Configuration { get; }
        public DepartmentController(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {

            string query = @"
                            Select DepartmentId, DepartmentName from Department";
            DataTable department = new DataTable();
            string sqlDataSource = _Configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader sqlDataReader;
            using(SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    department.Load(sqlDataReader);

                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
                   
            }
            return new JsonResult(department);           
        }
        [HttpPost]
        public JsonResult Post(Department dept)
        {
            string query = @"
                            insert into Department values('"+ dept.DepartmentName + @"')";
            DataTable department = new DataTable();
            string sqlDataSource = _Configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    department.Load(sqlDataReader);

                    sqlDataReader.Close();
                    sqlConnection.Close();
                }

            }
            return new JsonResult("Addedd Sucessfully");
        }
        [HttpPut]
        public JsonResult update(Department dept)
        {
            string query = @"
                            update  Department set 
                        departmentname='" + dept.DepartmentName + @"'
                        where departmentid="+dept.DepartmentId+"";
            DataTable department = new DataTable();
            string sqlDataSource = _Configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    department.Load(sqlDataReader);

                    sqlDataReader.Close();
                    sqlConnection.Close();
                }

            }
            return new JsonResult("Updated Sucessfully");
        }
        [HttpDelete("{id}")]
        public JsonResult delete(int id)
        {
            string query = @"
                            delete from Department where departmentid=" + id + "";
            DataTable department = new DataTable();
            string sqlDataSource = _Configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    department.Load(sqlDataReader);

                    sqlDataReader.Close();
                    sqlConnection.Close();
                }

            }
            return new JsonResult("deleted Sucessfully");
        }


    }
}
