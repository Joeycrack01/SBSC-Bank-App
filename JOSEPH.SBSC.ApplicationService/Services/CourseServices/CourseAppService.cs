using JOSEPH.SBSC.Core.Models;
using JOSEPH.SBSC.Repository.Repositories.CourseRepo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JOSEPH.SBSC.ApplicationService.Services.CourseServices
{
    public class CourseAppService : ICourseAppService
    {
        private readonly ICourseRepository _courseRepo;
        public CourseAppService(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            var courses = await _courseRepo.GetAllCourses();
            return courses;
        }

        public async Task<Course> GetById(int courseId)
        {
            var courses = await _courseRepo.GetCourseById(courseId);
            return courses;
        }

        public async Task CreateEmployeeCourse(int courseId, int userId, int status, DateTime dateCreated)
        {
            await _courseRepo.CreateEmployeeCourse(courseId, userId, status, dateCreated);
        }

        public async Task CreateCourse(string courseCode, string courseContent, string courseName, int userId, DateTime dateCreated)
        {
            await _courseRepo.CreateCourse(courseCode, courseContent, courseName, userId, dateCreated);
        }

        public async Task<IEnumerable<EmployeeCourse>> GetEmployeeCourses(int userId)
        {
            var employeeCourses = await _courseRepo.GetEmployeeCourses(userId);
            return employeeCourses;
        }

        public async Task<IEnumerable<EmployeeCourse>> GetEmployeeCoursesByStatus(int userId, int courseStatus)
        {
            var employeeCourses = await _courseRepo.GetEmployeeCoursesByStatus(userId, courseStatus);
            return employeeCourses;
        }

        public async Task UpdateEmployeeCourseStatus(int courseId, int userId, int status)
        {
            await _courseRepo.CompleteEmployeeCourse(courseId, userId, status);
        }

        public async Task<int> GetEmployeeCoursesCountByStatus(int userId, int courseStatus)
        {
            var count = await _courseRepo.GetEmployeeCoursesCountByStatus(userId, courseStatus);
            return count;
        }

    }
}
