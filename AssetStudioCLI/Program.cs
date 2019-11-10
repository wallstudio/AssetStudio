using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AssetStudio;

namespace AssetStudioCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            //args = new string[] { @"C:\Users\huser\Desktop\TC\ABs\texab" };
            MainAsync(args).Wait();
            Console.ReadKey();
        }

        static async Task MainAsync(string[] args)
        {
            var path = args[0];
            if (path.StartsWith("http"))
            {
                using (var cli = new HttpClient())
                {
                    var res = await cli.GetAsync(path);
                    var buf = await res.Content.ReadAsByteArrayAsync();
                    File.WriteAllBytes("tmp", buf);
                    path = "tmp";
                }
            }

            AssetsManager manager = new AssetsManager();
            manager.LoadFiles(path);

            foreach (var asset in manager.assetsFileList)
            {
                foreach (var objKV in asset.Objects)
                {
                    Console.WriteLine($"\t{objKV.Value.GetType()}");
                    foreach (var field in objKV.Value.GetType().GetFields())
                    {
                        Console.WriteLine($"\t\t{field.Name} = {field.GetValue(objKV.Value)}");
                    }
                }
            }
        }
    }
}
