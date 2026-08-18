// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.CodeHandlersFactory
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Server.Sync;

internal class CodeHandlersFactory
{
  private Dictionary<int, CodeHandler> _handlers = new Dictionary<int, CodeHandler>();

  public void Register(CodeHandler handler, params int[] codes)
  {
    if (codes == null)
      return;
    foreach (int code in codes)
    {
      if (!this._handlers.ContainsKey(code))
        this._handlers.Add(code, handler);
    }
  }

  public void UnRegister(params int[] codes)
  {
    if (codes == null)
      return;
    foreach (int code in codes)
    {
      if (this._handlers.ContainsKey(code))
        this._handlers.Remove(code);
    }
  }

  public CodeHandler GetHandler(int code)
  {
    CodeHandler codeHandler;
    return !this._handlers.TryGetValue(code, out codeHandler) ? (CodeHandler) null : codeHandler;
  }

  public int[] GetHandledCodes() => this._handlers.Keys.ToArray<int>();
}
