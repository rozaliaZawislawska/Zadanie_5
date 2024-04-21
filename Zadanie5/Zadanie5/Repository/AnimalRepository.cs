using System.Data.SqlClient;

namespace Zadanie5;

public class AnimalRepository: IAnimalRepository
{
    private readonly IConfiguration _configuration;
    public IEnumerable<Animal> FetchAllAnimals(string orderBy)
    {
        var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        var safeOrderBy = new string[] { "name", "description", "category", "area" }.Contains(orderBy)
            ? orderBy
            : "name";
        using var command = new SqlCommand($"SELECT IdAnimal, Name FROM Animal ORDER BY "+safeOrderBy, connection);
        using var reader = command.ExecuteReader();

        var animals = new List<Animal>();
        while (reader.Read())
        {
            var animal = new Animal()
            {
                IdAnimal = (int)reader["IdAnimal"],
                Name = reader["Name"].ToString()!,
                Description = reader["description"].ToString()!,
                Category = reader["category"].ToString()!,
                Area = reader["Area"].ToString()!
            };
            animals.Add(animal);
        }

        return animals;
    }

    public int CreateAnimal(Animal animal)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        
        using var command = new SqlCommand("INSERT INTO Animal (Name, Description, CATEGORY, AREA) VALUES (@Name, @Description, @CATEGORY, @AREA)", connection);
        command.Parameters.AddWithValue("@Name", animal.Name);
        command.Parameters.AddWithValue("@Description", animal.Description);
        command.Parameters.AddWithValue("@Category", animal.Category);
        command.Parameters.AddWithValue("@Area", animal.Area);
        
        var affectedCount = command.ExecuteNonQuery();
        return affectedCount;
    }

    public int UpdateAnimal(Animal animal)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        
        using var command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "UPDATE Animal SET Name=@Name, Description=@Description, Category=@Category, Area=@Area WHERE IdAnimal = @IdAnimal";
        command.Parameters.AddWithValue("@Name", animal.Name);
        command.Parameters.AddWithValue("@Description", animal.Description);
        command.Parameters.AddWithValue("@Category", animal.Category);
        command.Parameters.AddWithValue("@Area", animal.Area);
        command.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
        
        var affectedCount = command.ExecuteNonQuery();
        return affectedCount;
    }
    
    public int DeleteAnimal(int IdAnimal)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        
        using var command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "DELETE Animal WHERE IdAnimal = @IdAnimal";
        command.Parameters.AddWithValue("@IdAnimal", IdAnimal);
        
        var affectedCount = command.ExecuteNonQuery();
        return affectedCount;
    }
}