
// Type: Intermech.Controls.OleContainer.OleHelper
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Edanmo.OleStorage;
using System;
using System.Collections;
using System.IO;


namespace Intermech.Controls.OleContainer;

/// <summary>Хелпер для работы с OLE</summary>
public sealed class OleHelper
{
  /// <summary>Размер буффера по умолчанию</summary>
  private const int cnt_Buffer_Size = 4096 /*0x1000*/;
  /// <summary>
  /// 
  /// </summary>
  private const string cnt_EntryContents = "CONTENTS";
  /// <summary>
  /// 
  /// </summary>
  private const string cnt_EntryOle10Native = "OLE10NATIVE";

  /// <summary>Получение имя для времееного файла</summary>
  /// <returns></returns>
  private static string GetTempFileName() => Path.GetTempFileName();

  /// <summary>
  /// 
  /// </summary>
  private OleHelper()
  {
  }

  /// <summary>
  /// "Распаковка" OLE attachment объекта и извлечение "внутреннего" файла
  /// </summary>
  /// <remarks>Чтение из потока производим с тек. позиции. Вызывающий сам должен позаботиться о корректности данных.
  /// Тип внутренних данных заранее не известен (это может быть jpeg, bmp, или наприм документ WORD)</remarks>
  /// <param name="stream">Данные OLE объекта </param>
  /// <returns>"Внутренние" данные OLE или null</returns>
  public static System.IO.Stream ExtractOleData(System.IO.Stream stream)
  {
    if (stream == null || stream.Length == 0L || stream.Length == stream.Position)
      return (System.IO.Stream) null;
    string tempFileName = OleHelper.GetTempFileName();
    using (FileStream fileStream = new FileStream(tempFileName, FileMode.OpenOrCreate))
    {
      try
      {
        byte[] buffer = new byte[4096 /*0x1000*/];
        for (int count = stream.Read(buffer, 0, 4096 /*0x1000*/); count > 0; count = stream.Read(buffer, 0, 4096 /*0x1000*/))
          fileStream.Write(buffer, 0, count);
      }
      finally
      {
        fileStream.Flush();
        fileStream.Close();
      }
    }
    Storage storage = (Storage) null;
    try
    {
      if (!Storage.IsCompoundStorageFile(tempFileName))
        return (System.IO.Stream) null;
      storage = new Storage(tempFileName);
      foreach (StatStg element in (ReadOnlyCollectionBase) storage.Elements())
      {
        if (element.Type == StatStg.ElementType.Stream)
        {
          string str = element.Name.Trim();
          if (str.IndexOf("CONTENTS", StringComparison.OrdinalIgnoreCase) > 0)
          {
            using (Edanmo.OleStorage.Stream stream1 = storage.OpenStream(element.Name))
            {
              System.IO.Stream destination = (System.IO.Stream) new MemoryStream((int) stream1.Length);
              stream1.CopyTo(destination);
              destination.Position = 0L;
              return destination;
            }
          }
          if (str.IndexOf("OLE10NATIVE", StringComparison.OrdinalIgnoreCase) > 0)
          {
            using (Edanmo.OleStorage.Stream inputStream = storage.OpenStream(element.Name))
            {
              inputStream.Position = 0L;
              using (Ole10Native ole10Native = new Ole10Native((System.IO.Stream) inputStream))
              {
                if (ole10Native.NativeData == null)
                  return (System.IO.Stream) null;
                MemoryStream oleData = new MemoryStream(ole10Native.NativeData);
                oleData.Position = 0L;
                return (System.IO.Stream) oleData;
              }
            }
          }
        }
      }
    }
    finally
    {
      if (storage != null)
      {
        storage.Close();
        ((IDisposable) storage).Dispose();
      }
      if (File.Exists(tempFileName))
      {
        try
        {
          File.Delete(tempFileName);
        }
        catch (Exception ex)
        {
          switch (ex)
          {
            case IOException _:
            case UnauthorizedAccessException _:
              break;
            default:
              throw;
          }
        }
      }
    }
    return (System.IO.Stream) null;
  }
}
