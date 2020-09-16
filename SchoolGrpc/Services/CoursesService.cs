using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using SchoolAPI.Models;
using SchoolGrpc.Protos;
using System.Linq;
using System.Threading.Tasks;
using static SchoolGrpc.Protos.Course;

namespace SchoolGrpc.Services
{
    public class CoursesService : CourseBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public override Task<CoursesReply> GetCourse(CourseRequest request, ServerCallContext context)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == request.Id);
            return Task.FromResult(new CoursesReply
            {
                Id = course.Id,
                CourseName = course.CourseName
            });
        }
        public override Task<CoursesList> GetCourses(Empty request, ServerCallContext context)
        {
            var coursesList = new CoursesList();
            var courses = _context.Courses.ToList();
            foreach (var item in courses)
            {
                coursesList.Courses.Add(new CoursesReply
                {
                    Id = item.Id,
                    CourseName = item.CourseName
                });
            }
            return Task.FromResult(coursesList);
        }
    
        public override Task<Empty> CreateCourses(CoursesCreate request, ServerCallContext context)
        {
            Courses course = new Courses()
            {
                Id = request.Id,
                CourseName = request.CourseName
            };
            _context.Courses.Add(course);
            _context.SaveChanges();
            return Task.FromResult(new Empty());
        }
    }
}
