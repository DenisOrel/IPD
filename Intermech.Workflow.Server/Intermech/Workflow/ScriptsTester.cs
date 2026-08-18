// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.ScriptsTester
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Intermech.Workflow;

internal class ScriptsTester
{
  private string scriptCode = "\r\nusing System;\r\nusing Intermech.Interfaces;\r\nusing Intermech.Interfaces.Workflow;\r\n\r\npublic class Script\r\n{\r\n public static int Execute (IActivity activity, int param)\r\n {\r\nSystem.Threading.Thread.Sleep(new Random().Next(0, 4)); //0 to 3 seconds\r\nint i = %i%;\r\nif (i != param)\r\n throw new Exception(i.ToString());\r\n//throw new Exception(i.ToString());\r\nreturn 0;\r\n }\r\n}";
  private long scriptCounter;
  private object _logLock = new object();

  private void WriteLog(string message, string fn = "c:\\temp\\wf.log")
  {
    lock (this._logLock)
    {
      using (FileStream fileStream = new FileStream(fn, FileMode.Append, FileAccess.Write, FileShare.Read))
      {
        StreamWriter streamWriter = new StreamWriter((Stream) fileStream, Encoding.UTF8);
        try
        {
          streamWriter.WriteLine($"{DateTime.Now:G}\t{message}");
        }
        finally
        {
          streamWriter.Close();
        }
      }
    }
  }

  private bool TestScript(int i)
  {
    Interlocked.Increment(ref this.scriptCounter);
    long scriptCounter = this.scriptCounter;
    string str1 = ScriptExecHelper.IsolatedExecScript(this.scriptCode.Replace("%i%", i.ToString()), CSharpScriptInvocationOptions.Default, null, (object) i);
    string str2 = !string.IsNullOrEmpty(str1) ? $"FAIL! {str1}!={i}" : string.Empty;
    this.WriteLog($"End script\t{scriptCounter,5} ({i})\t{str2}");
    return str1 == i.ToString();
  }

  public void TestScripts()
  {
    List<int> source = new List<int>();
    Random random = new Random();
    for (int index = 1; index <= 100000; ++index)
      source.Add(random.Next(30));
    new ParallelOptions().MaxDegreeOfParallelism = 200;
    Parallel.ForEach<int>((IEnumerable<int>) source, (Action<int>) (key => this.TestScript(key)));
  }
}
