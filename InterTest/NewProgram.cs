using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;

namespace InterTest
{
    class NewProgram
    {
        public static void Main(string[] args)
        {
            Dictionary<string, List<string>> storage = new Dictionary<string, List<string>>();

            List<string> names = new List<string>();
            List<string> titles = new List<string>();

            List<string> dates = new List<string>();
            List<string> workHours = new List<string>();
            using (StreamReader reader = new StreamReader(@"E:\Programming\InterLink\acme_worksheet.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    names.Add(values[0]);
                    workHours.Add(values[2]);
                    dates.Add(values[1]);
                }
            };
            
            titles.Add(names[0]);
            titles.Add(dates[0]);
            names.RemoveAt(0);
            dates.RemoveAt(0);
            workHours.RemoveAt(0);



            for (int i = 0; i < workHours.Count; i++)
            {
                if (storage.Count > 0) {
                    if (storage.ContainsKey(names[i]))
                    {
                        storage[names[i]].Add(workHours[i]);
                    }
                    else
                    {
                        storage.Add(names[i], new List<string> { workHours[i] });
                    }
                }
                else
                {
                    storage.Add(names[i], new List<string> { workHours[i] });
                }
            }

            using (StreamWriter writer = new StreamWriter(@"E:\Programming\InterLink\InterLink.csv"))
            {
                string titleStr = "";

                for (int i = 0; i <= titles.Count - 2; i++) {

                    for (int j = 0; j < dates.Count; j++)
                        {
                            var cultureInfo = new CultureInfo("uk-UA");
                            var dateTime = DateTime.Parse(dates[j], cultureInfo);
                            dates[j] = dateTime.ToString("dd-MM-yyyy");

                        if (!titles.Contains(dates[j]))
                        {
                            titles.Add(dates[j]);
                        }
                    }
                    if (i + 1 > 1)
                    {
                        titleStr += "," + titles.ElementAt(i + 1);
                    }
                }
                writer.WriteLine(titles[0] + " / " + titles[1] + titleStr);

            
                Dictionary<string, List<string>>.KeyCollection keys = storage.Keys;
                Dictionary<string, List<string>>.ValueCollection vals = storage.Values;

                for (int i = 0; i < keys.Count; i++)
                {
                    string valStr = "";
                    
                    valStr += String.Join(',', vals.ElementAt(i));
                    
                    writer.WriteLine(keys.ElementAt(i) + "," + valStr);

                }

            };
        }
    }
}

           
        
            
        
