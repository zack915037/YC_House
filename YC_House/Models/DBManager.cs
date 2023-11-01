using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using YC_House.ViewModels;
using Dapper;

namespace YC_House.Models
{
    public class DBManager
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        
        public List<StudentBasicInfoViewModel> QueryStudentInfoTable()
        {
            var sql = @"Select * 
                        From StudentBasicInfo info
                        Left Join StudentGrades grades
	                        ON info.StudentId = grades.StudentId";

            return new SqlConnection(connectionString).Query<StudentBasicInfoViewModel>(sql).ToList();
        }
    }
}