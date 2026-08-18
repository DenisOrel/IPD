// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ContentAttributes
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

[Flags]
public enum ContentAttributes
{
  None = 0,
  Folder = 1,
  Hidden = 2,
  HasChildren = 4,
  Slow = 8,
  Large = 16, // 0x00000010
}
