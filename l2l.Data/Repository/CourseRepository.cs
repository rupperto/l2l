using l2l.Data.Model;

namespace l2l.Data.Repository
{
    public class CourseRepository
    {
        private readonly L2lDBContext db;

        public CourseRepository()
        {
            var factory = new L2lDBContextFactory();
            db = factory.CreateDbContext(new string[] { });

        }


        public void Add(Course entity)
        {

            //throw new NotImplementedException();
            //TODO: async
            db.Courses.Add(entity);
        }

        public Course GetByID(int id)
        {

            //return new Course() { ID = 1, Name = "Test Course" };
            //throw new NotImplementedException();
            //TODO: async
            return db.Courses.Find(id);

        }
    }
}