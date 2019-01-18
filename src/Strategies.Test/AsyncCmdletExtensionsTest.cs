﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Rest;
using Xunit;
using System.Threading;

namespace Microsoft.Azure.Commands.Common.Strategies.UnitTest
{
    public class AsyncCmdletExtensionsTest
    {
        class Client : IClient
        {
            public T GetClient<T>() where T : ServiceClient<T>
                => null;
        }

        class RG { }

        class Parameters : IParameters<RG>
        {
            public string DefaultLocation => "eastus";

            public string Location
            {
                get
                {
                    return "somelocation";
                }

                set
                {
                }
            }

            //https://stackoverflow.com/a/29923484/294804
            public Task<ResourceConfig<RG>> CreateConfigAsync()
                => Task.FromResult(new ResourceConfig<RG>(
                    new ResourceStrategy<RG>(
                        null,
                        (c, p) => Task.FromResult(new RG()),
                        null,
                        null,
                        null,
                        false),
                    null,
                    null,
                    null,
                    null));
        }

        class AsyncCmdlet : IAsyncCmdlet
        {
            public CancellationToken CancellationToken { get; }
                = new CancellationToken();

            public IEnumerable<KeyValuePair<string, object>> Parameters
            {
                get
                {
                    yield return new KeyValuePair<string, object>("Str", "str");
                }
            }

            public string VerbsNew
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public void ReportTaskProgress(ITaskProgress taskProgress)
            {
                throw new NotImplementedException();
            }

            public Task<bool> ShouldProcessAsync(string target, string action)
            {
                throw new NotImplementedException();
            }

            public void WriteObject(object value)
            {
                throw new NotImplementedException();
            }

            public void WriteVerbose(string message)
            {
                Assert.Equal("Str = \"str\"", message);
            }

            public void WriteWarning(string message)
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public async Task TestVerboseStream()
        {
            var client = new Client();
            var parameters = new Parameters();
            var asyncCmdlet = new AsyncCmdlet();

            await client.RunAsync("subscriptionId", parameters, asyncCmdlet);
        }
    }
}