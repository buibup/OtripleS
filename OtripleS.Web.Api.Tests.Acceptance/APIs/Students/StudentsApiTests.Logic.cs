﻿using FluentAssertions;
using OtripleS.Web.Api.Models.Students;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OtripleS.Web.Api.Tests.Acceptance.APIs.Students
{
    public partial class StudentsApiTests
    {
        [Fact]
        public async Task ShouldPostStudentAsync()
        {
            // given
            Student randomStudent = CreateRandomStudent();
            Student inputStudent = randomStudent;
            Student expectedStudent = inputStudent;

            // when 
            await this.otripleSApiBroker.PostStudentAsync(inputStudent);

            Student actualStudent =
                await this.otripleSApiBroker.GetStudentByIdAsync(inputStudent.Id);

            // then
            actualStudent.Should().BeEquivalentTo(expectedStudent);

            await this.otripleSApiBroker.DeleteStudentByIdAsync(actualStudent.Id);
        }

        [Fact]
        public async Task ShouldPutStudentAsync()
        {
            // given
            Student randomStudent = CreateRandomStudent();
            await this.otripleSApiBroker.PostStudentAsync(randomStudent);
            Student modifiedStudent = UpdateStudentRandom(randomStudent);

            // when
            await this.otripleSApiBroker.PutStudentAsync(modifiedStudent);

            Student actualStudent =
                await this.otripleSApiBroker.GetStudentByIdAsync(randomStudent.Id);

            // then
            actualStudent.Should().BeEquivalentTo(modifiedStudent);

            await this.otripleSApiBroker.DeleteStudentByIdAsync(actualStudent.Id);
        }
    }
}
