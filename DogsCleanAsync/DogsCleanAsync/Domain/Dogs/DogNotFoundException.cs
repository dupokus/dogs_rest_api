using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dogs
{
    public sealed class DogNotFoundException : Exception
    {
        public DogNotFoundException(DogId id) 
            : base($"The dog with the ID = {id.Value} was not found")
        { 
        }
    }
}
