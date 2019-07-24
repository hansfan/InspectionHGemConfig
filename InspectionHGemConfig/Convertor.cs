using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionHGemConfig
{
    static class Standard
    {
        static public string key4EC = "EC";
        static public string key4Vid = "VID";
        static public string key4Event = "EVENT";
        static public string key4Report = "REPORT";
        static public string key4Link = "REPORTLINK";


        static public Dictionary<string, string> getLibary()
        {

            Dictionary<string, string> list = new Dictionary<string, string>();

            list.Add(key4EC, "EC=[ECID={0}, Name={1}, Type={2}, Value={3}]");
            list.Add(key4Vid, "Vid=[ID={0}, Name={1}, Type={2}, Class={3}]");
            list.Add(key4Event, "Event=[ID={0}, Name={1}, Enable={2}]");
            list.Add(key4Report, "Report=[ID={0}, Name={1}, Vids=[{2}]]");
            list.Add(key4Link, "ReportLink=[Event={0}, Reports=[{1}]]");

            return list;
        }

        static public bool IsNumeric(string value)
        {
            int i = 0;
            return Int32.TryParse(value, out i);
        }
    }
    class outputPrototype
    {
        private string category = string.Empty;
        private string id = string.Empty;
        private string name = string.Empty;
        private string type = string.Empty;
        private string value = string.Empty;

        public string CATEGORY
        {
            get { return this.category; }
            set { this.category = value; }
        }

        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string NAME
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string TYPE
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public string VALUE
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
    class Convertor
    {
        private List<string> headString = new List<string>();
        private List<string> vidString = new List<string>();
        private List<string> eventString = new List<string>();
        private List<string> reportString = new List<string>();
        private List<string> linkString = new List<string>();
        private List<string> alarmString = new List<string>();
        private List<string> ecString = new List<string>();
        

        public Convertor()
        {
            initial();
        }

        private void initial()
        {
            headString.Add("[GemDCConfig]");
            headString.Add("Settings=[DATAIDType=U4, VIDType=U4, CEIDType=U4, RPTIDType=U4, ECIDType=U4, ALIDType=U4]");

            ecString.Add("[EqConst]");
            vidString.Add("[Vids]");
            eventString.Add("[Events]");
            reportString.Add("[Reports]");
            linkString.Add("[ReportLinks]");
            alarmString.Add("[Alarms]");
            alarmString.Add("");
        }

        public List<List<string>> Process(List<string> list)
        {
            HGemConfigFormat outputStream = new HGemConfigFormat();
             List<List<string>> total = new List<List<string>>();
            try
            {
                foreach (string tmp in list)
                {
                    string[] arraySplit = tmp.Split(',');
                    string category = arraySplit[0];

                    if (category.ToUpper().IndexOf(Standard.key4EC) == 0)
                    {
                        ecString.Add(outputStream.outputEC(tmp));
                    }
                    else if (category.ToUpper().IndexOf(Standard.key4Vid) == 0)
                    {
                        vidString.Add(outputStream.outputVid(tmp));
                    }
                    else if (category.ToUpper().IndexOf(Standard.key4Event) == 0)
                    {
                        eventString.Add(outputStream.outputEvent(tmp));
                    }
                    else if (category.ToUpper().IndexOf(Standard.key4Link) == 0)
                    {
                        linkString.Add(outputStream.outputReportLink(tmp));

                    }
                    else if (category.ToUpper().IndexOf(Standard.key4Report) == 0)
                    {
                        reportString.Add(outputStream.outputReport(tmp));
                    }


                }
                total.Add(headString);
                total.Add(ecString);
                total.Add(vidString);
                total.Add(eventString);
                total.Add(reportString);
                total.Add(linkString);
                total.Add(alarmString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return total;
        }




    }
}
