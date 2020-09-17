using JOSEPH.SBSC.ApplicationService.ViewModels;
using JOSEPH.SBSC.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JOSEPH.SBSC.ApplicationService.Services.ExamServices
{
    public interface IExamAppService
    {
        Task CreateExam(CreateExamsViewModel createExams);
        Task CreateExamQuestion(int examId, int courseId, string question, string answer, int marks, int userID);
        Task DeleteExam(int examId);
        Task<IEnumerable<Exam>> GetCourseExamsByCourseID(int courseId);
        Task<IEnumerable<Exam>> GetCoursePracticeExamsByCourseID(int courseId, int examType);
        Task<Exam> GetExamsById(int examId);
        Task UpdateExam(string examName, int passScore, int ExamType, int courseId, string examNo, int examId);
    }
}