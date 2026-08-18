// Decompiled with JetBrains decompiler
// Type: Intermech.Checksums.ChecksumTask
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;


namespace Intermech.Checksums;

public class ChecksumTask
{
  public SetChecksumProgressHandler SetChecksumProgressEvent;
  public ChecksumTaskFinishHandler ChecksumTaskFinishEvent;
  private Guid sessionGuid;
  private Guid taskGuid;
  private ChecksumInputStructure checksumInputStructure;

  public ChecksumTask(Guid sessionGuid, Guid taskGuid, ChecksumInputStructure cis)
  {
    this.sessionGuid = sessionGuid;
    this.taskGuid = taskGuid;
    this.checksumInputStructure = cis;
  }

  public void Calc()
  {
    new Thread(new ThreadStart(this.CalcChecksum))
    {
      IsBackground = true
    }.Start();
  }

  private void CalcChecksum()
  {
    ChecksumClass сhecksumClass = (ChecksumClass) null;
    ChecksumTaskProgress checksumTaskProgress = new ChecksumTaskProgress(ChecksumOperationType.Idle);
    IUserSession uSession = (UserSession.GetSessionByID(this.sessionGuid) as UserSession).Clone(true, nameof (CalcChecksum));
    try
    {
      checksumTaskProgress.Operation = ChecksumOperationType.Preparing;
      this.SetChecksumProgressEvent((object) this, this.taskGuid, checksumTaskProgress);
      switch (this.checksumInputStructure.algorithm)
      {
        case ChecksumAlgorithm.Crc32:
          using (Crc32Stream aDestStream = new Crc32Stream(Stream.Null))
          {
            new BlobProcReader(this.checksumInputStructure.elementId, this.checksumInputStructure.kind, this.checksumInputStructure.attributeId, this.checksumInputStructure.index, 0, (Stream) aDestStream, new BlobProcCustomClass.ProgressEventHandler(this.ProgressEvent), (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(uSession);
            сhecksumClass = new ChecksumClass(this.checksumInputStructure.algorithm, (object) aDestStream.WriteCrc);
            break;
          }
        case ChecksumAlgorithm.Md5:
          using (MD5 transform = MD5.Create())
          {
            using (LengthedCryptoStream aDestStream = new LengthedCryptoStream(Stream.Null, (ICryptoTransform) transform, CryptoStreamMode.Write))
            {
              new BlobProcReader(this.checksumInputStructure.elementId, this.checksumInputStructure.kind, this.checksumInputStructure.attributeId, this.checksumInputStructure.index, 0, (Stream) aDestStream, new BlobProcCustomClass.ProgressEventHandler(this.ProgressEvent), (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(uSession);
              aDestStream.Close();
              сhecksumClass = new ChecksumClass(this.checksumInputStructure.algorithm, (object) transform.Hash);
              break;
            }
          }
        case ChecksumAlgorithm.Gost3411_2012_256:
        case ChecksumAlgorithm.Gost3411_2012_512:
          using (MemoryStream aDestStream = new MemoryStream())
          {
            new BlobProcReader(this.checksumInputStructure.elementId, this.checksumInputStructure.kind, this.checksumInputStructure.attributeId, this.checksumInputStructure.index, 0, (Stream) aDestStream, new BlobProcCustomClass.ProgressEventHandler(this.ProgressEvent), (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(uSession);
            сhecksumClass = new GostChecksum(this.checksumInputStructure.algorithm).Compute((Stream) aDestStream);
            break;
          }
        default:
          сhecksumClass = (ChecksumClass) null;
          break;
      }
      checksumTaskProgress.Operation = ChecksumOperationType.Finished;
      checksumTaskProgress.Percent = 100;
    }
    catch (Exception ex)
    {
      сhecksumClass = (ChecksumClass) null;
      checksumTaskProgress.OnErrorOperation = checksumTaskProgress.Operation;
      checksumTaskProgress.Operation = ChecksumOperationType.Error;
      checksumTaskProgress.ErrorException = ex;
    }
    finally
    {
      uSession.Logout(nameof (CalcChecksum));
      this.ChecksumTaskFinishEvent((object) this, this.taskGuid, checksumTaskProgress, сhecksumClass);
    }
  }

  private void ProgressEvent(BlobProcCustomClass sender, BlobProcessorMode mode, int progress)
  {
    this.SetChecksumProgressEvent((object) this, this.taskGuid, new ChecksumTaskProgress(ChecksumOperationType.Calculating)
    {
      Percent = progress
    });
  }
}
