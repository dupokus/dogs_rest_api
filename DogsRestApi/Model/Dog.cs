namespace DogsRestApi.Model
{
    public class Dog
    {
        public Dog(DogId id, string name, string color, int tailLenght, int weight)
        {
            Id = id;
            Name = name;
            Color = color;
            TailLength = tailLenght;
            Weight = weight;
        }
        public DogId Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Color { get; private set; } = string.Empty;
        public int TailLength { get; private set; }
        public int Weight { get; private set; }
        public void Update(string name, string color, int tailLength, int weight)
        {
            Name = name;
            Color = color;
            TailLength = tailLength;
            Weight = weight;
        }
    }
}
