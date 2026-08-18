
// Type: Intermech.Interfaces.Briefcase.TemporaryStorage
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.IO;
using System.IO;


namespace Intermech.Interfaces.Briefcase
{
    /// <summary>Хранилище временных файлов</summary>
    public class TemporaryStorage
    {
      private const int _bufferSize = 16384 /*0x4000*/;
      private const string TempFolderName = "TemporaryData";
      private FilesStorage FStorage;

      public TemporaryStorage() => this.FStorage = new FilesStorage(string.Empty, "TemporaryData");

      /// <summary>
      /// Возвращает полное имя файла с путем во временном хранилище
      /// </summary>
      /// <param name="FileName">Имя файла без пути</param>
      /// <returns></returns>
      public string GetFullFileName(string FileName) => this.FStorage.GetFullFileName(FileName);

      /// <summary>Создает новый темповый файл и записывает туда Content</summary>
      /// <param name="FileName">Имя файла</param>
      /// <param name="Content">Наполнение</param>
      /// <returns></returns>
      public long Create(string FileName, FileStream Content)
      {
        FileName = this.FStorage.GetFullFileName(FileName);
        FileStream IFileStream = new FileStream(FileName, FileMode.Create);
        return this.WriteIntoFile(FileName, Content, IFileStream, (PercentEventHandler) null);
      }

      public long Create(string FileName, FileStream Content, PercentEventHandler handler)
      {
        FileName = this.FStorage.GetFullFileName(FileName);
        FileStream IFileStream = new FileStream(FileName, FileMode.Create);
        return this.WriteIntoFile(FileName, Content, IFileStream, handler);
      }

      /// <summary>
      /// Открывает уже созданный или если такого нет, создает новый темповый файл и записывает туда Content
      /// </summary>
      /// <param name="FileName">Имя файла</param>
      /// <param name="Content">Наполнение</param>
      /// <returns>Размер получившегося файла</returns>
      public long Append(string FileName, FileStream Content)
      {
        FileName = this.FStorage.GetFullFileName(FileName);
        FileStream IFileStream = new FileStream(FileName, FileMode.Append, FileAccess.Write);
        return this.WriteIntoFile(FileName, Content, IFileStream, (PercentEventHandler) null);
      }

      public void Delete(string[] FileNames)
      {
        foreach (string fileName in FileNames)
        {
          if (!string.IsNullOrWhiteSpace(fileName))
          {
            if (this.FStorage.FileExists(fileName))
            {
              try
              {
                this.FStorage.DeleteFile(fileName);
              }
              catch
              {
              }
            }
          }
        }
      }

      public long Append(string FileName, FileStream Content, PercentEventHandler handler)
      {
        FileName = this.FStorage.GetFullFileName(FileName);
        FileStream IFileStream = new FileStream(FileName, FileMode.Append, FileAccess.Write);
        return this.WriteIntoFile(FileName, Content, IFileStream, handler);
      }

      public void Write(string FileName, byte[] Content, int Length)
      {
        FileStream fileStream = (FileStream) null;
        try
        {
          FileName = this.FStorage.GetFullFileName(FileName);
          fileStream = new FileStream(FileName, FileMode.Append, FileAccess.Write);
          fileStream.Write(Content, 0, Length);
        }
        finally
        {
          if (fileStream != null)
          {
            fileStream.Flush();
            fileStream.Close();
          }
        }
      }

      public FileStream GetStreamRead(string FileName)
      {
        FileName = this.FStorage.GetFullFileName(FileName);
        return new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Read);
      }

      public FileStream GetStreamWrite(string FileName)
      {
        FileName = this.FStorage.GetFullFileName(FileName);
        return new FileStream(FileName, FileMode.Append, FileAccess.Write);
      }

      /// <summary>Удалить временные файлы</summary>
      public void Clear()
      {
        try
        {
          this.FStorage.Clear();
        }
        catch
        {
        }
      }

      private long WriteIntoFile(
        string FileName,
        FileStream Content,
        FileStream IFileStream,
        PercentEventHandler handler)
      {
        try
        {
          byte[] buffer = new byte[16384 /*0x4000*/];
          long length = Content.Length;
          long num = 0;
          int count;
          while ((count = Content.Read(buffer, 0, 16384 /*0x4000*/)) > 0)
          {
            IFileStream.Write(buffer, 0, count);
            if (handler != null && length > 0L)
            {
              num += (long) count;
              double percent = (double) num / (double) length * 100.0;
              handler((object) this, new PercentEventArgs((int) percent));
            }
          }
          if (handler != null)
            handler((object) this, new PercentEventArgs(100));
          return IFileStream.Length;
        }
        catch
        {
          return 0;
        }
        finally
        {
          IFileStream.Flush();
          IFileStream.Close();
        }
      }
    }
}
