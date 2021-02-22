using CoreAngularLearning.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAngularLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = "select * from tblDepartments";
            DataTable dt = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlConnection con = new SqlConnection(SqlDataSource);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            
                dt.Load(reader);
            
            //reader.Close();
            con.Close();
            return new JsonResult(dt);
        }


        [HttpPut]
        public JsonResult Put(Department dep)
        {
            string query = "update tblDepartments set DepartmentName='"+dep.DepartmentName+"' where DepartmentID='"+dep.DepartmentID+"'";
            DataTable dt = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlConnection con = new SqlConnection(SqlDataSource);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return new JsonResult("UpdateSuccessfully!");
        }

        [HttpPost]
        public JsonResult Post(Department dep)
        {
            string query = "insert into tblDepartments(DepartmentName)values('"+dep.DepartmentName+"')";
            DataTable dt = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlConnection con = new SqlConnection(SqlDataSource);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return new JsonResult("Added Successfully!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = "delete from tblDepartments where DepartmentID='"+id+"'";
            DataTable dt = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlConnection con = new SqlConnection(SqlDataSource);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return new JsonResult("Deleted Successfully!");
        }

        [HttpGet("{DeptID}")]
        public JsonResult GetDepartmentWithID(int DeptID)
        {
            string query = "select * from tblDepartments where DepartmentID='"+ DeptID + "'";
            DataTable dt = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlConnection con = new SqlConnection(SqlDataSource);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            dt.Load(reader);

            //reader.Close();
            con.Close();
            return new JsonResult(dt);
        }
    }
}
