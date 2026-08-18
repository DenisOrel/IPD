// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.JTDerivedFileInfo
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.IO;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Предоставляет сведения о документах CAD-системы, полученных в результате импорта JT-документа.
/// </summary>
public class JTDerivedFileInfo
{
  private static readonly Regex jtDerivedDocumentPattern = new Regex("^(?<part_name>.+)_(?<form_ext>jt)\\.(?<part_ext>.+)$");
  private readonly string sourceFilePath;
  private bool hasData;
  private bool isDerivedFromJTFile;
  private string jtFilePath;
  private long? jtDocId;

  /// <summary>Создает объект.</summary>
  /// <param name="sourceFilePath">Абсолютный путь к файлу документа CAD-системы</param>
  public JTDerivedFileInfo(string sourceFilePath)
  {
    if (sourceFilePath == null)
      throw new ArgumentNullException(nameof (sourceFilePath));
    this.sourceFilePath = Path.IsPathRooted(sourceFilePath) ? sourceFilePath : throw new ArgumentException();
  }

  /// <summary>
  /// Возвращает абсолютный путь к файлу документа CAD-системы
  /// </summary>
  public string SourceFilePath => this.sourceFilePath;

  /// <summary>
  /// Проверяет и возвращает true, если документ CAD-системы действительно основан на JT-документе.
  /// </summary>
  public bool IsDerivedFromJTFile
  {
    get
    {
      this.GetDataIfEmpty();
      return this.isDerivedFromJTFile;
    }
  }

  /// <summary>
  /// Возвращает абсолютный путь к файлу исходного JT-документа. Наличие самого файла на диске не проверяется. Может быть null, если документ CAD-системы не
  /// был основан на JT-документе.
  /// </summary>
  public string JTFilePath
  {
    get
    {
      this.GetDataIfEmpty();
      return this.jtFilePath;
    }
  }

  /// <summary>Обновляет сведения о документе CAD-системы.</summary>
  public void Refresh()
  {
    this.ResetData();
    this.GetDataIfEmpty();
  }

  private void ResetData()
  {
    this.hasData = false;
    this.isDerivedFromJTFile = false;
    this.jtFilePath = (string) null;
    this.jtDocId = new long?();
  }

  private void GetDataIfEmpty()
  {
    if (this.hasData)
      return;
    try
    {
      this.GetDataCore();
      this.hasData = true;
    }
    catch
    {
      this.ResetData();
      throw;
    }
  }

  private void GetDataCore()
  {
    Match match = JTDerivedFileInfo.jtDerivedDocumentPattern.Match(this.sourceFilePath);
    if (match.Success)
    {
      this.isDerivedFromJTFile = true;
      this.jtFilePath = $"{match.Groups["part_name"].Value}.{match.Groups["form_ext"].Value}";
    }
    else
    {
      this.isDerivedFromJTFile = false;
      this.jtFilePath = (string) null;
    }
  }

  /// <summary>
  /// Возвращает идентификатор JT-документа в базе IPS. Можеть быть равен Intermech.Consts.UnknownObjectId, если если документ CAD-системы не
  /// был основан на JT-документе, либо JT-документ не зарегистрирован в базе IPS.
  /// </summary>
  public long JTDocumentId
  {
    get
    {
      this.GetDataIfEmpty();
      if (!this.jtDocId.HasValue)
        this.jtDocId = new long?(this.isDerivedFromJTFile ? this.FindJTDocumentId() : 0L);
      return this.jtDocId.Value;
    }
  }

  private long FindJTDocumentId()
  {
    FileOrigin fileOrigin = ClientContext.FileVault.WorkArea.GetFileOrigin(this.jtFilePath, false);
    if (fileOrigin.OriginType == FileOriginType.NewFile)
      return 0;
    if (fileOrigin.OriginType == FileOriginType.WorkFile)
      return fileOrigin.WorkObject.ObjectId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
      return sessionKeeper.Session.GetObjectByVersionsRule(fileOrigin.Id, editorRule.OwnerId, true).ObjectID;
    }
  }
}
