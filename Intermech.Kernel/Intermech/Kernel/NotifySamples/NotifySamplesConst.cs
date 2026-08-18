// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.NotifySamples.NotifySamplesConst
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;


namespace Intermech.Kernel.NotifySamples;

public class NotifySamplesConst
{
  public static Guid NotifySamplesTypeGuid = new Guid("cadd96c2-306c-11d8-b4e9-00304f19f545");
  public const string NotifyModeAttrGuid = "cadd96c5-306c-11d8-b4e9-00304f19f545";
  public const string NotifyPeriodAttrGuid = "cadd96c3-306c-11d8-b4e9-00304f19f545";
  public const string ObjectsListAttrGuid = "cadd96c4-306c-11d8-b4e9-00304f19f545";
  public const string CheckDateAttrGuid = "cad00703-306c-11d8-b4e9-00304f19f545";
  public const string SampleConditionsAttrGuidStr = "cad0069b-306c-11d8-b4e9-00304f19f545";
  private static int _SampleConditionsAttr;
  private static int _NotifySamplesType;
  private static int _NotifyModeAttr;
  private static int _NotifyPeriodAttr;
  private static int _ObjectsListAttr;
  private static int _NameAttr;
  private static int _CheckDateAttr;

  internal static void Init(IIDHelper idHelper)
  {
    NotifySamplesConst._NotifySamplesType = idHelper.GetObjectTypeID("cadd96c2-306c-11d8-b4e9-00304f19f545");
    NotifySamplesConst._NotifyModeAttr = idHelper.GetAttributeID("cadd96c5-306c-11d8-b4e9-00304f19f545");
    NotifySamplesConst._NotifyPeriodAttr = idHelper.GetAttributeID("cadd96c3-306c-11d8-b4e9-00304f19f545");
    NotifySamplesConst._ObjectsListAttr = idHelper.GetAttributeID("cadd96c4-306c-11d8-b4e9-00304f19f545");
    NotifySamplesConst._CheckDateAttr = idHelper.GetAttributeID("cad00703-306c-11d8-b4e9-00304f19f545");
    NotifySamplesConst._SampleConditionsAttr = idHelper.GetAttributeID("cad0069b-306c-11d8-b4e9-00304f19f545");
    NotifySamplesConst._NameAttr = idHelper.NameID;
  }

  public static int SampleConditionsAttr => NotifySamplesConst._SampleConditionsAttr;

  public static int NotifySamplesType => NotifySamplesConst._NotifySamplesType;

  public static int NotifyModeAttr => NotifySamplesConst._NotifyModeAttr;

  public static int NotifyPeriodAttr => NotifySamplesConst._NotifyPeriodAttr;

  public static int ObjectsListAttr => NotifySamplesConst._ObjectsListAttr;

  public static int NameAttr => NotifySamplesConst._NameAttr;

  public static int CheckDateAttr => NotifySamplesConst._CheckDateAttr;
}
