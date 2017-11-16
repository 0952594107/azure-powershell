﻿//
// Copyright (c) Microsoft.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Microsoft.Azure.Commands.Network.Models
{
    public class PSSecureGatewayNetworkRule
    {
        public string Name { get; set; }

        public uint Priority { get; set; }

        public string Description { get; set; }

        public string Direction { get; set; }

        public List<string> Protocols { get; set; }

        public List<string> SourceIps { get; set; }

        public List<string> DestinationIps { get; set; }

        public List<string> SourcePorts { get; set; }

        public List<string> DestinationPorts { get; set; }

        public List<PSSecureGatewayAction> Actions { get; set; }

        [JsonIgnore]
        public string ProtocolsText
        {
            get { return JsonConvert.SerializeObject(Protocols, Formatting.Indented); }
        }

        [JsonIgnore]
        public string SourceIpsText
        {
            get { return JsonConvert.SerializeObject(SourceIps, Formatting.Indented); }
        }

        [JsonIgnore]
        public string DestinationIpsText
        {
            get { return JsonConvert.SerializeObject(DestinationIps, Formatting.Indented); }
        }


        [JsonIgnore]
        public string SourcePortsText
        {
            get { return JsonConvert.SerializeObject(SourcePorts, Formatting.Indented); }
        }


        [JsonIgnore]
        public string DestinationPortsText
        {
            get { return JsonConvert.SerializeObject(DestinationPorts, Formatting.Indented); }
        }


        [JsonIgnore]
        public string ActionsText
        {
            get { return JsonConvert.SerializeObject(Actions, Formatting.Indented); }
        }
    }
}
