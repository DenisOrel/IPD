// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.IEventHandlerSet`2
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System;


namespace Intermech.Kernel;

internal interface IEventHandlerSet<TSender, TEventArgs>
  where TSender : class
  where TEventArgs : EventArgs
{
  void AddHandler(object eventKey, Action<TSender, TEventArgs> handler);

  void RemoveHandler(object eventKey, Action<TSender, TEventArgs> handler);

  void Fire(object eventKey, TSender sender, TEventArgs e);
}
