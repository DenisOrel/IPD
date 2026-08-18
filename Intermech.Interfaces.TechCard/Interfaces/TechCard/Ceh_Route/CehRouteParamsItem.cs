// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.Ceh_Route.CehRouteParamsItem
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Interfaces.TechCard.Ceh_Route.Settings;
using System;

#nullable disable
namespace Intermech.Interfaces.TechCard.Ceh_Route;

/// <summary>Класс для параметров расцеховки</summary>
[Obsolete("Use CehRouteSettings instead. Will be removed in IPS 8.0", true)]
[Serializable]
public class CehRouteParamsItem : CehRouteSettings, ICehRouteParamsItem, ICehRouteSettings
{
}
