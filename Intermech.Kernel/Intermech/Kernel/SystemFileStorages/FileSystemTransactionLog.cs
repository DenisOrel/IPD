// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.SystemFileStorages.FileSystemTransactionLog
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System.Collections.Generic;


namespace Intermech.Kernel.SystemFileStorages;

internal class FileSystemTransactionLog
{
  private FileSystemBlobStorage BlobStorage;
  private List<FileSystemOperationBase> Operations = new List<FileSystemOperationBase>();
  private int TransactionDepth;

  public FileSystemTransactionLog(FileSystemBlobStorage storage) => this.BlobStorage = storage;

  public void AddOperation(
    string tmpFileName,
    string dstFileName,
    FileSystemOperationType operType)
  {
    FileSystemOperationBase systemOperationBase;
    switch (operType)
    {
      case FileSystemOperationType.NewFile:
        systemOperationBase = (FileSystemOperationBase) new FileSystemOperationNewFile(this, tmpFileName, dstFileName);
        break;
      case FileSystemOperationType.ReplaceFile:
        systemOperationBase = (FileSystemOperationBase) new FileSystemOperationReplaceFile(this, tmpFileName, dstFileName);
        break;
      case FileSystemOperationType.DeleteFile:
        systemOperationBase = (FileSystemOperationBase) new FileSystemOperationDeleteFile(this, tmpFileName, dstFileName);
        break;
      default:
        throw new KernelException("Попытка добавить неизвестную операцию в журнал транзакций: " + operType.ToString());
    }
    if (this.TransactionDepth == 0)
      systemOperationBase.Commit();
    else
      this.Operations.Add(systemOperationBase);
  }

  public void StartTransaction() => ++this.TransactionDepth;

  public void Commit()
  {
    if (--this.TransactionDepth != 0)
      return;
    foreach (FileSystemOperationBase operation in this.Operations)
      operation.Commit();
    this.Operations.Clear();
  }

  public void Rollback()
  {
    foreach (FileSystemOperationBase operation in this.Operations)
      operation.Rollback();
    this.Operations.Clear();
    this.TransactionDepth = 0;
  }

  public void LogOperation(string message, bool logAlways)
  {
    this.BlobStorage.LogOperation(message, logAlways);
  }
}
