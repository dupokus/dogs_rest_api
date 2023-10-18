namespace DogsRestApi.Model
{
    public interface IDogRepository
    {
        Task<Dog?> GetByIdAsync(DogId id);
        void Add(Dog dog);
        void Update(Dog dog);
        void Remove(Dog dog);
    }
}
