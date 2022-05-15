using Xunit;
using l2l.Data.Model;
using l2l.Data.Repository;
using FluentAssertions;

namespace l2l.Data.Tests
{
    /// <summary>
    /// CRUD and  List Test / adatbazis muveletek
    /// </summary>
    public class CourseRepositoryTests : IClassFixture<DatabaseFixture>
    {

        public DatabaseFixture fixture { get; }

        //construktor letrehozasa CTOR tab tab
        //classikus dependenci Injection
        public CourseRepositoryTests(DatabaseFixture fixture)
        {
            this.fixture = fixture
                ?? throw new System.ArgumentNullException(nameof(fixture)); // null check

        }


        [Fact]

        public void CourseRepositoryTests_AddedCoursesShouldBeAppearInRepository()
        {
            //Arrange - elokeszuletek
            // SUT : System Under Test
            var sut = new CourseRepository(fixture.GetNewL2lDBContext());
            var course = new Course { ID = 1, Name = "Test Course" };

            //Act tesztelendo muveletek
            sut.Add(course);
            var result = sut.GetByID(course.ID);

            //Asserts
            //Assert.NotNull(result); //resultra azt szeretnenk hogy ne legyen Null
            //Antipattern mert override oljuk
            //Assert.Equal(course, result); //Elvart es tenyleges ertek
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(course);


        }

        //ahhoz hogy tesztnek kelezelje kell ez az atttributum
        [Fact]

        public void CourseRepositoryTests_ExistingCoursesShouldBeAppearInRepository()
        {
            //Arrange - elokeszuletek
            // SUT : System Under Test
            var sut = new CourseRepository(fixture.GetNewL2lDBContext());
            var course = new Course { ID = 1, Name = "Test Course" };
            sut.Add(course);

            //Act

            var result = sut.GetByID(course.ID);

            //Asserts
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(course);

        }

        ///summmary
        //UPDATE TEST

        //ahhoz hogy tesztnek kelezelje kell ez az atttributum
        [Fact]

        public void CourseRepositoryTests_ExistingCoursesShouldBeChange()
        {
            //Arrange - elokeszuletek
            // SUT : System Under Test
            var sut = new CourseRepository(fixture.GetNewL2lDBContext());
            var course = new Course { ID = 1, Name = "Test Course" };
            sut.Add(course);

            //Act
            var toUpdate = sut.GetByID(course.ID);
            toUpdate.Name = "Modified Test Course";
            sut.Update(toUpdate);

            var afterUpdate = sut.GetByID(course.ID);

            //Asserts
            afterUpdate.Should().BeEquivalentTo(toUpdate);

        }

        ///summmary
        //DELETE TEST

        //ahhoz hogy tesztnek kelezelje kell ez az atttributum
        [Fact]
        public void CourseRepositoryTests_ExistingCoursesShouldBeDelete()
        {

            var sut = new CourseRepository(fixture.GetNewL2lDBContext());
            var course = new Course { ID = 1, Name = "Test Course" };
            sut.Add(course);


            //Act
            var toDelete = sut.GetByID(course.ID);
            sut.Remove(toDelete);
            var afterDelete = sut.GetByID(course.ID);
            //Asserts
            afterDelete.Should().BeNull();

        }
    }
}