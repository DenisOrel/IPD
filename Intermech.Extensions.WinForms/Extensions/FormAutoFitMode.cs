// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.FormAutoFitMode
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using System;

#nullable disable
namespace Intermech.Extensions;

[Flags]
public enum FormAutoFitMode
{
  None = 0,
  Width = 1,
  Height = 2,
  WidthAndHeight = Height | Width, // 0x00000003
}
