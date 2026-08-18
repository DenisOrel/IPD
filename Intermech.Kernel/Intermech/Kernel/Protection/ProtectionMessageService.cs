// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Protection.ProtectionMessageService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Protection;
using System;
using System.Collections.Generic;
using System.Threading;


namespace Intermech.Kernel.Protection;

[ServerService(ClientVisible = true)]
internal class ProtectionMessageService : LongLifeObject, IProtectionMessageService
{
  private const int TIMER_PERIOD = 600000;
  private const int MAX_MESSAGES = 30;
  private bool _canSend = true;
  private Dictionary<string, string> _buffer = new Dictionary<string, string>();
  private int _messagesCount;
  private Timer _sendTimer;
  private object _lockSection = new object();

  public ProtectionMessageService()
  {
    this._sendTimer = new Timer(new System.Threading.TimerCallback(this.TimerCallback), (object) null, -1, 600000);
  }

  private void TimerCallback(object state)
  {
    lock (this._lockSection)
    {
      if (this._messagesCount > 0)
      {
        foreach (string key in this._buffer.Keys)
        {
          string text = this._buffer[key];
          this.InternalSend(key, text);
        }
      }
      this._messagesCount = 0;
      this._buffer.Clear();
      this.ResetTimer();
    }
  }

  private void ResetTimer() => this._sendTimer.Change(600000, 600000);

  private void AppendMessage(string subject, string text)
  {
    lock (this._lockSection)
    {
      if (this._buffer.ContainsKey(subject))
      {
        string str = this._buffer[subject] + Environment.NewLine + text;
        this._buffer[subject] = str;
      }
      else
        this._buffer.Add(subject, text);
      ++this._messagesCount;
      if (this._messagesCount <= 30)
        return;
      this.TimerCallback((object) null);
    }
  }

  private void InternalSend(string subject, string text)
  {
    if (!(ServerServices.GetService(typeof (ISystemDiagnosticsTask)) is ISystemDiagnosticsTask service))
      return;
    service.SendLetterToAdmins(subject, text);
  }

  public void SendMessage(string subject, string text)
  {
    if (this._canSend)
    {
      this.InternalSend(subject, text);
      this._canSend = false;
      this.ResetTimer();
    }
    else
      this.AppendMessage(subject, text);
  }
}
