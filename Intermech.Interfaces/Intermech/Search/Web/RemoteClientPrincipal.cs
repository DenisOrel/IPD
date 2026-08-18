
// Type: Intermech.Search.Web.RemoteClientPrincipal
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Security;
using System;


namespace Intermech.Search.Web
{
    public sealed class RemoteClientPrincipal : IPSPrincipal
    {
      public RemoteClientPrincipal(
        IPSIdentity identity,
        Guid securityToken,
        IPSBuiltInRole role,
        RemoteClientDescription remoteClientDescription)
        : base(identity, securityToken, role)
      {
        this.RemoteClientDescription = remoteClientDescription != null ? remoteClientDescription : throw new ArgumentNullException(nameof (remoteClientDescription));
      }

      public RemoteClientDescription RemoteClientDescription { get; private set; }

      public override IPSPrincipal Clone()
      {
        return (IPSPrincipal) new RemoteClientPrincipal(this.Identity, this.SecurityToken, this.Role, this.RemoteClientDescription);
      }
    }
}
