
// Type: Intermech.Interfaces.BlobProcReader
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.IO;
using Intermech.Remoting.Sponsors;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Чтение двоичных данных из BLOB-полей СУБД напрямую и через потоки
    /// </summary>
    public class BlobProcReader : BlobProcCustomClass
    {
      /// <summary>Структура с информацией о файле (BLOB-поле)</summary>
      private BlobInformation blobInformation;
      /// <summary>
      /// Поток, в который будет выполняться чтение данных из атрибута
      /// </summary>
      private Stream destStream;
      /// <summary>Закрывать поток-назначение</summary>
      private bool closeDestStream;

      /// <summary>Структура с информацией о файле (BLOB-поле)</summary>
      public BlobInformation BlobInformation => this.blobInformation;

      /// <summary>
      /// Поток, в который будет выполняться чтение данных из атрибута
      /// </summary>
      public Stream DestStream => this.destStream;

      /// <summary>Закрывать поток-назначение</summary>
      public bool CloseDestStream => this.closeDestStream;

      /// <summary>Создать класс для чтения BLOB-поля</summary>
      /// <param name="aElementID">Идентификатор объекта или связи</param>
      /// <param name="aAttributableElement">Указывает, является значение, передаваемое в aElementID, идентификтором объекта или связи</param>
      /// <param name="aAttributeID">Идентификатор атрибута, хранящего двоичные данные</param>
      /// <param name="aIndex">Индекс значения (для многозначного атрибута)</param>
      /// <param name="aDataBlockSize">Размер блока зачитываемых данных. 0 – по умолчанию</param>
      /// <param name="aDestStream">Поток, в который будет выполняться чтение данных из атрибута</param>
      /// <param name="aProgress">Событие, сообщающее процент выполнения процесса чтения (0-100)</param>
      /// <param name="aThreadFinish">Событие, происходящее при завершении чтения данных при чтении данных в отдельном потоке</param>
      public BlobProcReader(
        long aElementID,
        AttributableElements aAttributableElement,
        int aAttributeID,
        int aIndex,
        int aDataBlockSize,
        Stream aDestStream,
        BlobProcCustomClass.ProgressEventHandler aProgress,
        BlobProcCustomClass.ThreadFinishEventHandler aThreadFinish)
      {
        this.lIDBAttribute = (IDBAttribute) null;
        this.elementID = aElementID;
        this.attributableElement = aAttributableElement;
        this.attributeID = aAttributeID;
        this.index = aIndex;
        this.dataBlockSize = aDataBlockSize;
        this.destStream = aDestStream;
        if (aProgress != null)
          this.Progress += aProgress;
        if (aThreadFinish == null)
          return;
        this.ThreadFinish += aThreadFinish;
      }

      /// <summary>Создать класс для чтения BLOB-поля</summary>
      /// <param name="aIDBAttribute">Интерфейс атрибута, из которого необходимо произвести чтение</param>
      /// <param name="aDataBlockSize">Размер блока зачитываемых данных. 0 – по умолчанию</param>
      /// <param name="aDestStream">Поток, в который будет выполняться чтение данных из атрибута</param>
      /// <param name="aProgress">Событие, сообщающее процент выполнения процесса чтения (0-100)</param>
      /// <param name="aThreadFinish">Событие, происходящее при завершении чтения данных при чтении данных в отдельном потоке</param>
      public BlobProcReader(
        IDBAttribute aIDBAttribute,
        int aDataBlockSize,
        Stream aDestStream,
        BlobProcCustomClass.ProgressEventHandler aProgress,
        BlobProcCustomClass.ThreadFinishEventHandler aThreadFinish)
      {
        this.lIDBAttribute = aIDBAttribute;
        this.elementID = 0L;
        this.attributableElement = AttributableElements.None;
        this.attributeID = 0;
        this.index = 0;
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
      /// На серверной стороне использовать вариант функции с сессией.
      /// </summary>
      /// <param name="aCloseDestStream">Указывает, производить ли закрытие потока aDestStream по окончании чтения данных</param>
      /// <returns>Фоновый поток, в котором выполняется чтение BLOB-поля</returns>
      public Thread ReadDataThread(bool aCloseDestStream)
      {
        return this.ReadDataThread((IUserSession) null, aCloseDestStream);
      }

      /// <summary>
      /// Считывание двоичных данных данных из BLOB-поля в фоновом потоке.
      /// По окончании считывания данных вызывается обработчик, указанный в параметере конструктора aThreadFinish.
      /// </summary>
      /// <param name="uSession">обязательна для чтения на серверной стороне</param>
      /// <param name="aCloseDestStream"></param>
      /// <returns></returns>
      public Thread ReadDataThread(IUserSession uSession, bool aCloseDestStream)
      {
        this.session = uSession;
        this.inThread = true;
        this.closeDestStream = aCloseDestStream;
        this.thread = new Thread(new ThreadStart(this.ReadDataCustom));
        this.thread.IsBackground = true;
        this.thread.Start();
        return this.thread;
      }

      /// <summary>
      /// Произвести чтение информации из BLOB-поля
      /// На серверной стороне использовать вариант функции с сессией.
      /// </summary>
      public void ReadData() => this.ReadData((IUserSession) null);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="uSession">обязательна для чтения на серверной стороне</param>
      public void ReadData(IUserSession uSession)
      {
        this.session = uSession;
        this.inThread = false;
        this.closeDestStream = false;
        this.ReadDataCustom();
      }

      private void ReadDataCustom()
      {
        this.mode = BlobProcessorMode.Unknown;
        using (SessionKeeper sessionKeeper = this.session == null ? new SessionKeeper() : (SessionKeeper) null)
        {
          IUserSession iSession = this.session == null ? sessionKeeper.Session : this.session.Clone("BlobProcessor.ReadDataCustom");
          try
          {
            this.result = false;
            if (this.destStream.CanSeek)
            {
              this.destStream.Position = 0L;
              this.destStream.SetLength(0L);
            }
            IDBAttribute objToLock = this.lIDBAttribute == null ? BlobProcCustomClass.GetAttributeInterface(this.elementID, this.attributableElement, this.attributeID, this.index, iSession) : this.lIDBAttribute;
            if (objToLock == null)
              throw new AttributeNotFoundException(this.attributeID, this.elementID);
            if (objToLock is IDBShortBlobAttribute && !this.InThread)
            {
              this.mode = BlobProcessorMode.Read;
              this.OnProgress(0);
              ShortBlobValue blobValue = (objToLock as IDBShortBlobAttribute).GetBlobValue();
              if (!blobValue.Empty)
              {
                this.blobInformation = new BlobInformation((ShortBlobInfo) blobValue);
                if (blobValue.ArcMethod == ArcMethods.NotPacked)
                {
                  this.destStream.Write(blobValue.Value, 0, blobValue.Value.Length);
                }
                else
                {
                  IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
                  using (Stream inStream = (Stream) new MemoryStream(blobValue.Value))
                  {
                    if (inStream.Length != 0L)
                      service.UnpackStream(this.destStream, inStream);
                  }
                }
                this.destStream.Position = 0L;
                this.OnProgress(100);
                this.result = true;
                return;
              }
            }
            IsolatedStorageFile isf = (IsolatedStorageFile) null;
            lock (typeof (IsolatedStorageFile))
              isf = IsolatedStorageFile.GetUserStoreForDomain();
            using (RemoteLock remoteLock = new RemoteLock())
            {
              using (isf)
              {
                try
                {
                  this.mode = BlobProcessorMode.Read;
                  this.OnProgress(0);
                  if (this.dataBlockSize <= 0)
                    this.dataBlockSize = Intermech.Consts.DefaultBlobBlockSize;
                  try
                  {
                    if (!(objToLock is IBlobReader blobReader))
                      return;
                    remoteLock.Add((object) objToLock);
                    this.blobInformation = blobReader.OpenBlob(this.dataBlockSize);
                    try
                    {
                      if (this.dataBlockSize >= 0)
                      {
                        long num1 = 1;
                        long num2 = this.blobInformation.ArcMethod == ArcMethods.NotPacked ? this.blobInformation.RealFileSize : this.blobInformation.PackedFileSize;
                        if (this.dataBlockSize > 0)
                          num1 = num2 % (long) this.dataBlockSize == 0L ? num2 / (long) this.dataBlockSize : num2 / (long) this.dataBlockSize + 1L;
                        bool flag1 = false;
                        Stream inStream = (Stream) null;
                        bool flag2 = true;
                        string empty = string.Empty;
                        try
                        {
                          if (this.blobInformation.ArcMethod == ArcMethods.NotPacked)
                          {
                            inStream = this.destStream;
                            flag2 = false;
                          }
                          else
                          {
                            empty = Guid.NewGuid().ToString();
                            flag1 = this.blobInformation.PackedFileSize <= Intermech.Consts.BlobInMemoryOperationalLimit;
                            inStream = flag1 ? (Stream) new ImChunkedStream() : (Stream) new IsolatedStorageFileStream(empty, FileMode.Create, FileAccess.ReadWrite, isf);
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
                            byte[] buffer = blobReader.ReadDataBlock(this.dataBlockSize);
                            inStream.Write(buffer, 0, buffer.Length);
                          }
                          switch (this.blobInformation.ArcMethod)
                          {
                            case ArcMethods.NotPacked:
                              this.result = true;
                              break;
                            case ArcMethods.ZLibPacked:
                              inStream.Position = 0L;
                              IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
                              if (this.inThread)
                              {
                                Thread.Sleep(1);
                                if (this.abortThreadFlag)
                                  throw new MyThreadAbortException();
                              }
                              BlobProcessorMode mode = this.mode;
                              this.mode = BlobProcessorMode.Unpack;
                              try
                              {
                                if (inStream.Length != 0L)
                                  service.UnpackStream(this.destStream, inStream, new PercentEventHandler(((BlobProcCustomClass) this).OnUnpackProgress));
                              }
                              finally
                              {
                                this.mode = mode;
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
                              return;
                          }
                        }
                        finally
                        {
                          if (flag2)
                          {
                            inStream.Close();
                            inStream.Dispose();
                            if (!flag1)
                            {
                              try
                              {
                                isf.DeleteFile(empty);
                              }
                              catch
                              {
                              }
                            }
                          }
                        }
                      }
                    }
                    finally
                    {
                      blobReader.CloseBlob();
                    }
                    this.mode = BlobProcessorMode.Unknown;
                    this.OnProgress(100);
                  }
                  finally
                  {
                    if (this.closeDestStream)
                      this.destStream.Close();
                    if (this.inThread && this.result)
                    {
                      this.inThread = false;
                      this.OnThreadFinish(this.result, this.result ? MessageDialogs.msgSuccess : MessageDialogs.msgError, (Exception) null, this.blobInformation);
                    }
                  }
                }
                catch (Exception ex)
                {
                  this.result = false;
                  int num = this.inThread ? 1 : 0;
                  this.inThread = false;
                  string message = ex.Message + (ex.StackTrace != null ? ": " + ex.StackTrace : "");
                  switch (ex)
                  {
                    case ThreadAbortException _:
                    case MyThreadAbortException _:
                      message = MessageDialogs.msgProcessTerminated;
                      break;
                  }
                  this.OnThreadFinish(this.result, message, ex, this.blobInformation);
                  if (num != 0)
                    return;
                  throw;
                }
              }
            }
          }
          finally
          {
            if (this.session != null)
              iSession.Logout("BlobProcessor.ReadDataCustom");
          }
        }
      }
    }
}
