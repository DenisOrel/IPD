// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.BeforeDraftCreateEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Аргументы для события, возникающего перед созданием заготовки нового объекта
/// </summary>
public sealed class BeforeDraftCreateEventArgs
{
  /// <summary>Идентификатор типа создаваемого объекта</summary>
  public int ObjectTypeID { get; private set; }

  /// <summary>
  /// Идентификатор шаблона, по которому будет создаваться заготовка.
  /// </summary>
  public long TemplateID { get; private set; }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="creatingObjType">Идентификатор типа создаваемого объекта</param>
  /// <param name="templateID">Идентификатор шаблона, по которому будет создаваться заготовка. </param>
  public BeforeDraftCreateEventArgs(int creatingObjType, long templateID)
  {
    this.ObjectTypeID = creatingObjType;
    this.TemplateID = templateID;
  }
}
