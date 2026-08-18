
// Type: Intermech.PropertyEditors.ExceptionOutput
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;


namespace Intermech.PropertyEditors;

public class ExceptionOutput
{
  public static void Write(string category, Exception e)
  {
    ExceptionOutput.Write(category, e.Message);
  }

  public static void Write(string category, string text)
  {
    ((IOutputView) ServicesManager.GetService(typeof (IOutputView)))?.WriteString(category, text);
  }
}
