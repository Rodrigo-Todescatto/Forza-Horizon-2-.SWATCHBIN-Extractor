﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace XGXTYPES
{
    //***************************************ENUMS***************************************//
    public enum XG_USAGE
    {
        XG_USAGE_DEFAULT = 0,
        XG_USAGE_IMMUTABLE = 1,
        XG_USAGE_DYNAMIC = 2,
        XG_USAGE_STAGING = 3
    }

    public enum XG_BIND_FLAG
    {
        XG_BIND_VERTEX_BUFFER = 0x1,
        XG_BIND_INDEX_BUFFER = 0x2,
        XG_BIND_CONSTANT_BUFFER = 0x4,
        XG_BIND_SHADER_RESOURCE = 0x8,
        XG_BIND_STREAM_OUTPUT = 0x10,
        XG_BIND_RENDER_TARGET = 0x20,
        XG_BIND_DEPTH_STENCIL = 0x40,
        XG_BIND_UNORDERED_ACCESS = 0x80,
        XG_BIND_DECODER = 0x200,
        XG_BIND_VIDEO_ENCODER = 0x400
    }

    public enum XG_PLANE_USAGE
    {
        XG_PLANE_USAGE_UNUSED = 0,
        XG_PLANE_USAGE_DEFAULT = 1,
        XG_PLANE_USAGE_COLOR_MASK = 2,
        XG_PLANE_USAGE_FRAGMENT_MASK = 3,
        XG_PLANE_USAGE_HTILE = 4,
        XG_PLANE_USAGE_LUMA = 5,
        XG_PLANE_USAGE_CHROMA = 6,
        XG_PLANE_USAGE_DEPTH = 7,
        XG_PLANE_USAGE_STENCIL = 8,
        XG_PLANE_USAGE_DELTA_COLOR_COMPRESSION = 9,
    }
    public enum XG_TILE_MODE
    {
        XG_TILE_MODE_INVALID = -1,
        XG_TILE_MODE_COMP_DEPTH_0 = 0,
        XG_TILE_MODE_COMP_DEPTH_1 = 1,
        XG_TILE_MODE_COMP_DEPTH_2 = 2,
        XG_TILE_MODE_COMP_DEPTH_3 = 3,
        XG_TILE_MODE_COMP_DEPTH_4 = 4,
        XG_TILE_MODE_UNC_DEPTH_5 = 5,
        XG_TILE_MODE_UNC_DEPTH_6 = 6,
        XG_TILE_MODE_UNC_DEPTH_7 = 7,
        XG_TILE_MODE_LINEAR = 8,
        XG_TILE_MODE_DISPLAY = 9,
        XG_TILE_MODE_2D_DISPLAY = 10,
        XG_TILE_MODE_TILED_DISPLAY = 11,
        XG_TILE_MODE_TILED_2D_DISPLAY = 12,
        XG_TILE_MODE_1D_THIN = 13,
        XG_TILE_MODE_2D_THIN = 14,
        XG_TILE_MODE_3D_THIN = 15,
        XG_TILE_MODE_TILED_1D_THIN = 16,
        XG_TILE_MODE_TILED_2D_THIN = 17,
        XG_TILE_MODE_TILED_3D_THIN = 18,
        XG_TILE_MODE_1D_THICK = 19,
        XG_TILE_MODE_2D_THICK = 20,
        XG_TILE_MODE_3D_THICK = 21,
        XG_TILE_MODE_TILED_1D_THICK = 22,
        XG_TILE_MODE_TILED_2D_THICK = 23,
        XG_TILE_MODE_TILED_3D_THICK = 24,
        XG_TILE_MODE_2D_XTHICK = 25,
        XG_TILE_MODE_3D_XTHICK = 26,
        XG_TILE_MODE_RESERVED_27 = 27,
        XG_TILE_MODE_RESERVED_28 = 28,
        XG_TILE_MODE_RESERVED_29 = 29,
        XG_TILE_MODE_RESERVED_30 = 30,
        XG_TILE_MODE_LINEAR_GENERAL = 31,
        XG_TILE_MODE_TILED_2D_DEPTH = XG_TILE_MODE_UNC_DEPTH_7,
    }

    public enum XG_RESOURCE_DIMENSION
    {
        XG_RESOURCE_DIMENSION_UNKNOWN = 0,
        XG_RESOURCE_DIMENSION_BUFFER = 1,
        XG_RESOURCE_DIMENSION_TEXTURE1D = 2,
        XG_RESOURCE_DIMENSION_TEXTURE2D = 3,
        XG_RESOURCE_DIMENSION_TEXTURE3D = 4
    }

    public enum XG_FORMAT
    {
        XG_FORMAT_UNKNOWN = 0,
        XG_FORMAT_R32G32B32A32_TYPELESS = 1,
        XG_FORMAT_R32G32B32A32_FLOAT = 2,
        XG_FORMAT_R32G32B32A32_UINT = 3,
        XG_FORMAT_R32G32B32A32_SINT = 4,
        XG_FORMAT_R32G32B32_TYPELESS = 5,
        XG_FORMAT_R32G32B32_FLOAT = 6,
        XG_FORMAT_R32G32B32_UINT = 7,
        XG_FORMAT_R32G32B32_SINT = 8,
        XG_FORMAT_R16G16B16A16_TYPELESS = 9,
        XG_FORMAT_R16G16B16A16_FLOAT = 10,
        XG_FORMAT_R16G16B16A16_UNORM = 11,
        XG_FORMAT_R16G16B16A16_UINT = 12,
        XG_FORMAT_R16G16B16A16_SNORM = 13,
        XG_FORMAT_R16G16B16A16_SINT = 14,
        XG_FORMAT_R32G32_TYPELESS = 15,
        XG_FORMAT_R32G32_FLOAT = 16,
        XG_FORMAT_R32G32_UINT = 17,
        XG_FORMAT_R32G32_SINT = 18,
        XG_FORMAT_R32G8X24_TYPELESS = 19,
        XG_FORMAT_D32_FLOAT_S8X24_UINT = 20,
        XG_FORMAT_R32_FLOAT_X8X24_TYPELESS = 21,
        XG_FORMAT_X32_TYPELESS_G8X24_UINT = 22,
        XG_FORMAT_R10G10B10A2_TYPELESS = 23,
        XG_FORMAT_R10G10B10A2_UNORM = 24,
        XG_FORMAT_R10G10B10A2_UINT = 25,
        XG_FORMAT_R11G11B10_FLOAT = 26,
        XG_FORMAT_R8G8B8A8_TYPELESS = 27,
        XG_FORMAT_R8G8B8A8_UNORM = 28,
        XG_FORMAT_R8G8B8A8_UNORM_SRGB = 29,
        XG_FORMAT_R8G8B8A8_UINT = 30,
        XG_FORMAT_R8G8B8A8_SNORM = 31,
        XG_FORMAT_R8G8B8A8_SINT = 32,
        XG_FORMAT_R16G16_TYPELESS = 33,
        XG_FORMAT_R16G16_FLOAT = 34,
        XG_FORMAT_R16G16_UNORM = 35,
        XG_FORMAT_R16G16_UINT = 36,
        XG_FORMAT_R16G16_SNORM = 37,
        XG_FORMAT_R16G16_SINT = 38,
        XG_FORMAT_R32_TYPELESS = 39,
        XG_FORMAT_D32_FLOAT = 40,
        XG_FORMAT_R32_FLOAT = 41,
        XG_FORMAT_R32_UINT = 42,
        XG_FORMAT_R32_SINT = 43,
        XG_FORMAT_R24G8_TYPELESS = 44,
        XG_FORMAT_D24_UNORM_S8_UINT = 45,
        XG_FORMAT_R24_UNORM_X8_TYPELESS = 46,
        XG_FORMAT_X24_TYPELESS_G8_UINT = 47,
        XG_FORMAT_R8G8_TYPELESS = 48,
        XG_FORMAT_R8G8_UNORM = 49,
        XG_FORMAT_R8G8_UINT = 50,
        XG_FORMAT_R8G8_SNORM = 51,
        XG_FORMAT_R8G8_SINT = 52,
        XG_FORMAT_R16_TYPELESS = 53,
        XG_FORMAT_R16_FLOAT = 54,
        XG_FORMAT_D16_UNORM = 55,
        XG_FORMAT_R16_UNORM = 56,
        XG_FORMAT_R16_UINT = 57,
        XG_FORMAT_R16_SNORM = 58,
        XG_FORMAT_R16_SINT = 59,
        XG_FORMAT_R8_TYPELESS = 60,
        XG_FORMAT_R8_UNORM = 61,
        XG_FORMAT_R8_UINT = 62,
        XG_FORMAT_R8_SNORM = 63,
        XG_FORMAT_R8_SINT = 64,
        XG_FORMAT_A8_UNORM = 65,
        XG_FORMAT_R1_UNORM = 66,
        XG_FORMAT_R9G9B9E5_SHAREDEXP = 67,
        XG_FORMAT_R8G8_B8G8_UNORM = 68,
        XG_FORMAT_G8R8_G8B8_UNORM = 69,
        XG_FORMAT_BC1_TYPELESS = 70,
        XG_FORMAT_BC1_UNORM = 71,
        XG_FORMAT_BC1_UNORM_SRGB = 72,
        XG_FORMAT_BC2_TYPELESS = 73,
        XG_FORMAT_BC2_UNORM = 74,
        XG_FORMAT_BC2_UNORM_SRGB = 75,
        XG_FORMAT_BC3_TYPELESS = 76,
        XG_FORMAT_BC3_UNORM = 77,
        XG_FORMAT_BC3_UNORM_SRGB = 78,
        XG_FORMAT_BC4_TYPELESS = 79,
        XG_FORMAT_BC4_UNORM = 80,
        XG_FORMAT_BC4_SNORM = 81,
        XG_FORMAT_BC5_TYPELESS = 82,
        XG_FORMAT_BC5_UNORM = 83,
        XG_FORMAT_BC5_SNORM = 84,
        XG_FORMAT_B5G6R5_UNORM = 85,
        XG_FORMAT_B5G5R5A1_UNORM = 86,
        XG_FORMAT_B8G8R8A8_UNORM = 87,
        XG_FORMAT_B8G8R8X8_UNORM = 88,
        XG_FORMAT_R10G10B10_XR_BIAS_A2_UNORM = 89,
        XG_FORMAT_B8G8R8A8_TYPELESS = 90,
        XG_FORMAT_B8G8R8A8_UNORM_SRGB = 91,
        XG_FORMAT_B8G8R8X8_TYPELESS = 92,
        XG_FORMAT_B8G8R8X8_UNORM_SRGB = 93,
        XG_FORMAT_BC6H_TYPELESS = 94,
        XG_FORMAT_BC6H_UF16 = 95,
        XG_FORMAT_BC6H_SF16 = 96,
        XG_FORMAT_BC7_TYPELESS = 97,
        XG_FORMAT_BC7_UNORM = 98,
        XG_FORMAT_BC7_UNORM_SRGB = 99,
        XG_FORMAT_AYUV = 100,
        XG_FORMAT_Y410 = 101,
        XG_FORMAT_Y416 = 102,
        XG_FORMAT_NV12 = 103,
        XG_FORMAT_P010 = 104,
        XG_FORMAT_P016 = 105,
        XG_FORMAT_420_OPAQUE = 106,
        XG_FORMAT_YUY2 = 107,
        XG_FORMAT_Y210 = 108,
        XG_FORMAT_Y216 = 109,
        XG_FORMAT_NV11 = 110,
        XG_FORMAT_AI44 = 111,
        XG_FORMAT_IA44 = 112,
        XG_FORMAT_P8 = 113,
        XG_FORMAT_A8P8 = 114,
        XG_FORMAT_B4G4R4A4_UNORM = 115,
        XG_FORMAT_R10G10B10_7E3_A2_FLOAT = 116,
        XG_FORMAT_R10G10B10_6E4_A2_FLOAT = 117,
        XG_FORMAT_D16_UNORM_S8_UINT = 118,
        XG_FORMAT_R16_UNORM_X8_TYPELESS = 119,
        XG_FORMAT_X16_TYPELESS_G8_UINT = 120,
        XG_FORMAT_FORCE_UINT = -1,
    }
    //***************************************ENUMS***************************************//

    //***************************************STRUCTS***************************************//
    public struct XG_SAMPLE_DESC
    {
        public uint Count;
        public uint Quality;
    }

    public struct XG_MIPLEVEL_LAYOUT
    {
        public ulong SizeBytes;
        public ulong OffsetBytes;
        public ulong Slice2DSizeBytes;
        public uint PitchPixels;
        public uint PitchBytes;
        public uint AlignmentBytes;
        public uint PaddedWidthElements;
        public uint PaddedHeightElements;
        public uint PaddedDepthOrArraySize;
        public uint WidthElements;
        public uint HeightElements;
        public uint DepthOrArraySize;
        public uint SampleCount;
        public XG_TILE_MODE TileMode;
        public ulong BankRotationAddressBitMask;
        public ulong BankRotationBytesPerSlice;
        public uint SliceDepthElements;
    }

    public unsafe struct XG_PLANE_LAYOUT
    {
        public XG_PLANE_USAGE Usage;
        public ulong SizeBytes;
        public ulong BaseOffsetBytes;
        public ulong BaseAlignmentBytes;
        public uint BytesPerElement;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
        public XG_MIPLEVEL_LAYOUT[] MipLayout;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct XG_RESOURCE_LAYOUT
    {
        public ulong SizeBytes;
        public ulong BaseAlignmentBytes;
        public uint MipLevels;
        public uint Planes;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public XG_PLANE_LAYOUT[] Plane;

        public XG_RESOURCE_DIMENSION Dimension;
    }

    public struct XG_TEXTURE1D_DESC
    {
        public uint Width;                     //1D Texture Width.
        public uint MipLevels;                 //1D Texture Mip Levels.
        public uint ArraySize;                 //1D Texture Array Size.
        public XG_FORMAT Format;               //1D Texture Format.
        public XG_USAGE Usage;                 //1D Texture Usage.
        public uint BindFlags;                 //1D Texture Bind Flags(XG_BIND_FLAG).
        public uint CPUAccessFlags;            //1D Texture CPU Access Flags(XG_CPU_ACCESS_FLAG).
        public uint MiscFlags;                 //1D Texure Miscellaneous Flags(XG_RESOURCE_MISC_FLAG).
    }

    public struct XG_TEXTURE2D_DESC
    {
        public uint Width;                     //2D Texture Width.
        public uint Height;                    //2D Texture Height.
        public uint MipLevels;                 //2D Texture Mip Levels.
        public uint ArraySize;                 //2D Texture Array Size.
        public XG_FORMAT Format;               //2D Texture Format.
        public XG_SAMPLE_DESC SampleDesc;      //2D Texture Sample Description.
        public XG_USAGE Usage;                 //2D Texture Usage.
        public uint BindFlags;                 //2D Texture Bind Flags(XG_BIND_FLAG).
        public uint CPUAccessFlags;            //2D Texture CPU Access Flags(XG_CPU_ACCESS_FLAG).
        public uint MiscFlags;                 //2D Texure Miscellaneous Flags(XG_RESOURCE_MISC_FLAG).
        public uint ESRAMOffsetBytes;          //2D Texure ESRAM Offset Bytes.
        public uint ESRAMUsageBytes;           //2D Texure ESRAM Usage Bytes.
        public XG_TILE_MODE TileMode;          //2D Texure Tile Mode.
        public uint Pitch;                     //2D Texure Pitch.
    }

    public struct XG_TEXTURE3D_DESC
    {
        public uint Width;                     //3D Texture Width.
        public uint Height;                    //3D Texture Height.
        public uint Depth;                     //3D Texture Depth.
        public uint MipLevels;                 //3D Texture Mip Levels.
        public XG_FORMAT Format;               //3D Texture Format.
        public XG_USAGE Usage;                 //3D Texture Usage.
        public uint BindFlags;                 //3D Texture Bind Flags(XG_BIND_FLAG).
        public uint CPUAccessFlags;            //3D Texture CPU Access Flags(XG_CPU_ACCESS_FLAG).
        public uint MiscFlags;                 //3D Texure Miscellaneous Flags(XG_RESOURCE_MISC_FLAG).
    }

    public unsafe struct XGTextureAddressComputer
    {
        public XGTextureAddressComputer_vt* vt;
    }

    public unsafe struct XGTextureAddressComputer_vt
    {
        public delegate* unmanaged<XGTextureAddressComputer*> AddRef;
        public delegate* unmanaged<XGTextureAddressComputer*> Release;

        //HRESULT GetResourceLayout(XG_RESOURCE_LAYOUT* pLayout); 
        public delegate* unmanaged<XGTextureAddressComputer*, XG_RESOURCE_LAYOUT*, int> GetResourceLayout;

        //ulong GetResourceSizeBytes(); 
        public delegate* unmanaged<XGTextureAddressComputer*, ulong> GetResourceSizeBytes;

        //ulong GetResourceBaseAlignmentBytes(); 
        public delegate* unmanaged<XGTextureAddressComputer*, ulong> GetResourceBaseAlignmentBytes;

        //ulong GetMipLevelOffsetBytes(uint32 plane, uint32 miplevel);
        public delegate* unmanaged<XGTextureAddressComputer*, uint, uint, ulong> GetMipLevelOffsetBytes;

        //ulong CopyFromSubresource(uint plane, uint miplevel, uint64 x, uint y, uint zOrSlice, uint sample);
        public nint GetTexelElementOffsetBytes;

        //ulong CopyFromSubresource(uint plane, uint miplevel, uint64 x, uint y, uint zOrSlice, uint sample);
        public nint GetTexelCoordinate;

        //HRESULT CopyFromSubresource(void* pTiledResourceBaseAddress, uint plane, uint subresource, void* pLinearData, uint rowPitchBytes, uint slicePitchBytes);
        public delegate* unmanaged<XGTextureAddressComputer*, byte*, uint, uint, byte*, uint, uint, int> CopyIntoSubresource;

        //HRESULT CopyFromSubresource(void* pLinearData, uint plane, uint subresource, void* pTiledResourceBaseAddress, uint rowPitchBytes, uint slicePitchBytes);
        public delegate* unmanaged<XGTextureAddressComputer*, byte*, uint, uint, byte*, uint, uint, int> CopyFromSubresource;

        public nint GetResourceTiling;
        public nint GetTextureViewDescriptor;

        //bool IsTiledResource();
        public delegate* unmanaged<XGTextureAddressComputer*, bool> IsTiledResource;
    }

    //***************************************STRUCTS***************************************//
}