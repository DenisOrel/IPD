// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.DocumentEditorIntegrator
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Localization;
using Intermech.Tools;
using Intermech.Tools.Integrators;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Document.Client;

internal sealed class DocumentEditorIntegrator : IntegratorBase
{
  internal static readonly string ApplicationName = LocalizationHolder.rm.GetString("Document.Client_127");
  internal static readonly string IntegratorName = LocalizationHolder.rm.GetString("Document.Client_128");
  internal static readonly Guid IntegratorId = new Guid("7ADB51AF-5E17-4816-90E7-9F4A43499EF4");

  /// <summary>
  /// Возвращает глобальный идентификатор объекта интегратора в базе IPS.
  /// </summary>
  public override Guid Id
  {
    [DebuggerStepThrough] get => DocumentEditorIntegrator.IntegratorId;
  }

  /// <summary>Возвращает название интегратора.</summary>
  public override string DisplayName
  {
    [DebuggerStepThrough] get => DocumentEditorIntegrator.IntegratorName;
  }

  /// <summary>
  /// Возвращает шаблон для серверного объекта интегратора в форме xml-документа.
  /// Он используется при создании нового объекта интегратора в базе IPS.
  /// </summary>
  /// <returns>Шаблон для серверного объекта интегратора в форме xml-документа</returns>
  public override string GetServerObjectTemplate() => this.GetEmptyServerObjectTemplate();

  /// <summary>
  /// Создает и возвращает визуальный редактор настроек интегратора.
  /// </summary>
  /// <returns>Элемент управления</returns>
  public override DataEditorControl CreateSettingsEditor() => (DataEditorControl) new DataEditor();
}
