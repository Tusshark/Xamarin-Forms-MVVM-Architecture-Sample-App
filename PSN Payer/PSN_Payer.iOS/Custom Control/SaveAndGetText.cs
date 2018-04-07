using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using PSN_Payer.Helper;
using System.Threading.Tasks;
using System.IO;

namespace PSN_Payer.iOS.Custom_Control
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
            catch (Exception)
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
    }
}