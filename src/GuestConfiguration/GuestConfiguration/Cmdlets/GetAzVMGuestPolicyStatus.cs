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

namespace Microsoft.Azure.Commands.GuestConfiguration.Cmdlets
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Management.Automation;
    using Microsoft.Azure.Commands.GuestConfiguration.Common;
    using Microsoft.Azure.Commands.GuestConfiguration.Models;
    using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
    using Microsoft.Azure.Management.GuestConfiguration.Models;

    /// <summary>
    /// Gets Vm Guest Policy reports (GuestConfiguration policy reports)
    /// </summary>
    [Cmdlet("Get", ResourceManager.Common.AzureRMConstants.AzurePrefix + "VMGuestPolicyStatus", DefaultParameterSetName = ParameterSetNames.VmNameScope)]
    [OutputType(typeof(GuestConfigurationAssignmentReport))]
    [OutputType(typeof(GuestConfigurationAssignmentReport[]))]
    public class GetAzVMGuestPolicyStatus : GuestConfigurationCmdletBase
    {
        [Parameter(ParameterSetName = ParameterSetNames.VmNameScope, Mandatory = true, Position = 0, HelpMessage = ParameterHelpMessages.ResourceGroupName)]
        [Parameter(ParameterSetName = ParameterSetNames.InitiativeIdScope, Mandatory = true, Position = 0, HelpMessage = ParameterHelpMessages.ResourceGroupName)]
        [Parameter(ParameterSetName = ParameterSetNames.InitiativeNameScope, Mandatory = true, Position = 0, HelpMessage = ParameterHelpMessages.ResourceGroupName)]
        [ResourceGroupCompleter]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.VmNameScope, Mandatory = true, Position = 1, HelpMessage = ParameterHelpMessages.VMName)]
        [Parameter(ParameterSetName = ParameterSetNames.InitiativeIdScope, Mandatory = true, Position = 1, HelpMessage = ParameterHelpMessages.VMName)]
        [Parameter(ParameterSetName = ParameterSetNames.InitiativeNameScope, Mandatory = true, Position = 1, HelpMessage = ParameterHelpMessages.VMName)]
        [ValidateNotNullOrEmpty]
        public string VMName { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.InitiativeNameScope, Mandatory = true, Position = 2, HelpMessage = ParameterHelpMessages.InitiativeName)]
        [ValidateNotNullOrEmpty]
        public string InitiativeName { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.InitiativeIdScope, Mandatory = true, Position = 2, HelpMessage = ParameterHelpMessages.InitiativeId)]
        [ValidateNotNullOrEmpty]
        public string InitiativeId { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.ReportIdScope, Mandatory = true, Position = 0, HelpMessage = ParameterHelpMessages.ReportId)]
        [ValidateNotNullOrEmpty]
        public string ReportId { get; set; }

        /// <summary>
        /// Executes the cmdlet
        /// </summary>
        public override void ExecuteCmdlet()
        {
            IEnumerable<GuestConfigurationPolicyAssignmentReport> gcPolicyAssignmentReports = null;

            switch (ParameterSetName)
            {
                case ParameterSetNames.InitiativeNameScope:
                    // get all gcrp assignments first
                    var gcrpAssignments = GetAllGCRPAssignments(ResourceGroupName, VMName);

                    // Process results for cmdlet
                    gcPolicyAssignmentReports = GetAllGuestConfigurationAssignmentReportsByInitiativeName(ResourceGroupName, VMName, InitiativeName, false, gcrpAssignments);
                    if(gcPolicyAssignmentReports == null || gcPolicyAssignmentReports.Count() > 0)
                    {
                        WriteObject(gcPolicyAssignmentReports, true);
                    }                        
                    break;

                case ParameterSetNames.InitiativeIdScope:
                    // get all gcrp assignments first
                    gcrpAssignments = GetAllGCRPAssignments(ResourceGroupName, VMName);

                    // Process results for cmdlet
                    gcPolicyAssignmentReports = GetAllGuestConfigurationAssignmentReportsByInitiativeId(ResourceGroupName, VMName, InitiativeId, false, gcrpAssignments);

                    if (gcPolicyAssignmentReports == null || gcPolicyAssignmentReports.Count() > 0)
                    {
                        WriteObject(gcPolicyAssignmentReports, true);
                    }                   
                    break;

                case ParameterSetNames.ReportIdScope:
                    var policyReport = GetGuestConfigurationAssignmentReportById(ReportId);
                    if (policyReport != null)
                    {
                        WriteObject(policyReport);
                    }
                    break;

                case ParameterSetNames.VmNameScope:
                    gcPolicyAssignmentReports = GetAllGuestConfigurationAssignmentReports(ResourceGroupName, VMName, false);
                    if (gcPolicyAssignmentReports == null || gcPolicyAssignmentReports.Count() > 0)
                    {
                        WriteObject(gcPolicyAssignmentReports, true);
                    }
                    break;
            }    
        }
    }
}
