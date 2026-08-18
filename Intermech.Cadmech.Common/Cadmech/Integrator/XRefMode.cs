// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.XRefMode
// Assembly: Intermech.Cadmech.Common, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3D1D989-0F34-4F5C-8A7E-7002449397DA
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Common.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Common.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Cadmech.Integrator;

/// <summary>
/// Описывает режимы регистрации в IPS внешних ссылок чертежа AutoCAD.
/// </summary>
public enum XRefMode
{
  /// <summary>Все внешние ссылки игнорируются</summary>
  [Description("Игнорировать внешние ссылки")] Ignore,
  /// <summary>
  /// Относительные внешние ссылки регистрируются в IPS как документы
  /// </summary>
  [Description("Регистрировать внешние ссылки как документы")] Documents,
  /// <summary>
  /// Относительные внешние ссылки регистрируются в IPS как дополнительные файлы чертежа
  /// </summary>
  [Description("Регистрировать внешние ссылки как дополнительные файлы")] AncillaryFiles,
}
