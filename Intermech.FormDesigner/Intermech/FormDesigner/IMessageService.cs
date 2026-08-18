// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.IMessageService
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System;

#nullable disable
namespace Intermech.FormDesigner;

/// <summary>
/// 
/// </summary>
internal interface IMessageService
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="ex"></param>
  void ShowError(Exception ex);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="message"></param>
  void ShowError(string message);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ex"></param>
  /// <param name="message"></param>
  void ShowError(Exception ex, string message);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="formatstring"></param>
  /// <param name="formatitems"></param>
  void ShowErrorFormatted(string formatstring, params string[] formatitems);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="message"></param>
  void ShowWarning(string message);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="formatstring"></param>
  /// <param name="formatitems"></param>
  void ShowWarningFormatted(string formatstring, params string[] formatitems);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="message"></param>
  void ShowMessage(string message);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="message"></param>
  /// <param name="caption"></param>
  void ShowMessage(string message, string caption);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="formatstring"></param>
  /// <param name="formatitems"></param>
  void ShowMessageFormatted(string formatstring, params string[] formatitems);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="caption"></param>
  /// <param name="formatstring"></param>
  /// <param name="formatitems"></param>
  void ShowMessageFormatted(string caption, string formatstring, params string[] formatitems);

  /// <summary>Returns the number of the chosen button.</summary>
  int ShowCustomDialog(string caption, string dialogText, params string[] buttontexts);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="question"></param>
  /// <returns></returns>
  bool AskQuestion(string question);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="formatstring"></param>
  /// <param name="formatitems"></param>
  /// <returns></returns>
  bool AskQuestionFormatted(string formatstring, params string[] formatitems);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="question"></param>
  /// <param name="caption"></param>
  /// <returns></returns>
  bool AskQuestion(string question, string caption);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="caption"></param>
  /// <param name="formatstring"></param>
  /// <param name="formatitems"></param>
  /// <returns></returns>
  bool AskQuestionFormatted(string caption, string formatstring, params string[] formatitems);
}
