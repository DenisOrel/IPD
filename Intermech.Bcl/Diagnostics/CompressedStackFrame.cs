
// Type: Intermech.Diagnostics.CompressedStackFrame
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Реализует упрощенное представление кадра стека вызова, достаточное для восстановления точного места падения исключения.
    /// </summary>
    [Serializable]
    public sealed class CompressedStackFrame
    {
      private readonly string assemblyFileName;
      private readonly int methodToken;
      private readonly int ilOffset;

      /// <summary>Создает объект.</summary>
      /// <param name="assemblyFileName">Имя файла сборки без пути</param>
      /// <param name="methodToken">Уникальный токен метода внутри сборки</param>
      /// <param name="ilOffset">Смещение от начала IL-кода метода в байтах до текущей выполняемой инструкции</param>
      public CompressedStackFrame(string assemblyFileName, int methodToken, int ilOffset)
      {
        if (string.IsNullOrEmpty(assemblyFileName))
          throw new ArgumentException("Имя файла сборки не должно быть пустым.", nameof (assemblyFileName));
        if (methodToken < 0)
          throw new ArgumentOutOfRangeException(nameof (methodToken));
        if (ilOffset < 0)
          throw new ArgumentOutOfRangeException(nameof (ilOffset));
        this.assemblyFileName = assemblyFileName;
        this.methodToken = methodToken;
        this.ilOffset = ilOffset;
      }

      /// <summary>Возвращает имя файла сборки без пути.</summary>
      public string AssemblyFileName => this.assemblyFileName;

      /// <summary>Возвращает уникальный токен метода внутри сборки.</summary>
      public int MethodToken => this.methodToken;

      /// <summary>
      /// Возвращает смещение от начала IL-кода метода в байтах до текущей выполняемой инструкции. Это смещение
      /// может быть аппроксимацией в зависимости от режима работы JIT.
      /// </summary>
      public int ILOffset => this.ilOffset;
    }
}
