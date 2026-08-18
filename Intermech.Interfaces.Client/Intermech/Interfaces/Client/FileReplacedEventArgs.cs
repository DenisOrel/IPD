// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.FileReplacedEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Аргументы для события замены файла в файловом атрибуте.
/// </summary>
public class FileReplacedEventArgs : NotificationEventArgs
{
  private readonly AttributableElements attributableElement;
  private readonly long elementID;
  private readonly int elementType;
  private readonly int attributeID;
  private readonly int replaceFileIndex;
  private readonly FileTypes fileType;

  public FileReplacedEventArgs(
    string eventName,
    AttributableElements attributableElement,
    long elementID,
    int elementType,
    int attributeID,
    int replaceFileIndex,
    FileTypes fileType)
    : base(eventName)
  {
    this.attributableElement = attributableElement;
    this.elementID = elementID;
    this.elementType = elementType;
    this.attributeID = attributeID;
    this.replaceFileIndex = replaceFileIndex;
    this.fileType = fileType;
  }

  /// <summary>Индекс заменённого файла</summary>
  public int ReplaceFileIndex
  {
    [DebuggerStepThrough] get => this.replaceFileIndex;
  }

  /// <summary>Вид элемента - объект IPS, связь и т.д.</summary>
  public AttributableElements AttributableElement
  {
    [DebuggerStepThrough] get => this.attributableElement;
  }

  /// <summary>
  /// Идентификатор объекта или связи, у которого произошла замена
  /// </summary>
  public long ElementID
  {
    [DebuggerStepThrough] get => this.elementID;
  }

  /// <summary>
  /// Идентификатор типа объекта или связи, у которого произошла замена
  /// </summary>
  public int ElementType
  {
    [DebuggerStepThrough] get => this.elementType;
  }

  /// <summary>Тип файла где произошла замена</summary>
  public FileTypes FileType
  {
    [DebuggerStepThrough] get => this.fileType;
  }

  /// <summary>
  /// Идентификатор атрибута в котором произошла замена файла
  /// </summary>
  public int AttributeID
  {
    [DebuggerStepThrough] get => this.attributeID;
  }
}
