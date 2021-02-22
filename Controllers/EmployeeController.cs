using CoreAngularLearning.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CoreAngularLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult GetEmployees()
        {
            string SqlDataSource = _configuration.GetConnectionString("EmployeeAppcon");
            SqlConnection con = new SqlConnection(SqlDataSource);
            con.Open();
            string query = "select * from tblEmployee";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            con.Close();
            return new JsonResult(dt);

        }

        [HttpPost]
        public JsonResult AddEmployee(Employee emp)
        {
            string SqlDataSource = _configuration.GetConnectionString("EmployeeAppcon");
            SqlConnection con = new SqlConnection(SqlDataSource);
            con.Open();
            string query = "insert into tblEmployee(Name,Department,DateOfJoining,PhotoFileName)values('" + emp.Name + "','" + emp.Department + "','" + emp.DateOfJoining + "','" + emp.PhotoFileName + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return new JsonResult("Employee Added");

        }

        [HttpPut]
        public JsonResult updateEmployee(Employee emp)
        {
            string SqlDataSource = _configuration.GetConnectionString("EmployeeAppcon");
            SqlConnection con = new SqlConnection(SqlDataSource);
            con.Open();
            string query = "update tblEmployee set Name='" + emp.Name + "',Department='" + emp.Department + "',DateOfJoining='" + emp.DateOfJoining + "',PhotoFileName='" + emp.PhotoFileName + "' where EmployeeID='" + emp.EmployeeID + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return new JsonResult("Updated Successfully!");

        }

        [HttpDelete("{EmpID}")]
        public JsonResult DeleteEmployee(int EmpID)
        {
            string SqlDataSource = _configuration.GetConnectionString("EmployeeAppcon");
            SqlConnection con = new SqlConnection(SqlDataSource);
            con.Open();
            string query = "delete from tblEmployee where EmployeeID='" + EmpID + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return new JsonResult("Employee Deleted Successfully!");

        }


        [HttpGet("{EmpID}")]
        public JsonResult GetEmployeeWithID(int EmpID)
        {
            string SqlDataSource = _configuration.GetConnectionString("EmployeeAppcon");
            SqlConnection con = new SqlConnection(SqlDataSource);
            con.Open();
            string query = "select *  from tblEmployee where EmployeeID='" + EmpID + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            con.Close();
            return new JsonResult("Employee Deleted Successfully!");

        }

        [HttpPost("SaveFile")]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos" + filename;
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception ex)
            {

                return new JsonResult("anonymous.png");
            }
        }

        [HttpGet("GetAllDepartments")]
        public JsonResult GetAllDepartmentNames()
        {
            string SqlDataSource = _configuration.GetConnectionString("EmployeeAppcon");
            SqlConnection con = new SqlConnection(SqlDataSource);
            con.Open();
            string query = "select DepartmentName from tblDepartments";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            con.Close();
            return new JsonResult(dt);
        }


    }

}
