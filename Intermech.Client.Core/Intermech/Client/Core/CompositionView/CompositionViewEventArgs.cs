
// Type: Intermech.Client.Core.CompositionView.CompositionViewEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;


namespace Intermech.Client.Core.CompositionView;

/// <summary>Класс описывающий аргументы (общие)</summary>
public class CompositionViewEventArgs
{
  /// <summary>
  /// 
  /// </summary>
  private CVButtonMethod _method;
  /// <summary>
  /// 
  /// </summary>
  private List<long> _relationIDs = new List<long>();

  /// <summary>Конструктор</summary>
  /// <param name="method">вызванный метод</param>
  public CompositionViewEventArgs(CVButtonMethod method) => this._method = method;

  /// <summary>Метод</summary>
  public CVButtonMethod Method => this._method;

  /// <summary>
  /// Список идентификаторов связей для
  /// обновления в дереве
  /// </summary>
  public List<long> RelationIDs => this._relationIDs;
}
