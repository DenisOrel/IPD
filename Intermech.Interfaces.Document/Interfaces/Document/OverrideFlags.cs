// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.OverrideFlags
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Для внутреннего использования. Флаги переопределения свойств в элементах сделанных по шаблону</summary>
[Flags]
[Serializable]
public enum OverrideFlags
{
  None = 0,
  CharFormat = 1,
  ParagraphFormat = 2,
  Width = 4,
  Height = 8,
  Geometry = 16, // 0x00000010
  DefaultRowSize = 32, // 0x00000020
  TopBorder = 128, // 0x00000080
  BottomBorder = 256, // 0x00000100
  LeftBorder = 512, // 0x00000200
  RightBorder = 1024, // 0x00000400
  Grid = 2048, // 0x00000800
  MinHeight = 4096, // 0x00001000
  MaxHeight = 8192, // 0x00002000
  BackColor = 16384, // 0x00004000
  SkipBefore = 32768, // 0x00008000
  SkipAfter = 65536, // 0x00010000
  AutoSize = 131072, // 0x00020000
  TextFormat = 262144, // 0x00040000
  Data = 524288, // 0x00080000
  ScaleMode = 1048576, // 0x00100000
  FitToPage = 2097152, // 0x00200000
  ShifPage = 4194304, // 0x00400000
  AllowFormatingForReadOnly = 8388608, // 0x00800000
  StartPageNumber = 16777216, // 0x01000000
  PrintPageBounds = 33554432, // 0x02000000
  ImageLayers = 67108864, // 0x04000000
  MinWidth = 134217728, // 0x08000000
  FromNewPage = 268435456, // 0x10000000
  KeepWithNext = 536870912, // 0x20000000
  ReadOnly = 1073741824, // 0x40000000
}
