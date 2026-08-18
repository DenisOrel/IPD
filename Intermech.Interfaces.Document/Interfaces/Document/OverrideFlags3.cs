// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.OverrideFlags3
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
public enum OverrideFlags3
{
  None = 0,
  IgnoreSkipOuterCells = 1,
  ReplaceOldAVSSpecChars = 2,
  ContainerVertAlign = 4,
  ContainerHorzAlign = 8,
  ReplaceAVSMaterial = 16, // 0x00000010
  SaveValueFromRefToDBAttr = 32, // 0x00000020
  IgnoreSkipBefore = 64, // 0x00000040
  IgnoreSkipAfter = 128, // 0x00000080
  NonSkipBeforeAtStartPage = 256, // 0x00000100
  Visible = 512, // 0x00000200
  InnerHorizontalLine = 1024, // 0x00000400
  ForeColor = 2048, // 0x00000800
  Transparent = 4096, // 0x00001000
  DrawParentCellFrames = 8192, // 0x00002000
  UseFontAutoSize = 16384, // 0x00004000
  RelativeHeight = 32768, // 0x00008000
  RelativeWidth = 65536, // 0x00010000
  UseTextFormatInRef = 131072, // 0x00020000
}
