// Decompiled with JetBrains decompiler
// Type: Media.MediaPlayer
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Media;

public class MediaPlayer
{
  public const int MM_MCINOTIFY = 953;
  private bool m_filterInstalled;

  [DllImport("winmm.dll")]
  private static extern int mciSendString(
    string strCommand,
    StringBuilder strReturn,
    int iReturnLength,
    IntPtr hwndCallback);

  [DllImport("winmm.dll")]
  private static extern bool mciGetErrorString(
    int fdwError,
    StringBuilder lpszErrorText,
    uint cchErrorText);

  public void Close()
  {
    MediaPlayer.mciSendString("close MediaFile", (StringBuilder) null, 0, IntPtr.Zero);
  }

  public void Play(string fileName, bool async)
  {
    this.Close();
    StringBuilder stringBuilder = new StringBuilder(100);
    int fdwError1 = MediaPlayer.mciSendString($"open \"{fileName}\" type mpegvideo alias MediaFile", stringBuilder, 100, IntPtr.Zero);
    if (fdwError1 != 0)
    {
      MediaPlayer.mciGetErrorString(fdwError1, stringBuilder, 100U);
    }
    else
    {
      IntPtr hwndCallback = IntPtr.Zero;
      string str = "play MediaFile";
      string strCommand;
      if (!async)
      {
        strCommand = str + " wait";
      }
      else
      {
        strCommand = str + " notify";
        hwndCallback = Application.OpenForms[0].Handle;
        if (!this.m_filterInstalled)
        {
          Application.AddMessageFilter((IMessageFilter) new MediaPlayer.MsgFilter(this));
          this.m_filterInstalled = true;
        }
      }
      int fdwError2 = MediaPlayer.mciSendString(strCommand, stringBuilder, 100, hwndCallback);
      if (fdwError2 != 0)
      {
        MediaPlayer.mciGetErrorString(fdwError2, stringBuilder, 100U);
      }
      else
      {
        if (async)
          return;
        MediaPlayer.mciSendString("close MediaFile", stringBuilder, 100, IntPtr.Zero);
      }
    }
  }

  private void Notify() => this.Close();

  public void Play(string FileName) => this.Play(FileName, true);

  public static void PlaySound(string FileName)
  {
    MediaPlayer mediaPlayer = new MediaPlayer();
    try
    {
      mediaPlayer.Play(FileName);
    }
    catch
    {
    }
  }

  public string ModeString
  {
    get
    {
      StringBuilder strReturn = new StringBuilder(128 /*0x80*/);
      MediaPlayer.mciSendString("status MediaFile mode", strReturn, 128 /*0x80*/, IntPtr.Zero);
      return strReturn.ToString();
    }
  }

  public class MsgFilter : IMessageFilter
  {
    private MediaPlayer _player;

    public MsgFilter(MediaPlayer player) => this._player = player;

    public bool PreFilterMessage(ref Message m)
    {
      if (m.Msg != 953)
        return false;
      this._player.Notify();
      return true;
    }
  }

  public enum MPModes
  {
    mpNotReady,
    mpStopped,
    mpPlaying,
    mpRecording,
    mpSeeking,
    mpPaused,
    mpOpen,
  }
}
