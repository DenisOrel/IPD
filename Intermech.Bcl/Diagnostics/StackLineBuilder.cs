
// Type: Intermech.Diagnostics.StackLineBuilder
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Pools;
using Intermech.Text;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;


namespace Intermech.Diagnostics
{
    public class StackLineBuilder
    {
      private static readonly Regex Pattern = new Regex("^(?<Intro>\\s*\\w\\S*\\s+)?(?<ThrowLocation>(?<AssemblyName>\\S.*?)!0x(?<MethodToken>[\\da-f]+?)\\+0x(?<ILOffset>[\\da-f]+?)!)?(?<MethodName>\\S[^\\[\\]()]*?)(\\[(?<MethodGenericArgs>.+?)\\])?\\((?<MethodArgs>.*?)\\)(?<Outro>.*)$", RegexOptions.Compiled | RegexOptions.Singleline);
      private static readonly string ConstructorName = ".ctor";
      private static readonly string StaticConstructorName = ".cctor";
      private string introText;
      private CompressedStackFrame throwLocation;
      private string fullTypeName;
      private string methodName;
      private string methodGenericArgs;
      private string methodArgs;
      private string outroText;
      private string stringCache;

      private StackLineBuilder(
        string introText,
        CompressedStackFrame throwLocation,
        string fullTypeName,
        string methodName,
        string methodGenericArgs,
        string methodArgs,
        string outroText)
      {
        this.introText = introText;
        this.throwLocation = throwLocation;
        this.fullTypeName = fullTypeName;
        this.methodName = methodName;
        this.methodGenericArgs = methodGenericArgs;
        this.methodArgs = methodArgs;
        this.outroText = outroText;
      }

      private void ResetCachedValues() => this.stringCache = (string) null;

      /// <summary>
      /// Возвращает или задает вводный текст. Значение свойства может быть пустым.
      /// </summary>
      public string IntroText
      {
        get => this.introText;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (IntroText));
          if (!(this.introText != value))
            return;
          this.introText = value;
          this.ResetCachedValues();
        }
      }

      /// <summary>
      /// Возвращает или задает точку падения исключение внутри метода. Значение свойства может быть не задано.
      /// </summary>
      public CompressedStackFrame ThrowLocation
      {
        get => this.throwLocation;
        set
        {
          if (this.throwLocation == value)
            return;
          this.throwLocation = value;
          this.ResetCachedValues();
        }
      }

      /// <summary>
      /// Возвращает или задает полное имя типа, включая пространство имен и имена внешних типов. В качестве разделителя используется для внешних типов используется
      /// символ '.', поэтому внешние типы нельзя отличить от пространства имен.
      /// </summary>
      public string FullTypeName
      {
        get => this.fullTypeName;
        set
        {
          if (string.IsNullOrEmpty(value))
            throw new ArgumentException("Значение свойства не должно быть пустым.", nameof (FullTypeName));
          if (!(this.fullTypeName != value))
            return;
          this.fullTypeName = value;
          this.ResetCachedValues();
        }
      }

      /// <summary>Возвращает или задает имя метода.</summary>
      public string MethodName
      {
        get => this.methodName;
        set
        {
          if (string.IsNullOrEmpty(value))
            throw new ArgumentException("Значение свойства не должно быть пустым.", nameof (MethodName));
          if (!(this.methodName != value))
            return;
          this.methodName = value;
          this.ResetCachedValues();
        }
      }

      /// <summary>
      /// Возвращает или задает текст аргументов для generic-методов. Значение свойства может быть пустым.
      /// </summary>
      public string MethodGenericArguments
      {
        get => this.methodGenericArgs;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (MethodGenericArguments));
          if (!(this.methodGenericArgs != value))
            return;
          this.methodGenericArgs = value;
          this.ResetCachedValues();
        }
      }

      /// <summary>
      /// Возвращает или задает текст аргументов вызова метода. Значение свойства может быть пустым.
      /// </summary>
      public string MethodArguments
      {
        get => this.methodArgs;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (MethodArguments));
          if (!(this.methodArgs != value))
            return;
          this.methodArgs = value;
          this.ResetCachedValues();
        }
      }

      /// <summary>
      /// Возвращает или задает заключительный текст, который следует за сигнатурой метода. Значение свойства может быть пустым.
      /// </summary>
      public string OutroText
      {
        get => this.outroText;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (OutroText));
          if (!(this.outroText != value))
            return;
          this.outroText = value;
          this.ResetCachedValues();
        }
      }

      /// <summary>Возвращает строковое представление.</summary>
      /// <returns>Строковое представление</returns>
      public override string ToString()
      {
        if (this.stringCache == null)
          this.stringCache = this.CompileString();
        return this.stringCache;
      }

      private string CompileString()
      {
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(this.fullTypeName.Length + this.methodName.Length + 30))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          if (!string.IsNullOrEmpty(this.introText))
          {
            stringBuilder.Append(this.introText);
            if (!char.IsWhiteSpace(this.introText[this.introText.Length - 1]))
              stringBuilder.Append(' ');
          }
          if (this.throwLocation != null)
            stringBuilder.AppendFormat("{0}!0x{1:x8}+0x{2:x4}!", (object) this.throwLocation.AssemblyFileName, (object) this.throwLocation.MethodToken, (object) this.throwLocation.ILOffset);
          stringBuilder.Append(this.fullTypeName);
          stringBuilder.Append('.');
          stringBuilder.AppendFormat(this.methodName);
          if (!string.IsNullOrEmpty(this.methodGenericArgs))
          {
            stringBuilder.Append('[');
            stringBuilder.Append(this.methodGenericArgs);
            stringBuilder.Append(']');
          }
          stringBuilder.Append('(');
          if (!string.IsNullOrEmpty(this.methodArgs))
            stringBuilder.Append(this.methodArgs);
          stringBuilder.Append(')');
          if (!string.IsNullOrEmpty(this.outroText))
          {
            if (!char.IsWhiteSpace(this.outroText[0]))
              stringBuilder.Append(' ');
            stringBuilder.Append(this.outroText);
          }
          return stringBuilder.ToString();
        }
      }

      /// <summary>Возвращает true, если это конструктор типа.</summary>
      public bool IsConstructor => this.methodName == StackLineBuilder.ConstructorName;

      /// <summary>Возвращает true, если статический конструктор типа.</summary>
      public bool IsStaticConstructor => this.methodName == StackLineBuilder.StaticConstructorName;

      /// <summary>
      /// Выделяет из строки stack trace имя вызванного метода, включая имя типа и пространства имен. Особенностью формирования stack trace является то, что
      /// внешние типы разделяются также с помощью '.', поэтому внешний тип неотличим от пространства имен.
      /// </summary>
      /// <param name="textLine">Строка stack trace</param>
      /// <returns>Выделенное имя вызванного метода или null</returns>
      public static StackLineBuilder TryParse(string textLine)
      {
        Match m = textLine != null ? StackLineBuilder.Pattern.Match(textLine) : throw new ArgumentNullException(nameof (textLine));
        if (!m.Success)
          return (StackLineBuilder) null;
        string introText = m.Groups["Intro"].Value;
        CompressedStackFrame throwLocation = StackLineBuilder.ParseThrowLocation(m);
        string typeAndMethod = m.Groups["MethodName"].Value;
        string methodName = StackLineBuilder.TryParseMethodName(typeAndMethod);
        if (methodName == null)
          return (StackLineBuilder) null;
        string fullTypeName = typeAndMethod.Substring(0, typeAndMethod.Length - methodName.Length - 1);
        string methodGenericArgs = m.Groups["MethodGenericArgs"].Value;
        string methodArgs = m.Groups["MethodArgs"].Value;
        string outroText = m.Groups["Outro"].Value;
        return new StackLineBuilder(introText, throwLocation, fullTypeName, methodName, methodGenericArgs, methodArgs, outroText);
      }

      private static CompressedStackFrame ParseThrowLocation(Match m)
      {
        if (!m.Groups["ThrowLocation"].Success)
          return (CompressedStackFrame) null;
        string assemblyFileName = m.Groups["AssemblyName"].Value;
        int num1 = int.Parse(m.Groups["MethodToken"].Value, NumberStyles.HexNumber);
        int num2 = int.Parse(m.Groups["ILOffset"].Value, NumberStyles.HexNumber);
        int methodToken = num1;
        int ilOffset = num2;
        return new CompressedStackFrame(assemblyFileName, methodToken, ilOffset);
      }

      private static string TryParseMethodName(string typeAndMethod)
      {
        if (typeAndMethod.EndsWith(StackLineBuilder.ConstructorName))
          return StackLineBuilder.ConstructorName;
        if (typeAndMethod.EndsWith(StackLineBuilder.StaticConstructorName))
          return StackLineBuilder.StaticConstructorName;
        int num = typeAndMethod.LastIndexOf('.');
        return num > 0 ? typeAndMethod.Substring(num + 1, typeAndMethod.Length - num - 1) : (string) null;
      }
    }
}
