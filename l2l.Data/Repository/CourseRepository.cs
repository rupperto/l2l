using l2l.Data.Model;

namespace l2l.Data.Repository
{
    public class CourseRepository
    {
        private readonly L2lDBContext db;

        public CourseRepository()
        {
            //TODO: Antipattern
            var factory = new L2lDBContextFactory();
            db = factory.CreateDbContext(new string[] { });

        }

        public CourseRepository(L2lDBContext db)
        {
            //dependenci Injection eseten mindig erdemes Null izsgalat
            this.db = db
                ?? throw new ArgumentNullException(nameof(db));
        }

        public void Add(Course course)
        {

            //throw new NotImplementedException();
            //TODO: async
            db.Courses.Add(course);
        }

        public Course GetByID(int Id)
        {

            //return new Course() { ID = 1, Name = "Test Course" };
            //throw new NotImplementedException();
            //TODO: async
            return db.Courses.Find(Id);

        }

        public void Update(Course course)
        {
            //TODO: return with void?
            db.Courses.Update(course);
        }

        public void Remove(Course course)
        {
            //TODO:Reurn
            db.Courses.Remove(course);
        }
    }
}