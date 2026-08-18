
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.ViewProcess
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers;

internal class ViewProcess : Panel
{
  private Process _process;
  private IntPtr _hWndDocked = IntPtr.Zero;
  private IntPtr _hWndOriginalParent = IntPtr.Zero;
  private IntPtr _originalStyle = IntPtr.Zero;

  /// <summary>Приложение вместе с которыми запускается процесс.</summary>
  public string FileName { get; set; }

  /// <summary>задает набор аргументов командной строки, используемых при запуске приложения.</summary>
  public string Arguments { get; set; }

  public string WorkingDirectory { get; set; }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this.ReleaseProcess();
    base.Dispose(disposing);
  }

  /// <summary>Ensures the the process' window is docked to the panel.</summary>
  private void ResizeWindow(bool ShowWindow)
  {
    if (!(this._hWndDocked != IntPtr.Zero))
      return;
    SetWindowPosFlags setWindowPosFlags = SetWindowPosFlags.SWP_DRAWFRAME | SetWindowPosFlags.SWP_NOZORDER;
    Win32.SetWindowPos(this._hWndDocked, new IntPtr(0), 0, 0, this.Width, this.Height, !ShowWindow ? setWindowPosFlags | SetWindowPosFlags.SWP_NOACTIVATE : setWindowPosFlags | SetWindowPosFlags.SWP_SHOWWINDOW);
  }

  /// <summary>Update display of the executable</summary>
  /// <param name="e">Not used</param>
  protected override void OnResize(EventArgs e)
  {
    base.OnResize(e);
    if (this._process == null)
      return;
    this.ResizeWindow(false);
  }

  /// <summary>Closes panel</summary>
  /// <param name="e">Not used</param>
  protected override void OnHandleDestroyed(EventArgs e)
  {
    if (this._process != null)
      this.ReleaseProcess();
    base.OnHandleDestroyed(e);
  }

  /// <summary>Closes the process (if any) that has been loaded into the panel</summary>
  public void ReleaseProcess()
  {
    if (this._process == null)
      return;
    List<int> procIds = new List<int>() { this._process.Id };
    this.GetChildProcess(this._process.Id, (ICollection<int>) procIds);
    procIds.Reverse();
    foreach (int processId in procIds)
    {
      Process process = (Process) null;
      try
      {
        process = Process.GetProcessById(processId);
        if (!process.HasExited)
        {
          if (!process.CloseMainWindow())
            process.Kill();
          process.WaitForExit(200);
        }
      }
      catch
      {
      }
      finally
      {
        this._hWndDocked = IntPtr.Zero;
        try
        {
          process?.Kill();
        }
        catch
        {
        }
      }
    }
    this._process = (Process) null;
  }

  public ProcessStartInfo CreateInfo(string fileName, string arguments)
  {
    return new ProcessStartInfo()
    {
      UseShellExecute = false,
      CreateNoWindow = true,
      WindowStyle = ProcessWindowStyle.Minimized,
      FileName = fileName,
      Arguments = arguments ?? string.Empty,
      WorkingDirectory = this.WorkingDirectory ?? Environment.SystemDirectory
    };
  }

  private void GetChildProcess(int parProcId, ICollection<int> procIds)
  {
    ManagementObjectCollection objectCollection = new ManagementObjectSearcher("SELECT * FROM Win32_Process WHERE ParentProcessId=" + (object) parProcId).Get();
    if (objectCollection.Count == 0)
      return;
    foreach (ManagementBaseObject managementBaseObject in objectCollection)
    {
      int parProcId1 = (int) (uint) managementBaseObject["ProcessId"];
      if (parProcId1 != parProcId && !procIds.Contains(parProcId1))
      {
        procIds.Add(parProcId1);
        this.GetChildProcess(parProcId1, procIds);
      }
    }
  }

  /// <summary>Получить Handle окна процесса</summary>
  /// <param name="process"></param>
  /// <returns></returns>
  private IntPtr GetProcessWindowHandle(Process process)
  {
    IntPtr processWindowHandle = IntPtr.Zero;
    if (process == null)
      return processWindowHandle;
    try
    {
      processWindowHandle = this.GetProcWindowHandle(process);
      if (processWindowHandle != IntPtr.Zero)
        return processWindowHandle;
      List<int> procIds = new List<int>();
      this.GetChildProcess(process.Id, (ICollection<int>) procIds);
      foreach (int processId in procIds)
      {
        try
        {
          IntPtr procWindowHandle = this.GetProcWindowHandle(Process.GetProcessById(processId));
          if (procWindowHandle != IntPtr.Zero)
          {
            processWindowHandle = procWindowHandle;
            break;
          }
        }
        catch
        {
        }
      }
    }
    catch
    {
      Thread.Sleep(50);
      this.ReleaseProcess();
    }
    return processWindowHandle;
  }

  private IntPtr GetProcWindowHandle(Process process)
  {
    IntPtr procWindowHandle = IntPtr.Zero;
    for (int index = 0; index < 4; ++index)
    {
      if (process.MainWindowHandle == IntPtr.Zero || !Win32.IsWindowVisible(process.MainWindowHandle))
      {
        Thread.Sleep(500);
        process.Refresh();
      }
      else
      {
        process.WaitForInputIdle();
        break;
      }
    }
    if (process.MainWindowHandle != IntPtr.Zero && Win32.IsWindowVisible(process.MainWindowHandle))
      procWindowHandle = process.MainWindowHandle;
    return procWindowHandle;
  }

  public void AttachProcess(Process process)
  {
    this._process = process;
    this._hWndDocked = this.GetProcessWindowHandle(this._process);
    if (this._hWndDocked != IntPtr.Zero)
    {
      this._originalStyle = Win32.GetWindowLongPtr(this._hWndDocked, WindowLongFlags.GWL_STYLE);
      Win32.SetWindowStyles(this._hWndDocked, WindowStyles.WS_CAPTION | WindowStyles.WS_CHILD);
      this._hWndOriginalParent = Win32.SetParent(this._hWndDocked, this.Handle);
      Win32.SetWindowLongPtr(this._hWndDocked, WindowLongFlags.GWL_HWNDPARENT, this.Handle);
      Win32.UnsetWindowStyles(this._hWndDocked, WindowStyles.WS_GROUP | WindowStyles.WS_MAXIMIZEBOX);
      Win32.UnsetWindowStyles(this._hWndDocked, WindowStyles.WS_CAPTION);
      Win32.ShowWindow(this._hWndDocked, ShowWindowCommands.Maximize);
      this.ResizeWindow(true);
    }
    this.ResizeWindow(true);
  }

  public void DetachProcess()
  {
    Win32.SetWindowLongPtr(this._hWndDocked, WindowLongFlags.GWL_STYLE, this._originalStyle);
    Win32.SetParent(this._hWndDocked, this._hWndOriginalParent);
  }

  /// <summary>Loads the specified process into the panel.</summary>
  public void LoadProcess()
  {
    if (this._process != null)
      throw new Exception("A process is already associated with this panel. Use ReleaseProcess first.");
    this.AttachProcess(Process.Start(this.CreateInfo(this.FileName, this.Arguments ?? "")));
  }
}
