
// Type: Intermech.Client.Core.IFindOrReplaceController
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;


namespace Intermech.Client.Core;

/// <summary> Интерфейс, который должен поддерживать формы способные переключаться между поиском или поиском с заменой </summary>
public interface IFindOrReplaceController
{
  /// <summary> Если true, то производиться поиск с заменой, если false, то производиться простой поиск </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  bool IsReplaceMode { get; set; }
}
