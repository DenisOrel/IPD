// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Interfaces.TechAcadLoadMode
// Assembly: Intermech.TechAcad.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 512FF008-192B-42A6-A8D1-B0B0A687059D
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.TechAcad.Interfaces.xml

#nullable disable
namespace Intermech.TechAcad.Interfaces;

/// <summary>Режимы загрузки приложения</summary>
public enum TechAcadLoadMode
{
  /// <summary>Режим обычной загрузки</summary>
  /// <remarks>Показываем диалоги и сообщения</remarks>
  Normal,
  /// <summary>Режим "тихой" загрузки</summary>
  /// <remarks>Не отображаем диалоги и сообщения</remarks>
  Silent,
}
