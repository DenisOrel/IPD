// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADSettings
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Tools.Integrators.Mechanical;
using Intermech.Tools.Settings;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Базовый тип для объектов настроек интеграторов, созданных на основе CAD-интерфейса. Он содержит общую часть настроек всех таких интеграторов.
/// </summary>
public class CADSettings : ISettingsObject
{
  private readonly DocumentGroupCollection fileDocumentGroups;
  private GlobalId<int> standardPartType;
  private GlobalId<int> jtDerivedDocumentType;
  private NewDrawingMode newDrawingMode;
  private readonly List<string> drawingSuffixes;
  private readonly List<GlobalId<int>> customDocAttrs;
  private readonly List<GlobalId<int>> customArticleAttrs;
  private bool syncSubstitutions;
  private bool enableCADLinkTypeAttribute;
  private DocumentGroup unpairedDocumentTypes;
  private DocumentGroup neutralDocumentTypes;
  private bool enableIMViewerFiles;
  private bool updateModelAuthenticFilesOnCheckin;
  private bool updateDrawingAuthenticFilesOnCheckin;

  /// <summary>Создает объект.</summary>
  public CADSettings()
  {
    this.fileDocumentGroups = new DocumentGroupCollection(8);
    this.fileDocumentGroups.Add(new DocumentGroup("Assembly", "Модели сборочных единиц", new string[1]
    {
      "model"
    }));
    this.fileDocumentGroups.Add(new DocumentGroup("Part", "Модели деталей", new string[1]
    {
      "model"
    }));
    this.fileDocumentGroups.Add(new DocumentGroup("AssemblyDrawing", "Чертежи сборочных единиц", new string[1]
    {
      "drawing"
    }));
    this.fileDocumentGroups.Add(new DocumentGroup("PartDrawing", "Чертежи деталей", new string[1]
    {
      "drawing"
    }));
    this.drawingSuffixes = new List<string>();
    this.syncSubstitutions = true;
    this.customDocAttrs = new List<GlobalId<int>>();
    this.customArticleAttrs = new List<GlobalId<int>>();
    this.unpairedDocumentTypes = new DocumentGroup(nameof (UnpairedDocumentTypes), "Непарные типы документов", new string[0]);
    this.neutralDocumentTypes = new DocumentGroup(nameof (NeutralDocumentTypes), "Нейтральные типы документов", new string[0]);
  }

  /// <summary>
  /// Возвращает коллекцию групп для типов файловых документов.
  /// К файловым типам документов относятся те, которые используются для хранения в IPS файлов, создаваемых пользователем в интегрируемом приложении.
  /// </summary>
  public DocumentGroupCollection FileDocumentGroups => this.fileDocumentGroups;

  /// <summary>
  /// Возвращает или задает специальный тип документа, используемый для хранения в IPS моделей стандартных изделий CADMECH.
  /// </summary>
  public GlobalId<int> StandardPartType
  {
    get => this.standardPartType;
    set => this.standardPartType = value;
  }

  /// <summary>
  /// Возвращает или задает режим помещения в базу IPS новых чертежей CAD-системы.
  /// </summary>
  public NewDrawingMode NewDrawingMode
  {
    get => this.newDrawingMode;
    set => this.newDrawingMode = value;
  }

  /// <summary>
  /// Возвращает список суффиксов для имен файлов чертежей, выполненных по 3D-моделям. Может быть пусто.
  /// </summary>
  public List<string> DrawingSuffixes => this.drawingSuffixes;

  /// <summary>
  /// Возвращает список атрибутов документов, синхронизируемых между CAD-системой и IPS, в дополнение к
  /// атрибутам, которые синхронизируются обязательно.
  /// </summary>
  public List<GlobalId<int>> CustomDocumentAttributes => this.customDocAttrs;

  /// <summary>
  /// Возвращает список атрибутов изделий, синхронизируемых между CAD-системой и IPS, в дополнение к
  /// атрибутам, которые синхронизируются обязательно.
  /// </summary>
  public List<GlobalId<int>> CustomArticleAttributes => this.customArticleAttrs;

  /// <summary>
  /// Включает и выключает передачу информации о допзаменах из 3D-модели в состав объекта.
  /// </summary>
  public bool SynchronizeSubstitutions
  {
    get => this.syncSubstitutions;
    set => this.syncSubstitutions = value;
  }

  /// <summary>
  /// Включает и выключает заполнение атрибута 'Тип связи в CAD-системе' на связях между 3D-моделями.
  /// </summary>
  public bool EnableCADLinkTypeAttribute
  {
    get => this.enableCADLinkTypeAttribute;
    set => this.enableCADLinkTypeAttribute = value;
  }

  /// <summary>
  /// Возвращает true, если интегратор с CAD-системой поддерживает использование документов, созданных на основе JT-представлений других документов.
  /// </summary>
  public bool JTDerivativesEnabled => this.JTDerivedDocumentType != null;

  /// <summary>
  /// Возвращает и задает специальный тип для документов, созданных на основе JT-представлений других документов.
  /// Значение свойства может быть равно null, если поддержка JT-компонентов не включена или не реализована.
  /// </summary>
  public GlobalId<int> JTDerivedDocumentType
  {
    get => this.jtDerivedDocumentType;
    set => this.jtDerivedDocumentType = value;
  }

  /// <summary>
  /// Возвращает группу типов документов, для которых отключен парный выпуск версий при создании версии изделия.
  /// </summary>
  public DocumentGroup UnpairedDocumentTypes
  {
    [DebuggerStepThrough] get => this.unpairedDocumentTypes;
  }

  /// <summary>
  /// Возвращает группу т.н. нейтральных типов документов.
  /// Такие документы не являются родными документами CAD-системы, но могут ей использоваться (например, step, parasolid и др.).
  /// При вставке в сборочную модель документа нейтрального формата он, как правило, преобразуется в родной формат CAD-системы.
  /// </summary>
  public DocumentGroup NeutralDocumentTypes
  {
    [DebuggerStepThrough] get => this.neutralDocumentTypes;
  }

  /// <summary>
  /// Включает и выключает обновление файлов IMViewer при сохранении изменений в документах CAD-системы.
  /// </summary>
  public bool EnableIMViewerFiles
  {
    [DebuggerStepThrough] get => this.enableIMViewerFiles;
    [DebuggerStepThrough] set => this.enableIMViewerFiles = value;
  }

  /// <summary>
  /// Включает и выключает автоматическое обновление аутентичных .pdf-файлов при завершении редактирования моделей.
  /// </summary>
  public bool UpdateModelAuthenticFilesOnCheckin
  {
    [DebuggerStepThrough] get => this.updateModelAuthenticFilesOnCheckin;
    [DebuggerStepThrough] set => this.updateModelAuthenticFilesOnCheckin = value;
  }

  /// <summary>
  /// Включает и выключает автоматическое обновление аутентичных .pdf-файлов при завершении редактирования чертежей.
  /// </summary>
  public bool UpdateDrawingAuthenticFilesOnCheckin
  {
    [DebuggerStepThrough] get => this.updateDrawingAuthenticFilesOnCheckin;
    [DebuggerStepThrough] set => this.updateDrawingAuthenticFilesOnCheckin = value;
  }

  /// <summary>
  /// Возвращает список всех файловых типов документов, поддерживаемых интегратором. К файловым типам документов относятся те,
  /// которые используются для хранения в IPS файлов, создаваемых пользователем в интегрируемом приложении.
  /// Этот список не будет содержать специальные типы документов - стандартных CADMECH, JT-представлений и их производных и др.
  /// Специальные типы документов используются интегратором в особых случаях, документы этих типов создаются только интегратором.
  /// </summary>
  /// <returns>Список типов документов</returns>
  internal List<LocalId<int>> GetCommonFileDocumentTypes()
  {
    List<LocalId<int>> fileDocumentTypes = new List<LocalId<int>>(32 /*0x20*/);
    fileDocumentTypes.AddRange((IEnumerable<LocalId<int>>) this.FileDocumentGroups.FindByName("Assembly", true).DocumentTypes);
    fileDocumentTypes.AddRange((IEnumerable<LocalId<int>>) this.FileDocumentGroups.FindByName("Part", true).DocumentTypes);
    fileDocumentTypes.AddRange((IEnumerable<LocalId<int>>) this.FileDocumentGroups.FindByName("AssemblyDrawing", true).DocumentTypes);
    fileDocumentTypes.AddRange((IEnumerable<LocalId<int>>) this.FileDocumentGroups.FindByName("PartDrawing", true).DocumentTypes);
    return fileDocumentTypes;
  }

  /// <summary>
  /// Имена групп документов, общих для всех интеграторов на основе CAD-интерфейса.
  /// </summary>
  public static class CommonGroups
  {
    /// <summary>Модели сборочных единиц</summary>
    public const string Assembly = "Assembly";
    /// <summary>Модели деталей</summary>
    public const string Part = "Part";
    /// <summary>Чертежи сборочных единиц</summary>
    public const string AssemblyDrawing = "AssemblyDrawing";
    /// <summary>Чертежи деталей</summary>
    public const string PartDrawing = "PartDrawing";
    /// <summary>
    /// Возвращает коллекцию имен всех групп документов, общих для всех интеграторов на основе CAD-интерфейса.
    /// </summary>
    public static readonly string[] All = new string[4]
    {
      nameof (Assembly),
      nameof (Part),
      nameof (AssemblyDrawing),
      nameof (PartDrawing)
    };
  }
}
