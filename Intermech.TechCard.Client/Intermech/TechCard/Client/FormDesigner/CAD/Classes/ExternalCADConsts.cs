// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.FormDesigner.CAD.Classes.ExternalCADConsts
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.TechCard.Client.FormDesigner.CAD.Classes;

/// <summary>Consts for ExternalCADAction/ExternalCADParams</summary>
internal class ExternalCADConsts
{
  /// <summary>Guid for CAD action</summary>
  public static Guid ExternalCADActionGuid = new Guid("{C864B3A2-86F8-4a42-A783-A465C0CC98E3}");
  /// <summary>Caption for CAD action</summary>
  public static string ExternalCADActionCaption = LocalizationHolder.rm.GetString("TechCard.Client_434");
  /// <summary>Guid of object type "Электронные модели деталей"</summary>
  public static Guid ExternalCADModelTypeGuid = TechCardConsts.ObjectTypes.ExternalCADModelTypeGuid;
}
