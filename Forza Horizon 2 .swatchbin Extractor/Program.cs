using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using XGXTYPES;
using static System.Runtime.InteropServices.JavaScript.JSType;
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
                        Console.WriteLine("Opening file...");
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
            var globalClass = new GlobalClass();
            try
            {
                using var fs = File.OpenRead(file);
                globalClass.Load(fs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load file! {ex.Message}");
            }

            //var txcBlob = globalClass.GetBlobById<TextureContentBlob>(GlobalClass.TAG_BLOB_TextureContentBlob, 0);
            //var txchMetadata = globalClass.GetMetadataByTag<TextureContentHeaderMetadata>(GlobalClass.TAG_METADATA_TextureContentHeader);

            //ProcessSWATCHBIN(file, txcBlob, txchMetadata);
        }

        private static void ProcessSWATCHBIN(string file, TextureContentBlob txcBlob, TextureContentHeaderMetadata txchMetadata)
        {
            var Data = txchMetadata.GetContents();
            TextureContentHeader hdr = new TextureContentHeader();
            hdr.Read(Data);

            XG_FORMAT format = hdr.DetermineFormat();

            var textureContentHeaderMetadata = new TextureContentHeaderMetadata();
            byte[] data = textureContentHeaderMetadata.GetContents();
            XG_FORMAT Format = hdr.DetermineFormat();

            if (hdr.TileMode == XG_TILE_MODE.XG_TILE_MODE_2D_THIN)
            {
                //data = Detile(hdr);
            }
        }

        public static void Detile(TextureContentHeader hdr)
        {
            XG_FORMAT format = hdr.DetermineFormat();

            XG_TEXTURE2D_DESC desc;
            desc.Width = hdr.Width;
            desc.Height = hdr.Height;
            desc.Format = format;
            desc.Usage = XG_USAGE.XG_USAGE_DEFAULT;
            desc.SampleDesc.Count = 1;
            desc.ArraySize = hdr.Depth_NumSlice;
            desc.MipLevels = hdr.MipLevels;
            desc.BindFlags = (uint)XG_BIND_FLAG.XG_BIND_SHADER_RESOURCE;
            desc.MiscFlags = 0;
            desc.TileMode = hdr.TileMode;

            XGTextureAddressComputer* compWrapper;

            int result = XGXImports.XGCreateTexture2DComputer(&desc, &compWrapper);

            XGTextureAddressComputer computer = *compWrapper;
            int size = Marshal.SizeOf<XG_RESOURCE_LAYOUT>();
            nint arrPtr = Marshal.AllocHGlobal(size);

            result = computer.vt->GetResourceLayout(compWrapper, (XG_RESOURCE_LAYOUT*)arrPtr);

            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine((ex.Message));
            }
        }

        public static void Deswizzle(byte[] output, ushort WIDTH, ushort HEIGHT, XG_FORMAT FORMAT, XG_TILE_MODE TILEMODE, byte[] pTiledData, int rowPitchBytes)
        {
            int wElements = WIDTH / 4;
            int hElements = HEIGHT / 4;

            int xTiles = (wElements + 7) / 8;
            int yTiles = (hElements + 7) / 8;

            int bytesPerElement = 0x10;

            for (int yTile = 0; yTile < yTiles; yTile++)
            {
                int startY = yTile * 8;
                int endY = yTile * 8 + 8;
                if (endY > hElements)
                    endY = hElements;

                int destLinearRowOffset = rowPitchBytes * startY;

                for (int xTile = 0; xTile < xTiles; xTile++)
                {
                    int startX = xTile * 8;
                    int endX = xTile * 8 + 8;
                    if (endX > wElements)
                        endX = wElements;

                    Span<byte> startTileBytes = output.AsSpan(destLinearRowOffset + bytesPerElement * 8 * xTile);
                    for (int y = startY; y < endY; y++)
                    {
                        var outputRow = startTileBytes;
                        for (int x = startX; x < endX; x++)
                        {
                            outputRow = outputRow[bytesPerElement..];
                        }

                        startTileBytes = startTileBytes[rowPitchBytes..];
                    }
                }
            }
        }
    }
}