
// Type: Intermech.IO.PathUtils
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;


namespace Intermech.IO
{
    public static class PathUtils
    {
      [ThreadStatic]
      private static PathComparer threadBoundPathComparer;
      private static readonly char[] Separators = new char[2]
      {
        Path.DirectorySeparatorChar,
        Path.AltDirectorySeparatorChar
      };

      public static bool IsSamePath(string firstPath, string secondPath)
      {
        return PathUtils.CurrentPathComparer.Compare(firstPath, secondPath) == 0;
      }

      /// <summary>
      /// Проверяет, содержится ли элемент в указанной папке или ее подпапках.
      /// </summary>
      /// <param name="itemPath">Путь к проверяемому элементу</param>
      /// <param name="locationPath">Путь к папке</param>
      /// <returns>Результат проверки</returns>
      public static bool IsPlacedIn(string itemPath, string locationPath)
      {
        if (!Path.IsPathRooted(itemPath))
          throw new InvalidOperationException();
        if (!Path.IsPathRooted(locationPath))
          throw new InvalidOperationException();
        itemPath = Path.GetFullPath(itemPath);
        locationPath = Path.GetFullPath(locationPath);
        int num = locationPath.Length - 1;
        if ((int) locationPath[num] == (int) Path.DirectorySeparatorChar || (int) locationPath[num] == (int) Path.PathSeparator)
          locationPath = locationPath.Remove(num, 1);
        return itemPath.Length >= locationPath.Length && PathUtils.IsSamePath(itemPath.Substring(0, locationPath.Length), locationPath);
      }

      /// <summary>
      /// Преобразовывает путь в относительный по указанному базовому пути.
      /// </summary>
      /// <param name="itemPath">Преобразовываемый путь</param>
      /// <param name="baseDir">Базовый путь</param>
      /// <param name="options">Опции, управляющие процессом вычисления относительного пути</param>
      /// <returns>Результат преобразования. Может быть null, если невозможно сформировать относительный путь</returns>
      /// <exception cref="T:System.InvalidOperationException">Невозможно вычислить относительный путь</exception>
      public static string GetRelativePath(
        string itemPath,
        string baseDir,
        RelativePathOptions options)
      {
        if (itemPath == null)
          throw new ArgumentNullException(nameof (itemPath));
        if (baseDir == null)
          throw new ArgumentNullException(nameof (baseDir));
        PathComparer currentPathComparer = PathUtils.CurrentPathComparer;
        List<string> stringList1 = PathUtils.SplitPath(itemPath);
        List<string> stringList2 = PathUtils.SplitPath(baseDir);
        if (Path.IsPathRooted(stringList1[0]) && Path.IsPathRooted(stringList2[0]) || !Path.IsPathRooted(stringList1[0]) && !Path.IsPathRooted(stringList2[0]))
        {
          if (currentPathComparer.Compare(stringList1[0], stringList2[0]) != 0)
          {
            if ((options & RelativePathOptions.ThrowIfNotPossible) != RelativePathOptions.None)
              throw new InvalidOperationException();
            return (string) null;
          }
          int num1 = stringList1.Count - 1;
          int num2 = Math.Min(num1, stringList2.Count);
          int num3 = num2;
          for (int index = 1; index < num2; ++index)
          {
            if (currentPathComparer.Compare(stringList1[index], stringList2[index]) != 0)
            {
              num3 = index;
              break;
            }
          }
          if (num3 == num1 && num3 >= stringList2.Count)
            return stringList1[num1];
          using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(itemPath.Length))
          {
            StringBuilder stringBuilder = objectPoolScope.Object;
            int num4 = stringList2.Count - num3;
            if (num4 > 0)
            {
              if ((options & RelativePathOptions.AllowEnterToParentDirectory) == RelativePathOptions.None)
              {
                if ((options & RelativePathOptions.ThrowIfNotPossible) != RelativePathOptions.None)
                  throw new InvalidOperationException();
                return (string) null;
              }
              for (int index = 0; index < num4; ++index)
              {
                stringBuilder.Append("..");
                stringBuilder.Append(Path.DirectorySeparatorChar);
              }
            }
            for (int index = num3; index < num1; ++index)
            {
              stringBuilder.Append(stringList1[index]);
              stringBuilder.Append(Path.DirectorySeparatorChar);
            }
            stringBuilder.Append(stringList1[num1]);
            return stringBuilder.ToString();
          }
        }
        if ((options & RelativePathOptions.ThrowIfNotPossible) != RelativePathOptions.None)
          throw new InvalidOperationException();
        return (string) null;
      }

      public static List<string> SplitPath(string path)
      {
        if (path == null)
          throw new ArgumentNullException(nameof (path));
        List<string> stringList = new List<string>(8);
        if (PathUtils.HasTerminalSeparator(path))
          path = path.Remove(path.Length - 1, 1);
        string path1 = Path.GetPathRoot(path);
        if (!string.IsNullOrEmpty(path1))
        {
          int length = path1.Length;
          if (PathUtils.HasTerminalSeparator(path1))
            path1 = path1.Remove(path1.Length - 1, 1);
          stringList.Add(path1);
          path = path.Remove(0, length);
        }
        if (path.Length > 0)
          stringList.AddRange((IEnumerable<string>) path.Split(PathUtils.Separators, StringSplitOptions.None));
        return stringList;
      }

      public static bool HasTerminalSeparator(string path)
      {
        if (path == null)
          throw new ArgumentNullException(nameof (path));
        return path.Length > 0 && Array.IndexOf(PathUtils.Separators, path[path.Length - 1], 0) >= 0;
      }

      public static string AddTerminalSeparator(string path)
      {
        if (!PathUtils.HasTerminalSeparator(path))
          path += Path.DirectorySeparatorChar.ToString();
        return path;
      }

      /// <summary>
      /// Возвращает объект для сравнения файловых имен и путей для текущего потока.
      /// </summary>
      public static PathComparer CurrentPathComparer
      {
        [DebuggerStepThrough] get
        {
          if (PathUtils.threadBoundPathComparer == null)
            PathUtils.threadBoundPathComparer = new PathComparer();
          return PathUtils.threadBoundPathComparer;
        }
      }

      [NotNull]
      public static string GetFileNameWithoutLastExtension([NotNull] string fileName)
      {
        if (string.IsNullOrWhiteSpace(fileName))
          return string.Empty;
        if (fileName.IndexOfAny(PathUtils.Separators) >= 0)
          fileName = Path.GetFileName(fileName);
        int length = fileName.LastIndexOf('.');
        if (length >= 0)
          fileName = fileName.Substring(0, length);
        return fileName;
      }
    }
}
