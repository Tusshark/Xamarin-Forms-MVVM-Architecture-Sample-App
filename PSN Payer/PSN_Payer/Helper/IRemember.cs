using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSN_Payer.Helper
{
    public interface IRemember
    {
        Task SaveTextAsync(string filename, string text);
        Task<string> GetTextAsync(string filename);
        string GetFilePath();
    }
}
