using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RegisterSystem.Tests.IntegrationTests.TestUtils.Extensions
{
    public static class ProblemDetailsExtensions
    {
        public static Dictionary<string, List<string>> GetErrors(this ProblemDetails details)
        {
            return JsonSerializer.Deserialize<Dictionary<string, List<string>>>(details.Extensions["errors"].ToString());
        }

        public static List<string> GetErrors(this ProblemDetails details, string error)
        {
            return details.GetErrors()[error];
        }
    }
}
