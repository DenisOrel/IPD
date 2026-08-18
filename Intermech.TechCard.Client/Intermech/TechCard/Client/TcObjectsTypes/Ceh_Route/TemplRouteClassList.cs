// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.TemplRouteClassList
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;

/// <summary>Список шаблонов расцеховки</summary>
[Obsolete("Will be removed in IPS 8.0")]
/// <summary>Конструктор</summary>
/// <param name="owner"></param>
public class TemplRouteClassList(CustomTechClass owner) : CustomTechClassList<CehRouteTemplateClass>(owner)
{
}
