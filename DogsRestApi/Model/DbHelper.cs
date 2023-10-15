using DogsRestApi.Data;

namespace DogsRestApi.Model
{
    public class DbHelper
    {
        private DogDbContext _context;
        public DbHelper(DogDbContext context) 
        {
            _context = context;
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <returns></returns>
        public List<DogModel> GetDogs() 
        {
            List<DogModel> response = new List<DogModel>();
            var datalist = _context.Dogs.ToList();
            datalist.ForEach(row => response.Add(new DogModel()
            {
                Id = row.Id,
                Name = row.Name,
                Color = row.Color,
                TailLength = row.TailLength,
                Weight = row.Weight,
            }));
            return response; 
        }
        public DogModel GetDogById(int Id)
        {
            DogModel response = new DogModel();
            var row = _context.Dogs.Where(d=>d.Id.Equals(Id)).FirstOrDefault();
            return new DogModel()
            {
                Id = row.Id,
                Name = row.Name,
                Color = row.Color,
                TailLength = row.TailLength,
                Weight = row.Weight,
            };
        }
        /// <summary>
        /// POST/PATCH/PUT
        /// </summary>
        public void SaveDog(DogModel dogModel)  
        {
            Dog dbTable = new Dog();
            if (dogModel.Id > 0) 
            {
                dbTable = _context.Dogs.Where(d => d.Id.Equals(dogModel.Id)).FirstOrDefault();
                if (dbTable != null) 
                {
                    dbTable.Name = dogModel.Name;
                    dbTable.Color = dogModel.Color; 
                    dbTable.TailLength = dogModel.TailLength;
                    dbTable.Weight = dogModel.Weight;
                }
            }
            else
            {
                dbTable.Name = dogModel.Name;
                dbTable.Color = dogModel.Color;
                dbTable.TailLength = dogModel.TailLength;
                dbTable.Weight = dogModel.Weight;
                _context.Dogs.Add(dbTable);
            }
            _context.SaveChanges();
        }
        /// <summary>
        /// DELETE
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteDog(int Id)
        {
            var dog = _context.Dogs.Where(d => d.Id.Equals(Id)).FirstOrDefault();
            if (dog != null)
            {
                _context.Dogs.Remove(dog);
                _context.SaveChanges();
            }
        }
    }
}
