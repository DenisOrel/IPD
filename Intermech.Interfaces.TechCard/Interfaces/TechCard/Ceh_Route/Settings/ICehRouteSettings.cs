// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.Ceh_Route.Settings.ICehRouteSettings
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

#nullable disable
namespace Intermech.Interfaces.TechCard.Ceh_Route.Settings;

/// <summary>Интерфейс для параметров (настроек) расцеховки</summary>
public interface ICehRouteSettings
{
  /// <summary>Учитывать связь ТП с расцеховкой</summary>
  int LinkTpToCehRoute { get; set; }
}
