// Decompiled with JetBrains decompiler
// Type: ConsoleServer.OutputViewService
// Assembly: ConsoleServer, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A2572001-4A8A-44C7-AECE-87B2080D6C9F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\ConsoleServer.exe

using Intermech.Kernel.Services;
using System;

#nullable disable
namespace ConsoleServer;

internal sealed class OutputViewService : OutputViewServiceBase
{
  protected override void OnAfterWriteString(string category, string text)
  {
    base.OnAfterWriteString(category, text);
    Console.WriteLine(this.CombineCategoryWithText(category, text));
  }
}
