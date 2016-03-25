// Copyright (c) Tusk Software, LLC. All rights reserved. See License.txt in the project root for license information.
using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Your.Namespace.Here
{
    /// <summary>
    ///Convention to discover foreign key properties whose names are a combination of the dependent navigation property name followed by an underscore and the principal type primary key property name(s). This follows the naming convention used by Entity Framework when generating a foreign key column for a navigation property.
    /// </summary>
    public class UnderscoreSuffixedNavigationPropertyNameForeignKeyDiscoveryConvention :
        NavigationPropertyNameForeignKeyDiscoveryConvention
    {
        protected override bool MatchDependentKeyProperty(AssociationType associationType,
            AssociationEndMember dependentAssociationEnd,
            EdmProperty dependentProperty,
            EntityType principalEntityType,
            EdmProperty principalKeyProperty)
        {
            if (principalKeyProperty == null)
            {
                throw new ArgumentNullException(nameof(principalKeyProperty));
            }
            var originalPrincipalKeyPropertyName = principalKeyProperty.Name;
            principalKeyProperty.Name = "_" + originalPrincipalKeyPropertyName;
            var result = base.MatchDependentKeyProperty(associationType, dependentAssociationEnd, dependentProperty,
                principalEntityType, principalKeyProperty);
            principalKeyProperty.Name = originalPrincipalKeyPropertyName;
            return result;
        }
    }
}
