using JOSEPH.SBSC.ApplicationService.ViewModels;
using JOSEPH.SBSC.Core.Models;
using JOSEPH.SBSC.Repository.Repositories.ExamsRepo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JOSEPH.SBSC.ApplicationService.Services.ExamServices
{
    public class ExamAppService : IExamAppService
    {
        private readonly IExamsRepository _examsRepo;

        public ExamAppService(IExamsRepository examsRepo)
        {
            _examsRepo = examsRepo;
        }

        public async Task CreateExam(CreateExamsViewModel createExams)
        {
            Exam exam = new Exam
            {
                CourseID = createExams.CourseID,
                CreatedBy = createExams.CreatedBy,
                DateCreated = DateTime.Now,
                ExamName = createExams.ExamNo,
                ExamType = createExams.ExamType,
                ExamNo = createExams.ExamNo,
                PassScore = createExams.PassMark
            };

            await _examsRepo.CreateExams(exam);
        }
        public async Task CreateExamQuestion(int examId, int courseId, string question, string answer, int marks, int userId)
        {
            await _examsRepo.AddExamQuestions(examId, courseId, question, answer, marks, userId);
        }

        public async Task<IEnumerable<Exam>> GetCourseExamsByCourseID(int courseId)
        {
            var exams = await _examsRepo.GetCourseExams(courseId);
            return exams;
        }

        public async Task<IEnumerable<Exam>> GetCoursePracticeExamsByCourseID(int courseId, int examType)
        {
            var exams = await _examsRepo.GetCoursePracticeExams(courseId, examType);
            return exams;
        }

        public async Task<Exam> GetExamsById(int examId)
        {
            var exams = await _examsRepo.GetExamById(examId);
            return exams;
        }

        public async Task DeleteExam(int examId)
        {
            await _examsRepo.DeleteExam(examId);
        }

        public async Task UpdateExam(string examName, int passScore, int ExamType, int courseId, string examNo, int examId)
        {
            await _examsRepo.UpdateExams(examName, passScore, ExamType, courseId, examNo, examId);
        }

    }
}
