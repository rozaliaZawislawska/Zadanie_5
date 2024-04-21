namespace Zadanie5;

public interface IAnimalRepository
{
        public IEnumerable<Animal> FetchAllAnimals(string orderBy);
        public int CreateAnimal(Animal animal);

        public int UpdateAnimal(Animal animal);
        public int DeleteAnimal(int IdAnimal);
}