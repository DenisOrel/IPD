// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.Program
// Assembly: Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EB4A0A0B-E62B-4D21-A944-3B5D877E45CE
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.exe

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App;

internal static class Program
{
  [STAThread]
  private static void Main(string[] args)
  {
    InputParams inputParams = new InputParams();
    if (args.Length != 0)
    {
      inputParams.UserName = ((IEnumerable<string>) args).FirstOrDefault<string>((Func<string, bool>) (param => param.StartsWith("-usr:")))?.Replace("-usr:", string.Empty) ?? string.Empty;
      inputParams.UserPassword = ((IEnumerable<string>) args).FirstOrDefault<string>((Func<string, bool>) (param => param.StartsWith("-pwd:")))?.Replace("-pwd:", string.Empty) ?? string.Empty;
      inputParams.UserRole = ((IEnumerable<string>) args).FirstOrDefault<string>((Func<string, bool>) (param => param.StartsWith("-role:")))?.Replace("-role:", string.Empty) ?? string.Empty;
      inputParams.InputFile = ((IEnumerable<string>) args).FirstOrDefault<string>((Func<string, bool>) (param => param.StartsWith("-src:")))?.Replace("-src:", string.Empty).Replace("\"", "") ?? string.Empty;
      inputParams.ConfigFile = ((IEnumerable<string>) args).FirstOrDefault<string>((Func<string, bool>) (param => param.StartsWith("-cfg:")))?.Replace("-cfg:", string.Empty).Replace("\"", "") ?? string.Empty;
      inputParams.WorkDir = ((IEnumerable<string>) args).FirstOrDefault<string>((Func<string, bool>) (param => param.StartsWith("-wrkdir:")))?.Replace("-wrkdir:", string.Empty).Replace("\"", "") ?? string.Empty;
      string s = ((IEnumerable<string>) args).FirstOrDefault<string>((Func<string, bool>) (param => param.StartsWith("-sp:")))?.Replace("-sp:", string.Empty) ?? string.Empty;
      int result;
      inputParams.ShowProgress = !int.TryParse(s, out result) ? new bool?() : new bool?(result > 0);
    }
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    InputParams sourceParams = AppConfig.ReadConfig();
    inputParams.AssignNotEmpty(sourceParams);
    if (!AppConfig.CheckInputParams(inputParams) || args.Length == 0)
    {
      InputParamsForm inputParamsForm = new InputParamsForm();
      inputParamsForm.InputParams.Assign((object) inputParams);
      if (inputParamsForm.ShowDialog() != DialogResult.OK)
        return;
      inputParams.Assign((object) inputParamsForm.InputParams);
    }
    AppConfig.SaveConfig(inputParams);
    Application.Run((ApplicationContext) new Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.Converter(inputParams));
  }
}
