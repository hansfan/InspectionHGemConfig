using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InspectionHGemConfig
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length != 1)
            {
                Console.WriteLine("please check args for input full path.");
            }
            else
            {
                string path = args[0].Trim();

                MainProcess(path);

            }
            Console.WriteLine("Success!");
            Console.ReadKey();
        }


        static void MainProcess(string path)
        {
            InputStream input = new InputStream(path);

            CheckHGemConfig check = new CheckHGemConfig();
            String result = check.Check(input.getContent());

            OutputStream output = new OutputStream();
            string outputFullPath = output.outputFullPath(path);
            output.toHGemConfig(outputFullPath, result);
        }
    }


    class InputStream
    {
        private string _path = string.Empty;

        public InputStream(string path)
        {
            this._path = path;
        }

        public List<string> getContent()
        {
            List<string> list = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(this._path))
                {

                    while (reader.Peek() >= 0)
                    {
                        string tmp = reader.ReadLine();

                        list.Add(tmp);


                    }

                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Please check file path.");
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return list;
        }

    }
    class OutputStream
    {
        public OutputStream()
        {

        }

        public void toHGemConfig(string fullPath, string obj)
        {
            try
            {
                using (StreamWriter output = new StreamWriter(fullPath))
                {
                    output.WriteLine(obj);
                }

            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        public string outputFullPath(string input)
        {
            string fileName = string.Empty;

            try
            {
                fileName = input + ".tool";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return fileName;
        }
    }
}
