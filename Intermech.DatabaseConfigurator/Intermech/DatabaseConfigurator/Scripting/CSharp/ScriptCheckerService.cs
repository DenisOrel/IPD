// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripting.CSharp.ScriptCheckerService
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces;
using Intermech.Scripting.CSharp.Hosting;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripting.CSharp;

internal sealed class ScriptCheckerService
{
  private ScriptCheckerIDCache idCache;
  private CSharpScriptCodeAnalyzer scriptCodeAnalyzer;

  public ScriptCheckerService(ScriptCheckerIDCache idCache)
  {
    this.idCache = idCache != null ? idCache : throw new ArgumentNullException(nameof (idCache));
    this.scriptCodeAnalyzer = new CSharpScriptCodeAnalyzer();
  }

  public List<ScriptCheckResult> CanExecuteInSandbox(ICollection<ScriptInfo> scripts)
  {
    List<ScriptCheckResult> scriptCheckResultList = scripts != null ? new List<ScriptCheckResult>(scripts.Count) : throw new ArgumentNullException(nameof (scripts));
    foreach (ScriptInfo script in (IEnumerable<ScriptInfo>) scripts)
    {
      ScriptCheckResult scriptCheckResult = this.CheckForCanExecuteInSandbox(script);
      scriptCheckResultList.Add(scriptCheckResult);
    }
    return scriptCheckResultList;
  }

  private ScriptCheckResult CheckForCanExecuteInSandbox(ScriptInfo scriptInfo)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(scriptInfo.ObjectId, false);
      if (dbObject == null)
        return new ScriptCheckResult(scriptInfo, false, "Не удалось проанализировать сценарий, так он не был найден в базе данных.");
      IDBAttribute attributeById = dbObject.GetAttributeByID(this.idCache.ScriptCode.Id);
      if (attributeById == null || attributeById.IsNull)
        return new ScriptCheckResult(scriptInfo, false, $"Не удалось проанализировать сценарий, так как у него отсутствует атрибут '{this.idCache.ScriptCode.Text}'.");
      return this.scriptCodeAnalyzer.CanExecuteInSandbox((string) attributeById.Value) ? new ScriptCheckResult(scriptInfo, true, "Код сценария не требует преобразования.") : new ScriptCheckResult(scriptInfo, false, "Код сценария должен быть преобразован в соответствии с правилами, приведенными в руководстве программиста. Класс сценария содержать конструктор, экземплярный метод Execute и экземплярное свойство ScriptContext типа ICSharpScriptContext. Использование в коде сценария статических полей данных и статических свойств, доступных для записи, запрещено.");
    }
  }
}
