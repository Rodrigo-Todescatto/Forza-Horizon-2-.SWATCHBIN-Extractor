using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using XGXTYPES;

public partial class XGXImports
{
    [LibraryImport("xg_x.dll", EntryPoint = "XGCreateTexture1DComputer")]
    public unsafe static partial int XGCreateTexture1DComputer(XG_TEXTURE1D_DESC* desc, XGTextureAddressComputer** computer);

    [LibraryImport("xg_x.dll", EntryPoint = "XGCreateTexture2DComputer")]
    public unsafe static partial int XGCreateTexture2DComputer(XG_TEXTURE2D_DESC* desc, XGTextureAddressComputer** computer);

    [LibraryImport("xg_x.dll", EntryPoint = "XGCreateTexture3DComputer")]
    public unsafe static partial int XGCreateTexture3DComputer(XG_TEXTURE3D_DESC* desc, XGTextureAddressComputer** computer);
}