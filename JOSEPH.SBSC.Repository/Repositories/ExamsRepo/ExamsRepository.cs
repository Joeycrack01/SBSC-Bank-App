using JOSEPH.SBSC.Core.Models;
using JOSEPH.SBSC.Core.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JOSEPH.SBSC.Repository.Repositories.ExamsRepo
{
    public class ExamsRepository : IExamsRepository
    {
        private readonly DataContext _context;
        public ExamsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task CreateExams(Exam exam)
        {
            _context.Add(exam);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExamsQuestion>> GetExamQuestions(int examId, int courseId)
        {
            var examquestions = await _context.ExamsQuestions.Where(e => e.ID == examId && e.CourseId == courseId).ToListAsync();
            return examquestions;
        }
        
        public async Task AddExamQuestions(int examId, int courseId, string question, string answer, int marks, int userId)
        {
            ExamsQuestion examQuestions = new ExamsQuestion
            {
                CourseId = courseId,
                Answer = answer,
                ExamId = examId,
                Marks = marks,
                Question = question,
                CreatedBy = userId
            };
            _context.Add(examQuestions);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Exam>> GetCourseExams(int courseId)
        {
            var examquestions = await _context.Exams.Where(e => e.CourseID == courseId).ToListAsync();
            return examquestions;
        }

        public async Task<IEnumerable<Exam>> GetCoursePracticeExams(int courseId, int examType)
        {
            var practiceExam = await _context.Exams.Where(e => e.CourseID == courseId && e.ExamType == examType).ToListAsync();
            return practiceExam;
        }

        public async Task<Exam> GetExamById(int examId)
        {
            var practiceExam = await _context.Exams.Where(e => e.ID == examId).FirstOrDefaultAsync();
            return practiceExam;
        }

        public async Task DeleteExam(int examId)
        {
            var examquestion = await _context.ExamsQuestions.Where(eq => eq.ExamId == examId).ToListAsync();
            _context.RemoveRange(examquestion);

            var exam = await _context.Exams.Where(e => e.ID == examId).FirstOrDefaultAsync();
            _context.Remove(exam);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateExams(string examName, int passScore, int ExamType, int courseId, string examNo, int examId)
        {
            var exam = await _context.Exams.Where(e => e.ID == examId).FirstOrDefaultAsync();

            exam.CourseID = courseId;
            exam.ExamType = ExamType;
            exam.ExamName = examName;
            exam.PassScore = passScore;
            exam.ExamNo = examNo;

            _context.Update(exam);
            await _context.SaveChangesAsync();
        }




    }
}
