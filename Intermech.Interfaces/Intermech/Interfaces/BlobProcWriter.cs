
// Type: Intermech.Interfaces.BlobProcWriter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.BlobStream;
using Intermech.IO;
using Intermech.Remoting.Sponsors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Запись двоичных данных в BLOB-поля СУБД напрямую и через потоки
    /// </summary>
    public class BlobProcWriter : BlobProcCustomClass
    {
      /// <summary>Структура с информацией о файле (BLOB-поле)</summary>
      private BlobInformation blobInformation;
      /// <summary>Поток, содержимое которого будет записано в BLOB-поле</summary>
      private Stream sourceStream;
      private bool virtualWrite;
      /// <summary>
      /// Поток, в который записывается данные вместо записи напрямую в базу данных.
      /// Запись в поток производится только при вызове метода WriteData(true)
      /// </summary>
      private Stream virtualStream;
      /// <summary>Требуется ли закрывать поток-источник</summary>
      private bool closeSourceStream;
      private string fileZipExclusions;
      /// <summary>Расширения файлов, которые заведомо не сжимаются</summary>
      public static string[] DefaultZipExclusions = new string[22]
      {
        ".JPG",
        ".JPEG",
        ".PNG",
        ".GIF",
        ".WEBP",
        ".DOCX",
        ".DOCM",
        ".XLSX",
        ".XSLSM",
        ".7Z",
        ".RAR",
        ".ZIP",
        ".SLDPRT",
        ".SLDASM",
        ".SLDDRW",
        ".IPT",
        ".IAM",
        ".PRT",
        ".IDW",
        ".ASM",
        ".DRW",
        ".IMV"
      };

      /// <summary>Структура с информацией о файле (BLOB-поле)</summary>
      public BlobInformation BlobInformation => this.blobInformation;

      /// <summary>Поток, содержимое которого будет записано в BLOB-поле</summary>
      public Stream SourceStream => this.sourceStream;

      /// <summary>
      /// Поток, в который записывается данные вместо записи напрямую в базу данных.
      /// Запись в поток производится только при вызове метода WriteData(true).
      /// После получения и работы с потоком требуется его закрытие!
      /// </summary>
      public Stream VirtualStream => this.virtualStream;

      /// <summary>Требуется ли закрывать поток-источник</summary>
      public bool CloseSourceStream => this.closeSourceStream;

      private string FileZipExclusions
      {
        get
        {
          if (this.fileZipExclusions == null)
          {
            using (SessionKeeper sessionKeeper = this.session == null ? new SessionKeeper() : (SessionKeeper) null)
            {
              IDBConfigurations configurations = (this.session == null ? sessionKeeper.Session : this.session).Configurations;
              if (configurations != null)
                this.fileZipExclusions = configurations.ReadString("KERNEL", "OptimizationSettings", nameof (FileZipExclusions), "", DBConfigMode.GlobalOnly);
            }
          }
          return this.fileZipExclusions;
        }
      }

      /// <summary>Вернуть список типов файлов, которые не нужно сжимать</summary>
      /// <param name="includingDefaultZipExclusions">Включая расширения, которые заведомо не сжимаются</param>
      /// <returns></returns>
      private string[] GetFileZipExclusionsList(bool includingDefaultZipExclusions)
      {
        string fileZipExclusions = this.FileZipExclusions;
        string[] zipExclusionsList;
        if (fileZipExclusions == null)
        {
          zipExclusionsList = new string[0];
        }
        else
        {
          zipExclusionsList = fileZipExclusions.Split(new char[2]
          {
            ';',
            ','
          }, StringSplitOptions.RemoveEmptyEntries);
          for (int index = 0; index < zipExclusionsList.Length; ++index)
            zipExclusionsList[index] = "." + zipExclusionsList[index].ToUpper();
        }
        if (includingDefaultZipExclusions)
        {
          List<string> stringList = new List<string>((IEnumerable<string>) BlobProcWriter.DefaultZipExclusions);
          for (int index = 0; index < zipExclusionsList.Length; ++index)
          {
            if (stringList.IndexOf(zipExclusionsList[index]) == -1)
              stringList.Add(zipExclusionsList[index]);
          }
          zipExclusionsList = stringList.ToArray();
        }
        return zipExclusionsList;
      }

      /// <summary>
      /// Если пишем файл и он подпадает под список исключений, то не выполнять упаковку данных
      /// </summary>
      /// <param name="aBlobInformation">Описание BLOB-поля</param>
      protected void CheckForZipExclusions(ref BlobInformation aBlobInformation)
      {
        if (aBlobInformation.FileName == null || aBlobInformation.ArcMethod == ArcMethods.NotPacked)
          return;
        string ext = Path.GetExtension(aBlobInformation.FileName);
        if (!Array.Exists<string>(this.GetFileZipExclusionsList(true), (Predicate<string>) (listItem => string.Compare(listItem, ext, true) == 0)))
          return;
        aBlobInformation.ArcMethod = ArcMethods.NotPacked;
      }

      /// <summary>Создать класс для записи информации в BLOB-поле</summary>
      /// <param name="aElementID">ID объекта/связи</param>
      /// <param name="aAttributableElement">Признак объект/связь</param>
      /// <param name="aAttributeID">Идентификатор атрибута</param>
      /// <param name="aIndex">Индекс значения (для многозначного атрибута)</param>
      /// <param name="aDataBlockSize">Размер блока (0 - по умолчанию)</param>
      /// <param name="aBlobInformation">Описание BLOB-поля (не заполнять BlobID, RealSize и PacketSize; заполнять Name, Note, ArcMethod, ModifyDate)</param>
      /// <param name="aSourceStream">Поток-источник (содержимое будет записано в BLOB-поле)</param>
      /// <param name="aProgress">Событие, сообщающее процент выполнения процесса записи (0-100)</param>
      /// <param name="aThreadFinish">Событие, происходящее при завершении записи данных при записи данных в отдельном потоке</param>
      public BlobProcWriter(
        long aElementID,
        AttributableElements aAttributableElement,
        int aAttributeID,
        int aIndex,
        int aDataBlockSize,
        BlobInformation aBlobInformation,
        Stream aSourceStream,
        BlobProcCustomClass.ProgressEventHandler aProgress,
        BlobProcCustomClass.ThreadFinishEventHandler aThreadFinish)
      {
        this.lIDBAttribute = (IDBAttribute) null;
        this.elementID = aElementID;
        this.attributableElement = aAttributableElement;
        this.attributeID = aAttributeID;
        this.index = aIndex;
        this.dataBlockSize = aDataBlockSize;
        this.blobInformation = aBlobInformation;
        this.sourceStream = aSourceStream;
        if (aProgress != null)
          this.Progress += aProgress;
        if (aThreadFinish == null)
          return;
        this.ThreadFinish += aThreadFinish;
      }

      /// <summary>Создать класс для записи информации в BLOB-поле</summary>
      /// <param name="aIDBAttribute">Интерфейс атрибута, в который необходимо произвести запись</param>
      /// <param name="aDataBlockSize">Размер блока (0 - по умолчанию)</param>
      /// <param name="aBlobInformation">Описание BLOB-поля (не заполнять BlobID, RealSize и PacketSize; заполнять Name, Note, ArcMethod, ModifyDate)</param>
      /// <param name="aSourceStream">Поток-источник (содержимое будет записано в BLOB-поле)</param>
      /// <param name="aProgress">Событие, сообщающее процент выполнения процесса записи (0-100)</param>
      /// <param name="aThreadFinish">Событие, происходящее при завершении записи данных при записи данных в отдельном потоке</param>
      public BlobProcWriter(
        IDBAttribute aIDBAttribute,
        int aDataBlockSize,
        BlobInformation aBlobInformation,
        Stream aSourceStream,
        BlobProcCustomClass.ProgressEventHandler aProgress,
        BlobProcCustomClass.ThreadFinishEventHandler aThreadFinish)
      {
        this.lIDBAttribute = aIDBAttribute;
        this.elementID = 0L;
        this.attributableElement = AttributableElements.None;
        this.attributeID = 0;
        this.index = 0;
        this.dataBlockSize = aDataBlockSize;
        this.blobInformation = aBlobInformation;
        this.sourceStream = aSourceStream;
        if (aProgress != null)
          this.Progress += aProgress;
        if (aThreadFinish == null)
          return;
        this.ThreadFinish += aThreadFinish;
      }

      /// <summary>
      /// Выполнить запись информации в BLOB-поле в фоновом потоке. На серверной стороне использовать вариант функции с сессией.
      /// </summary>
      /// <param name="aCloseSourceStream">Закрывать ли поток при завершении процесса</param>
      /// <returns>Фоновый поток, в котором выполняется запись информации</returns>
      public Thread WriteDataThread(bool aCloseSourceStream)
      {
        return this.WriteDataThread((IUserSession) null, aCloseSourceStream);
      }

      /// <summary>
      /// Выполнить запись информации в BLOB-поле в фоновом потоке
      /// </summary>
      /// <param name="uSession">обязательна для записи на серверной стороне</param>
      /// <param name="aCloseSourceStream">Закрывать ли поток при завершении процесса</param>
      /// <returns>Фоновый поток, в котором выполняется запись информации</returns>
      public Thread WriteDataThread(IUserSession uSession, bool aCloseSourceStream)
      {
        this.session = uSession;
        this.CheckForZipExclusions(ref this.blobInformation);
        this.virtualWrite = false;
        this.inThread = true;
        this.closeSourceStream = aCloseSourceStream;
        this.thread = new Thread(new ThreadStart(this.WriteDataCustom));
        this.thread.IsBackground = true;
        this.thread.Start();
        return this.thread;
      }

      /// <summary>
      /// Записать информацию в BLOB-поле. На серверной стороне использовать вариант функции с сессией.
      /// </summary>
      /// <param name="virtualWrite">При значении true производится запись информации в virtualStream, а не в базу данных. Также производится заполнение BlobInformation</param>
      public void WriteData(bool virtualWrite) => this.WriteData((IUserSession) null, virtualWrite);

      /// <summary>Записать информацию в BLOB-поле</summary>
      /// <param name="virtualWrite">При значении true производится запись информации в virtualStream, а не в базу данных. Также производится заполнение BlobInformation</param>
      /// <param name="uSession">обязательна для записи на серверной стороне</param>
      public void WriteData(IUserSession uSession, bool virtualWrite)
      {
        this.session = uSession;
        this.CheckForZipExclusions(ref this.blobInformation);
        this.virtualWrite = virtualWrite;
        this.inThread = false;
        this.closeSourceStream = false;
        this.WriteDataCustom();
      }

      /// <summary>Записать информацию в BLOB-поле</summary>
      public void WriteData() => this.WriteData((IUserSession) null);

      /// <summary>Записать информацию в BLOB-поле</summary>
      /// <param name="uSession">сессия</param>
      public void WriteData(IUserSession uSession) => this.WriteData(uSession, false);

      private void WriteDataCustom()
      {
        using (SessionKeeper sessionKeeper = this.session == null ? new SessionKeeper() : (SessionKeeper) null)
        {
          IUserSession userSession = this.session == null ? sessionKeeper.Session : (this.inThread ? this.session.Clone("BlobProcessor.WriteDataCustom") : this.session);
          try
          {
            this.mode = BlobProcessorMode.Unknown;
            this.result = false;
            using (RemoteLock remoteLock = new RemoteLock())
            {
              try
              {
                int num1 = 0;
                this.mode = BlobProcessorMode.Write;
                this.OnProgress(0);
                IDBAttribute dbAttribute = (IDBAttribute) null;
                try
                {
                  userSession.ClearObjectSmartCache();
                  dbAttribute = this.lIDBAttribute == null ? BlobProcCustomClass.GetAttributeInterface(this.elementID, this.attributableElement, this.attributeID, this.index, userSession) : this.lIDBAttribute;
                  if (dbAttribute == null)
                    throw new AttributeNotFoundException(this.attributeID, this.elementID);
                  if (!(dbAttribute is IBlobWriter blobWriter))
                    return;
                  bool flag = blobWriter is IBlobWriterEx;
                  remoteLock.Add((object) dbAttribute);
                  string str = Path.IsPathRooted(this.blobInformation.FileName) ? Path.GetFileName(this.blobInformation.FileName) : this.blobInformation.FileName;
                  this.blobInformation.RealFileSize = this.sourceStream.Length;
                  this.blobInformation.PackedFileSize = 0L;
                  this.blobInformation.FileName = str;
                  this.sourceStream.Position = 0L;
                  if (this.virtualWrite || !flag)
                  {
                    Stream stream = (Stream) new ImChunkedStream();
                    try
                    {
                      if (this.blobInformation.ArcMethod == ArcMethods.ZLibPacked)
                      {
                        IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
                        BlobProcessorMode mode = this.mode;
                        this.mode = BlobProcessorMode.Pack;
                        try
                        {
                          service.PackStream(stream, this.sourceStream, 5, new PercentEventHandler(((BlobProcCustomClass) this).OnPackProgress));
                        }
                        finally
                        {
                          this.mode = mode;
                        }
                      }
                      else
                        this.sourceStream.CopyTo(stream);
                      this.blobInformation.PackedFileSize = stream.Length;
                      stream.Position = 0L;
                      if (this.virtualWrite)
                        this.virtualStream = stream;
                      else if (blobWriter.OpenBlob(this.blobInformation, false))
                      {
                        try
                        {
                          byte[] numArray = new byte[stream.Length];
                          stream.Read(numArray, 0, numArray.Length);
                          blobWriter.WriteDataBlock(numArray);
                        }
                        catch
                        {
                          blobWriter.CancelWrite();
                          throw;
                        }
                      }
                    }
                    finally
                    {
                      if (!this.virtualWrite)
                        stream.Close();
                    }
                  }
                  else
                  {
                    using (BlobWriterStream blobWriterStream = new BlobWriterStream(dbAttribute, 0, this.blobInformation, userSession))
                    {
                      int count1 = 4096 /*0x1000*/;
                      long num2 = this.sourceStream.Length % (long) count1 == 0L ? this.sourceStream.Length / (long) count1 : this.sourceStream.Length / (long) count1 + 1L;
                      byte[] buffer = new byte[count1];
                      long num3 = 1;
                      for (; num3 <= num2; ++num3)
                      {
                        int count2 = this.sourceStream.Read(buffer, 0, count1);
                        blobWriterStream.Write(buffer, 0, count2);
                        int int32 = Convert.ToInt32(num3 * 100L / num2);
                        if (int32 > num1)
                        {
                          num1 = int32;
                          this.OnProgress(int32);
                        }
                        if (this.inThread)
                        {
                          Thread.Sleep(0);
                          if (this.abortThreadFlag)
                            throw new MyThreadAbortException();
                        }
                      }
                      blobWriterStream.Commit();
                    }
                  }
                  this.mode = BlobProcessorMode.Unknown;
                  this.OnProgress(100);
                  this.result = true;
                }
                finally
                {
                  if (this.closeSourceStream)
                    this.sourceStream.Close();
                  if (this.inThread && this.result)
                  {
                    this.inThread = false;
                    IBlobReader blobReader = dbAttribute as IBlobReader;
                    try
                    {
                      BlobInformation bi = blobReader.OpenBlob(-1);
                      this.OnThreadFinish(this.result, MessageDialogs.msgSuccess, (Exception) null, bi);
                    }
                    catch (Exception ex)
                    {
                      this.result = false;
                      this.OnThreadFinish(this.result, ex.Message, ex, BlobInformation.EmptyBlobInformation());
                    }
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
          finally
          {
            if (this.session != null && this.inThread)
              userSession.Logout("BlobProcessor.WriteDataCustom");
          }
        }
      }
    }
}
