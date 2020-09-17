using JOSEPH.SBSC.ApplicationService.Infrastructure.Enums;
using JOSEPH.SBSC.ApplicationService.Services.CourseServices;
using JOSEPH.SBSC.Core.Models;
using JOSEPH.SBSC.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JOSEPH.SBSC.API.Controllers
{
    [Route("api/[controller]")]
    public class CourseController : BaseApiController
    {
        private readonly ICourseAppService _courseAppService;
        public CourseController(IHttpContextAccessor httpContextAccessor, ICourseAppService courseAppService) 
            : base(httpContextAccessor)
        {
            _courseAppService = courseAppService;
        }

        /// <summary>
        /// use this function to create a course
        /// </summary>
        /// <param name="courseCode"></param>
        /// <param name="courseContent"></param>
        /// <param name="courseName"></param>
        /// <param name="dateCreated"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet, Route("CreateCourse")]
        public async Task<ResponseBase> CreateCourse([FromQuery] string courseCode, string courseContent, string courseName, DateTime dateCreated)
        {
            try
            {
                GetUserId();
                await _courseAppService.CreateCourse(courseCode, courseContent, courseName, UserId, dateCreated);

                return new ResponseBase() { IsSuccess = true, ErrorDetails = null };
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new ResponseBase() { ErrorDetails = Error(), IsSuccess = false };
            }
        }

        /// <summary>
        /// call this function to enroll an employee for a course
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="dateCreated"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet, Route("EmployeeCourseEnroll")]
        public async Task<ResponseBase> EmployeeCourseEnroll([FromQuery] int courseId, DateTime dateCreated)
        {
            try
            {
                GetUserId();
                await _courseAppService.CreateEmployeeCourse(courseId, UserId, (int)(CourseStatusEnum.Enrolled), dateCreated);

                return new ResponseBase() { IsSuccess = true, ErrorDetails = null };
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new ResponseBase() { ErrorDetails = Error(), IsSuccess = false };
            }
        }

        /// <summary>
        /// Gets a list of courses
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet, Route("GetAll")]
        public async Task<ResponseBase<IEnumerable<Course>>> GetAll()
        {
            try
            {
                GetUserId();
                var courses = await _courseAppService.GetAllCourses();

                return new ResponseBase<IEnumerable<Course>>() { IsSuccess = true, ErrorDetails = null, Payload = courses };
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new ResponseBase<IEnumerable<Course>>() { ErrorDetails = Error(), IsSuccess = false, Payload = null };
            }
        }
        
        /// <summary>
        /// Get course details by courseId
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet, Route("GetById")]
        public async Task<ResponseBase<Course>> GetById([FromQuery] int courseId)
        {
            try
            {
                GetUserId();
                var courses = await _courseAppService.GetById(courseId);

                return new ResponseBase<Course>() { IsSuccess = true, ErrorDetails = null, Payload = courses };
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new ResponseBase<Course>() { ErrorDetails = Error(), IsSuccess = false, Payload = null };
            }
        }

        /// <summary>
        /// A very flexible function that can be used to get courses an employee has started, completed
        /// or has just enrolled for. it all depends on the status passed from the ui
        /// </summary>
        /// <param name="courseStatus"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet, Route("GetEmployeeCourseByStatus")]
        public async Task<ResponseBase<IEnumerable<EmployeeCourse>>> GetEmployeeCourseByStatus([FromQuery] int courseStatus)
        {
            try
            {
                GetUserId();
                var employeeCourses = await _courseAppService.GetEmployeeCoursesByStatus(UserId, courseStatus);

                return new ResponseBase<IEnumerable<EmployeeCourse>>() { IsSuccess = true, ErrorDetails = null, Payload = employeeCourses };
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new ResponseBase<IEnumerable<EmployeeCourse>>() { ErrorDetails = Error(), IsSuccess = false, Payload = null };
            }
        }

        /// <summary>
        /// Gets a list of all courses an employee ever enrolled for no matter the
        /// status of the course
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet, Route("GetEmployeeCourses")]
        public async Task<ResponseBase<IEnumerable<EmployeeCourse>>> GetEmployeeCourses()
        {
            try
            {
                GetUserId();
                var employeeCourses = await _courseAppService.GetEmployeeCourses(UserId);

                return new ResponseBase<IEnumerable<EmployeeCourse>>() { IsSuccess = true, ErrorDetails = null, Payload = employeeCourses };
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new ResponseBase<IEnumerable<EmployeeCourse>>() { ErrorDetails = Error(), IsSuccess = false, Payload = null };
            }
        }

        /// <summary>
        /// Gets a list of all courses an employee ever enrolled for no matter the
        /// status of the course. but in this case the user might not necessarily be the logged in user
        /// it could be the HR selecting an employee to see what course they have enrolled for
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet, Route("GetEmployeeCoursesByUserId")]
        public async Task<ResponseBase<IEnumerable<EmployeeCourse>>> GetEmployeeCoursesByUserId([FromQuery] int userId)
        {
            try
            {
                var employeeCourses = await _courseAppService.GetEmployeeCourses(userId);

                return new ResponseBase<IEnumerable<EmployeeCourse>>() { IsSuccess = true, ErrorDetails = null, Payload = employeeCourses };
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new ResponseBase<IEnumerable<EmployeeCourse>>() { ErrorDetails = Error(), IsSuccess = false, Payload = null };
            }
        }

        /// <summary>
        /// Use this function to update the status of a course an employee has
        /// enrolled for, from either enrolled, started or completed to your desired
        /// status.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet, Route("UpdateEmployeeCourseStatus")]
        public async Task<ResponseBase> UpdateEmployeeCourseStatus([FromQuery] int courseId, int userId, int status)
        {
            try
            {
                await _courseAppService.UpdateEmployeeCourseStatus(courseId, userId, status);

                return new ResponseBase() { IsSuccess = true, ErrorDetails = null };
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new ResponseBase() { ErrorDetails = Error(), IsSuccess = false };
            }
        }

    }
}
