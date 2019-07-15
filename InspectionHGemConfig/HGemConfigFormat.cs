using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionHGemConfig
{
    class HGemConfigFormat
    {
        Dictionary<string, string> list = Standard.getLibary();

        public HGemConfigFormat()
        {

        }

        public string outputEC(string content)
        {
            string[] array = content.Split(',');
            string outputString = string.Empty;
            if (array[0].IndexOf(';') >= 0)
            {
                outputString = ";;;;;;;;";
            }
            outputPrototype data = new outputPrototype();
            data.ID = array[1];
            data.NAME = array[2];
            data.TYPE = array[3];
            data.VALUE = combination4Output(array, 4);
            outputString += string.Format(list[Standard.key4EC].ToString(), data.ID, data.NAME, data.TYPE, data.VALUE);

            return outputString;
        }

        public string outputVid(string content)
        {
            string[] array = content.Split(',');
            string outputString = string.Empty;

            if (array[0].IndexOf(';') >=0)
            {
                outputString = ";;;;;;;;";
            }


            outputPrototype data = new outputPrototype();
            data.ID = array[1];
            data.NAME = array[2];
            data.TYPE = array[3];
            data.VALUE = array[4];
            outputString += string.Format(list[Standard.key4Vid].ToString(), data.ID, data.NAME, data.TYPE, data.VALUE);

            return outputString;
        }

        public string outputEvent(string content)
        {
            string[] array = content.Split(',');
            string outputString = string.Empty;
            if (array[0].IndexOf(';') >= 0)
            {
                outputString = ";;;;;;;;";
            }
            outputPrototype data = new outputPrototype();
            data.ID = array[1];
            data.NAME = array[2];
            data.VALUE = array[3];
            outputString += string.Format(list[Standard.key4Event].ToString(), data.ID, data.NAME, data.VALUE);

            return outputString;
        }

        public string outputReport(string content)
        {
            string[] array = content.Split(',');
            string outputString = string.Empty;
            if (array[0].IndexOf(';') >= 0)
            {
                outputString = ";;;;;;;;";
            }
            outputPrototype data = new outputPrototype();
            data.ID = array[1];
            data.NAME = array[2];
            data.VALUE = combination4Output(array, 3);
            outputString+= string.Format(list[Standard.key4Report].ToString(), data.ID, data.NAME, data.VALUE);

            return outputString;
        }

        public string outputReportLink(string content)
        {
            string[] array = content.Split(',');
            string outputString = string.Empty;
            if (array[0].IndexOf(';') >= 0)
            {
                outputString = ";;;;;;;;";
            }
            outputPrototype data = new outputPrototype();
            data.ID = array[1];
            data.VALUE = combination4Output(array, 2);
            outputString += string.Format(list[Standard.key4Link].ToString(), data.ID, data.VALUE);

            return outputString;
        }

        private string combination4Output(string[] stringLine, int startIndex)
        {
            string value = string.Empty;

            for (int i = startIndex; i < stringLine.Length; i++)
            {
                string tmp = stringLine[i].Trim();

                if (tmp != string.Empty)
                {
                    if (value != String.Empty)
                    {
                        value += ",";
                    }
                    value += tmp;
                }
            }

            return value;
        }
    }
}
