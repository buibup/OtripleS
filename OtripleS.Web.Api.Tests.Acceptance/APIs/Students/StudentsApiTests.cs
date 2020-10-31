using OtripleS.Web.Api.Models.Students;
using OtripleS.Web.Api.Tests.Acceptance.Brokers;
using System;
using System.Collections.Generic;
using System.Text;
using Tynamix.ObjectFiller;
using Xunit;

namespace OtripleS.Web.Api.Tests.Acceptance.APIs.Students
{
    [Collection(nameof(ApiTestCollection))]
    public partial class StudentsApiTests
    {
        private readonly OtripleSApiBroker otripleSApiBroker;

        public StudentsApiTests(OtripleSApiBroker otripleSApiBroker)
        {
            this.otripleSApiBroker = otripleSApiBroker;
        }

        private Student CreateRandomStudent()
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            Guid posterId = Guid.NewGuid();

            var filler = new Filler<Student>();

            filler.Setup()
                .OnProperty(student => student.CreatedBy).Use(posterId)
                .OnProperty(student => student.UpdatedBy).Use(posterId)
                .OnProperty(student => student.CreatedDate).Use(now)
                .OnProperty(student => student.UpdatedDate).Use(now)
                .OnType<DateTimeOffset>().Use(GetRandomDateTime());

            return filler.Create();
        }

        private Student UpdateStudentRandom(Student student)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;

            var filler = new Filler<Student>();

            filler.Setup()
                .OnProperty(student => student.Id).Use(student.Id)
                .OnProperty(student => student.CreatedBy).Use(student.CreatedBy)
                .OnProperty(student => student.UpdatedBy).Use(student.UpdatedBy)
                .OnProperty(student => student.CreatedDate).Use(student.CreatedDate)
                .OnProperty(student => student.UpdatedDate).Use(now)
                .OnType<DateTimeOffset>().Use(GetRandomDateTime());

            return filler.Create();
        }

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();
    }
}
