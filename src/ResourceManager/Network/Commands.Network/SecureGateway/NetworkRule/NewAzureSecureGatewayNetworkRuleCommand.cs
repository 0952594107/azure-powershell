﻿// ----------------------------------------------------------------------------------
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

using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Commands.Network.Models;
using MNM = Microsoft.Azure.Management.Network.Models;

namespace Microsoft.Azure.Commands.Network
{
    [Cmdlet(VerbsCommon.New, "AzureRmSecureGatewayNetworkRule", SupportsShouldProcess = true), OutputType(typeof(PSSecureGatewayNetworkRule))]
    public class NewAzureSecureGatewayNetworkRuleCommand : NetworkBaseCmdlet
    {
        [Parameter(
            Mandatory = true,
            HelpMessage = "The name of the Network Rule")]
        [ValidateNotNullOrEmpty]
        public virtual string Name { get; set; }

        [Parameter(
            Mandatory = true,
            HelpMessage = "The priority of the rule")]
        [ValidateNotNullOrEmpty]
        public uint Priority { get; set; }

        [Parameter(
            Mandatory = false,
            HelpMessage = "The description of the rule")]
        [ValidateNotNullOrEmpty]
        public string Description { get; set; }

        [Parameter(
            Mandatory = true,
            HelpMessage = "The protocols of the rule")]
        [ValidateNotNullOrEmpty]
        public List<string> Protocols { get; set; }

        [Parameter(
            Mandatory = true,
            HelpMessage = "The source IPs of the rule")]
        [ValidateNotNullOrEmpty]
        public List<string> SourceIps { get; set; }

        [Parameter(
            Mandatory = true,
            HelpMessage = "The destination IPs of the rule")]
        [ValidateNotNullOrEmpty]
        public List<string> DestinationIps { get; set; }

        [Parameter(
            Mandatory = true,
            HelpMessage = "The source ports of the rule")]
        [ValidateNotNullOrEmpty]
        public List<string> SourcePorts { get; set; }

        [Parameter(
            Mandatory = true,
            HelpMessage = "The destination ports of the rule")]
        [ValidateNotNullOrEmpty]
        public List<string> DestinationPorts { get; set; }

        [Parameter(
            Mandatory = true,
            HelpMessage = "The actions of the rule")]
        [ValidateNotNullOrEmpty]
        public List<PSSecureGatewayNetworkRuleAction> Actions { get; set; }

        public override void Execute()
        {
            base.Execute();

            if (this.Protocols == null || this.Protocols.Count == 0)
            {
                throw new ArgumentException("At least one network rule protocol should be specified!");
            }
            if (this.SourcePorts == null || this.SourcePorts.Count == 0)
            {
                throw new ArgumentException("At least one network rule source IP should be specified!");
            }
            if (this.DestinationPorts == null || this.DestinationPorts.Count == 0)
            {
                throw new ArgumentException("At least one network rule destination IP should be specified!");
            }
            if (this.SourcePorts == null || this.SourcePorts.Count == 0)
            {
                throw new ArgumentException("At least one network rule source port should be specified!");
            }
            if (this.DestinationPorts == null || this.DestinationPorts.Count == 0)
            {
                throw new ArgumentException("At least one network rule destination port should be specified!");
            }
            if (this.Actions == null || this.Actions.Count == 0)
            {
                throw new ArgumentException("At least one network rule action should be specified!");
            }

            var networkRule = new PSSecureGatewayNetworkRule
            {
                Name = this.Name,
                Priority = this.Priority,
                Description = this.Description,
                Direction = MNM.SecureGatewayRuleDirection.Outbound,
                Protocols = this.Protocols,
                SourceIps = this.SourceIps,
                DestinationIps = this.DestinationIps,
                SourcePorts = this.SourcePorts,
                DestinationPorts = this.DestinationPorts,
                Actions = this.Actions
            };
            WriteObject(networkRule);
        }
    }
}
