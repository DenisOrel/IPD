// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.ReportsConsts
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Reports;

/// <summary>Константы для генератора документов</summary>
public static class ReportsConsts
{
  /// <summary>тип связей "Простая связь с сортировкой"</summary>
  public static readonly Guid SimpleWithSortRelation = new Guid("cad00151-306c-11d8-b4e9-00304f19f545");
  /// <summary>тип объектов "Комплект документов базовый"</summary>
  public static readonly Guid DocPackageBaseTypeGuid = new Guid("cad00199-306c-11d8-b4e9-00304f19f545");
  /// <summary>тип объектов "Шаблоны комплекта документов" (ЭС)</summary>
  public static readonly Guid ScriptPackageTypeGuid = new Guid("cad01488-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Базовый тип документов, наследники которого будем обрабатывать в составе комплекта
  /// на данный момент это "Документы"
  /// </summary>
  public static readonly Guid DocumentBaseTypeGuid = new Guid("cad00070-306c-11d8-b4e9-00304f19f545");
  /// <summary>Object type id  "Комплект документов базовый"</summary>
  public static readonly int DocPackageBaseTypeID;
  /// <summary>тип объектов "Шаблоны комплекта документов" (ЭС)</summary>
  public static readonly int ScriptPackageTypeID;
  /// <summary>
  /// Базовый тип документов , наследники которого будем обрабатывать в составе комплекта
  /// на данный момент это "Документы"
  /// </summary>
  public static readonly int DocumentBaseTypeID;
  /// <summary>тип атрибута "Тип объекта-результата"</summary>
  public static readonly Guid ObjTypeResultAttrTypeGuid = new Guid("cad00067-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут "Условие"</summary>
  public static readonly Guid ConditionAttrTypeGuid = new Guid("cad00064-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут "Ссылка на скрипт ЭС"</summary>
  public static readonly Guid ScriptPackageAttrTypeGuid = new Guid("cad014b3-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут "Заголовок"</summary>
  public static readonly Guid CaptionAttrTypeGuid = new Guid("cad00047-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут "Файл"</summary>
  public static readonly Guid FileAttributeTypeGuid = new Guid("cad0004b-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут необходимость архива</summary>
  public static readonly Guid NeedArchiveAttributeTypeGuid = new Guid("cadd966c-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут "Архив"</summary>
  public static readonly Guid ArchiveAttributeTypeGuid = SystemGUIDs.attributeArchive;
  /// <summary>Атрибут Ссылка на объект-источник</summary>
  public static readonly Guid SourceLinkAttributeTypeGuid = new Guid("cadd95b4-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// 
  /// </summary>
  public static readonly int SimpleWithSortRelationID;
  /// <summary>Тип атрибута "Тип объекта-результата"</summary>
  public static readonly int ObjTypeResultAttrTypeID;
  /// <summary>Тип атрибута "Условие"</summary>
  public static readonly int ConditionAttrTypeID;
  /// <summary>Атрибут "Ссылка на скрипт ЭС"</summary>
  public static readonly int ScriptPackageAttrTypeID;
  /// <summary>Атрибут "Заголовок"</summary>
  public static readonly int CaptionAttrTypeID;
  /// <summary>Атрибут "Файл"</summary>
  public static readonly int FileAttributeTypeID;
  /// <summary>Атрибут необходимость архива</summary>
  public static readonly int NeedArchiveAttributeTypeID;
  /// <summary>Атрибут "Архив"</summary>
  public static readonly int ArchiveAttributeTypeID;

  /// <summary>Инициализация констант</summary>
  static ReportsConsts()
  {
    ReportsConsts.DocPackageBaseTypeID = MetaDataHelper.GetObjectTypeID(ReportsConsts.DocPackageBaseTypeGuid);
    ReportsConsts.ScriptPackageTypeID = MetaDataHelper.GetObjectTypeID(ReportsConsts.ScriptPackageTypeGuid);
    ReportsConsts.DocumentBaseTypeID = MetaDataHelper.GetObjectTypeID(ReportsConsts.DocumentBaseTypeGuid);
    ReportsConsts.SimpleWithSortRelationID = MetaDataHelper.GetRelationTypeID(ReportsConsts.SimpleWithSortRelation);
    ReportsConsts.ObjTypeResultAttrTypeID = MetaDataHelper.GetAttributeTypeID(ReportsConsts.ObjTypeResultAttrTypeGuid);
    ReportsConsts.ConditionAttrTypeID = MetaDataHelper.GetAttributeTypeID(ReportsConsts.ConditionAttrTypeGuid);
    ReportsConsts.ScriptPackageAttrTypeID = MetaDataHelper.GetAttributeTypeID(ReportsConsts.ScriptPackageAttrTypeGuid);
    ReportsConsts.CaptionAttrTypeID = MetaDataHelper.GetAttributeTypeID(ReportsConsts.CaptionAttrTypeGuid);
    ReportsConsts.FileAttributeTypeID = MetaDataHelper.GetAttributeTypeID(ReportsConsts.FileAttributeTypeGuid);
    ReportsConsts.NeedArchiveAttributeTypeID = MetaDataHelper.GetAttributeTypeID(ReportsConsts.NeedArchiveAttributeTypeGuid);
    ReportsConsts.ArchiveAttributeTypeID = MetaDataHelper.GetAttributeTypeID(ReportsConsts.ArchiveAttributeTypeGuid);
  }
}
