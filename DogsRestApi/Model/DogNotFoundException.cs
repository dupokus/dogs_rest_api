using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsRestApi.Model
{
    public sealed class DogNotFoundException : Exception
    {
        public DogNotFoundException(DogId id) 
            : base($"The dog with the ID = {id.Value} was not found")
        { 
        }
    }
}
