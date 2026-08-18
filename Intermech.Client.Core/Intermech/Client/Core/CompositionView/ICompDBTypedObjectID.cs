
// Type: Intermech.Client.Core.CompositionView.ICompDBTypedObjectID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;


namespace Intermech.Client.Core.CompositionView;

/// <summary>Composition's dbTyped object interface</summary>
public interface ICompDBTypedObjectID : IDBTypedObjectID, IDBObjectID
{
  /// <summary>
  /// Storing additional info object (Example: Imbase table record)
  /// </summary>
  object InfoObject { get; set; }
}
