// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Scripting.CSharpScriptServerContext
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Diagnostics;


namespace Intermech.Kernel.Scripting;

internal sealed class CSharpScriptServerContext : 
  LongLifeObject,
  ICSharpScriptServerContext,
  ICSharpScriptContext
{
  private Lazy<ICSharpScriptExecutor> scriptExecutor;
  private IMetaDataHelper metaDataHelper;
  private IDBTimedEvents dbTimedEvents;

  public CSharpScriptServerContext(
    Lazy<ICSharpScriptExecutor> scriptExecutor,
    IMetaDataHelper metaDataHelper,
    IDBTimedEvents dbTimedEvents)
  {
    if (scriptExecutor == null)
      throw new ArgumentNullException(nameof (scriptExecutor));
    if (scriptExecutor == null)
      throw new ArgumentNullException(nameof (metaDataHelper));
    if (dbTimedEvents == null)
      throw new ArgumentNullException(nameof (dbTimedEvents));
    this.scriptExecutor = scriptExecutor;
    this.metaDataHelper = metaDataHelper;
    this.dbTimedEvents = dbTimedEvents;
  }

  public ICSharpScriptExecutor ScriptExecutor
  {
    [DebuggerStepThrough] get => this.scriptExecutor.Value;
  }

  public IMetaDataHelper MetaDataHelper
  {
    [DebuggerStepThrough] get => this.metaDataHelper;
  }

  [Obsolete("Вместо обращения к этому свойству в ScriptContext следует объявить и использовать аналогичное свойство в классе Script", true)]
  public IDBTimedEvents DBTimedEvents => this.dbTimedEvents;
}
