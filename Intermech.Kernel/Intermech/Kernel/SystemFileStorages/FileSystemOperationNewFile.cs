// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.SystemFileStorages.FileSystemOperationNewFile
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System;
using System.IO;


namespace Intermech.Kernel.SystemFileStorages;

internal class FileSystemOperationNewFile(
  FileSystemTransactionLog transactionLog,
  string tmpFileName,
  string dstFileName) : FileSystemOperationBase(transactionLog, tmpFileName, dstFileName)
{
  public override void Commit()
  {
    try
    {
      File.Move(this.TemporaryFileName, this.DestinationFileName);
      this.TransactionLog.LogOperation($"Перемещен файл {this.TemporaryFileName} в {this.DestinationFileName}", false);
    }
    catch (Exception ex)
    {
      this.TransactionLog.LogOperation($"Ошибка перемещения файла {this.TemporaryFileName} в {this.DestinationFileName}: {ex.Message}", true);
      this.TransactionLog.LogOperation(ex.StackTrace, true);
      throw;
    }
  }

  public override void Rollback()
  {
    try
    {
      File.Delete(this.TemporaryFileName);
      this.TransactionLog.LogOperation($"Удален файл {this.TemporaryFileName}", false);
    }
    catch (Exception ex)
    {
      this.TransactionLog.LogOperation($"Ошибка удаления файла {this.TemporaryFileName}: {ex.Message}", true);
      this.TransactionLog.LogOperation(ex.StackTrace, true);
      throw;
    }
  }
}
