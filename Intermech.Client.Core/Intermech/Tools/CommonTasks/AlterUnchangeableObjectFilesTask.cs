
// Type: Intermech.Tools.CommonTasks.AlterUnchangeableObjectFilesTask
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using Intermech.Interfaces;
using System;
using System.IO;


namespace Intermech.Tools.CommonTasks;

/// <summary>
/// Базовый класс для задач модификации файлов документов, когда в файл документа требуется записать служебные сведения о состоянии или статусе документа.
/// Такая возможность поддерживается для любых версий документов, которые не могут быть изменены через рабочую копию.
/// </summary>
public abstract class AlterUnchangeableObjectFilesTask : IAction
{
  private bool isInitialized;
  private long objectId;
  private string fileName;
  private string filePath;
  private bool canPerform;

  /// <summary>Создает объект.</summary>
  public AlterUnchangeableObjectFilesTask() => this.DoClear();

  public void Initialize(long objectId, string fileName, string filePath)
  {
    if (objectId == 0L)
      throw new ArgumentException("Не задан идентификатор версии объекта.", nameof (objectId));
    if (string.IsNullOrEmpty(fileName))
      throw new ArgumentException("Не задано имя файла объекта.", nameof (fileName));
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    if (!Path.IsPathRooted(filePath))
      throw new ArgumentException("Путь к файлу должен быть указан в абсолютной форме.", nameof (filePath));
    if (this.isInitialized)
      this.Clear();
    try
    {
      this.objectId = objectId;
      this.fileName = fileName;
      this.filePath = filePath;
      this.canPerform = this.DoInitialize();
      this.isInitialized = true;
    }
    catch
    {
      this.DoClear();
      throw;
    }
  }

  protected virtual bool DoInitialize()
  {
    if (!File.Exists(this.filePath) || this.objectId < 0L)
      return false;
    switch (this.GetObjectModifyMode(this.objectId))
    {
      case ObjectModifyModes.CreateVersion:
      case ObjectModifyModes.CantModify:
        return true;
      default:
        return false;
    }
  }

  private ObjectModifyModes GetObjectModifyMode(long objectId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObject(objectId, true).ObjectModifyMode;
  }

  public void Clear()
  {
    if (!this.isInitialized)
      return;
    this.DoClear();
    this.isInitialized = false;
  }

  /// <summary>
  /// Выполняет полную очистку внутреннего состояния. Может вызываться для очистки частично заполненного состояния. Метод не должен бросать исключений.
  /// </summary>
  protected virtual void DoClear()
  {
    this.objectId = 0L;
    this.fileName = (string) null;
    this.filePath = (string) null;
    this.canPerform = false;
  }

  public bool IsInitialized => this.isInitialized;

  private void RequireInitialized()
  {
    if (!this.isInitialized)
      throw new InvalidOperationException("Object must be initialized first.");
  }

  public long ObjectId => this.objectId;

  public string FileName => this.fileName;

  public string FilePath => this.filePath;

  public bool CanPerform => this.canPerform;

  public void Perform()
  {
    this.RequireInitialized();
    if (!this.CanPerform)
      return;
    this.DoAlterFile();
  }

  protected virtual void DoAlterFile()
  {
  }
}
