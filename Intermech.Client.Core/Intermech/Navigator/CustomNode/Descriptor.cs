
// Type: Intermech.Navigator.CustomNode.Descriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;


namespace Intermech.Navigator.CustomNode;

/// <summary>
/// Дескриптор для создания произвольного составного узла "Навигатора"
/// </summary>
public class Descriptor : HiveDescriptor
{
  /// <summary>Коллекция дескрпипторов</summary>
  protected DescriptorCollection _descriptors;

  /// <summary>Создать дескриптор составного узла "Навигатора"</summary>
  /// <param name="caption">Заголовок узла</param>
  /// <param name="descriptors">Коллекция дескрипторов частей узла "Навигатора"</param>
  public Descriptor(string caption, DescriptorCollection descriptors)
    : base(Intermech.Navigator.Consts.CategoryCustomNode, 0, caption)
  {
    this._descriptors = descriptors;
  }

  /// <summary>Создать дескриптор составного узла "Навигатора"</summary>
  /// <param name="categoryID">Категория узла</param>
  /// <param name="typeID">Тип узла</param>
  /// <param name="caption">Заголовок узла</param>
  /// <param name="descriptors">Коллекция дескрипторов частей узла "Навигатора"</param>
  public Descriptor(int categoryID, int typeID, string caption, DescriptorCollection descriptors)
    : base(categoryID, typeID, caption)
  {
    this._descriptors = descriptors;
  }

  public Descriptor(PersistentState state)
    : base(state)
  {
    this._descriptors = (DescriptorCollection) FormatterServices.RestoreObject((PersistentState) state.GetValue("Descriptors"));
  }

  public override void GetObjectData(PersistentState state)
  {
    base.GetObjectData(state);
    if (this._descriptors == null)
      return;
    state.AddValue("Descriptors", (object) FormatterServices.GetObjectState((object) this._descriptors));
  }

  public override INode GetChild(INodeID nodeID)
  {
    return ((INodesFactory) ServicesManager.GetService(typeof (IFactory))).GetNode(nodeID, (object) this._descriptors);
  }

  public override bool Equals(object obj)
  {
    if (!(obj is Descriptor descriptor1))
      return base.Equals(obj);
    if (descriptor1._descriptors.Count != this._descriptors.Count)
      return false;
    bool flag = true;
    for (int index = 0; index < descriptor1._descriptors.Count; ++index)
    {
      IDescriptor descriptor2 = descriptor1._descriptors[index];
      IDescriptor descriptor3 = this._descriptors[index];
      flag = ((descriptor2 == null || descriptor3 == null ? 0 : (descriptor2.Equals((object) descriptor3) ? 1 : 0)) & (flag ? 1 : 0)) != 0;
      if (!flag)
        return false;
    }
    return flag;
  }

  public override int GetHashCode()
  {
    return this._categoryID.GetHashCode() ^ this._typeID.GetHashCode() ^ (this._caption != null ? this._caption.GetHashCode() : 0) ^ this._descriptors.Count;
  }
}
