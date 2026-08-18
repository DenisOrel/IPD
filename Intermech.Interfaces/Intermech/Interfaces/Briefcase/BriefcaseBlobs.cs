
// Type: Intermech.Interfaces.Briefcase.BriefcaseBlobs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.IO;
using System.Text;


namespace Intermech.Interfaces.Briefcase
{
    /// <summary>
    /// Работа с memo и blob, сохраняемых/извлекаемых из портфеля
    /// </summary>
    public class BriefcaseBlobs
    {
      private static string GetFileName(
        long id,
        int attrId,
        long blobMemoId,
        string baseFolder,
        bool createMissingFolders,
        bool isBlob)
      {
        string empty = string.Empty;
        string str1 = id.ToString("X");
        string str2 = (str1.Length < 2 ? new string('0', 2 - str1.Length) : string.Empty) + str1;
        string str3 = str2.Substring(str2.Length - 2);
        string str4 = attrId.ToString("X");
        string str5 = (str4.Length < 2 ? new string('0', 2 - str4.Length) : string.Empty) + str4;
        string str6 = str5.Substring(str5.Length - 2);
        string str7 = blobMemoId.ToString("X");
        string str8 = (str7.Length < 2 ? new string('0', 2 - str7.Length) : string.Empty) + str7;
        string str9 = str8.Substring(str8.Length - 2);
        string str10 = blobMemoId.ToString("X");
        string str11 = (str10.Length < 8 ? new string('0', 8 - str10.Length) : string.Empty) + str10;
        char directorySeparatorChar = Path.DirectorySeparatorChar;
        string fileName = baseFolder + directorySeparatorChar.ToString() + str3 + directorySeparatorChar.ToString() + str6 + directorySeparatorChar.ToString() + str9 + directorySeparatorChar.ToString() + str11 + (isBlob ? BriefcaseConsts.BlobFileExt : BriefcaseConsts.MemoFileExt);
        if (createMissingFolders)
        {
          string path1 = baseFolder;
          if (!Directory.Exists(path1))
            Directory.CreateDirectory(path1);
          string path2 = path1 + directorySeparatorChar.ToString() + str3;
          if (!Directory.Exists(path2))
            Directory.CreateDirectory(path2);
          string path3 = path2 + directorySeparatorChar.ToString() + str6;
          if (!Directory.Exists(path3))
            Directory.CreateDirectory(path3);
          string path4 = path3 + directorySeparatorChar.ToString() + str9;
          if (!Directory.Exists(path4))
            Directory.CreateDirectory(path4);
        }
        return fileName;
      }

      public static string GetBlobFileName(
        long id,
        int attrId,
        long blobId,
        string baseBlobFolder,
        bool createMissingFolders)
      {
        try
        {
          return BriefcaseBlobs.GetFileName(id, attrId, blobId, baseBlobFolder, createMissingFolders, true);
        }
        catch
        {
        }
        return string.Empty;
      }

      public static string GetMemoFileName(
        long id,
        int attrId,
        long memoId,
        string baseMemoFolder,
        bool createMissingFolders)
      {
        try
        {
          return BriefcaseBlobs.GetFileName(id, attrId, memoId, baseMemoFolder, createMissingFolders, false);
        }
        catch
        {
        }
        return string.Empty;
      }

      public static bool CreateFoldersForFile(string filename, string baseFolder)
      {
        char directorySeparatorChar = Path.DirectorySeparatorChar;
        string str1 = baseFolder + directorySeparatorChar.ToString();
        if (filename.IndexOf(str1) != 0)
          return false;
        string str2 = filename.Substring(str1.Length);
        int length1 = str2.LastIndexOf(Path.DirectorySeparatorChar);
        if (length1 >= 0)
          str2 = str2.Substring(0, length1);
        if (str2.Length == 0)
          return true;
        bool flag = true;
        while (flag)
        {
          string empty = string.Empty;
          int length2 = str2.IndexOf(Path.DirectorySeparatorChar);
          string str3;
          if (length2 > 0)
          {
            str3 = str2.Substring(0, length2);
            str2 = str2.Substring(length2 + 1);
          }
          else
          {
            str3 = str2;
            flag = false;
          }
          baseFolder = baseFolder + directorySeparatorChar.ToString() + str3;
          try
          {
            if (!Directory.Exists(baseFolder))
              Directory.CreateDirectory(baseFolder);
          }
          catch
          {
            return false;
          }
        }
        return true;
      }

      public static bool WriteBlob(
        string filename,
        string basefolder,
        IBlobReader br,
        int dataBlockSize,
        out BlobInformation bi,
        out Exception exception)
      {
        exception = (Exception) null;
        bi = new BlobInformation(0L, 0L, DateTime.MinValue, "", ArcMethods.NotPacked, "");
        try
        {
          bi = br.OpenBlob(dataBlockSize);
          try
          {
            if (bi.PackedFileSize != 0L)
            {
              if (!BriefcaseBlobs.CreateFoldersForFile(filename, basefolder))
                return false;
              try
              {
                using (BinaryWriter binaryWriter = new BinaryWriter((Stream) new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read), Encoding.UTF8))
                {
                  long num = bi.PackedFileSize % (long) dataBlockSize == 0L ? bi.PackedFileSize / (long) dataBlockSize : bi.PackedFileSize / (long) dataBlockSize + 1L;
                  for (long index = 1; index <= num; ++index)
                  {
                    byte[] buffer = br.ReadDataBlock();
                    binaryWriter.Write(buffer, 0, buffer.Length);
                  }
                }
              }
              catch (Exception ex)
              {
                exception = ex;
                return false;
              }
            }
          }
          finally
          {
            br.CloseBlob();
          }
        }
        catch (Exception ex)
        {
          exception = ex;
          return false;
        }
        return true;
      }

      public static bool WriteBlob(
        string filename,
        string basefolder,
        byte[] array,
        out Exception exception)
      {
        exception = (Exception) null;
        if (array != null && array.Length != 0)
        {
          if (!BriefcaseBlobs.CreateFoldersForFile(filename, basefolder))
            return false;
          try
          {
            using (FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read))
              fileStream.Write(array, 0, array.Length);
          }
          catch (Exception ex)
          {
            exception = ex;
            return false;
          }
        }
        return true;
      }

      public static bool WriteMemo(
        string filename,
        string basefolder,
        char[] array,
        out Exception exception)
      {
        exception = (Exception) null;
        if (array != null && array.Length != 0)
        {
          if (!BriefcaseBlobs.CreateFoldersForFile(filename, basefolder))
            return false;
          try
          {
            using (FileStream output = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read))
              new BinaryWriter((Stream) output, BriefcaseConsts.MemoEncoding).Write(array, 0, array.Length);
          }
          catch (Exception ex)
          {
            exception = ex;
            return false;
          }
        }
        return true;
      }
    }
}
