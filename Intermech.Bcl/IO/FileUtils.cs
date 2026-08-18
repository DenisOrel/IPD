
// Type: Intermech.IO.FileUtils
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.IO;


namespace Intermech.IO
{
    public static class FileUtils
    {
      public static void DeleteFileSilently(string fullPath)
      {
        if (fullPath == null)
          throw new ArgumentNullException(nameof (fullPath));
        try
        {
          if (!File.Exists(fullPath))
            return;
          File.SetAttributes(fullPath, FileAttributes.Normal);
          File.Delete(fullPath);
        }
        catch (Exception ex)
        {
          switch (ex)
          {
            case IOException _:
              break;
            case UnauthorizedAccessException _:
              break;
            default:
              throw;
          }
        }
      }

      public static void DeleteDirectorySilently(string fullPath, bool recursive)
      {
        if (fullPath == null)
          throw new ArgumentNullException(nameof (fullPath));
        try
        {
          if (!Directory.Exists(fullPath))
            return;
          Directory.Delete(fullPath, recursive);
        }
        catch (Exception ex)
        {
          switch (ex)
          {
            case IOException _:
              break;
            case UnauthorizedAccessException _:
              break;
            default:
              throw;
          }
        }
      }

      public static Tuple<PathCollection, bool> DeleteFilesSilently(string dirPath, bool recursive)
      {
        string[] strArray = dirPath != null ? Directory.GetFiles(dirPath, "*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly) : throw new ArgumentNullException(nameof (dirPath));
        PathCollection pathCollection = new PathCollection(strArray.Length);
        foreach (string str in strArray)
        {
          if (File.Exists(str) && FileUtils.CanDeleteFile(str))
          {
            File.SetAttributes(str, FileAttributes.Normal);
            File.Delete(str);
            pathCollection.Add(PathUtils.GetRelativePath(str, dirPath, RelativePathOptions.ThrowIfNotPossible));
          }
        }
        return Tuple.Create(pathCollection, pathCollection.Count == strArray.Length);
      }

      public static void DeleteFilesSilently(ICollection<string> files)
      {
        if (files == null)
          throw new ArgumentNullException(nameof (files));
        foreach (string file in (IEnumerable<string>) files)
        {
          if (File.Exists(file) && FileUtils.CanDeleteFile(file))
            FileUtils.DeleteFileSilently(file);
        }
      }

      public static bool CanDeleteFile(string fullPath)
      {
        if (fullPath == null)
          throw new ArgumentNullException(nameof (fullPath));
        try
        {
          File.Open(fullPath, FileMode.Open, FileAccess.Read, FileShare.Delete).Close();
          return true;
        }
        catch (IOException ex)
        {
          return false;
        }
        catch (UnauthorizedAccessException ex)
        {
          return false;
        }
      }

      public static bool CanWriteFile(string fullPath)
      {
        if (fullPath == null)
          throw new ArgumentNullException(nameof (fullPath));
        try
        {
          new FileStream(fullPath, FileMode.Open, FileAccess.Write, FileShare.ReadWrite).Close();
          return true;
        }
        catch (IOException ex)
        {
          return false;
        }
        catch (UnauthorizedAccessException ex)
        {
          return false;
        }
      }

      public static bool GetReadOnlyAttribute(string fullPath)
      {
        if (fullPath == null)
          throw new ArgumentNullException(nameof (fullPath));
        return (File.GetAttributes(fullPath) & FileAttributes.ReadOnly) != 0;
      }

      public static void SetReadOnlyAttribute(string fullPath, bool readOnly)
      {
        if (fullPath == null)
          throw new ArgumentNullException(nameof (fullPath));
        if (!File.Exists(fullPath))
          return;
        FileAttributes attributes = File.GetAttributes(fullPath);
        FileAttributes fileAttributes = readOnly ? attributes | FileAttributes.ReadOnly : attributes & ~FileAttributes.ReadOnly;
        if (fileAttributes == attributes)
          return;
        File.SetAttributes(fullPath, fileAttributes);
      }
    }
}
