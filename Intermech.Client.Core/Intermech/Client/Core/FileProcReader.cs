
// Type: Intermech.Client.Core.FileProcReader
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.DiskStorage;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
/// для чтения файлов на закладке истории изменения файлов.
/// т.к. BlobProcReader, читает файлы из атрибута объекта,
/// то он соверешнно ничего не знает о файлах истории, и
/// воспользоваться им не получится.
/// соот-но добавлен этот класс.
/// практически точная копии BlobProcReader
/// </summary>
public class FileProcReader : BlobProcCustomClass
{
  /// <summary>Структура с информацией о файле (BLOB-поле)</summary>
  private FileHistoryNodeID fileInformation;
  /// <summary>
  /// Поток, в который будет выполняться чтение данных из атрибута
  /// </summary>
  private Stream destStream;
  /// <summary>Закрывать поток-назначение</summary>
  private bool closeDestStream;

  /// <summary>Структура с информацией о файле (BLOB-поле)</summary>
  public FileHistoryNodeID FileInformation => this.fileInformation;

  /// <summary>
  /// Поток, в который будет выполняться чтение данных из атрибута
  /// </summary>
  public Stream DestStream => this.destStream;

  /// <summary>Закрывать поток-назначение</summary>
  public bool CloseDestStream => this.closeDestStream;

  /// <summary>Создать класс для чтения BLOB-поля</summary>
  /// <param name="fileInfo"></param>
  /// <param name="aAttributableElement">Указывает, является значение, передаваемое в aElementID, идентификтором объекта или связи</param>
  /// <param name="aIndex">Индекс значения (для многозначного атрибута)</param>
  /// <param name="aDataBlockSize">Размер блока зачитываемых данных. 0 – по умолчанию</param>
  /// <param name="aDestStream">Поток, в который будет выполняться чтение данных из атрибута</param>
  /// <param name="aProgress">Событие, сообщающее процент выполнения процесса чтения (0-100)</param>
  /// <param name="aThreadFinish">Событие, происходящее при завершении чтения данных при чтении данных в отдельном потоке</param>
  public FileProcReader(
    FileHistoryNodeID fileInfo,
    AttributableElements aAttributableElement,
    int aDataBlockSize,
    Stream aDestStream,
    BlobProcCustomClass.ProgressEventHandler aProgress,
    BlobProcCustomClass.ThreadFinishEventHandler aThreadFinish)
  {
    this.fileInformation = fileInfo;
    this.attributableElement = aAttributableElement;
    this.dataBlockSize = aDataBlockSize;
    this.destStream = aDestStream;
    if (aProgress != null)
      this.Progress += aProgress;
    if (aThreadFinish == null)
      return;
    this.ThreadFinish += aThreadFinish;
  }

  /// <summary>
  /// Считывание двоичных данных данных из BLOB-поля в фоновом потоке.
  /// По окончании считывания данных вызывается обработчик, указанный в параметере конструктора aThreadFinish.
  /// </summary>
  /// <param name="aCloseDestStream">Указывает, производить ли закрытие потока aDestStream по окончании чтения данных</param>
  /// <returns>Фоновый поток, в котором выполняется чтение BLOB-поля</returns>
  public Thread ReadDataThread(bool aCloseDestStream)
  {
    this.inThread = true;
    this.closeDestStream = aCloseDestStream;
    this.thread = new Thread(new ThreadStart(this.ReadDataCustom));
    this.thread.Start();
    return this.thread;
  }

  /// <summary>Произвести чтение информации из BLOB-поля</summary>
  public void ReadData()
  {
    this.inThread = false;
    this.closeDestStream = false;
    this.ReadDataCustom();
  }

  private void ReadDataCustom()
  {
    this.mode = BlobProcessorMode.Unknown;
    BlobInformation bi = new BlobInformation();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.result = false;
      IsolatedStorageFile isf = (IsolatedStorageFile) null;
      lock (typeof (IsolatedStorageFile))
        isf = IsolatedStorageFile.GetUserStoreForDomain();
      using (isf)
      {
        try
        {
          this.mode = BlobProcessorMode.Read;
          this.OnProgress(0);
          if (this.dataBlockSize <= 0)
            this.dataBlockSize = Consts.DefaultBlobBlockSize;
          this.destStream.Position = 0L;
          this.destStream.SetLength(0L);
          IUserSession session = sessionKeeper.Session;
          try
          {
            IVaultFileReader vaultFileReader = (session.GetCustomService(typeof (IVaultFileReaderService)) as IVaultFileReaderService).GetVaultFileReader(session.SessionGUID);
            bi = vaultFileReader.OpenBlob(this.dataBlockSize, this.fileInformation.ObjectID, this.fileInformation.HistoryID, this.fileInformation.StorageID);
            try
            {
              if (this.dataBlockSize >= 0)
              {
                long num1 = 1;
                long packedFileSize = this.fileInformation.PackedFileSize;
                if (this.dataBlockSize > 0)
                  num1 = packedFileSize % (long) this.dataBlockSize == 0L ? packedFileSize / (long) this.dataBlockSize : packedFileSize / (long) this.dataBlockSize + 1L;
                Stream inStream = (Stream) null;
                bool flag = true;
                string empty = string.Empty;
                try
                {
                  if (this.fileInformation.ArcMethod == ArcMethods.NotPacked)
                  {
                    inStream = this.destStream;
                    flag = false;
                  }
                  else
                  {
                    empty = Guid.NewGuid().ToString();
                    FileMode mode = File.Exists(empty) ? FileMode.Truncate : FileMode.CreateNew;
                    inStream = (Stream) new IsolatedStorageFileStream(empty, mode, FileAccess.ReadWrite, isf);
                  }
                  for (int index = 1; (long) index <= num1; ++index)
                  {
                    this.OnProgress(Convert.ToInt32((long) (index * 100) / num1));
                    if (this.inThread)
                    {
                      Thread.Sleep(1);
                      if (this.abortThreadFlag)
                        return;
                    }
                    byte[] buffer = vaultFileReader.ReadDataBlock(this.dataBlockSize);
                    inStream.Write(buffer, 0, buffer.Length);
                  }
                  inStream.Position = 0L;
                  switch (bi.ArcMethod)
                  {
                    case ArcMethods.NotPacked:
                      this.result = true;
                      break;
                    case ArcMethods.ZLibPacked:
                      IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
                      if (this.inThread)
                      {
                        Thread.Sleep(1);
                        if (this.abortThreadFlag)
                          throw new MyThreadAbortException();
                      }
                      BlobProcessorMode mode1 = this.mode;
                      this.mode = BlobProcessorMode.Unpack;
                      try
                      {
                        if (inStream.Length != 0L)
                          service.UnpackStream(this.destStream, inStream, new PercentEventHandler(((BlobProcCustomClass) this).OnUnpackProgress));
                      }
                      finally
                      {
                        this.mode = mode1;
                      }
                      if (this.inThread)
                      {
                        Thread.Sleep(1);
                        if (this.abortThreadFlag)
                          return;
                        goto case ArcMethods.NotPacked;
                      }
                      goto case ArcMethods.NotPacked;
                    default:
                      int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1052"), LocalizationHolder.rm.GetString("Client.Core_82"), MessageBoxButtons.OK);
                      return;
                  }
                }
                finally
                {
                  if (flag)
                  {
                    inStream.Close();
                    isf.DeleteFile(empty);
                  }
                  if (this.closeDestStream)
                    this.destStream.Close();
                }
              }
            }
            finally
            {
              vaultFileReader.CloseBlob();
            }
            this.mode = BlobProcessorMode.Unknown;
            this.OnProgress(100);
          }
          finally
          {
            if (this.inThread && this.result)
            {
              this.inThread = false;
              this.OnThreadFinish(this.result, this.result ? MessageDialogs.msgSuccess : MessageDialogs.msgError, (Exception) null, bi);
            }
          }
        }
        catch (SerializationException ex)
        {
        }
        catch (Exception ex)
        {
          this.result = false;
          int num = this.inThread ? 1 : 0;
          this.inThread = false;
          string message = ex.Message;
          switch (ex)
          {
            case ThreadAbortException _:
            case MyThreadAbortException _:
              message = MessageDialogs.msgProcessTerminated;
              break;
          }
          this.OnThreadFinish(this.result, message, ex, bi);
          if (num != 0)
            return;
          throw;
        }
      }
    }
  }
}
