
// Type: Intermech.Remoting.Optimized.OptimizedBinaryClientFormatterSinkProvider
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Globalization;
using System.Runtime.Remoting.Channels;
using System.Runtime.Serialization.Formatters;
using System.Security.Permissions;


namespace Intermech.Remoting.Optimized
{
    public class OptimizedBinaryClientFormatterSinkProvider : 
      IClientFormatterSinkProvider,
      IClientChannelSinkProvider
    {
      private IClientChannelSinkProvider _next;
      private bool _includeVersioning;
      private bool _strictBinding;
      private TypeFilterLevel _formatterSecurityLevel;
      private Lazy<FormatterSinkSharedData> _formatterSinkSharedData;
      private Func<IClientFormatterSinkInterceptor> _interceptors;

      public IClientChannelSinkProvider Next
      {
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)] get
        {
          return this._next;
        }
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)] set
        {
          this._next = value;
        }
      }

      public OptimizedBinaryClientFormatterSinkProvider()
        : this((IDictionary) null, (ICollection) null)
      {
      }

      public OptimizedBinaryClientFormatterSinkProvider(
        IDictionary properties,
        ICollection providerData)
      {
        this._includeVersioning = true;
        this._strictBinding = false;
        this._formatterSecurityLevel = TypeFilterLevel.Full;
        this._formatterSinkSharedData = new Lazy<FormatterSinkSharedData>(new Func<FormatterSinkSharedData>(this.CreateFormatterSinkSharedData));
        if (properties == null)
          return;
        foreach (DictionaryEntry property in properties)
        {
          switch (property.Key.ToString())
          {
            case "interceptors":
              this._interceptors = property.Value as Func<IClientFormatterSinkInterceptor>;
              continue;
            case "includeVersions":
              this._includeVersioning = Convert.ToBoolean(property.Value, (IFormatProvider) CultureInfo.InvariantCulture);
              continue;
            case "strictBinding":
              this._strictBinding = Convert.ToBoolean(property.Value, (IFormatProvider) CultureInfo.InvariantCulture);
              continue;
            default:
              continue;
          }
        }
      }

      private FormatterSinkSharedData CreateFormatterSinkSharedData()
      {
        return new FormatterSinkSharedData(FormatterSinkChannelProtocol.Other, this._includeVersioning, this._strictBinding, this._formatterSecurityLevel);
      }

      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)]
      public IClientChannelSink CreateSink(
        IChannelSender channel,
        string url,
        object remoteChannelData)
      {
        IClientChannelSink nextSink = (IClientChannelSink) null;
        if (this._next != null)
        {
          nextSink = this._next.CreateSink(channel, url, remoteChannelData);
          if (nextSink == null)
            return (IClientChannelSink) null;
        }
        return (IClientChannelSink) new OptimizedBinaryClientFormatterSink(nextSink, this._formatterSinkSharedData.Value, this._interceptors);
      }
    }
}
