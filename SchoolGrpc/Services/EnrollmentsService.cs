using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using SchoolAPI.Models;
using SchoolGrpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SchoolGrpc.Protos.Enrollments;

namespace SchoolGrpc.Services
{
    public class EnrollmentsService : EnrollmentsBase
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public override Task<EnrollmentReply> GetEnrollment(RequestId request, ServerCallContext context)
        {
            var enrollment = _context.Enrollments.FirstOrDefault(e => e.Id == request.Id);
            return Task.FromResult(new EnrollmentReply { Id = enrollment.Id, CourseId = enrollment.CourseId, StudentId = enrollment.StudentId });
        }
        public override Task<EnrollmentsList> GetEnrollments(Empty request, ServerCallContext context)
        {
            var enrollmentsList = new EnrollmentsList();
            var enrollments = _context.Enrollments.ToArray();
            foreach (var item in enrollments)
            {
                enrollmentsList.Reply.Add(new EnrollmentReply { Id = item.Id, CourseId = item.CourseId, StudentId = item.StudentId });
            }
            return Task.FromResult(enrollmentsList);
        }
        public override Task<Empty> CreateEnrollment(CreateRequest request, ServerCallContext context)
        {
            _context.Enrollments.Add(new SchoolAPI.Models.Enrollments {Id=request.Id, CourseId=request.CourseId, StudentId=request.StudentId });
            _context.SaveChanges();
            return Task.FromResult(new Empty());
        }
        public override Task<Empty> DeleteEnrollment(RequestId request, ServerCallContext context)
        {
            var enrollmentInDb = _context.Enrollments.FirstOrDefault(e => e.Id == request.Id);
            _context.Enrollments.Remove(enrollmentInDb);
            _context.SaveChanges();
            return Task.FromResult(new Empty());
        }
    }
}
