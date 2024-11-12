using Syroot.BinaryData;
using XGXTYPES;
using static System.Reflection.Metadata.BlobBuilder;

public class GlobalClass
{
    public const uint TAG_METADATA_Name = 0x4E616D65; 
    public const uint TAG_METADATA_TextureContentHeader = 0x54584348; 
    public const uint TAG_METADATA_Identifier = 0x49642020; 
    public const uint TAG_METADATA_BBox = 0x42426F78;
    public List<GlobalClass> Blobs { get; set; } = new List<GlobalClass>();

    public const uint TAG_BLOB_TextureContentBlob = 0x54584342;

    public const uint Tag = 0x47727562;
    public byte VersionMajor { get; set; }
    public byte VersionMinor { get; set; }
    public void Load(Stream stream)
    {
        long baseBundleOffset = stream.Position;

        var BinaryStream = new BinaryStream(stream);
        uint tag = BinaryStream.ReadUInt32();

        VersionMajor = BinaryStream.Read1Byte();
        VersionMinor = BinaryStream.Read1Byte();

        uint blobCount;
        if (VersionMinor >= 1)
        {
            BinaryStream.ReadInt16();
            uint headerSize = BinaryStream.ReadUInt32();
            uint totalSize = BinaryStream.ReadUInt32();
            blobCount = BinaryStream.ReadUInt32();
        }
        else
        {
            blobCount = BinaryStream.ReadUInt16();
            BinaryStream.Position += 0x08;
        }
    }
}

public class TextureContentHeader
{
    public Guid Guid { get; set; }
    public ushort Width { get; set; }
    public ushort Height { get; set; }
    public ushort Depth_NumSlice { get; set; }
    public ushort Width2 { get; set; }
    public ushort Height2 { get; set; }
    public ushort UnkMip1 { get; set; }
    public byte UnkMip2 { get; set; }
    public byte MipLevels { get; set; }
    public byte BaseMipLevel { get; set; }
    public uint RawBitFlags { get; set; }

    private byte[] _data { get; set; }

    public byte[] GetContents() => _data;

    public XG_TILE_MODE TileMode
    {
        get => (XG_TILE_MODE)(RawBitFlags & 0b11111);
        set => RawBitFlags |= (byte)((byte)value & 0b11111);
    }

    public byte Encoding
    {
        get => (byte)(RawBitFlags >> 5 & 0b111111);
        set => RawBitFlags |= (byte)((value & 0b111111) << 5);
    }

    public byte Transcoding
    {
        get => (byte)(RawBitFlags >> 11 & 0b111111);
        set => RawBitFlags |= (byte)((value & 0b111111) << 11);
    }

    public byte UnkBits1
    {
        get => (byte)(RawBitFlags >> 17 & 0b111);
        set => RawBitFlags |= (byte)((value & 0b111) << 17);
    }

    public byte ColorProfile
    {
        get => (byte)(RawBitFlags >> 20 & 0b111);
        set => RawBitFlags |= (byte)((value & 0b111) << 20);
    }

    public bool Flag1
    {
        get => (RawBitFlags >> 25 & 1) == 1;
        set => RawBitFlags |= (byte)((value ? 1 : 0) << 25);
    }

    public bool Flag2
    {
        get => (RawBitFlags >> 26 & 1) == 1;
        set => RawBitFlags |= (byte)((value ? 1 : 0) << 26);
    }

    public bool Flag3
    {
        get => (RawBitFlags >> 27 & 1) == 1;
        set => RawBitFlags |= (byte)((value ? 1 : 0) << 27);
    }

    public byte PitchOrLinearSize
    {
        get => (byte)(RawBitFlags >> 28 & 0b1111);
        set => RawBitFlags |= (byte)((value & 0b1111) << 28);
    }

    public void Read(byte[] data)
    {
        using BinaryStream bs = new BinaryStream(new MemoryStream(data));
        Guid = new Guid(bs.ReadBytes(0x10));
        Width = bs.ReadUInt16();
        Height = bs.ReadUInt16();
        Depth_NumSlice = bs.ReadUInt16();
        Width2 = bs.ReadUInt16();
        Height2 = bs.ReadUInt16();
        UnkMip1 = bs.ReadUInt16();
        UnkMip2 = bs.Read1Byte();
        MipLevels = bs.Read1Byte();
        BaseMipLevel = bs.Read1Byte();
        RawBitFlags = bs.ReadUInt32();
    }

    public XG_FORMAT DetermineFormat()
    {
        if (Transcoding <= 1)
            return (XG_FORMAT)(ColorProfile == 0 ? encodingToDxgiFormats[Encoding].format : encodingToDxgiFormats[Encoding].formatSrgb);
        else
            return (XG_FORMAT)(ColorProfile == 0 ? transcodingToDxgiFormats[Transcoding].format : transcodingToDxgiFormats[Transcoding].formatSrgb);
    }

    public record DxgiFormatEntry(DXGI_FORMAT format, DXGI_FORMAT formatSrgb, byte encodeValue);
    public List<DxgiFormatEntry> encodingToDxgiFormats = new()
    {
        new(DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM, DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM_SRGB, 1),
        new(DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM, DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM_SRGB, 2),
        new(DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM, DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM_SRGB, 3),
        new(DXGI_FORMAT.DXGI_FORMAT_BC4_UNORM, DXGI_FORMAT.DXGI_FORMAT_BC4_UNORM, 4),
        new(DXGI_FORMAT.DXGI_FORMAT_BC4_SNORM, DXGI_FORMAT.DXGI_FORMAT_BC4_SNORM, 5),
        new(DXGI_FORMAT.DXGI_FORMAT_BC5_UNORM, DXGI_FORMAT.DXGI_FORMAT_BC5_UNORM, 6),
        new(DXGI_FORMAT.DXGI_FORMAT_BC5_SNORM, DXGI_FORMAT.DXGI_FORMAT_BC5_SNORM, 7),
        new(DXGI_FORMAT.DXGI_FORMAT_BC6H_UF16, 0, 8),
        new(DXGI_FORMAT.DXGI_FORMAT_BC6H_SF16, 0, 9),
        new(DXGI_FORMAT.DXGI_FORMAT_BC7_UNORM, DXGI_FORMAT.DXGI_FORMAT_BC7_UNORM_SRGB, 10),
        new(DXGI_FORMAT.DXGI_FORMAT_R32G32B32A32_FLOAT, 0, 11),
        new(DXGI_FORMAT.DXGI_FORMAT_R16G16B16A16_UNORM, 0, 12),
        new(DXGI_FORMAT.DXGI_FORMAT_R16G16B16A16_FLOAT, 0, 13),
        new(DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM, DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM_SRGB, 14),
        new(DXGI_FORMAT.DXGI_FORMAT_B5G6R5_UNORM, 0, 15),
        new(DXGI_FORMAT.DXGI_FORMAT_B5G5R5A1_UNORM, 0, 16),
        new(0, 0, 17),
        new(0, 0, 18),
        new(0, 0, 19),
        new(DXGI_FORMAT.DXGI_FORMAT_R8_UNORM, 0, 20),
        new(DXGI_FORMAT.DXGI_FORMAT_A8_UNORM, 0, 0),
    };

    public List<DxgiFormatEntry> transcodingToDxgiFormats = new()
    {
        new(0, 0, 1),
        new(0, 0, 2),
        new(DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM, DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM_SRGB, 3),
        new(DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM, DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM_SRGB, 4),
        new(DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM, DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM_SRGB, 5),
        new(DXGI_FORMAT.DXGI_FORMAT_BC4_UNORM, DXGI_FORMAT.DXGI_FORMAT_BC4_UNORM, 6),
        new(DXGI_FORMAT.DXGI_FORMAT_BC4_SNORM, DXGI_FORMAT.DXGI_FORMAT_BC4_SNORM, 7),
        new(DXGI_FORMAT.DXGI_FORMAT_BC5_UNORM, DXGI_FORMAT.DXGI_FORMAT_BC5_UNORM, 8),
        new(DXGI_FORMAT.DXGI_FORMAT_BC5_SNORM, DXGI_FORMAT.DXGI_FORMAT_BC5_SNORM, 9),
        new(DXGI_FORMAT.DXGI_FORMAT_BC6H_UF16, 0, 10),
        new(DXGI_FORMAT.DXGI_FORMAT_BC6H_SF16, 0, 11),
        new(DXGI_FORMAT.DXGI_FORMAT_BC7_UNORM, DXGI_FORMAT.DXGI_FORMAT_BC7_UNORM_SRGB, 0),
    };
}

public enum DXGI_FORMAT : int
{
    DXGI_FORMAT_UNKNOWN = 0,
    DXGI_FORMAT_R32G32B32A32_TYPELESS = 1,
    DXGI_FORMAT_R32G32B32A32_FLOAT = 2,
    DXGI_FORMAT_R32G32B32A32_UINT = 3,
    DXGI_FORMAT_R32G32B32A32_SINT = 4,
    DXGI_FORMAT_R32G32B32_TYPELESS = 5,
    DXGI_FORMAT_R32G32B32_FLOAT = 6,
    DXGI_FORMAT_R32G32B32_UINT = 7,
    DXGI_FORMAT_R32G32B32_SINT = 8,
    DXGI_FORMAT_R16G16B16A16_TYPELESS = 9,
    DXGI_FORMAT_R16G16B16A16_FLOAT = 10,
    DXGI_FORMAT_R16G16B16A16_UNORM = 11,
    DXGI_FORMAT_R16G16B16A16_UINT = 12,
    DXGI_FORMAT_R16G16B16A16_SNORM = 13,
    DXGI_FORMAT_R16G16B16A16_SINT = 14,
    DXGI_FORMAT_R32G32_TYPELESS = 15,
    DXGI_FORMAT_R32G32_FLOAT = 16,
    DXGI_FORMAT_R32G32_UINT = 17,
    DXGI_FORMAT_R32G32_SINT = 18,
    DXGI_FORMAT_R32G8X24_TYPELESS = 19,
    DXGI_FORMAT_D32_FLOAT_S8X24_UINT = 20,
    DXGI_FORMAT_R32_FLOAT_X8X24_TYPELESS = 21,
    DXGI_FORMAT_X32_TYPELESS_G8X24_UINT = 22,
    DXGI_FORMAT_R10G10B10A2_TYPELESS = 23,
    DXGI_FORMAT_R10G10B10A2_UNORM = 24,
    DXGI_FORMAT_R10G10B10A2_UINT = 25,
    DXGI_FORMAT_R11G11B10_FLOAT = 26,
    DXGI_FORMAT_R8G8B8A8_TYPELESS = 27,
    DXGI_FORMAT_R8G8B8A8_UNORM = 28,
    DXGI_FORMAT_R8G8B8A8_UNORM_SRGB = 29,
    DXGI_FORMAT_R8G8B8A8_UINT = 30,
    DXGI_FORMAT_R8G8B8A8_SNORM = 31,
    DXGI_FORMAT_R8G8B8A8_SINT = 32,
    DXGI_FORMAT_R16G16_TYPELESS = 33,
    DXGI_FORMAT_R16G16_FLOAT = 34,
    DXGI_FORMAT_R16G16_UNORM = 35,
    DXGI_FORMAT_R16G16_UINT = 36,
    DXGI_FORMAT_R16G16_SNORM = 37,
    DXGI_FORMAT_R16G16_SINT = 38,
    DXGI_FORMAT_R32_TYPELESS = 39,
    DXGI_FORMAT_D32_FLOAT = 40,
    DXGI_FORMAT_R32_FLOAT = 41,
    DXGI_FORMAT_R32_UINT = 42,
    DXGI_FORMAT_R32_SINT = 43,
    DXGI_FORMAT_R24G8_TYPELESS = 44,
    DXGI_FORMAT_D24_UNORM_S8_UINT = 45,
    DXGI_FORMAT_R24_UNORM_X8_TYPELESS = 46,
    DXGI_FORMAT_X24_TYPELESS_G8_UINT = 47,
    DXGI_FORMAT_R8G8_TYPELESS = 48,
    DXGI_FORMAT_R8G8_UNORM = 49,
    DXGI_FORMAT_R8G8_UINT = 50,
    DXGI_FORMAT_R8G8_SNORM = 51,
    DXGI_FORMAT_R8G8_SINT = 52,
    DXGI_FORMAT_R16_TYPELESS = 53,
    DXGI_FORMAT_R16_FLOAT = 54,
    DXGI_FORMAT_D16_UNORM = 55,
    DXGI_FORMAT_R16_UNORM = 56,
    DXGI_FORMAT_R16_UINT = 57,
    DXGI_FORMAT_R16_SNORM = 58,
    DXGI_FORMAT_R16_SINT = 59,
    DXGI_FORMAT_R8_TYPELESS = 60,
    DXGI_FORMAT_R8_UNORM = 61,
    DXGI_FORMAT_R8_UINT = 62,
    DXGI_FORMAT_R8_SNORM = 63,
    DXGI_FORMAT_R8_SINT = 64,
    DXGI_FORMAT_A8_UNORM = 65,
    DXGI_FORMAT_R1_UNORM = 66,
    DXGI_FORMAT_R9G9B9E5_SHAREDEXP = 67,
    DXGI_FORMAT_R8G8_B8G8_UNORM = 68,
    DXGI_FORMAT_G8R8_G8B8_UNORM = 69,
    DXGI_FORMAT_BC1_TYPELESS = 70,
    DXGI_FORMAT_BC1_UNORM = 71,
    DXGI_FORMAT_BC1_UNORM_SRGB = 72,
    DXGI_FORMAT_BC2_TYPELESS = 73,
    DXGI_FORMAT_BC2_UNORM = 74,
    DXGI_FORMAT_BC2_UNORM_SRGB = 75,
    DXGI_FORMAT_BC3_TYPELESS = 76,
    DXGI_FORMAT_BC3_UNORM = 77,
    DXGI_FORMAT_BC3_UNORM_SRGB = 78,
    DXGI_FORMAT_BC4_TYPELESS = 79,
    DXGI_FORMAT_BC4_UNORM = 80,
    DXGI_FORMAT_BC4_SNORM = 81,
    DXGI_FORMAT_BC5_TYPELESS = 82,
    DXGI_FORMAT_BC5_UNORM = 83,
    DXGI_FORMAT_BC5_SNORM = 84,
    DXGI_FORMAT_B5G6R5_UNORM = 85,
    DXGI_FORMAT_B5G5R5A1_UNORM = 86,
    DXGI_FORMAT_B8G8R8A8_UNORM = 87,
    DXGI_FORMAT_B8G8R8X8_UNORM = 88,
    DXGI_FORMAT_R10G10B10_XR_BIAS_A2_UNORM = 89,
    DXGI_FORMAT_B8G8R8A8_TYPELESS = 90,
    DXGI_FORMAT_B8G8R8A8_UNORM_SRGB = 91,
    DXGI_FORMAT_B8G8R8X8_TYPELESS = 92,
    DXGI_FORMAT_B8G8R8X8_UNORM_SRGB = 93,
    DXGI_FORMAT_BC6H_TYPELESS = 94,
    DXGI_FORMAT_BC6H_UF16 = 95,
    DXGI_FORMAT_BC6H_SF16 = 96,
    DXGI_FORMAT_BC7_TYPELESS = 97,
    DXGI_FORMAT_BC7_UNORM = 98,
    DXGI_FORMAT_BC7_UNORM_SRGB = 99,
    DXGI_FORMAT_AYUV = 100,
    DXGI_FORMAT_Y410 = 101,
    DXGI_FORMAT_Y416 = 102,
    DXGI_FORMAT_NV12 = 103,
    DXGI_FORMAT_P010 = 104,
    DXGI_FORMAT_P016 = 105,
    DXGI_FORMAT_420_OPAQUE = 106,
    DXGI_FORMAT_YUY2 = 107,
    DXGI_FORMAT_Y210 = 108,
    DXGI_FORMAT_Y216 = 109,
    DXGI_FORMAT_NV11 = 110,
    DXGI_FORMAT_AI44 = 111,
    DXGI_FORMAT_IA44 = 112,
    DXGI_FORMAT_P8 = 113,
    DXGI_FORMAT_A8P8 = 114,
    DXGI_FORMAT_B4G4R4A4_UNORM = 115,
    DXGI_FORMAT_P208 = 130,
    DXGI_FORMAT_V208 = 131,
    DXGI_FORMAT_V408 = 132,
    DXGI_FORMAT_SAMPLER_FEEDBACK_MIN_MIP_OPAQUE,
    DXGI_FORMAT_SAMPLER_FEEDBACK_MIP_REGION_USED_OPAQUE,
    DXGI_FORMAT_FORCE_UINT = -1
}

public class TextureContentHeaderMetadata
{
    private byte[] _data { get; set; }
    public byte[] GetContents() => _data;
    public void ReadMetadataData(BinaryStream bs)
    {

    }

    public void SerializeMetadataData(BinaryStream bs)
    {

    }
}

public class TextureContentBlob
{
    public void ReadBlobData(BinaryStream bs)
    {

    }

    public void SerializeBlobData(BinaryStream bs)
    {

    }
}

