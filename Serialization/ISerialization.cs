using SerializerLab.Classes;
using System.Collections.Generic;

namespace SerializerLab.Serialization
{
    public interface ISerialization
    {
        string Serialize(List<Animal> listOfPets);   
        List<Animal> Deserialize(string str);
    }
}