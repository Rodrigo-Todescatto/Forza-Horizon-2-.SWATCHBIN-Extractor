using System;
using System.Reflection.Metadata;
using XGXTYPES;
using static XGXImports;

namespace FH2SW
{
    public unsafe class Program
    {
        public const string Version = "Test 0.0.1";
        static void Main(string[] args)
        {
            Console.WriteLine("***************************************************");
            Console.WriteLine($"  Forza Horizon 2 .SWATCHBIN Extractor by Rod :D");
            Console.WriteLine("***************************************************");
            Console.WriteLine($"***************(Version: {Version})***************");
            Console.WriteLine("***************************************************");

            if (args.Length < 1)
            {
                Console.WriteLine("Usage: Drag a .SWATCHBIN file");
                return;
            }

            if (Directory.Exists(args[0]))
            {
                foreach (var file in Directory.GetFiles(args[0], "*.swatchbin", SearchOption.AllDirectories))
                {
                    try
                    {
                        ConvertSWATCHBINFILE(file);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine((ex.Message));
                    }
                }
            }
            else
            {
                ConvertSWATCHBINFILE(args[0]);
            }
        }

        private static void ConvertSWATCHBINFILE(string file)
        {
            try
            {
                using var fs = File.OpenRead(file);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load file! {ex.Message}");
            }
        }
    }
}