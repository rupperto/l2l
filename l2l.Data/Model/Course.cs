namespace l2l.Data.Model
{
    public class Course : IEquatable<Course>
    {

        public int ID { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Course); //obj tipusbol cours tipust csinalunk   

        }

        public bool Equals(Course course)
        {
            if (null == course)
            {
                return false;

            }
            if (ID != course.ID || Name != course.Name)
            {
                return false;

            }

            return true;

        }

        public override int GetHashCode()
        {
            //gethascode implementacio, ha van tobb propertynk akkor szep sorban hozzarakhatjuk a tobbit is, de esetelg int32-nél mar tulcsordulas okozhat.
            unchecked   // tulcsordulas eseten vedelmet nyujt
            {
                var hash = 27;
                hash = (hash * 13) + ID.GetHashCode();
                hash = (hash * 13) + Name.GetHashCode();
                return hash;

            }



        }
    }


}