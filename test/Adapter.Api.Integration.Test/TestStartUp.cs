using System;
using System.Collections.Generic;
using System.Text;
using AdpterApi;
using Microsoft.Extensions.Configuration;

namespace Adapter.Api.Integration.Test
{
    public class TestStartUp:Startup
    {
        public TestStartUp(IConfiguration configuration) : base(configuration) { }
    }
}
