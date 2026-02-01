


using System.Text.Json;

public class CityManager<T> where T : CityNode
{
    public async Task SaveData(List<T> list, string location)
    {
        string jsonText = JsonSerializer.Serialize(list);
        await File.WriteAllTextAsync(location, jsonText);
    }


    public async Task<List<T>> LoadData(string location)
    {
        if (File.Exists(location))
        {
            string text = await File.ReadAllTextAsync(location);
            if (!string.IsNullOrEmpty(text))
            {
                var jsonText = JsonSerializer.Deserialize<List<T>>(text);
                return jsonText;
            }
            else return new List<T>();
        }
        else return new List<T>();

    }

}



