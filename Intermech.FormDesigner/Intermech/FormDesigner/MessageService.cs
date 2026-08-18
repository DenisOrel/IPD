// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.MessageService
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner;

/// <summary>
/// 
/// </summary>
internal class MessageService : IMessageService
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="ex"></param>
  public void ShowError(Exception ex)
  {
    int num = (int) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="message"></param>
  void IMessageService.ShowError(string message)
  {
    int num = (int) MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ex"></param>
  /// <param name="message"></param>
  void IMessageService.ShowError(Exception ex, string message)
  {
    int num = (int) MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="formatstring"></param>
  /// <param name="formatitems"></param>
  public void ShowErrorFormatted(string formatstring, params string[] formatitems)
  {
    int num = (int) MessageBox.Show(string.Format(formatstring, (object[]) formatitems), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="message"></param>
  public void ShowWarning(string message)
  {
    int num = (int) MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="formatstring"></param>
  /// <param name="formatitems"></param>
  public void ShowWarningFormatted(string formatstring, params string[] formatitems)
  {
    int num = (int) MessageBox.Show(string.Format(formatstring, (object[]) formatitems), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="message"></param>
  public void ShowMessage(string message)
  {
    int num = (int) MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="message"></param>
  /// <param name="caption"></param>
  void IMessageService.ShowMessage(string message, string caption)
  {
    int num = (int) MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="formatstring"></param>
  /// <param name="formatitems"></param>
  public void ShowMessageFormatted(string formatstring, params string[] formatitems)
  {
    int num = (int) MessageBox.Show(string.Format(formatstring, (object[]) formatitems), "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="caption"></param>
  /// <param name="formatstring"></param>
  /// <param name="formatitems"></param>
  void IMessageService.ShowMessageFormatted(
    string caption,
    string formatstring,
    params string[] formatitems)
  {
    int num = (int) MessageBox.Show(string.Format(formatstring, (object[]) formatitems), caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="caption"></param>
  /// <param name="dialogText"></param>
  /// <param name="buttontexts"></param>
  /// <returns></returns>
  public int ShowCustomDialog(string caption, string dialogText, params string[] buttontexts) => 0;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="question"></param>
  /// <returns></returns>
  public bool AskQuestion(string question)
  {
    return MessageBox.Show(question, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="formatstring"></param>
  /// <param name="formatitems"></param>
  /// <returns></returns>
  public bool AskQuestionFormatted(string formatstring, params string[] formatitems)
  {
    return MessageBox.Show(string.Format(formatstring, (object[]) formatitems), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="question"></param>
  /// <param name="caption"></param>
  /// <returns></returns>
  bool IMessageService.AskQuestion(string question, string caption)
  {
    return MessageBox.Show(question, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="caption"></param>
  /// <param name="formatstring"></param>
  /// <param name="formatitems"></param>
  /// <returns></returns>
  bool IMessageService.AskQuestionFormatted(
    string caption,
    string formatstring,
    params string[] formatitems)
  {
    return MessageBox.Show(string.Format(formatstring, (object[]) formatitems), caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
  }
}
