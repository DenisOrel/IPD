// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Interfaces.Const
// Assembly: Intermech.GTC.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 767EAE12-F30F-454C-81D0-2862AEDD13C4
// Assembly location: D:\IPS\Client\Intermech.GTC.Interfaces.dll

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.GTC.Interfaces;

internal class Const
{
  public static int ClassAttrTypeAttributeTypeId;
  public static readonly Guid ClassAttrTypeAttributeTypeGuid = new Guid("cadd989d-306c-11d8-b4e9-00304f19f545");

  static Const()
  {
    Const.ClassAttrTypeAttributeTypeId = MetaDataHelper.GetAttributeTypeID(Const.ClassAttrTypeAttributeTypeGuid);
  }
}
