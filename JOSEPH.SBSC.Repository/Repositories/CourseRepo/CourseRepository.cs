using JOSEPH.SBSC.Core.Models;
using JOSEPH.SBSC.Core.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JOSEPH.SBSC.Repository.Repositories.CourseRepo
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;
        public CourseRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            var courses = await _context.Courses.ToListAsync();
            return courses;
        }

        public async Task<Course> GetCourseById(int courseId)
        {
            var courses = await _context.Courses.Where(c => c.ID == courseId).FirstOrDefaultAsync();
            return courses;
        }

        public async Task CreateEmployeeCourse(int courseId, int userId, int status, DateTime dateCreated)
        {
            EmployeeCourse employeeCourse = new EmployeeCourse
            {
                CourseId = courseId,
                UserID = userId,
                Status = status,
                DateCreated = dateCreated
            };
            _context.EmployeeCourses.Add(employeeCourse);
            await _context.SaveChangesAsync();
        }

        public async Task CreateCourse(string courseCode, string courseContent, string courseName, int userId, DateTime dateCreated)
        {
            Course course = new Course
            {
                CourseCode = courseCode,
                CourseContent = courseContent,
                CourseName = courseName,
                CreatedBy = userId,
                DateCreated = dateCreated,
            };
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeCourse>> GetEmployeeCourses(int userId)
        {
            var employeeCourses = await _context.EmployeeCourses.Where(c => c.UserID == userId).ToListAsync();
            return employeeCourses;
        }

        public async Task<IEnumerable<EmployeeCourse>> GetEmployeeCoursesByStatus(int userId, int courseStatus)
        {
            var employeeCourses = await _context.EmployeeCourses.Where(c => c.UserID == userId && c.Status == courseStatus).ToListAsync();
            return employeeCourses;
        }

        public async Task CompleteEmployeeCourse(int courseId, int userId, int status)
        {
            var employeeCourses = await _context.EmployeeCourses.Where(c => c.UserID == userId && c.CourseId == courseId).FirstOrDefaultAsync();
            employeeCourses.Status = status;
            _context.Update(employeeCourses);

            await _context.SaveChangesAsync();
        }

        public async Task<int> GetEmployeeCoursesCountByStatus(int userId, int courseStatus)
        {
            var count = await _context.EmployeeCourses.Where(c => c.UserID == userId && c.Status == courseStatus).CountAsync();
            return count;
        }

    }
}
