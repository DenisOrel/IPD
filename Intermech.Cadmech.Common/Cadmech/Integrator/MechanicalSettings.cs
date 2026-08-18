// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.MechanicalSettings
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
/// Реализует контейнер для настроек интегратора, относящихся к конструкторским чертежам AutoCAD.
/// </summary>
public sealed class MechanicalSettings : ProcessingSchemaSettings
{
  /// <summary>Возвращает идентификатор группы сборочных чертежей.</summary>
  public static readonly Guid AssemblyDrawingsGroup = new Guid("{46ED4D21-2382-4013-AB94-2727633D7CD8}");
  /// <summary>Возвращает идентификатор группы чертежей деталей.</summary>
  public static readonly Guid PartDrawingsGroup = new Guid("{150F7366-EBE1-4d26-9FA9-422828891F3F}");
  private readonly List<DrawingTypeSettings> assemblyDrawings;
  private readonly List<DrawingTypeSettings> partDrawings;

  /// <summary>Создает объект.</summary>
  public MechanicalSettings()
  {
    this.assemblyDrawings = new List<DrawingTypeSettings>(16 /*0x10*/);
    this.partDrawings = new List<DrawingTypeSettings>(16 /*0x10*/);
  }

  /// <summary>
  /// Возвращает текст сообщения о том, что эта схема обработки чертежей отключена в настройках интегратора.
  /// </summary>
  /// <returns></returns>
  protected override string GetNotEnabledErrorMessage()
  {
    return "Обработка конструкторских чертежей dwg отключена в настройках интегратора.";
  }

  /// <summary>
  /// Возвращает список типов документов IPS, соответствующих сборочным чертежам. Каждый
  /// элемент списка дополнительно содержит настройки обработки чертежей.
  /// </summary>
  public List<DrawingTypeSettings> AssemblyDrawings => this.assemblyDrawings;

  /// <summary>
  /// Возвращает список типов документов IPS, соответствующих чертежам деталей. Каждый
  /// элемент списка дополнительно содержит настройки обработки чертежей.
  /// </summary>
  public List<DrawingTypeSettings> PartDrawings => this.partDrawings;

  /// <summary>
  /// Возвращает список всех типов чертежей, обрабатываемых в рамках данной схемы.
  /// </summary>
  /// <returns>Список типов документов</returns>
  public override List<LocalId<int>> GetAllDocumentTypes()
  {
    List<LocalId<int>> allDocumentTypes = new List<LocalId<int>>(16 /*0x10*/);
    allDocumentTypes.AddRange((IEnumerable<LocalId<int>>) this.GetDrawingTypesByGroupType(MechanicalSettings.AssemblyDrawingsGroup));
    allDocumentTypes.AddRange((IEnumerable<LocalId<int>>) this.GetDrawingTypesByGroupType(MechanicalSettings.PartDrawingsGroup));
    return allDocumentTypes;
  }

  /// <summary>
  /// Позволяет проверить, поддерживает ли схема указанную группу документов.
  /// </summary>
  /// <param name="groupType">Идентификатор группы документов</param>
  /// <returns>Результат проверки</returns>
  public override bool IsGroupSupported(Guid groupType)
  {
    return groupType == MechanicalSettings.AssemblyDrawingsGroup || groupType == MechanicalSettings.PartDrawingsGroup;
  }

  /// <summary>
  /// Возвращает идентификаторы типов документов, входящих в указанную группу документов.
  /// </summary>
  /// <param name="groupType">Идентификатор группы документов</param>
  /// <returns>Список идентификаторов типов документов</returns>
  /// <exception cref="T:System.InvalidOperationException">Не удалось найти группу документов по указанному идентификатору</exception>
  public override List<LocalId<int>> GetDrawingTypesByGroupType(Guid groupType)
  {
    if (groupType == MechanicalSettings.AssemblyDrawingsGroup)
      return this.assemblyDrawings.ConvertAll<LocalId<int>>((Converter<DrawingTypeSettings, LocalId<int>>) (dwgSettings => (LocalId<int>) dwgSettings.DocumentType));
    return groupType == MechanicalSettings.PartDrawingsGroup ? this.partDrawings.ConvertAll<LocalId<int>>((Converter<DrawingTypeSettings, LocalId<int>>) (dwgSettings => (LocalId<int>) dwgSettings.DocumentType)) : base.GetDrawingTypesByGroupType(groupType);
  }

  /// <summary>
  /// Реализует определение группы чертежей по идентификатору типа чертежа.
  /// </summary>
  /// <param name="documentType">Идентификатор типа чертежей</param>
  /// <returns>Идентификатор группы чертежей или Guid.Empty</returns>
  protected override Guid DoGetGroupType(int documentType)
  {
    foreach (DrawingTypeSettings assemblyDrawing in this.assemblyDrawings)
    {
      if (assemblyDrawing.DocumentType.Id == documentType)
        return MechanicalSettings.AssemblyDrawingsGroup;
    }
    foreach (DrawingTypeSettings partDrawing in this.partDrawings)
    {
      if (partDrawing.DocumentType.Id == documentType)
        return MechanicalSettings.PartDrawingsGroup;
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
    foreach (DrawingTypeSettings assemblyDrawing in this.assemblyDrawings)
    {
      if (assemblyDrawing.DocumentType.Id == documentType)
        return assemblyDrawing;
    }
    foreach (DrawingTypeSettings partDrawing in this.partDrawings)
    {
      if (partDrawing.DocumentType.Id == documentType)
        return partDrawing;
    }
    return base.DoFindSettings(documentType);
  }
}
