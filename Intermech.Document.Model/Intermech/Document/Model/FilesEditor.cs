// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.FilesEditor
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>
/// Класс отвечает за редактирование файлов (пока containerelement) выгружая их на диск
/// </summary>
public class FilesEditor
{
  private FileSystemWatcher fw;
  private List<FileData> files = new List<FileData>();
  private static FilesEditor instance;
  private List<FileData> listToSave = new List<FileData>();

  public void InitWatcher()
  {
    if (this.fw != null)
      return;
    this.CleanUp();
    this.fw = new FileSystemWatcher();
    this.fw.Path = this.TempPath;
    this.fw.IncludeSubdirectories = true;
    this.fw.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Attributes | NotifyFilters.Size | NotifyFilters.LastWrite | NotifyFilters.LastAccess;
    this.fw.Changed += new FileSystemEventHandler(this.fw_Changed);
    this.fw.Deleted += new FileSystemEventHandler(this.fw_Deleted);
    this.fw.EnableRaisingEvents = true;
  }

  /// <summary>Очистка папки темп</summary>
  private void CleanUp()
  {
    try
    {
      try
      {
        if (Directory.Exists(this.TempPath))
          Directory.Delete(this.TempPath, true);
      }
      catch
      {
      }
      if (Directory.Exists(this.TempPath))
        return;
      Directory.CreateDirectory(this.TempPath);
    }
    catch
    {
    }
  }

  private void fw_Changed(object sender, FileSystemEventArgs e)
  {
    FileData data = this.files.Find((Predicate<FileData>) (x => x.FileName == e.FullPath));
    if (data == null || (DateTime.Now - data.EditStartTime).TotalSeconds <= 5.0 || this.listToSave.Find((Predicate<FileData>) (x => x.FileName == data.FileName)) != null)
      return;
    this.listToSave.Add(data);
    new Thread(new ParameterizedThreadStart(this.SaveDataInThread)).Start((object) data);
  }

  public void SaveDataInThread(object data)
  {
    Thread.Sleep(4000);
    this.SaveData(data as FileData);
    this.listToSave.Remove(data as FileData);
  }

  /// <summary>Сохранение файла в DocumentTreeNode</summary>
  /// <param name="data"></param>
  private void SaveData(FileData data)
  {
    DocumentTreeNode node = data.Node;
    if (node.OwnerDocument == null || !(node is ContainerElement containerElement))
      return;
    containerElement.LoadDataObjectFromFile(data.FileName);
  }

  private void fw_Deleted(object sender, FileSystemEventArgs e)
  {
  }

  public static FilesEditor Instance
  {
    get
    {
      if (FilesEditor.instance == null)
        FilesEditor.instance = new FilesEditor();
      return FilesEditor.instance;
    }
    set => FilesEditor.instance = value;
  }

  /// <summary>Запуск файла на редактирование</summary>
  /// <param name="node">Элемент, файл в котором редактируем</param>
  /// <param name="inputStream">Поток в котором находится файл</param>
  /// <param name="fileName">Имя файла</param>
  public void EditFile(DocumentTreeNode node, Stream inputStream, string fileName)
  {
    this.InitWatcher();
    string str = Path.Combine(this.TempPath, Path.GetRandomFileName());
    if (!Directory.Exists(str))
      Directory.CreateDirectory(str);
    string fileName1 = Path.Combine(str, fileName);
    this.WriteFile(node, inputStream, fileName1);
    ProcessStartInfo startInfo = new ProcessStartInfo();
    startInfo.UseShellExecute = true;
    startInfo.FileName = fileName1;
    startInfo.Verb = "open";
    try
    {
      Process.Start(startInfo)?.Dispose();
      this.files.RemoveAll((Predicate<FileData>) (x => x.FileName == fileName));
      this.files.Add(new FileData()
      {
        FileName = fileName1,
        Node = node,
        EditStartTime = DateTime.Now
      });
    }
    catch (Win32Exception ex)
    {
      throw new FaultException(ex.Message, (Exception) ex);
    }
  }

  private string WriteFile(DocumentTreeNode node, Stream inputStream, string fileName)
  {
    using (FileStream destination = new FileStream(fileName, FileMode.Create, FileAccess.Write))
      inputStream.CopyTo((Stream) destination);
    return fileName;
  }

  /// <summary>Путь к папке temp</summary>
  public virtual string TempPath => Path.GetTempPath() + "ImDocEditor";
}
