
// Type: Intermech.Remoting.RemotingXmlDataHack
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;


namespace Intermech.Remoting
{
    public sealed class RemotingXmlDataHack
    {
      private readonly string filename;
      private XmlDocument document;
      private static readonly Dictionary<string, string> emptyProperties = new Dictionary<string, string>(0);

      public RemotingXmlDataHack(string filename)
      {
        this.filename = filename != null ? filename : throw new ArgumentNullException(nameof (filename));
        this.Port = 0;
      }

      public string ToFile()
      {
        if (this.document == null)
          return this.filename;
        string tempFileName = Path.GetTempFileName();
        this.document.Save(tempFileName);
        return tempFileName;
      }

      /// <summary>Порт</summary>
      public int Port { get; private set; }

      public string TryGetWellknownServiceUri(string serviceType)
      {
        if (serviceType == null)
          throw new ArgumentNullException(nameof (serviceType));
        XmlDocument document = this.GetDocument();
        XmlNode xmlNode = document.SelectSingleNode("/configuration/system.runtime.remoting/application/channels/channel[@ref='tcp' and @port and string-length(@port) != 0]");
        if (xmlNode != null)
        {
          string s = xmlNode.Attributes["port"].Value;
          int result;
          if (s != null && int.TryParse(s, out result))
            this.Port = result;
        }
        return document.SelectSingleNode($"/configuration/system.runtime.remoting/application/service/wellknown[@type='{serviceType}' and @objectUri and string-length(@objectUri) != 0]")?.Attributes["objectUri"].Value;
      }

      public void RemoveWellknownService(string serviceType)
      {
        XmlNode oldChild = serviceType != null ? this.GetDocument().SelectSingleNode($"/configuration/system.runtime.remoting/application/service/wellknown[@type='{serviceType}']") : throw new ArgumentNullException(nameof (serviceType));
        oldChild?.ParentNode.RemoveChild(oldChild);
      }

      public void ReplaceServerFormatter(
        string channelRef,
        string formatterRef,
        Type formatterProviderType,
        IDictionary<string, string> formatterProperties = null)
      {
        if (formatterProviderType == (Type) null)
          throw new ArgumentNullException(nameof (formatterProviderType));
        this.ReplaceServerFormatter(channelRef, formatterRef, this.GetAssemblyQualifiedNameWithoutVersion(formatterProviderType), formatterProperties);
      }

      public void ReplaceServerFormatter(
        string channelRef,
        string formatterRef,
        string formatterProviderType,
        IDictionary<string, string> formatterProperties = null)
      {
        if (channelRef == null)
          throw new ArgumentNullException(nameof (channelRef));
        if (formatterRef == null)
          throw new ArgumentNullException(nameof (formatterRef));
        if (formatterProviderType == null)
          throw new ArgumentNullException(nameof (formatterProviderType));
        if (formatterProperties == null)
          formatterProperties = (IDictionary<string, string>) RemotingXmlDataHack.emptyProperties;
        this.ReplaceChannelFormatter(channelRef, formatterRef, "serverProviders", formatterProviderType, formatterProperties);
      }

      public void ReplaceClientFormatter(
        string channelRef,
        string formatterRef,
        Type formatterProviderType,
        IDictionary<string, string> formatterProperties = null)
      {
        if (formatterProviderType == (Type) null)
          throw new ArgumentNullException(nameof (formatterProviderType));
        this.ReplaceClientFormatter(channelRef, formatterRef, this.GetAssemblyQualifiedNameWithoutVersion(formatterProviderType), formatterProperties);
      }

      public void ReplaceClientFormatter(
        string channelRef,
        string formatterRef,
        string formatterProviderType,
        IDictionary<string, string> formatterProperties = null)
      {
        if (channelRef == null)
          throw new ArgumentNullException(nameof (channelRef));
        if (formatterRef == null)
          throw new ArgumentNullException(nameof (formatterRef));
        if (formatterProviderType == null)
          throw new ArgumentNullException(nameof (formatterProviderType));
        if (formatterProperties == null)
          formatterProperties = (IDictionary<string, string>) RemotingXmlDataHack.emptyProperties;
        this.ReplaceChannelFormatter(channelRef, formatterRef, "clientProviders", formatterProviderType, formatterProperties);
      }

      private string GetAssemblyQualifiedNameWithoutVersion(Type type)
      {
        string typeName = Assembly.CreateQualifiedName(type.Assembly.GetName(false).Name, type.FullName);
        if (Type.GetType(typeName) != type)
          typeName = type.AssemblyQualifiedName;
        return typeName;
      }

      public bool HasChannelDefinition(string channelRef)
      {
        if (channelRef == null)
          throw new ArgumentNullException(nameof (channelRef));
        return this.GetDocument().SelectSingleNode($"/configuration/system.runtime.remoting/application/channels/channel[@ref='{channelRef}']") != null;
      }

      public void InjectServerSink(string channelRef, Type sinkProviderType)
      {
        if (sinkProviderType == (Type) null)
          throw new ArgumentNullException(nameof (sinkProviderType));
        this.InjectServerSink(channelRef, sinkProviderType.AssemblyQualifiedName);
      }

      public void InjectServerSink(string channelRef, string sinkProviderType)
      {
        if (channelRef == null)
          throw new ArgumentNullException(nameof (channelRef));
        if (sinkProviderType == null)
          throw new ArgumentNullException(nameof (sinkProviderType));
        this.InjectChannelSink(channelRef, "serverProviders", sinkProviderType, false);
      }

      public void InjectClientSink(string channelRef, Type sinkProviderType)
      {
        if (sinkProviderType == (Type) null)
          throw new ArgumentNullException(nameof (sinkProviderType));
        this.InjectClientSink(channelRef, sinkProviderType.AssemblyQualifiedName);
      }

      public void InjectClientSink(string channelRef, string sinkProviderType)
      {
        if (channelRef == null)
          throw new ArgumentNullException(nameof (channelRef));
        if (sinkProviderType == null)
          throw new ArgumentNullException(nameof (sinkProviderType));
        this.InjectChannelSink(channelRef, "clientProviders", sinkProviderType, true);
      }

      private void InjectChannelSink(
        string channelRef,
        string providersElement,
        string sinkProviderType,
        bool clientMode)
      {
        XmlDocument document = this.GetDocument();
        XmlNode xmlNode = document.SelectSingleNode($"/configuration/system.runtime.remoting/application/channels/channel[@ref='{channelRef}']/{providersElement}");
        if (xmlNode == null)
          throw new InvalidOperationException($"Unable to adjust the remoting configuration. A channel with ref '{channelRef}' or its providers section is not found in file '{this.filename}'.");
        XmlElement element = document.CreateElement("provider");
        element.Attributes.Append(document.CreateAttribute("type")).Value = sinkProviderType;
        if (clientMode)
          xmlNode.InsertAfter((XmlNode) element, xmlNode.SelectSingleNode("formatter") ?? throw new InvalidOperationException($"Unable to adjust the remoting configuration. A channel with ref '{channelRef}' in file '{this.filename}' must have a formatter."));
        else
          xmlNode.InsertBefore((XmlNode) element, xmlNode.FirstChild);
      }

      private void ReplaceChannelFormatter(
        string channelRef,
        string formatterRef,
        string providersElement,
        string formatterProviderType,
        IDictionary<string, string> formatterProperties)
      {
        XmlDocument document = this.GetDocument();
        XmlNode xmlNode = document.SelectSingleNode(string.Format("/configuration/system.runtime.remoting/application/channels/channel[@ref='{0}']/{2}/formatter[@ref='{1}']", (object) channelRef, (object) formatterRef, (object) providersElement));
        if (xmlNode == null)
          throw new InvalidOperationException(string.Format("Unable to adjust the remoting configuration. A channel with ref '{0}' in file '{2}' must have a formatter with ref '{1}'.", (object) channelRef, (object) formatterRef, (object) this.filename));
        xmlNode.Attributes.RemoveNamedItem("ref");
        xmlNode.Attributes.Append(document.CreateAttribute("type")).Value = formatterProviderType;
        foreach (KeyValuePair<string, string> formatterProperty in (IEnumerable<KeyValuePair<string, string>>) formatterProperties)
          xmlNode.Attributes.Append(document.CreateAttribute(formatterProperty.Key)).Value = formatterProperty.Value;
      }

      private XmlDocument GetDocument()
      {
        if (this.document == null)
        {
          try
          {
            this.document = new XmlDocument();
            this.document.PreserveWhitespace = true;
            this.document.Load(this.filename);
          }
          catch
          {
            this.document = (XmlDocument) null;
            throw;
          }
        }
        return this.document;
      }
    }
}
