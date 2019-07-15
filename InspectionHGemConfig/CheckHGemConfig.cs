using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace InspectionHGemConfig
{
    class CheckHGemConfig
    {
        List<string> vidList = new List<string>();
        List<string> eventList = new List<string>();
        List<string> reportList = new List<string>();
        private string pattern = @"^\[(?<item>[\w]+)\]$";
        //Regex regex = new Regex ("^\[(?<item>[\w]+)\]$");

        public CheckHGemConfig()
        {

        }

        public string Check(List<string> obj)
        {
            Demolition demolition = new Demolition();
            outputPrototype data = new outputPrototype();
            string tmp = string.Empty;
            StringBuilder result = new StringBuilder();
            try
            {

                for (int index = 0; index <= obj.Count - 1; index++)
                {
                    tmp = obj[index].Trim();

                    Match match = Regex.Match(tmp, pattern);
                    if (!match.Success)
                    {
                        data = demolition.HGemConfig(tmp);

                        switch (data.CATEGORY.ToLower())
                        {
                            case "vid":
                                if (!vidList.Contains(data.ID))
                                {
                                    vidList.Add(data.ID);
                                    
                                }
                                else
                                {
                                    tmp = ";;;;;;;;" + tmp;
                                }
                                result.AppendLine(tmp);
                                break;
                            case "event":
                                if (!eventList.Contains(data.ID))
                                {
                                    eventList.Add(data.ID);                                  
                                }
                                else
                                {
                                    tmp = ";;;;;;;;" + tmp;
                                }
                                result.AppendLine(tmp);
                                break;
                            case "report":
                                string[] vidValues = data.VALUE.Split(',');
                                if (!reportList.Contains(data.ID))
                                {
                                    reportList.Add(data.ID);
                                }
                                foreach (string value in vidValues)
                                {

                                    if (!vidList.Contains(value))
                                    {
                                        tmp = ";;;;;;;;" + tmp;
                                        break;
                                    }
                                }
                                result.AppendLine(tmp);

                                break;
                            case "reportlink":
                                string[] linkValue = data.VALUE.Split(',');
                                foreach (string value in linkValue)
                                {

                                    if (!reportList.Contains(value))
                                    {
                                        tmp = ";;;;;;;;" + tmp;
                                        break;
                                    }
                                }
                                result.AppendLine(tmp);
                                break;
                            default:
                                result.AppendLine(tmp);
                                break;
                        }
                    }
                    else
                    {
                        result.AppendLine(tmp);
                    }

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            return result.ToString();
        }

    }

    class Demolition
    {
        public Demolition()
        {

        }

        public outputPrototype HGemConfig(string lineString)
        {
            outputPrototype result = new outputPrototype();
            int equal = -1;
            string category = string.Empty;
            string propertyString = string.Empty;

            try
            {
                if (lineString != string.Empty)
                {
                    equal = lineString.IndexOf('=');
                    category = lineString.Substring(0, equal).ToUpper();
                    propertyString = lineString.Substring(equal + 1).Replace("[", "").Replace("]", "");
                    if (category == Standard.key4Vid)
                    {
                        result = vidFormat(propertyString);
                    }
                    else if (category == Standard.key4Event)
                    {
                        result = eventFormat(propertyString);
                    }
                    else if (category == Standard.key4Report)
                    {
                        result = reportFormat(propertyString);
                    }
                    else if (category == Standard.key4Link)
                    {
                        result = linkFormat(propertyString);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        private outputPrototype linkFormat(string propertyString)
        {
            outputPrototype data = new outputPrototype();
            string[] arr = Regex.Split(propertyString, "Reports=");

            string[] id = arr[0].Split('=');
            data.ID = id[1];

            data.VALUE = arr[1];


            return data;
        }

        private outputPrototype reportFormat(string propertyString)
        {
            outputPrototype data = new outputPrototype();
            string[] arr = Regex.Split(propertyString, "Vids=");
            string[] arr2 = arr[0].Split(',');

            string[] id = arr2[0].Split('=');
            data.ID = id[1];

            string[] name = arr2[1].Split('=');
            data.NAME = name[1];

            data.VALUE = arr[1];
            data.CATEGORY = "Report";

            return data;
        }

        private outputPrototype vidFormat(string propertyString)
        {
            //ID=34000, Name=CtrlJobID, Type=ASCII, Class=DV
            outputPrototype data = new outputPrototype();

            string[] arr = propertyString.Split(',');

            if (arr.Length == 4)
            {
                string[] id = arr[0].Split('=');
                data.ID = id[1];

                string[] name = arr[1].Split('=');
                data.NAME = name[1];

                string[] type = arr[2].Split('=');
                data.TYPE = type[1];

                string[] level = arr[3].Split('=');
                data.VALUE = level[1];

            }
            data.CATEGORY = "VID";

            return data;
        }

        private outputPrototype eventFormat(string propertyString)
        {
            outputPrototype data = new outputPrototype();

            string[] arr = propertyString.Split(',');
            if (arr.Length == 3)
            {
                string[] id = arr[0].Split('=');
                data.ID = id[1];

                string[] name = arr[1].Split('=');
                data.NAME = name[1];

                string[] enalbe = arr[2].Split('=');
                data.VALUE = enalbe[1];

            }
            data.CATEGORY = "EVENT";

            return data;
        }


    }

}
