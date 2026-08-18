// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.LocalizationHolder
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.Interfaces.Workflow;

public class LocalizationHolder
{
  public static ResourceManager rm = new ResourceManager("Intermech.Interfaces.Workflow.Resources.InterfacesWorkflowResources", Assembly.GetExecutingAssembly());
  public static ResourceManager rma = new ResourceManager("Intermech.Interfaces.Workflow.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());

  public static string GetCustomDescription(string name) => new CustomDisplayName(name).DisplayName;

  public static string GetString(string name, bool returnNameIfNotFound)
  {
    string str = LocalizationHolder.rm.GetString(name);
    if (str != null)
      return str;
    return returnNameIfNotFound ? name : "";
  }

  public static string GetString(string name) => LocalizationHolder.GetString(name, true);
}
