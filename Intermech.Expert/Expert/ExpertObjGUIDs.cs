// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ExpertObjGUIDs
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

#nullable disable
namespace Intermech.Expert;

public class ExpertObjGUIDs
{
  /// <summary>Abstract expert system object (Root)</summary>
  public static readonly string ExpertObject = "cad00100-306c-11d8-b4e9-00304f19f545";
  public static readonly string ExpertBaseScript = "cad0010b-306c-11d8-b4e9-00304f19f545";
  public static readonly string ExpertBaseFormula = "cad005c5-306c-11d8-b4e9-00304f19f545";
  /// <summary>Formula object</summary>
  public static readonly string ExpertFormula = "cad00101-306c-11d8-b4e9-00304f19f545";
  /// <summary>Table object</summary>
  public static readonly string ExpertTable = "cad00102-306c-11d8-b4e9-00304f19f545";
  /// <summary>Script object</summary>
  public static readonly string ExpertScript = "cad00103-306c-11d8-b4e9-00304f19f545";
  /// <summary>Condition object</summary>
  public static readonly string ExpertCond = "cad00104-306c-11d8-b4e9-00304f19f545";
  /// <summary>Function object</summary>
  public static readonly string ExpertFunction = "cad00105-306c-11d8-b4e9-00304f19f545";
  /// <summary>Attribute caclucation rules</summary>
  public static readonly string ExpertAttrRules = "cad00106-306c-11d8-b4e9-00304f19f545";
  /// <summary>Object searching rules</summary>
  public static readonly string ExpertObjRules = "cad0010c-306c-11d8-b4e9-00304f19f545";
  /// <summary>Выборка - НЕ объект ЭС</summary>
  public static readonly string Excerpt = "cad00119-306c-11d8-b4e9-00304f19f545";
  /// <summary>Скрипт генерации документа</summary>
  public static readonly string DocScript = "cad00108-306c-11d8-b4e9-00304f19f545";
  /// <summary>Скрипт пересчета</summary>
  public static readonly string RecalcScript = "cad00109-306c-11d8-b4e9-00304f19f545";
  /// <summary>Простая формула (без привязки к атрибуту)</summary>
  public static readonly string SimpleFormula = "cad0010a-306c-11d8-b4e9-00304f19f545";
  /// <summary>Шаблоны отчетов</summary>
  public static readonly string ReportTemplate = "cad0026d-306c-11d8-b4e9-00304f19f545";
  /// <summary>Imbase Folder</summary>
  public static readonly string ImbaseFolder = "cad00222-306c-11d8-b4e9-00304f19f545";
  /// <summary>Imbase Folder</summary>
  public static readonly string ImbaseBaseObject = "cad00220-306c-11d8-b4e9-00304f19f545";
  /// <summary>Excerpt for ExpertSystem</summary>
  public static readonly string ESExcept = "cad00111-306c-11d8-b4e9-00304f19f545";
  /// <summary>
  /// Attribute group that holds temporary attributes WITHOUT objects
  /// </summary>
  public static readonly string TempAttrGroup = "cad00114-306c-11d8-b4e9-00304f19f545";
  /// <summary>
  /// Attribute group that holds temporary attributes WITH objects
  /// </summary>
  public static readonly string TempAttrObjGroup = "cad00115-306c-11d8-b4e9-00304f19f545";
  /// <summary>
  /// Группа с атрибутами, которые надо прописать в сам объект документа после генерации
  /// </summary>
  public static readonly string DocAttrGroup = "cadd9595-306c-11d8-b4e9-00304f19f545";
  /// <summary>
  /// Группа с атрибутами, которые надо прописать в сам объект комплекта после генерации
  /// </summary>
  public static readonly string CompAttrGroup = "cadd9bf0-306c-11d8-b4e9-00304f19f545";
  /// <summary>Generic Document type</summary>
  public static readonly string DocRoot = "cad00070-306c-11d8-b4e9-00304f19f545";
  /// <summary>Default report type</summary>
  public static readonly string ReportObjects = "cad00293-306c-11d8-b4e9-00304f19f545";
  /// <summary>Script for document complects</summary>
  public static readonly string ComplectTemplate = "cad01488-306c-11d8-b4e9-00304f19f545";
  /// <summary>Комплект документов ТП</summary>
  public static readonly string docTPComplect = "cad009ed-306c-11d8-b4e9-00304f19f545";
  /// <summary>Документ ТП</summary>
  public static readonly string docTP = "cad00198-306c-11d8-b4e9-00304f19f545";
  /// <summary>Комплект документов</summary>
  public static readonly string docComplect = "cad00199-306c-11d8-b4e9-00304f19f545";
  /// <summary>
  /// Комплект технологических документов (родительский тип)
  /// </summary>
  public static readonly string techComplectRoot = "cad00169-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип связи "Технологический состав"</summary>
  public static readonly string linkTechSostav = "cad0019f-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип связи "Простая связь с сортировкой"</summary>
  public static readonly string linkSimpleSort = "cad00151-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип связи "Документация на изделие"</summary>
  public static readonly string linkDocForIzd = "cad00154-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип связи "Состав изделий"</summary>
  public static readonly string linkSostav = "cad00023-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип объекта "Комплект"</summary>
  public static readonly string IzdComplectGUID = "cad0025f-306c-11d8-b4e9-00304f19f545";
  /// <summary>Базовый объект IMBASE</summary>
  public static readonly string BaseImbaseObject = "cad00220-306c-11d8-b4e9-00304f19f545";
  public static readonly string objectMinute = "cad007db-306c-11d8-b4e9-00304f19f545";
  public static readonly string objectScenario = "cadd939d-306c-11d8-b4e9-00304f19f545";
  public static readonly string objectExpScenario = "cadd94bb-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidObjTechTemplate = "cad009eb-306c-11d8-b4e9-00304f19f545";
  public static readonly string objRootIzdelie = "cad00268-306c-11d8-b4e9-00304f19f545";
  public static readonly string objHeadIzdelie = "cadd940b-306c-11d8-b4e9-00304f19f545";
  public static readonly string objSignGUID = "cad00137-306c-11d8-b4e9-00304f19f545";
  public static readonly string linkSignGUID = "cad00139-306c-11d8-b4e9-00304f19f545";
  public static readonly string objESFolder = "cadd9715-306c-11d8-b4e9-00304f19f545";
  public static readonly string objVisScheme = "cadd9aa6-306c-11d8-b4e9-00304f19f545";
  public static readonly string objVisStyles = "cadd9aa7-306c-11d8-b4e9-00304f19f545";
  public static readonly string objTechDocSettings = "cadd99ae-306c-11d8-b4e9-00304f19f545";
  public static readonly string CommandScript = "cadd9c04-306c-11d8-b4e9-00304f19f545";
}
