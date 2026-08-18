// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADInterfaceApiResourceManager
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Diagnostics;
using Intermech.IO;
using Interop.CADInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует менеджер ресурсов CAD-системы, использованных интегратором в сессии подключения к API приложения.
/// Менеджер позволяет сохранить информацию о ресурсах приложения (COM-объекты и др.), открытых интегратором, а при
/// закрытии сессии подключения к API приложения - освободить записанные ресурсы.
/// </summary>
internal sealed class CADInterfaceApiResourceManager : 
  ApplicationApiResourceManager,
  ICADSystemResourceTracker
{
  private readonly CADSystemProxy cadProxy;
  private readonly PathDictionary<CADInterfaceApiResourceManager.ModelConfigurationHistoryRecord> openConfigurationHistory;
  private readonly PathDictionary<CADInterfaceApiResourceManager.DocumentHistoryRecord> openDocumentHistory;
  private int openHistorySeqIndex;

  /// <summary>Создает объект.</summary>
  /// <param name="cadProxy">API-объект CAD-системы</param>
  /// <exception cref="T:System.ArgumentNullException">cadProxy</exception>
  public CADInterfaceApiResourceManager(CADSystemProxy cadProxy)
  {
    this.cadProxy = cadProxy != null ? cadProxy : throw new ArgumentNullException(nameof (cadProxy));
    this.openConfigurationHistory = new PathDictionary<CADInterfaceApiResourceManager.ModelConfigurationHistoryRecord>(128 /*0x80*/);
    this.openDocumentHistory = new PathDictionary<CADInterfaceApiResourceManager.DocumentHistoryRecord>(128 /*0x80*/);
  }

  public void TrackOpenDocument(string fullPath, bool alreadyOpen)
  {
    if (this.openDocumentHistory.ContainsKey(fullPath))
      return;
    this.openDocumentHistory.Add(fullPath, new CADInterfaceApiResourceManager.DocumentHistoryRecord(alreadyOpen, this.openHistorySeqIndex++));
  }

  public void TrackOpenConfiguration(IModelConfiguration modelConfiguration, bool alreadyOpen)
  {
    string fullPath = modelConfiguration.FullPath;
    if (!this.openConfigurationHistory.ContainsKey(fullPath))
      this.openConfigurationHistory.Add(fullPath, new CADInterfaceApiResourceManager.ModelConfigurationHistoryRecord(modelConfiguration, alreadyOpen, this.openHistorySeqIndex++));
    if (string.IsNullOrEmpty(fullPath) || !Path.IsPathRooted(fullPath))
      return;
    this.TrackOpenDocument(fullPath, alreadyOpen);
  }

  /// <summary>
  /// Активирует сохранение информации о ресурсах приложения (COM-объекты и др.), открытых интегратором.
  /// </summary>
  protected override void DoStart()
  {
    base.DoStart();
    this.cadProxy.ApiResourceTracker = (ICADSystemResourceTracker) this;
  }

  /// <summary>
  /// Освобождает ресурсы приложения, открытые интегратором, а также деактивирует сохранение информации об открытых ресурсах приложения.
  /// Метод не должен сбрасывать исключения. Все ошибки освобождения ресурсов приложения должны сохраняться в коллекции Errors.
  /// </summary>
  protected override void DoReleaseResourcesAndStop()
  {
    this.cadProxy.ApiResourceTracker = (ICADSystemResourceTracker) null;
    this.ConserveAppResources();
    base.DoReleaseResourcesAndStop();
  }

  private void ConserveAppResources()
  {
    try
    {
      if (this.openConfigurationHistory.Count > 0)
        this.ClearConfigurationHistory();
      if (this.openDocumentHistory.Count <= 0)
        return;
      this.ClearDocumentHistory();
    }
    finally
    {
      this.openConfigurationHistory.Clear();
      this.openDocumentHistory.Clear();
    }
  }

  private void ClearConfigurationHistory()
  {
    SortedList<int, string> sortedList = new SortedList<int, string>(this.openConfigurationHistory.Count);
    foreach (KeyValuePair<string, CADInterfaceApiResourceManager.ModelConfigurationHistoryRecord> keyValuePair in (Dictionary<string, CADInterfaceApiResourceManager.ModelConfigurationHistoryRecord>) this.openConfigurationHistory)
    {
      if (!keyValuePair.Value.AlreadyOpen && keyValuePair.Value.RawObject.IsAlive)
        sortedList.Add(keyValuePair.Value.SeqIndex, keyValuePair.Key);
    }
    if (sortedList.Count <= 0)
      return;
    LinkedList<string> list = new LinkedList<string>();
    foreach (KeyValuePair<int, string> keyValuePair in sortedList)
      list.AddFirst(keyValuePair.Value);
    if (CADInterfaceTracing.Proxies.TraceVerbose)
      this.TraceList("A list of model configurations to close", (ICollection<string>) list);
    foreach (string key in list)
    {
      try
      {
        if (this.openConfigurationHistory[key].RawObject.Target is IModelConfiguration target)
          target.Close();
        if (CADInterfaceTracing.Proxies.TraceVerbose)
          Trace.WriteLine($"A configuration '{key}' reported as closed.");
      }
      catch (Exception ex)
      {
        string message = $"Unable to close a configuration '{key}'";
        this.Errors.Add(ErrorInfo.FromException(ex, message));
        if (CADInterfaceTracing.Proxies.TraceVerbose)
          Trace.WriteLine(message);
      }
    }
  }

  private void ClearDocumentHistory()
  {
    SortedList<int, string> sortedList = new SortedList<int, string>(this.openDocumentHistory.Count);
    foreach (KeyValuePair<string, CADInterfaceApiResourceManager.DocumentHistoryRecord> keyValuePair in (Dictionary<string, CADInterfaceApiResourceManager.DocumentHistoryRecord>) this.openDocumentHistory)
    {
      if (!keyValuePair.Value.AlreadyOpen)
        sortedList.Add(keyValuePair.Value.SeqIndex, keyValuePair.Key);
    }
    if (sortedList.Count <= 0)
      return;
    LinkedList<string> linkedList = new LinkedList<string>();
    foreach (KeyValuePair<int, string> keyValuePair in sortedList)
      linkedList.AddFirst(keyValuePair.Value);
    if (CADInterfaceTracing.Proxies.TraceVerbose)
      this.TraceList("A list of model files to close", (ICollection<string>) linkedList);
    this.cadProxy.CloseFiles((ICollection<string>) linkedList);
  }

  private void TraceList(string listCaption, ICollection<string> list)
  {
    if (string.IsNullOrEmpty(listCaption))
      throw new ArgumentNullException();
    Trace.WriteLine(listCaption);
    this.TraceList(list);
  }

  private void TraceList(ICollection<string> list)
  {
    if (list == null)
      throw new ArgumentNullException(nameof (list));
    Trace.Indent();
    Trace.WriteLine($"(II) list items count: {list.Count}");
    int num = 1;
    foreach (string str in (IEnumerable<string>) list)
      Trace.WriteLine($"{num++}: {str}");
    Trace.Unindent();
  }

  private sealed class ModelConfigurationHistoryRecord
  {
    private readonly WeakReference rawObject;
    private readonly bool alreadyOpen;
    private readonly int seqIndex;

    public ModelConfigurationHistoryRecord(
      IModelConfiguration rawObject,
      bool alreadyOpen,
      int seqIndex)
    {
      this.rawObject = rawObject != null ? new WeakReference((object) rawObject, false) : throw new ArgumentNullException(nameof (rawObject));
      this.alreadyOpen = alreadyOpen;
      this.seqIndex = seqIndex;
    }

    public WeakReference RawObject => this.rawObject;

    public bool AlreadyOpen => this.alreadyOpen;

    public int SeqIndex => this.seqIndex;
  }

  private sealed class DocumentHistoryRecord
  {
    private readonly bool alreadyOpen;
    private readonly int seqIndex;

    public DocumentHistoryRecord(bool alreadyOpen, int seqIndex)
    {
      this.alreadyOpen = alreadyOpen;
      this.seqIndex = seqIndex;
    }

    public bool AlreadyOpen => this.alreadyOpen;

    public int SeqIndex => this.seqIndex;
  }
}
