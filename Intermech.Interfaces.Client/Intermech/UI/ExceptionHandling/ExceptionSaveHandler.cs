// Decompiled with JetBrains decompiler
// Type: Intermech.UI.ExceptionHandling.ExceptionSaveHandler
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.UI.ExceptionHandling;

public class ExceptionSaveHandler
{
  public void SaveToFile(Exception exception, string reportZipName)
  {
    if (exception == null)
      throw new ArgumentNullException(nameof (exception));
    if (reportZipName == null)
      throw new ArgumentNullException(nameof (reportZipName));
    this.DoSaveToFile(exception, reportZipName);
  }

  protected virtual void DoSaveToFile(Exception exception, string reportZipName)
  {
    new Intermech.Interfaces.Client.InformationRequest.InformationRequest().SaveReportToXml(exception, reportZipName);
  }

  public void SendByEmail(Exception exception, string reportTopic, string reportText)
  {
    if (exception == null)
      throw new ArgumentNullException(nameof (exception));
    if (reportTopic == null)
      throw new ArgumentNullException(nameof (reportTopic));
    if (reportText == null)
      throw new ArgumentNullException(nameof (reportText));
    this.DoSendByEmail(exception, reportTopic, reportText);
  }

  protected virtual void DoSendByEmail(Exception exception, string reportTopic, string reportText)
  {
    new Intermech.Interfaces.Client.InformationRequest.InformationRequest().SendReport(exception, reportTopic, reportText);
  }
}
