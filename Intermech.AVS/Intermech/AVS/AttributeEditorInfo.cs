// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AttributeEditorInfo
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.Collections.Generic;
using System.Drawing.Design;

#nullable disable
namespace Intermech.AVS;

/// <summary>Вспомогательный класс для внутреннего использования.
/// Кэшируют информацию о редактировании атрибута</summary>
[Serializable]
public class AttributeEditorInfo
{
  /// <summary>Только для чтения</summary>
  public bool? ReadOnly;
  /// <summary>Список допустимых стилей редактирования</summary>
  public List<UITypeEditorEditStyle> EditorStyleList;
  /// <summary>Можно ли редактировать как текст по месту</summary>
  public bool? CanInplaceEdit;
  /// <summary>Редактор был закэширован</summary>
  public bool EditorCached;
  /// <summary>Редактор</summary>
  public IAttributeEditorControl Editor;
}
