// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckArraysResult
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System.Collections;
using System.Text;


namespace Intermech.Kernel.Briefcase;

internal sealed class CheckArraysResult
{
  private Hashtable Data = new Hashtable();

  public CheckArraysResult(params string[] arrays)
  {
    foreach (object array in arrays)
      this.Data.Add(array, (object) new ArrayList());
  }

  public ArrayList this[string ArrayName] => this.Data[(object) ArrayName] as ArrayList;

  public void Add(string ArrayName, object Value)
  {
    if (this.Data[(object) ArrayName] == null)
      return;
    ((ArrayList) this.Data[(object) ArrayName]).Add(Value);
  }

  public string ToString(string ArrayName)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (this.Data[(object) ArrayName] == null)
      return string.Empty;
    foreach (string str in this.Data[(object) ArrayName] as ArrayList)
      stringBuilder.Append($"\"{str}\",");
    if (stringBuilder.Length > 1)
      stringBuilder.Remove(stringBuilder.Length - 1, 1);
    return stringBuilder.ToString();
  }
}
