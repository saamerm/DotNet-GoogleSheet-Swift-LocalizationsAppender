// See https://aka.ms/new-console-template for more information
// Get File names
using System.IO;
using Newtonsoft.Json;
using System.Text;
using SwiftLocalizationsAppender;
using System.Linq;

var filePath = "/Users/.../Localizations/"
var googleUrl = "https://script.google.com/macros/s/.../exec"
var x = new List<Task>();
var numberOfLanguages = 25;
for (int lang = 1; lang <= numberOfLanguages; lang++)
{
    // x.Add(MakeAPICall(lang));
    x.Add(MakeInfoPlistChangeStep2(lang));
    // x.Add(MakeCFBundleDisplayNameAPICall(lang));
}
await Task.WhenAll(x);

async Task MakeAPICall(int lang)
{
    var url = googleUrl;
    var firstRow = 168;
    var lastRow = 168;
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
        string strPath = filePath + translation.LanguageFile + "/Localizable.strings";

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

async Task MakeInfoPlistChangeStep1(int lang)
{
    var url = googleUrl;
    var firstRow = 171;
    var lastRow = 171;
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
        string strPath = filePath + translation.LanguageFile + "/InfoPlist.strings";
        var linesToKeep = File.ReadLines(strPath).ToList();
        linesToKeep.RemoveAt(2);
        File.WriteAllLines(strPath, linesToKeep.ToArray());
        StreamWriter writer = new StreamWriter(strPath, true);
        writer.WriteLine(translation.Line);
        writer.Flush();
        writer.Close();
    }
}

async Task MakeInfoPlistChangeStep2(int lang)
{
    var url = googleUrl;
    var firstRow = 172;
    var lastRow = 172;
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
        string strPath = filePath + translation.LanguageFile + "/InfoPlist.strings";
        var linesToKeep = File.ReadLines(strPath).ToList();
        linesToKeep.RemoveAt(2);
        File.WriteAllLines(strPath, linesToKeep.ToArray());
        StreamWriter writer = new StreamWriter(strPath, true);
        writer.WriteLine(translation.Line);
        writer.Flush();
        writer.Close();
    }
}