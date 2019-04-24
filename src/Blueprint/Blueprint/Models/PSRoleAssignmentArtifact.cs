﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Azure.Management.Blueprint.Models;

namespace Microsoft.Azure.Commands.Blueprint.Models
{
    public class PSRoleAssignmentArtifact : PSArtifact
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<string> DependsOn { get; set; }
        public string RoleDefinitionId { get; set; }
        public object PrincipleIds { get; set; }
        public string ResourceGroup { get; set; }

        internal static PSRoleAssignmentArtifact FromArtifactModel(RoleAssignmentArtifact artifact, string scope)
        {
            var psArtifact = new PSRoleAssignmentArtifact
            {
                Id = artifact.Id,
                Type = artifact.Type,
                Name = artifact.Name,
                DisplayName = artifact.DisplayName,
                Description = artifact.Description,
                RoleDefinitionId = artifact.RoleDefinitionId,
                DependsOn = new List<string>(),
                PrincipleIds = artifact.PrincipalIds, //What does backend expect this to be? A list of principles?
                ResourceGroup = artifact.ResourceGroup
            };

            psArtifact.DependsOn = artifact.DependsOn.Select(x => x) as List<string>;

            //To-Do: Spell out principleIds to the user (can be expression or list of principleids)

            return psArtifact;
        }
    }
}
