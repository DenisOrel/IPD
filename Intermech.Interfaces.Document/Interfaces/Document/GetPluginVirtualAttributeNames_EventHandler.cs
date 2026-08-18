// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.GetPluginVirtualAttributeNames_EventHandler
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System.Collections.Specialized;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Делегат для вызова внутри метода GetVirtualAttributeNames</summary>
/// <param name="attributeNames">Список в который добавляются имена атрибутов</param>
/// <param name="forSaveOnly">Добавлять в список только те атрибуты, которые должны сохраниться в XML или копироваться при копировании через буфер</param>
public delegate void GetPluginVirtualAttributeNames_EventHandler(
  object sender,
  StringCollection attributeNames,
  bool forSaveOnly);
