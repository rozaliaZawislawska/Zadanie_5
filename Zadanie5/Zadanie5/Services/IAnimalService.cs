using Microsoft.AspNetCore.Mvc;

namespace Zadanie5;

public interface IAnimalService
{
    public IEnumerable<Animal> GetAllAnimals(string orderBy);
    public int CreateAnimal(Animal animal);
    public int UpdateAnimal(Animal animal);
    public int DeleteAnimal(int idAnimal);
    
}