using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApi.Models;
using System.Data.SqlClient;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private IConfiguration _Configuration { get; }
        private IHostEnvironment _env { get; }
        public EmployeeController(IConfiguration configuration,IHostEnvironment env)
        {
            _Configuration = configuration;
            _env = env;
        }
        [HttpGet]
        public JsonResult Get()
        {

            string query = @"
                            Select employeeid, EmployeeName, DepartmentId, DateOfJoining,PhotoFileName from Employee";
            DataTable table = new DataTable();
            string sqlDataSource = _Configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    table.Load(sqlDataReader);

                    sqlDataReader.Close();
                    sqlConnection.Close();
                }

            }
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            string query = @"insert into Employee values
                            (
                            '" + emp.EmployeeName + @"',
                            '" + emp.DepartmentId + @"',
                            '" + emp.DateOfJoining + @"',
                            '" + emp.PhotoFileName + @"'
                            )";
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
        public JsonResult update(Employee emp)
        {
            string query = @"
                            update  Employee set 
                        employeename='" + emp.EmployeeName + @"',
                        departmentid='" + emp.DepartmentId + @"',
                        dateofjoining='" + emp.DateOfJoining + @"',
                        photofilename='" + emp.PhotoFileName + @"'
                        where employeeId=" + emp.EmployeeId + "";
            DataTable table = new DataTable();
            string sqlDataSource = _Configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    table.Load(sqlDataReader);

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
                            delete from employee where employeeid=" + id + "";
            DataTable table = new DataTable();
            string sqlDataSource = _Configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    table.Load(sqlDataReader);

                    sqlDataReader.Close();
                    sqlConnection.Close();
                }

            }
            return new JsonResult("deleted Sucessfully");
        }

        [Route("Savefile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile=httpRequest.Files[0];
                var fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + @"/photos/" + fileName;

                using (var stream=new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    
                }
                return new JsonResult(fileName);
            }
            catch (Exception) 
            {
                    return new JsonResult("Ananoumous.jpg");
             }


        }
        [Route("GetAllDepartmentName")]
        [HttpGet]
        public JsonResult GetAllDepartmentNames()
        {

            string query = @"
                            Select departmentid,DepartmentName from department";
            DataTable table = new DataTable();
            string sqlDataSource = _Configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    table.Load(sqlDataReader);

                    sqlDataReader.Close();
                    sqlConnection.Close();
                }

            }
            return new JsonResult(table);


        }


    }
}
