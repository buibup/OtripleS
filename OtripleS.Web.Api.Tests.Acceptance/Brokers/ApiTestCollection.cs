using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OtripleS.Web.Api.Tests.Acceptance.Brokers
{
    [CollectionDefinition(nameof(ApiTestCollection))]
    public class ApiTestCollection : ICollectionFixture<OtripleSApiBroker>
    {

    }
}
