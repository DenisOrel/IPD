// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlScripts.XmlImportScriptCommand
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Interfaces.XmlExchange.Services.Import;
using Intermech.Localization.Xml;
using System;
using System.IO;
using System.Reflection;

#nullable disable
namespace Intermech.Interfaces.XmlExchange.XmlScripts;

/// <summary>
/// Класс - скрипт импорт данных из XML в фоновых задачах (маршрутизаторе, планировщике задач)
/// </summary>
public static class XmlImportScriptCommand
{
  /// <summary>Импорт данных из XML</summary>
  /// <param name="importData"></param>
  /// <param name="xmlConfigId"></param>
  /// <param name="session"></param>
  /// <param name="importLog"></param>
  /// <returns></returns>
  public static bool Execute(
    string importData,
    long xmlConfigId,
    IUserSession session,
    out string importLog)
  {
    importLog = string.Empty;
    if (importData == string.Empty || !File.Exists(importData) || xmlConfigId == 0L || session == null)
      return false;
    IXmlExchangeService service = ServiceUtils.GetService<IXmlExchangeService>((object) session, true);
    IXmlExchangeImportTask importTask = service.CreateImportTask(session.SessionGUID);
    if (importTask == null)
      return false;
    try
    {
      try
      {
        using (FileStream fileStream = File.OpenRead(importData))
        {
          byte[] buffer = new byte[524288 /*0x080000*/];
          for (int bufferSize = fileStream.Read(buffer, 0, buffer.Length); bufferSize > 0; bufferSize = fileStream.Read(buffer, 0, buffer.Length))
            importTask.UploadData(importTask.TaskGuid.ToString() + ".zip", buffer, bufferSize, true);
        }
        importTask.Execute(new XmlExchangeImportTaskParams(xmlConfigId));
      }
      finally
      {
        importLog = importTask.Log;
        if (importTask.HasError)
        {
          string str = Environment.NewLine + LocalizationHolder.rm.GetString("Interfaces.XmlExchange_9") + Environment.NewLine + Environment.NewLine + importLog;
          throw new TargetInvocationException(LocalizationHolder.rm.GetString("Interfaces.XmlExchange_10") + str, importTask.Exception);
        }
      }
    }
    finally
    {
      service.DisposeImportTask(importTask.TaskGuid);
    }
    return true;
  }
}
