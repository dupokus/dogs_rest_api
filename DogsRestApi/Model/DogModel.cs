namespace DogsRestApi.Model
{
    public class DogModel
    {
        public DogModel(int id, string name, string color, int tailLength, int weight)
        {
            Id = id;
            Name = name;
            Color = color;
            TailLength = tailLength;
            Weight = weight;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int TailLength { get; set; }
        public int Weight { get; set; }
    }
}
