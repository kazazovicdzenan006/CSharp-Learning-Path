


using System.Text.Json;
using System.Text.Json.Serialization;

 public class StorageManager<T> where T : BibliotekaArtikal
{
  async  public Task SpasiPodatke(string lokacija, List<T> lista)
    {
        var jsonTekst = JsonSerializer.Serialize(lista);
        await File.WriteAllTextAsync(lokacija, jsonTekst); 
    }

    public async Task<List<T>> UcitajPodatke(string lokacija)
    {
        if (File.Exists(lokacija))
        {
           var tekst = await File.ReadAllTextAsync(lokacija);

            if (!string.IsNullOrWhiteSpace(tekst))
            {
                var jsonTekst =  JsonSerializer.Deserialize<List<T>>(tekst);
                return jsonTekst;
            }
            else
            {
                return new List<T>();
            }
            
        }
        else
        {
            return new List<T>();
        }
    }


}








