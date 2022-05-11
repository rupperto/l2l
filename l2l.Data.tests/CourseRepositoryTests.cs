using System;
using Xunit;
using l2l.Data.Model;
using l2l.Data.Repository;
using FluentAssertions;

namespace l2l.data.tests
{
    /// <summary>
    /// CRUD and  List Test / adatbazis muveletek
    /// </summary>
    public class CourseRepositoryTests
    {

        [Fact]

        public void CourseRepositoryTests_AddedCoursesShouldBeAppearInRepository()
        {
            //Arrange - elokeszuletek
            // SUT : System Under Test
            var sut = new CourseRepository();
            var course = new Course { ID = 1, Name = "Test Course" };

            //Act tesztelendo muveletek
            sut.Add(course);
            var result = sut.GetByID(course.ID);

            //Asserts
            Assert.NotNull(result); //resultra azt szeretnenk hogy ne legyen Null
            //Antipattern mert override oljuk
            //Assert.Equal(course, result); //Elvart es tenyleges ertek
            result.Should().BeEquivalentTo(course);


        }
    }
}