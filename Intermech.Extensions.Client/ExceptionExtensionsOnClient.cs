// Decompiled with JetBrains decompiler
// Type: Intermech.ExceptionExtensionsOnClient
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech;

public static class ExceptionExtensionsOnClient
{
  public static bool TryProcessExceptionOnClient([NotNull] this Exception exception, [CanBeNull] IWin32Window owner = null)
  {
    exception = exception.ExtractNotOperationCancelled();
    if (exception == null)
      return true;
    exception = exception.ExtractNotAbort();
    if (exception == null)
      return true;
    ErrorMessageException messageException = exception.TryGetErrorMessageException();
    if (messageException == null)
      return false;
    int num = (int) MessageBox.Show(owner, messageException.Message ?? "Неизвестная ошибка", messageException.Caption ?? "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
    return true;
  }
}
