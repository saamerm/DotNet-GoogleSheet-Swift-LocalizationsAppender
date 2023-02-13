// See https://aka.ms/new-console-template for more information
// Get File names
using System.IO;
using Newtonsoft.Json;
using System.Text;
using SwiftLocalizationsAppender;

var x = new List<Task>();
var numberOfLanguages = 25;
for (int lang = 1; lang <= numberOfLanguages; lang++)
{
    x.Add(MakeAPICall(lang));
}
await Task.WhenAll(x);

async Task MakeAPICall(int lang)
{
    var url = "https://script.google.com/macros/s/YourUrl/exec";
    var firstRow = 137;
    var lastRow = 137;
    for (int line = firstRow; line <= lastRow; line++)
    {
        var data = new TranslationRequest() { Language = lang, Line = line };
        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var response = await client.PostAsync(url, content);
        var result = await response.Content.ReadAsStringAsync();
        TranslationResponse translation = new TranslationResponse();
        try
        {
            translation = JsonConvert.DeserializeObject<TranslationResponse>(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        if (translation == null)
            return;
        string strPath = "/Users/YourPath/BeAware/Localizations/" + translation.LanguageFile + "/Localizable.strings";

        StreamWriter writer = new StreamWriter(strPath, true);
        writer.WriteLine(translation.Line);
        writer.Flush();
        writer.Close();

        string[] lines = System.IO.File.ReadAllLines(strPath);
        Console.WriteLine(lines.LastOrDefault());

        // Uncomment to view all lines
        //string text = System.IO.File.ReadAllText(strPath);
        //Console.WriteLine(text);
    }
}