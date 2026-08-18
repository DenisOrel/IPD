
// Type: Intermech.Interfaces.InformationNode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    [Serializable]
    public class InformationNode : List<InformationNode>
    {
      /// <summary>имя узла в xml</summary>
      private string nodeName;
      /// <summary>значение</summary>
      private string nodeValue;
      /// <summary>тип - атрибут или узел</summary>
      private NodeType type;

      /// <summary>имя узла в xml</summary>
      public string NodeName => this.nodeName;

      /// <summary>значение</summary>
      public string NodeValue => this.nodeValue;

      /// <summary>тип - атрибут или узел</summary>
      public NodeType Type => this.type;

      public InformationNode(string nodeName, string nodeValue, NodeType type)
      {
        this.nodeName = nodeName;
        this.nodeValue = nodeValue;
        this.type = type;
      }

      public InformationNode(string nodeName, string nodeValue)
        : this(nodeName, nodeValue, NodeType.Element)
      {
      }

      public InformationNode(string nodeName)
        : this(nodeName, string.Empty)
      {
      }

      public override string ToString() => $"Name: {this.nodeName} , Value: {this.nodeValue}";

      public new void Add(InformationNode item)
      {
        if (this.Type == NodeType.Attribute)
          throw new Exception("Атрибут не может иметь дочерних элементов!");
        base.Add(item);
      }
    }
}
