namespace Lesson07
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Vector Position { get; set; }

        public City(int x, int y, string name , int id)
        {
            Position = new Vector(new double[]{x, y});
            Name = name;
            Id = id;
        }
    }
}
