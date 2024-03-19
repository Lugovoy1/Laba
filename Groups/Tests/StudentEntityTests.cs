using NUnit.Framework;
using Groups.Implementions;

namespace Groups.Implementations.Tests
{
    [TestFixture]
    public class StudentEntityTests
    {
        [Test]
        public void AddCourse_WhenCourseAdded_CourseShouldBeInList()
        {
            var student = new StudentEntity("John", 20);

            student.AddCourse("Mathematics");

            Assert.That(student.Courses, Contains.Item("Mathematics"));
        }

        [Test]
        public void RemoveCourse_WhenCourseRemoved_CourseShouldNotBeInList()
        {
            var student = new StudentEntity("John", 20);
            student.AddCourse("Mathematics");

            student.RemoveCourse("Mathematics");

            Assert.That(student.Courses, Does.Not.Contain("Mathematics"));
        }

        [Test]
        public void DisplayCourses_WhenCalled_ShouldWriteCoursesToConsole()
        {
            var student = new StudentEntity("John", 20);
            student.AddCourse("Mathematics");
            student.AddCourse("Physics");

            using (var consoleOutput = new ConsoleOutput())
            {
                student.DisplayCourses();
                string output = consoleOutput.GetOutput();

                Assert.That(output, Contains.Substring("Mathematics"));
                Assert.That(output, Contains.Substring("Physics"));
            }
        }
    }

    // Допоміжний клас для перехоплення виводу в консоль
    public class ConsoleOutput : System.IDisposable
    {
        private readonly System.IO.StringWriter stringWriter;
        private readonly System.IO.TextWriter originalOutput;

        public ConsoleOutput()
        {
            stringWriter = new System.IO.StringWriter();
            originalOutput = System.Console.Out;
            System.Console.SetOut(stringWriter);
        }

        public string GetOutput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            System.Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }
}
