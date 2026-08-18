// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.Ceh_Route.Settings.CehRouteSettings
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.TechCard.Ceh_Route.Settings;

/// <summary>Класс для настроек (параметров) расцеховки</summary>
[Serializable]
public class CehRouteSettings : ICehRouteSettings
{
  /// <summary>Учитывать связь ТП с расцеховкой</summary>
  public int LinkTpToCehRoute { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }
}
