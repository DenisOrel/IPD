
// Type: Intermech.UI.ExceptionHandling.ExceptionRecoveryHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Diagnostics;
using System.Windows.Forms;


namespace Intermech.UI.ExceptionHandling;

/// <summary>
/// Клиентский обработчик восстановления после необработанного исключения.
/// Реализация не является thread safe.
/// </summary>
public class ExceptionRecoveryHandler : ErrorRecoveryHandler
{
  /// <summary>Выполняет действие по восстановлению после ошибки.</summary>
  /// <param name="recoveryUri">Данные для восстановления после ошибки</param>
  /// <returns>Признак успешного/неуспешного выполнения действия</returns>
  protected override bool DoInvokeRecoveryAction(Uri recoveryUri)
  {
    try
    {
      switch (recoveryUri.Scheme)
      {
        case "ips":
          HyperlinkHandler.OpenUrl(recoveryUri.AbsoluteUri, true);
          return true;
        case "file":
          Process.Start(recoveryUri.LocalPath).Dispose();
          return true;
      }
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show($"При открытии ссылки '{recoveryUri}' произошла ошибка. {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return true;
    }
    return base.DoInvokeRecoveryAction(recoveryUri);
  }
}
