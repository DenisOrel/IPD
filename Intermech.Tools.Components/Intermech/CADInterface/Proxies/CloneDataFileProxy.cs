// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CloneDataFileProxy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Interop.CADInterface;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Реализует обертку для COM-объекта клонируемого файла (интерфейс ICloneDataFile).
/// </summary>
public class CloneDataFileProxy : CADSystemComponentProxy
{
  private CloneDataFile rawObject;

  /// <summary>Создает объект.</summary>
  /// <param name="cadSystem">Объект CAD-системы</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="cadSystem" /> содержит null</exception>
  public CloneDataFileProxy(CADSystemProxy cadSystem)
    : base(cadSystem)
  {
    this.rawObject = (CloneDataFile) new CloneDataFileClass();
  }

  /// <summary>
  /// Возвращает или задает исходный путь к файлу (т.е. до клонирования)
  /// </summary>
  /// <exception cref="T:System.ArgumentNullException">Значение свойства не должно быть равно null</exception>
  public string OriginalPath
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICloneDataFile.get_OriginalPath()");
      try
      {
        return this.rawObject.OriginalPath;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "ICloneDataFile.get_OriginalPath()");
      }
    }
    set
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace<string>("ICloneDataFile.set_OriginalPath()", value);
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      try
      {
        this.rawObject.OriginalPath = value;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "ICloneDataFile.set_OriginalPath()");
      }
    }
  }

  /// <summary>
  /// Возвращает или задает результирующий путь к файлу (т.е. после клонирования).
  /// Значение свойства может совпадать с <see cref="P:Intermech.CADInterface.Proxies.CloneDataFileProxy.OriginalPath" />, если клонирование не требуется.
  /// </summary>
  /// <exception cref="T:System.ArgumentNullException">Значение свойства не должно быть равно null</exception>
  public string NewPath
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICloneDataFile.get_NewPath()");
      try
      {
        return this.rawObject.NewPath;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "ICloneDataFile.get_NewPath()");
      }
    }
    set
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace<string>("ICloneDataFile.set_NewPath()", value);
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      try
      {
        this.rawObject.NewPath = value;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "ICloneDataFile.set_NewPath()");
      }
    }
  }

  /// <summary>Возвращает или задает результат клонирования.</summary>
  public CloneDataFileResult Result
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICloneDataFile.get_Result()");
      ECloneResult result;
      try
      {
        result = this.rawObject.Result;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "ICloneDataFile.get_Result()");
      }
      return this.FromNativeResult(result);
    }
    set
    {
      ECloneResult nativeResult = this.ToNativeResult(value);
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace<ECloneResult>("ICloneDataFile.set_Result()", nativeResult);
      try
      {
        this.rawObject.Result = nativeResult;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "ICloneDataFile.set_Result()");
      }
    }
  }

  /// <summary>
  /// Возвращает или задает подробную информацию об ошибке клонирования файла.
  /// Значение может быть пусто, если файл еще не был клонирован, либо был клонирован успешно.
  /// </summary>
  /// <exception cref="T:System.ArgumentNullException">Значение свойства не должно быть равно null</exception>
  public string ErrorMessage
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICloneDataFile.get_ErrorMessage()");
      try
      {
        return this.rawObject.ErrorMessage;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "ICloneDataFile.get_ErrorMessage()");
      }
    }
    set
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace<string>("ICloneDataFile.set_ErrorMessage()", value);
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      try
      {
        this.rawObject.ErrorMessage = value;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "ICloneDataFile.set_ErrorMessage()");
      }
    }
  }

  private CloneDataFileResult FromNativeResult(ECloneResult nativeResult)
  {
    switch (nativeResult)
    {
      case ECloneResult.CR_NotProcessed:
        return CloneDataFileResult.NotProcessed;
      case ECloneResult.CR_RemainUnchanged:
        return CloneDataFileResult.RemainUnchanged;
      case ECloneResult.CR_Failed:
        return CloneDataFileResult.Failed;
      case ECloneResult.CR_FileOnly:
        return CloneDataFileResult.FileOnly;
      case ECloneResult.CR_FileAndAttributes:
        return CloneDataFileResult.FileAndAttributes;
      default:
        throw new NotSupportedEnumException((Enum) nativeResult);
    }
  }

  private ECloneResult ToNativeResult(CloneDataFileResult result)
  {
    switch (result)
    {
      case CloneDataFileResult.NotProcessed:
        return ECloneResult.CR_NotProcessed;
      case CloneDataFileResult.RemainUnchanged:
        return ECloneResult.CR_RemainUnchanged;
      case CloneDataFileResult.Failed:
        return ECloneResult.CR_Failed;
      case CloneDataFileResult.FileOnly:
        return ECloneResult.CR_FileOnly;
      case CloneDataFileResult.FileAndAttributes:
        return ECloneResult.CR_FileAndAttributes;
      default:
        throw new NotSupportedEnumException((Enum) result);
    }
  }

  /// <summary>
  /// Возвращает "сырой" COM-объект. Это свойство должно использоваться в тех случаях,
  /// когда текущий объект-обертку требуется передать наружу во внешнее приложение
  /// через COM-интерфейс. Внутри IPS должен использоваться только текущий объект-обертка.
  /// </summary>
  public CloneDataFile RawObject
  {
    [DebuggerStepThrough] get => this.rawObject;
  }
}
