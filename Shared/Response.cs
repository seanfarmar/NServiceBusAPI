using System;
using NServiceBus;

namespace Shared
{
  public class Response : IMessage
  {
    public Guid DataId { get; set; }
  }
}