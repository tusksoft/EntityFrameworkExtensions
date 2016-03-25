# EntityFrameworkExtensions
Utility classes to change/enhance Entity Framework behavior

##UnderscoreSuffixedNavigationPropertyNameForeignKeyDiscoveryConvention
###What?
A class that modifies how EF looks for foreign key property names that relate to navigation properties.
###Why?
Because EF uses two different naming conventions for foreign key properties derived from navigation properties- one when you explicitly define your FK property in code (e.g., "MyNavigationPropertyId") and another one when it's implied (e.g., "MyNavigationProperty_Id"). This means that if you explictly define a FK property for your navigation property on your model, you will ultimately end up with inconsistent FK column names in the generated database schema.
This class will dump the former convention in favor of the latter That is, EF will read an explicitly defined property name of "MyNavigationPropertyId" as just another property on your model, while it will recognize "MyNavigationProperty_Id" as a FK property associated with your navigation property.
###How?
In your derived DbContext class, add the following code:

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.AddBefore<NavigationPropertyNameForeignKeyDiscoveryConvention>(
                new UnderscoreSuffixedNavigationPropertyNameForeignKeyDiscoveryConvention());
            modelBuilder.Conventions.Remove<NavigationPropertyNameForeignKeyDiscoveryConvention>();
            //whatever else you need to do here
        }
        
