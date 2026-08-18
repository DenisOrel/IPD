
// Type: Intermech.Diagnostics.CompressedStackTrace
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;
using System.Reflection;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Реализует упрощенное представление стека вызова, достаточное для восстановления точного места падения исключения.
    /// </summary>
    [Serializable]
    public class CompressedStackTrace
    {
      private readonly CompressedStackFrame[] compressedFrames;

      /// <summary>Создает объект.</summary>
      /// <param name="stackTrace">Полный стек вызова</param>
      public CompressedStackTrace(StackTrace stackTrace)
      {
        this.compressedFrames = stackTrace != null ? new CompressedStackFrame[stackTrace.FrameCount] : throw new ArgumentNullException(nameof (stackTrace));
        for (int index = 0; index < this.compressedFrames.Length; ++index)
        {
          StackFrame frame = stackTrace.GetFrame(index);
          int ilOffset = frame.GetILOffset();
          if (ilOffset >= 0)
          {
            MethodBase method = frame.GetMethod();
            if (method != (MethodBase) null)
            {
              string assemblyFileName = CompressedStackTrace.TryGetAssemblyFileName(method.Module.Assembly);
              if (!string.IsNullOrEmpty(assemblyFileName))
                this.compressedFrames[index] = new CompressedStackFrame(assemblyFileName, method.MetadataToken, ilOffset);
            }
          }
        }
      }

      public CompressedStackTrace(CompressedStackFrame[] frames)
      {
        this.compressedFrames = frames != null ? frames : throw new ArgumentNullException(nameof (frames));
      }

      /// <summary>Возвращает количество кадров в стеке.</summary>
      public int FrameCount => this.compressedFrames.Length;

      /// <summary>Возвращает указанный кадр стека.</summary>
      /// <param name="frameIndex">Индекс кадра в стеке</param>
      /// <returns>Кадр стека или null, если эта информация недоступна</returns>
      /// <exception cref="T:System.ArgumentNullException">Индекс кадра находится вне допустимого диапазона</exception>
      public CompressedStackFrame TryGetFrame(int frameIndex)
      {
        if (frameIndex < 0 || frameIndex >= this.compressedFrames.Length)
          throw new ArgumentOutOfRangeException(nameof (frameIndex));
        return this.compressedFrames[frameIndex];
      }

      private static string TryGetAssemblyFileName(Assembly assembly)
      {
        return assembly.IsDynamic ? (string) null : assembly.ManifestModule.Name;
      }
    }
}
