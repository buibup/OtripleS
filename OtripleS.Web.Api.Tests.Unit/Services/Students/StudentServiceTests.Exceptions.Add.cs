﻿using Microsoft.EntityFrameworkCore;
using Moq;
using OtripleS.Web.Api.Models.Students;
using OtripleS.Web.Api.Models.Students.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OtripleS.Web.Api.Tests.Unit.Services.Students
{
    public partial class StudentServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRegisterWhenSqlExceptionOccursAndLogItAsync()
        {
            // given
            DateTimeOffset dateTime = GetRandomDateTime();
            Student randomStudent = CreateRandomStudent(dateTime);
            Student inputStudent = randomStudent;
            inputStudent.UpdatedBy = inputStudent.CreatedBy;
            var sqlException = GetSqlException();

            var expectedStudentDependencyException =
                new StudentDependencyException(sqlException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(dateTime);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertStudentAsync(inputStudent))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<Student> registerStudentByIdTask =
                this.studentService.RegisterStudentAsync(inputStudent);

            // then
            await Assert.ThrowsAsync<StudentDependencyException>(() =>
                registerStudentByIdTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(expectedStudentDependencyException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentAsync(inputStudent),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRegisterWhenDbExceptionOccursAndLogItAsync()
        {
            // given
            DateTimeOffset dateTime = GetRandomDateTime();
            Student randomStudent = CreateRandomStudent(dateTime);
            Student inputStudent = randomStudent;
            inputStudent.UpdatedBy = inputStudent.CreatedBy;
            var databaseUpdateException = new DbUpdateException();

            var expectedStudentDependencyException =
                new StudentDependencyException(databaseUpdateException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(dateTime);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertStudentAsync(inputStudent))
                    .ThrowsAsync(databaseUpdateException);

            // when
            ValueTask<Student> registerStudentByIdTask =
                this.studentService.RegisterStudentAsync(inputStudent);

            // then
            await Assert.ThrowsAsync<StudentDependencyException>(() =>
                registerStudentByIdTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedStudentDependencyException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentAsync(inputStudent),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRegisterWhenExceptionOccursAndLogItAsync()
        {
            // given
            DateTimeOffset dateTime = GetRandomDateTime();
            Student randomStudent = CreateRandomStudent(dateTime);
            Student inputStudent = randomStudent;
            inputStudent.UpdatedBy = inputStudent.CreatedBy;
            var exception = new Exception();

            var expectedStudentServiceException =
                new StudentServiceException(exception);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(dateTime);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertStudentAsync(inputStudent))
                    .ThrowsAsync(exception);

            // when
            ValueTask<Student> registerStudentByIdTask =
                 this.studentService.RegisterStudentAsync(inputStudent);

            // then
            await Assert.ThrowsAsync<StudentServiceException>(() =>
                registerStudentByIdTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedStudentServiceException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentAsync(inputStudent),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
