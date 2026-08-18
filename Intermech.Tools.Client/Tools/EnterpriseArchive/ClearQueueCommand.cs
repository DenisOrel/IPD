// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.ClearQueueCommand
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Localization;
using Intermech.Tools.EnterpriseArchive.SpecialFiles;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal sealed class ClearQueueCommand
{
  public void Perform()
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("SR_252"), LocalizationHolder.rm.GetString("SR_253"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    QueueFileServices.ReplaceQueue(new QueueFile());
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("SR_254"), LocalizationHolder.rm.GetString("SR_253"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }
}
