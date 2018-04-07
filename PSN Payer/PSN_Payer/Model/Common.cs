using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSN_Payer.Model
{
    public class Common
    {
        public static string DeviceOS { get; set; }
        public static string DeviceSDK { get;set;}
        public static string Device { get; set; }
        public static string Brand { get; set; }
        public static string Hardware { get; set; }
        public static string Model { get; set; }
        public static string Serial { get; set; }
        public static double ScreenWidth { get; set; }
        public static double ScreenHeight { get; set; }
        public static string BalanceValue { get; set; }
        public static string AppVersion { get; set; }
        public static string GreetingMsg { get; set; }
        public static string BusinessName { get; set; }
        public static List<MenuListItem> MenuList { get; set; }
        public static bool IsNavigation { get; set; }


        public static string CalculatedSpaceForNav()
        {
            string space= "";
            try
            {
                double NoSpaces = (double)Common.ScreenWidth / 9;
                for (int i = 0; i <= Math.Round(NoSpaces); i++)
                {
                    space += " ";
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                //space = "      ";
            }
            return space;
        }

    }
}
