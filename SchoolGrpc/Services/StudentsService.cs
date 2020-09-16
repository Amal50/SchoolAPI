using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using SchoolAPI.Models;
using SchoolGrpc.Protos;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolGrpc.Services
{
    public class StudentsService : Student.StudentBase
    {
        private readonly ApplicationDbContext _context;

        public StudentsService()
        {
            _context = new ApplicationDbContext();
        }
        public override Task<Empty> CreateStudent(StudentCreate request, ServerCallContext context)
        {
            var student = new Students
            {
                Id = request.Id,
                StudentName = request.StudentName
            };
            _context.Students.Add(student);
            _context.SaveChanges();
            return Task.FromResult<Empty>(null);
        }
        public override Task<StudentsReply> GetStudents(Empty request, ServerCallContext context)
        {
            var students = _context.Students.ToList();
            var studentsReply = new StudentsReply();
            foreach (var item in students)
            {
                studentsReply.Student.Add(new StudentReply { Id = item.Id, StudentName = item.StudentName });
            }
            return Task.FromResult(studentsReply);
        }

        public override Task<StudentReply> GetStudentInfo(StudentsRequest request, ServerCallContext context)
        {
            var student = _context.Students.FirstOrDefault(s=>s.Id== request.Id);
            return Task.FromResult(new StudentReply { Id = student.Id, StudentName= student.StudentName});
        }
    }
}
