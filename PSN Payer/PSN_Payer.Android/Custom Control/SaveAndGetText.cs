using System;
using System.Threading.Tasks;
using PSN_Payer.Helper;
using Xamarin.Forms;
using PSN_Payer.Droid.Custom_Control;
using System.IO;

[assembly: Dependency(typeof(SaveAndGetText))]
namespace PSN_Payer.Droid.Custom_Control
{
    public class SaveAndGetText : IRemember
    {
        public async Task<string> GetTextAsync(string filename)
        {
            try
            {
                string textData;
                var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var path = Path.Combine(docsPath, filename);
                using (StreamReader sr = File.OpenText(path))
                {
                    textData = await sr.ReadToEndAsync();
                }
                return textData;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task SaveTextAsync(string filename, string text)
        {
            try
            {
                var docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var path = Path.Combine(docPath, filename);
                using (StreamWriter sw = File.CreateText(path))
                {
                    await sw.WriteAsync(text);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GetFilePath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filePath = Path.Combine(path, "ServerResponse.txt");
             return filePath;
        }
    }
}