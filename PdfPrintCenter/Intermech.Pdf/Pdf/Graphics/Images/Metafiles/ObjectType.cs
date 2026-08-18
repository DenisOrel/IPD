// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Images.Metafiles.ObjectType
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Graphics.Images.Metafiles;

internal enum ObjectType
{
  Invalid = 0,
  Brush = 256, // 0x00000100
  Pen = 512, // 0x00000200
  Path = 768, // 0x00000300
  Region = 1024, // 0x00000400
  Image = 1280, // 0x00000500
  Font = 1536, // 0x00000600
  StringFormat = 1792, // 0x00000700
  ImageAttributes = 2048, // 0x00000800
  CustomLineCap = 2304, // 0x00000900
}
