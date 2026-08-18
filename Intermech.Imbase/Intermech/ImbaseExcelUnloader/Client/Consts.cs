// Decompiled with JetBrains decompiler
// Type: Intermech.ImbaseExcelUnloader.Client.Consts
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.ImbaseExcelUnloader.Client;

public class Consts
{
  public static readonly int MaxExcelRowCount = 1048576 /*0x100000*/;
  public static string ImbaseKeyAttrGuidStr = "cad00162-306c-11d8-b4e9-00304f19f545";
  public static Guid ImbaseKeyAttrGuid = new Guid(Consts.ImbaseKeyAttrGuidStr);
  public static int ImbaseKeyAttrID = MetaDataHelper.GetAttributeTypeID(Consts.ImbaseKeyAttrGuid);
  public static string ImbasePathAttrGuidStr = "cadd96b4-306c-11d8-b4e9-00304f19f545";
  public static Guid ImbasePathAttrGuid = new Guid(Consts.ImbasePathAttrGuidStr);
  public static int ImbasePathAttrID = MetaDataHelper.GetAttributeTypeID(Consts.ImbasePathAttrGuid);
  public static string ImbaseRecordGuidAttrGuidStr = "cadd96b5-306c-11d8-b4e9-00304f19f545";
  public static Guid ImbaseRecordGuidAttrGuid = new Guid(Consts.ImbaseRecordGuidAttrGuidStr);
  public static int ImbaseRecordGuidAttrID = MetaDataHelper.GetAttributeTypeID(Consts.ImbaseRecordGuidAttrGuid);
}
