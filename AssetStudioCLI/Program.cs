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
        private const int SHOW_PROGRESS_INTERVAL = 64;
        static readonly string SAMPLE_DIR = $"{Environment.CurrentDirectory}/../../../sample";
        static readonly string SAMPLE_FILE = SAMPLE_DIR + "/-1685865614.unity3d";
        static readonly object CONSOLE_LOCK = new object();

        //static void Main(string[] args)
        //{
        //    var files = Directory.GetFiles(@"C:\Users\huser\Desktop\ABHack\assets");
        //    var outputs = new List<string>[files.Length];
        //    var progress = 0;
        //    Parallel.For(0, files.Length, new ParallelOptions() { MaxDegreeOfParallelism = 64 },i =>
        //    {
        //        outputs[i] = Dump(files[i]);

        //        if (i % SHOW_PROGRESS_INTERVAL == 0)
        //        {
        //            lock (CONSOLE_LOCK)
        //            {
        //                progress += SHOW_PROGRESS_INTERVAL;
        //                Console.CursorLeft = 0;
        //                Console.Write($"{progress}/{files.Length}");
        //            }
        //        }
        //    });
        //    Console.CursorLeft = 0;

        //    for (int i = 0; i < files.Length; i++)
        //    {
        //        if (outputs[i].Any(s => s.Contains("10615004")))
        //        {
        //            Console.WriteLine(files[i]);
        //        }
        //    }

        //    Console.ReadKey();
        //}

        static void Main(string[] args)
        {
            //Dump(@"C:\Users\huser\Desktop\ABHack\assets\elenoa\111085745.unity3d");
            Dump(@"C:\Users\huser\Desktop\ABHack\AssetStudio\uc_111085745.unity3d");
        }

        static List<string> Dump(string filePath)
        {
            if (filePath.StartsWith("http"))
            {
                var task = Download(filePath);
                task.Wait();
                filePath = task.Result;
            }

            AssetsManager manager = new AssetsManager();
            manager.LoadFiles(filePath);

            var output = new List<string>();
            foreach (var asset in manager.assetsFileList)
            {
                foreach (var objKV in asset.Objects)
                {
                    if (objKV.Value is NamedObject namedObject)
                    {
                        long size = namedObject.byteSize;
                        switch (namedObject)
                        {
                            case Texture2D m_Texture2D:
                            {
                                size += m_Texture2D.m_StreamData?.size ?? 0;
                                break;
                            }
                            case AudioClip m_AudioClip:
                            {
                                size += m_AudioClip.m_Size;
                                break;
                            }
                            case VideoClip m_VideoClip:
                            {
                                size += (long)m_VideoClip.m_Size;
                                break;
                            }
                        }
                        output.Add($"{namedObject.m_Name} {namedObject.GetType()} {size / 1024f:0.#}kB");
                    }
                }
            }
            return output;
        }

        static async Task<string> Download(string url)
        {
            using (var cli = new HttpClient())
            {
                var res = await cli.GetAsync(url);
                var buf = await res.Content.ReadAsByteArrayAsync();
                var tmpPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.InternetCache)}/tmp_asset_{url.GetHashCode().ToString("D8")}";
                File.WriteAllBytes(tmpPath, buf);
                return tmpPath;
            }
        }
    }
}
