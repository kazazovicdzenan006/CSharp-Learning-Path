

using System.Text.Json;

public class StorageManager<T> where T : Uredjaj
{
    public StorageManager() { }

    public async Task spasiPodatke(string lokacija, List<T> lista)
    {
        string jsonTekst =  JsonSerializer.Serialize(lista);
        await File.WriteAllTextAsync(lokacija, jsonTekst);
    }


    public async Task<List<T>>   UcitajPodatke(string lokacija)
    {
        if (File.Exists(lokacija))
        {
             string tekst = await File.ReadAllTextAsync(lokacija);

            if (!string.IsNullOrWhiteSpace(tekst))
            {
                var jsonTekst =  JsonSerializer.Deserialize<List<T>>(tekst);

                return jsonTekst; 

            }
            else { return new List<T>(); }
        }
        else
        {
            return new List<T>();
        }
    }
}






