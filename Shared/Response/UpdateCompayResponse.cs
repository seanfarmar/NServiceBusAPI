using System;

namespace Shared.Responses
{
    using Shared.Models;

    [Serializable]
    public class UpdateCompanyResponse
    {
        public UpdateCompanyResponse()
        {
            DataId = Guid.NewGuid();
        }

        public Guid DataId { get; set; }
        public Company Company { get; set; }
    }
}