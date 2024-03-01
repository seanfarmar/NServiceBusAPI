using System;
using NServiceBus;

namespace Shared
{
  public class Request : IMessage
  {
    public Guid DataId { get; set; }
  }
}