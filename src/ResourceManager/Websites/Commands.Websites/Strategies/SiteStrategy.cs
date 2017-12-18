﻿//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.Azure.Management.WebSites;
using Microsoft.Azure.Management.WebSites.Models;
using Microsoft.Azure.Management.Internal.Resources.Models;

namespace Microsoft.Azure.Commands.Common.Strategies.WebApps
{
    static class SiteStrategy
    {
        public static ResourceStrategy<Site> Strategy { get; } = AppServicePolicy.Create(
            type: "Site",
            provider: "AppService",
            getOperations: client => client.Sites,
            getAsync: (o, p) => o.GetSiteAsync(p.ResourceGroupName, p.Name, null, p.CancellationToken),
            createOrUpdateAsync: (o, p) => o.CreateOrUpdateSiteAsync(p.ResourceGroupName, p.Name, p.Model, cancellationToken: p.CancellationToken),
            createTime: c => c.LastModifiedTimeUtc != null ? 240 : 120);

        public static ResourceConfig<Site> CreateSiteConfig(this ResourceConfig<ResourceGroup> resourceGroup, ResourceConfig<ServerFarmWithRichSku> plan, string siteName) => Strategy.CreateConfig(plan.ResourceGroupName, siteName,
            createModel: subscription =>
            new Site {SiteName = siteName, Name = siteName, ServerFarmId = plan.GetId(subscription).IdToString()},
            dependencies: new List<IEntityConfig> { plan, resourceGroup });
    }
}
