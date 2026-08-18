// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DwgTasks.DwgTask
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

#nullable disable
namespace Intermech.Cadmech.Integrator.DwgTasks;

internal abstract class DwgTask : IDisposable
{
  private static readonly Semaphore syncRoot = new Semaphore(1, 1);
  private static readonly Regex firstPagePattern = new Regex("^(\\w+\\s+)*(\\W)?1$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
  private string drawingFilePath;
  private string stmFilePath;
  private bool stmFilePathIsValid;
  private bool stmFileIsOpen;
  private int stampParameterCount;
  private Dictionary<StringKey, int> stampParameterMap;
  private bool isDisposed;

  protected DwgTask() => DwgTask.syncRoot.WaitOne();

  public void Dispose()
  {
    if (this.isDisposed)
      return;
    this.isDisposed = true;
    try
    {
      if (!this.IsDrawingOpen)
        return;
      this.CloseDrawing();
    }
    finally
    {
      DwgTask.syncRoot.Release();
    }
  }

  protected void CheckNotDisposed()
  {
    if (this.isDisposed)
      throw new ObjectDisposedException(this.GetType().FullName);
  }

  public void OpenDrawing(string drawingFilePath)
  {
    if (string.IsNullOrEmpty(drawingFilePath))
      throw new ArgumentException("Путь к файлу чертежа не задан.", nameof (drawingFilePath));
    this.CheckNotDisposed();
    if (this.IsDrawingOpen)
      this.CloseDrawing();
    this.drawingFilePath = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Open_DWG_Files(drawingFilePath) == 0 ? drawingFilePath : throw new FaultException($"Формат чертежа '{Path.GetFileName(drawingFilePath)}' не поддерживается сканером чертежей. Переключите в приложении настройку сохранение чертежей на формат AutoCAD 2007 и пересохраните чертеж.");
  }

  protected virtual void CloseDrawing()
  {
    this.StmFilePath = (string) null;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Close_Dwg_Files();
    this.drawingFilePath = (string) null;
  }

  protected void CheckDrawingIsOpen()
  {
    this.CheckNotDisposed();
    if (!this.IsDrawingOpen)
      throw new InvalidOperationException("Чертеж не был открыт. Воспользуйтесь методом OpenDrawing().");
  }

  public bool IsDrawingOpen
  {
    [DebuggerStepThrough] get => !string.IsNullOrEmpty(this.drawingFilePath);
  }

  public string DrawingFilePath
  {
    [DebuggerStepThrough] get => this.drawingFilePath;
  }

  protected bool TryProcessStamp(
    ICollection<StringKey> stampParameters,
    Predicate<ValueBag> stampPredicate,
    Action<ValueBag> stampAction)
  {
    if (stampPredicate == null)
      throw new ArgumentNullException(nameof (stampPredicate));
    if (stampAction == null)
      throw new ArgumentNullException(nameof (stampAction));
    this.CheckDrawingIsOpen();
    this.CheckStmFileIsOpen();
    string[] layoutNames = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.GetLayoutNames();
    for (int index = 0; index < layoutNames.Length; ++index)
    {
      if (layoutNames[index] == null)
        layoutNames[index] = string.Empty;
    }
    List<int> intList = new List<int>(layoutNames.Length);
    int index1 = Array.FindIndex<string>(layoutNames, (Predicate<string>) (item => DwgTask.firstPagePattern.IsMatch(item)));
    if (index1 >= 0)
      intList.Add(index1);
    if (Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Layout != index1)
      intList.Add(Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Layout);
    for (int index2 = 0; index2 < layoutNames.Length; ++index2)
    {
      if (!intList.Contains(index2))
        intList.Add(index2);
    }
    if (stampParameters == null)
    {
      stampParameters = (ICollection<StringKey>) new List<StringKey>(this.StampParameterCount);
      foreach (KeyValuePair<StringKey, int> stampParameter in this.StampParameterMap)
        stampParameters.Add(stampParameter.Key);
    }
    int layout = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Layout;
    try
    {
      foreach (int num in intList)
      {
        Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Layout = num;
        Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Scaning_Dwg();
        ValueBag valueBag = new ValueBag(stampParameters.Count);
        foreach (StringKey stampParameter in (IEnumerable<StringKey>) stampParameters)
        {
          int paramIndex;
          if (this.StampParameterMap.TryGetValue(stampParameter, out paramIndex) && !valueBag.Exists(stampParameter))
          {
            string str = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.GetParameter(paramIndex).Trim();
            valueBag.Add(stampParameter, (object) str);
          }
        }
        valueBag.AcceptChanges();
        if (stampPredicate(valueBag))
        {
          stampAction(valueBag);
          return true;
        }
      }
      return false;
    }
    finally
    {
      Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Layout = layout;
    }
  }

  public string StmFilePath
  {
    [DebuggerStepThrough] get => this.stmFilePath;
    [DebuggerStepThrough] set
    {
      if (object.Equals((object) this.stmFilePath, (object) value))
        return;
      this.ResetStmFileData();
      this.stmFilePathIsValid = false;
      this.stmFilePath = value;
    }
  }

  protected int StampParameterCount
  {
    [DebuggerStepThrough] get
    {
      this.CheckStmFileIsOpen();
      return this.stampParameterCount;
    }
  }

  protected Dictionary<StringKey, int> StampParameterMap
  {
    [DebuggerStepThrough] get
    {
      this.CheckStmFileIsOpen();
      this.CheckStmFilePathIsValid();
      return this.stampParameterMap;
    }
  }

  protected void CheckStmFilePathIsValid()
  {
    if (this.stmFilePathIsValid)
      return;
    if (string.IsNullOrEmpty(this.StmFilePath))
      throw new InvalidOperationException("Не задан путь к файлу с настройками сканирования штампа чертежа. Заполните свойство StmFilePath.");
    if (!File.Exists(this.StmFilePath))
      throw new InvalidOperationException($"Не удалось найти на диске файл '{this.StmFilePath}' с настройками сканирования штампа чертежа.");
    this.stmFilePathIsValid = true;
  }

  protected void CheckStmFileIsOpen()
  {
    if (this.stmFileIsOpen)
      return;
    this.CheckStmFilePathIsValid();
    try
    {
      Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Set_Scan_State((short) 1, (short) 0, (short) 0);
      int[] numArray = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Open_Scan_Files(this.stmFilePath);
      this.stampParameterCount = numArray[0] == 0 ? numArray[1] : throw new FaultException($"Файл '{this.stmFilePath}' с настройками сканирования штампа чертежа не является корректным.");
      this.stampParameterMap = new Dictionary<StringKey, int>(this.stampParameterCount);
      for (int paramIndex = 0; paramIndex < this.stampParameterCount; ++paramIndex)
      {
        StringKey nameParameter = (StringKey) Intermech.Client.Core.Show.Net.ShowDll.ShowDll.GetNameParameter(paramIndex);
        if (!this.stampParameterMap.ContainsKey(nameParameter))
          this.stampParameterMap.Add(nameParameter, paramIndex);
      }
      this.stmFileIsOpen = true;
    }
    catch
    {
      this.ResetStmFileData();
      throw;
    }
  }

  private void ResetStmFileData()
  {
    this.stmFileIsOpen = false;
    this.stampParameterCount = 0;
    this.stampParameterMap = (Dictionary<StringKey, int>) null;
  }
}
