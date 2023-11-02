using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using YC_House.ViewModels;
using Dapper;
using System.Xml.Linq;
using System.Reflection;

namespace YC_House.Models
{
    public class DBManager
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private readonly IDbConnection _dbConnection;

        public DBManager()
        {
            _dbConnection = new SqlConnection(connectionString);
        }

        public List<StudentBasicInfoViewModel> QueryStudentInfoTable()
        {
            var sql = @"Select * 
                        From StudentBasicInfo info
                        Left Join StudentGrades grades
	                        ON info.StudentId = grades.StudentId";

            return _dbConnection.Query<StudentBasicInfoViewModel>(sql).ToList();
        }

        public void CreateStudentBasicInfo(StudentBasicInfoViewModel model, string type)
        {

            string sql = type == "add"
                ? @"INSERT INTO StudentBasicInfo ([StudentId], [Name], [Sex], [Heigh], [Weight], [Residential_Address], [CellPhone]) 
                            VALUES (@StudentId, @Name, @Sex, @Heigh, @Weight, @Residential_Address, @CellPhone)"
                : @"Update StudentBasicInfo
                            Set Name = @Name,
                                Sex = @Sex,
	                            Heigh = @Heigh,
	                            Weight = @Weight,
	                            Residential_Address = @Residential_Address,
	                            CellPhone = @CellPhone
                            Where StudentId = @StudentId";

            var parameters = new
            {
                StudentId = model.StudentId,
                Name = model.Name,
                Sex = model.Sex,
                Heigh = model.Heigh,
                Weight = model.Weight,
                Residential_Address = model.Residential_Address,
                CellPhone = model.CellPhone
            };
            _dbConnection.Execute(sql, parameters);
        }

        public void CreateStudentGrades(StudentBasicInfoViewModel model, string type)
        {
            string sql = type == "add"
                ? @"INSERT INTO StudentGrades ([StudentId], [ChScores], [MathScores], [EnScores]) 
                            VALUES (@StudentId, @ChScores, @MathScores, @EnScores)" 
                : @"Update StudentGrades
                    Set ChScores = @ChScores,
                        MathScores = @MathScores,
	                    EnScores = @EnScores
                    Where StudentId = @StudentId";

            var parameters = new
            {
                StudentId = model.StudentId,
                ChScores = model.ChScores,
                MathScores = model.MathScores,
                EnScores = model.EnScores,
            };

            _dbConnection.Execute(sql, parameters);
        }

        public void DeleteStudentBasicInfo(string studentId)
        {
            string sql = @"Delete StudentBasicInfo Where StudentId = @StudentId";

            var parameters = new
            {
                StudentId = studentId,
            };

            _dbConnection.Execute(sql, parameters);
        }

        public void DeleteStudentGrades(string studentId)
        {
            string sql = @"Delete StudentGrades Where StudentId = @StudentId";

            var parameters = new
            {
                StudentId = studentId,
            };

            _dbConnection.Execute(sql, parameters);
        }
    }
}