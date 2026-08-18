// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.ConstructionalSettings
// Assembly: Intermech.Cadmech.Common, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3D1D989-0F34-4F5C-8A7E-7002449397DA
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Common.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Common.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

/// <summary>
/// Реализует контейнер для настроек интегратора, относящихся к СПДС-чертежам AutoCAD.
/// </summary>
public sealed class ConstructionalSettings : ProcessingSchemaSettings
{
  /// <summary>Возвращает идентификатор группы чертежей СПДС и ПГС.</summary>
  public static readonly Guid DrawingsGroup = new Guid("{2379829F-E2C8-40d8-80DD-F4A870D0D615}");
  private readonly List<DrawingTypeSettings> drawings;

  /// <summary>Создает объект.</summary>
  public ConstructionalSettings() => this.drawings = new List<DrawingTypeSettings>(16 /*0x10*/);

  /// <summary>
  /// Возвращает текст сообщения о том, что эта схема обработки чертежей отключена в настройках интегратора.
  /// </summary>
  /// <returns></returns>
  protected override string GetNotEnabledErrorMessage()
  {
    return "Обработка СПДС-чертежей dwg отключена в настройках интегратора.";
  }

  /// <summary>
  /// Возвращает список типов документов IPS, соответствующих СПДС-чертежам. Каждый
  /// элемент списка дополнительно содержит настройки обработки чертежей.
  /// </summary>
  public List<DrawingTypeSettings> Drawings => this.drawings;

  /// <summary>
  /// Возвращает список всех типов чертежей, обрабатываемых в рамках данной схемы.
  /// </summary>
  /// <returns>Список типов документов</returns>
  public override List<LocalId<int>> GetAllDocumentTypes()
  {
    List<LocalId<int>> allDocumentTypes = new List<LocalId<int>>(16 /*0x10*/);
    allDocumentTypes.AddRange((IEnumerable<LocalId<int>>) this.GetDrawingTypesByGroupType(ConstructionalSettings.DrawingsGroup));
    return allDocumentTypes;
  }

  /// <summary>
  /// Позволяет проверить, поддерживает ли схема указанную группу документов.
  /// </summary>
  /// <param name="groupType">Идентификатор группы документов</param>
  /// <returns>Результат проверки</returns>
  public override bool IsGroupSupported(Guid groupType)
  {
    return groupType == ConstructionalSettings.DrawingsGroup;
  }

  /// <summary>
  /// Возвращает идентификаторы типов документов, входящих в указанную группу документов.
  /// </summary>
  /// <param name="groupType">Идентификатор группы документов</param>
  /// <returns>Список идентификаторов типов документов</returns>
  /// <exception cref="T:System.InvalidOperationException">Не удалось найти группу документов по указанному идентификатору</exception>
  public override List<LocalId<int>> GetDrawingTypesByGroupType(Guid groupType)
  {
    return groupType == ConstructionalSettings.DrawingsGroup ? this.drawings.ConvertAll<LocalId<int>>((Converter<DrawingTypeSettings, LocalId<int>>) (dwgSettings => (LocalId<int>) dwgSettings.DocumentType)) : base.GetDrawingTypesByGroupType(groupType);
  }

  /// <summary>
  /// Реализует определение группы чертежей по идентификатору типа чертежа.
  /// </summary>
  /// <param name="documentType">Идентификатор типа чертежей</param>
  /// <returns>Идентификатор группы чертежей или Guid.Empty</returns>
  protected override Guid DoGetGroupType(int documentType)
  {
    foreach (DrawingTypeSettings drawing in this.drawings)
    {
      if (drawing.DocumentType.Id == documentType)
        return ConstructionalSettings.DrawingsGroup;
    }
    return base.DoGetGroupType(documentType);
  }

  /// <summary>
  /// Реализует поиск настроек для типа чертежей по идентификатору типа чертежей.
  /// </summary>
  /// <param name="documentType">Идентификатор типа чертежей</param>
  /// <returns>Найденные настройки или null</returns>
  protected override DrawingTypeSettings DoFindSettings(int documentType)
  {
    foreach (DrawingTypeSettings drawing in this.drawings)
    {
      if (drawing.DocumentType.Id == documentType)
        return drawing;
    }
    return base.DoFindSettings(documentType);
  }
}
