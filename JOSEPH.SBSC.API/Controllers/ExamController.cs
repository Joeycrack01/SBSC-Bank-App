using JOSEPH.SBSC.ApplicationService.Infrastructure.Enums;
using JOSEPH.SBSC.ApplicationService.Services.ExamServices;
using JOSEPH.SBSC.ApplicationService.ViewModels;
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
    public class ExamController : BaseApiController
    {
        private readonly IExamAppService _examsAppService;
        public ExamController(IHttpContextAccessor httpContextAccessor, IExamAppService examsAppService) 
            : base(httpContextAccessor)
        {
            examsAppService = _examsAppService;
        }

        /// <summary>
        /// Use this function to add an examination to the system
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet, Route("CreateExam")]
        public async Task<ResponseBase> CreateExam ([FromQuery] CreateExamsViewModel model)
        {
            try
            {
                GetUserId();
                model.CreatedBy = UserId;
                await _examsAppService.CreateExam(model);

                return new ResponseBase() { IsSuccess = true, ErrorDetails = null };
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new ResponseBase() { ErrorDetails = Error(), IsSuccess = false};
            }
        }
        
        /// <summary>
        /// Each exam should have a list of questions. Use this function to
        /// to add question for a particular exams.
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="courseId"></param>
        /// <param name="question"></param>
        /// <param name="answer"></param>
        /// <param name="marks"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet, Route("CreateExamQuestion")]
        public async Task<ResponseBase> CreateExamQuestion([FromQuery] int examId, int courseId, string question, string answer, int marks)
        {
            try
            {
                GetUserId();
                await _examsAppService.CreateExamQuestion(examId, courseId, question, answer, marks, UserId);

                return new ResponseBase() { IsSuccess = true, ErrorDetails = null };
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new ResponseBase() { ErrorDetails = Error(), IsSuccess = false};
            }
        }
        
        /// <summary>
        /// gets a list of exams to be taken for a particular course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet, Route("GetCourseExams")]
        public async Task<ResponseBase<IEnumerable<Exam>>> GetCourseExams([FromQuery] int courseId)
        {
            try
            {
                var exams = await _examsAppService.GetCourseExamsByCourseID(courseId);

                return new ResponseBase<IEnumerable<Exam>>() { IsSuccess = true, ErrorDetails = null, Payload = exams };
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new ResponseBase<IEnumerable<Exam>>() { ErrorDetails = Error(), IsSuccess = false};
            }
        }
        
        /// <summary>
        /// Gets a list of practice exams for a course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet, Route("GetCoursePracticeExams")]
        public async Task<ResponseBase<IEnumerable<Exam>>> GetCoursePracticeExams([FromQuery] int courseId)
        {
            try
            {
                var exams = await _examsAppService.GetCoursePracticeExamsByCourseID(courseId, (int)(ExamTypeEnum.Certification));

                return new ResponseBase<IEnumerable<Exam>>() { IsSuccess = true, ErrorDetails = null, Payload = exams };
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new ResponseBase<IEnumerable<Exam>>() { ErrorDetails = Error(), IsSuccess = false, Payload = null};
            }
        }
        
        /// <summary>
        /// Get details of an examination by the exams Id
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet, Route("GetExamByID")]
        public async Task<ResponseBase<Exam>> GetExamByID([FromQuery] int examId)
        {
            try
            {
                var exam = await _examsAppService.GetExamsById(examId);

                return new ResponseBase<Exam>() { IsSuccess = true, ErrorDetails = null, Payload = exam };
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new ResponseBase<Exam>() { ErrorDetails = Error(), IsSuccess = false, Payload = null};
            }
        }

        /// <summary>
        /// delete an examination using this function
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet, Route("DeleteExam")]
        public async Task<ResponseBase> DeleteExam([FromQuery] int examId)
        {
            try
            {
                await _examsAppService.DeleteExam(examId);

                return new ResponseBase() { IsSuccess = true, ErrorDetails = null };
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new ResponseBase<Exam>() { ErrorDetails = Error(), IsSuccess = false, Payload = null};
            }
        }
        
        /// <summary>
        /// Use this function to update an examination details
        /// </summary>
        /// <param name="examName"></param>
        /// <param name="passScore"></param>
        /// <param name="examType"></param>
        /// <param name="courseId"></param>
        /// <param name="examNo"></param>
        /// <param name="examId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet, Route("UpdateExam")]
        public async Task<ResponseBase> UpdateExam([FromQuery] string examName, int passScore, int examType, int courseId, string examNo, int examId)
        {
            try
            {
                await _examsAppService.UpdateExam(examName, passScore, examType, courseId, examNo, examId);

                return new ResponseBase() { IsSuccess = true, ErrorDetails = null };
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new ResponseBase<Exam>() { ErrorDetails = Error(), IsSuccess = false, Payload = null};
            }
        }

    }
}
