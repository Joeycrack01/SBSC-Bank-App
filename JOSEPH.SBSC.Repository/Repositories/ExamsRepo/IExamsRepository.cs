using JOSEPH.SBSC.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JOSEPH.SBSC.Repository.Repositories.ExamsRepo
{
    public interface IExamsRepository
    {
        Task CreateExams(Exam exam);
        Task DeleteExam(int examId);
        Task<IEnumerable<Exam>> GetCourseExams(int courseId);
        Task<IEnumerable<Exam>> GetCoursePracticeExams(int courseId, int examType);
        Task<Exam> GetExamById(int examId);
        Task<IEnumerable<ExamsQuestion>> GetExamQuestions(int examId, int courseId);
        Task UpdateExams(string examName, int passScore, int ExamType, int courseId, string examNo, int examId);
        Task AddExamQuestions(int examId, int courseId, string question, string answer, int marks, int userId);
    }
}