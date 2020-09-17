using JOSEPH.SBSC.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JOSEPH.SBSC.Repository.Repositories.CourseRepo
{
    public interface ICourseRepository
    {
        Task CompleteEmployeeCourse(int courseId, int userId, int status);
        Task CreateEmployeeCourse(int courseId, int userId, int status, DateTime dateCreated);
        Task<IEnumerable<Course>> GetAllCourses();
        Task<Course> GetCourseById(int courseId);
        Task<IEnumerable<EmployeeCourse>> GetEmployeeCourses(int userId);
        Task<IEnumerable<EmployeeCourse>> GetEmployeeCoursesByStatus(int userId, int courseStatus);
        Task<int> GetEmployeeCoursesCountByStatus(int userId, int courseStatus);
        Task CreateCourse(string courseCode, string courseContent, string courseName, int userId, DateTime dateCreated);
    }
}