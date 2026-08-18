// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.FormulaNode
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Expressions;
using Intermech.Interfaces;
using System;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class FormulaNode(IUserSession session, XmlNode node) : XMLPropertyNode<string>(session, node, "F_FORMULA")
{
  protected override string GetValue(IUserSession session, string nodeAttributeValue)
  {
    string text = nodeAttributeValue;
    if (nodeAttributeValue != string.Empty)
    {
      using (Parser parser = new Parser())
      {
        parser.AutoDetectVariables = true;
        parser.Validate = false;
        ExpressionVariablesCollection variables = parser.Parse(text).Variables;
        for (int index = 0; index < variables.Count; ++index)
        {
          if (!(variables[index].Name.ToUpper() == "VALUE") && GuidHelper.IsGuid(variables[index].Name))
          {
            IDBAttributeType attributeType = session.GetAttributeType(new Guid(variables[index].Name), true);
            text = text.Replace(variables[index].Name, attributeType.Name);
          }
        }
      }
    }
    return text;
  }
}
