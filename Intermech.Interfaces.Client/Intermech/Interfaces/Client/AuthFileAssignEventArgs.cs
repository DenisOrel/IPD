// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.AuthFileAssignEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

[Serializable]
public class AuthFileAssignEventArgs : EventArgs
{
  private bool isHandled;
  private int objectType;
  private bool internalDocument;
  private long objectId;
  private bool pdfOnly;

  /// <summary>Если true, то уже обработан другими обработчиками</summary>
  public bool IsHandled
  {
    get => this.isHandled;
    set => this.isHandled = value;
  }

  /// <summary>Тип объекта</summary>
  public int ObjectType => this.objectType;

  /// <summary>Идентификатор версии объекта</summary>
  public long ObjectId => this.objectId;

  /// <summary>
  /// Формировать аутентичные файлы только в формате pdf.
  /// Если можно создать аутентичный файл и PDFOnly = true, но создание в формате PDF не поддерживается, то isHandled = false
  /// </summary>
  public bool PDFOnly => this.pdfOnly;

  /// <summary>Внутренний документ IPS</summary>
  public bool InternalDocument
  {
    get => this.internalDocument;
    set => this.internalDocument = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="objectId">Версия объекта</param>
  public AuthFileAssignEventArgs(int objectType, long objectId)
    : this(objectType, objectId, false)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="objectId">Версия объекта</param>
  /// <param name="pdfOnly">Формировать аутентичные файлы только в формате pdf</param>
  public AuthFileAssignEventArgs(int objectType, long objectId, bool pdfOnly)
  {
    this.objectType = objectType;
    this.objectId = objectId;
    this.pdfOnly = pdfOnly;
  }
}
