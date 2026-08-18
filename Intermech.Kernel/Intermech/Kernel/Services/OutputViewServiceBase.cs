// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.OutputViewServiceBase
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services;

public class OutputViewServiceBase : LongLifeObject, IOutputView, IOutputViewHistory
{
  private object syncRoot;
  private Dictionary<string, string> categoryTable;

  public OutputViewServiceBase()
  {
    this.syncRoot = new object();
    this.categoryTable = new Dictionary<string, string>();
  }

  public void WriteString(string category, string text)
  {
    if (string.IsNullOrEmpty(category) || text == null)
      return;
    lock (this.syncRoot)
    {
      string str1;
      if (this.categoryTable.TryGetValue(category, out str1))
      {
        string str2 = str1 + Environment.NewLine + text;
        this.categoryTable[category] = str2;
      }
      else
      {
        string str3 = text;
        this.categoryTable.Add(category, str3);
      }
      this.OnAfterWriteString(category, text);
    }
  }

  protected virtual void OnAfterWriteString(string category, string text)
  {
  }

  public void ClearText(string category)
  {
    if (string.IsNullOrEmpty(category))
      return;
    lock (this.syncRoot)
    {
      this.categoryTable.Remove(category);
      this.OnAfterClearText(category);
    }
  }

  protected virtual void OnAfterClearText(string category)
  {
  }

  public void Activate(string category)
  {
  }

  public void ShowView()
  {
  }

  protected string CombineCategoryWithText(string category, string text)
  {
    if (category == null)
      throw new ArgumentNullException(nameof (category));
    if (text == null)
      throw new ArgumentNullException(nameof (text));
    return $"[{category}] {text}";
  }

  public List<Tuple<string, string>> GetOutputHistory()
  {
    lock (this.syncRoot)
    {
      List<Tuple<string, string>> outputHistory = new List<Tuple<string, string>>(this.categoryTable.Count);
      foreach (KeyValuePair<string, string> keyValuePair in this.categoryTable)
        outputHistory.Add(Tuple.Create<string, string>(keyValuePair.Key, keyValuePair.Value));
      return outputHistory;
    }
  }
}
