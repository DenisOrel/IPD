// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.BriefcaseUnpacker
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.IO;


namespace Intermech.Kernel.Briefcase;

internal sealed class BriefcaseUnpacker : ImportBriefcaseBase
{
  private int _bufferLength = 16384 /*0x4000*/;
  private BriefcaseImportStructure _bis;

  public BriefcaseUnpacker(
    UserSession session,
    ImportEventLog eventLog,
    SetImportProgressEventHandler setImportProgressEvent,
    Guid briefcase,
    string briefcasePath,
    BriefcaseImportStructure bis)
    : base(session, eventLog, setImportProgressEvent, briefcase, briefcasePath)
  {
    this._bis = bis;
  }

  public bool Unpack()
  {
    DirectoryInfo directoryInfo = new DirectoryInfo(this.briefcasePath);
    BriefcaseImportProgress importProgress = new BriefcaseImportProgress(OperationType.Unpacking);
    IPackedStream service = ServerServices.GetService(typeof (IPackedStream)) as IPackedStream;
    FileInfo fileInfo1 = new FileInfo(Path.Combine(this._bis.ImportProperties.ServerTempFolder, BriefcaseConsts.prefixPack + this.briefcase.ToString()));
    FileInfo fileInfo2 = new FileInfo(Path.Combine(this._bis.ImportProperties.ServerTempFolder, BriefcaseConsts.prefixUnpack + this.briefcase.ToString()));
    this.SetImportProgress(this.briefcase, importProgress);
    try
    {
      using (FileStream outStream = new FileStream(fileInfo2.FullName, FileMode.Create, FileAccess.ReadWrite))
      {
        using (FileStream inStream = new FileStream(fileInfo1.FullName, FileMode.Open, FileAccess.Read))
        {
          service.UnpackStream((Stream) outStream, (Stream) inStream);
          inStream.Flush();
          inStream.Close();
        }
        File.Delete(fileInfo1.FullName);
        if (directoryInfo.Exists)
          directoryInfo.Delete(true);
        directoryInfo.Create();
        foreach (string folder in this._bis.FileStructure.Folders)
          Directory.CreateDirectory(Path.Combine(this.briefcasePath, folder));
        outStream.Position = 0L;
        long num1 = 0;
        foreach (PartFile file in this._bis.FileStructure.Files)
        {
          using (FileStream fileStream = new FileStream(Path.Combine(this.briefcasePath, file.FileName), FileMode.Create, FileAccess.Write))
          {
            outStream.Position = file.Offset;
            int num2 = (int) Math.Ceiling((double) (file.Length / (long) this._bufferLength));
            int count = (int) (file.Length - (long) (num2 * this._bufferLength));
            byte[] buffer = new byte[this._bufferLength];
            for (int index = 0; index < num2; ++index)
            {
              if (outStream.Read(buffer, 0, this._bufferLength) > 0)
                fileStream.Write(buffer, 0, this._bufferLength);
              num1 += (long) this._bufferLength;
              importProgress.Percent = (int) Math.Ceiling((double) (num1 * 100L / outStream.Length));
              this.SetImportProgress(this.briefcase, importProgress);
            }
            if (count > 0)
            {
              if (outStream.Read(buffer, 0, count) > 0)
              {
                fileStream.Write(buffer, 0, count);
                num1 += (long) count;
                importProgress.Percent = (int) Math.Ceiling((double) (num1 * 100L / outStream.Length));
                this.SetImportProgress(this.briefcase, importProgress);
              }
            }
          }
        }
        outStream.Flush();
        outStream.Close();
      }
      File.Delete(fileInfo2.FullName);
      importProgress.Percent = 100;
      importProgress.Operation = OperationType.TerminateCurrent;
      return true;
    }
    catch (Exception ex)
    {
      importProgress.ErrorException = new Exception($"{LocalizationHolder.rm.GetString("Kernel_334")}: {ex.Message}", ex);
      importProgress.Operation = OperationType.Error;
      this.SetImportProgress(this.briefcase, importProgress);
      this.eventLog.AddToTrace(importProgress.ErrorException.Message);
      return false;
    }
  }
}
