// ----------------------------------------------------------------------------------
//
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

using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Azure.Commands.ManagedNetwork.Models
{
    /// <summary>
    /// ARM tracked resource
    /// </summary>
    public class PSManagedNetworkGroup: PSProxyResource
    {
        /// <summary>
        /// Gets or sets a list of Management Group Ids
        /// </summary>
        public List<PSResourceId> ManagementGroups { get; set; }

        /// <summary>
        /// Gets or sets a list of Subscription Ids
        /// </summary>
        public List<PSResourceId> Subscriptions { get; set; }

        /// <summary>
        /// Gets or sets a list of Virtual Network Ids
        /// </summary>
        public List<PSResourceId> VirtualNetworks { get; set; }

        /// <summary>
        /// Gets or sets a list of Subnet Ids
        /// </summary>
        public List<PSResourceId> Subnets { get; set; }
    }
}