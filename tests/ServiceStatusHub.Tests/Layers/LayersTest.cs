using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStatusHub.Tests.Layers;

public class LayersTest : BaseTest
{
    public LayersTest()
    {

    }

    [Fact]
    public void Domain_Should_NotHaveDependencyOnApplication()
    {
        var references = DomainAssembly.GetReferencedAssemblies();

        var applicationAssemblyName = ApplicationAssembly.GetName().Name;
        var hasDependency = references.Any(r => r.Name == applicationAssemblyName);

        Assert.False(hasDependency, $"Domain assembly should not depend on Application assembly. Found dependency on {applicationAssemblyName}.");
    }

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        var references = DomainAssembly.GetReferencedAssemblies();
        var infrastructureAssemblyName = InfrastructureAssembly.GetName().Name;
        var hasDependency = references.Any(r => r.Name == infrastructureAssemblyName);

        Assert.False(hasDependency, $"Domain assembly should not depend on Infrastructure assembly. Found dependency on {infrastructureAssemblyName}.");
    }

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    {
        var references = DomainAssembly.GetReferencedAssemblies();
        var presentationAssemblyName = PresentationAssembly.GetName().Name;
        var hasDependency = references.Any(r => r.Name == presentationAssemblyName);

        Assert.False(hasDependency, $"Domain assembly should not depend on Presentation assembly. Found dependency on {presentationAssemblyName}.");
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        var references = ApplicationAssembly.GetReferencedAssemblies();
        var infrastructureAssemblyName = InfrastructureAssembly.GetName().Name;
        var hasDependency = references.Any(r => r.Name == infrastructureAssemblyName);

        Assert.False(hasDependency, $"Application assembly should not depend on Infrastructure assembly. Found dependency on {infrastructureAssemblyName}.");
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    {
        var references = ApplicationAssembly.GetReferencedAssemblies();
        var presentationAssemblyName = PresentationAssembly.GetName().Name;
        var hasDependency = references.Any(r => r.Name == presentationAssemblyName);

        Assert.False(hasDependency, $"Application assembly should not depend on Presentation assembly. Found dependency on {presentationAssemblyName}.");
    }

    [Fact]
    public void InfrastructureLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    {
        var references = InfrastructureAssembly.GetReferencedAssemblies();
        var presentationAssemblyName = PresentationAssembly.GetName().Name;
        var hasDependency = references.Any(r => r.Name == presentationAssemblyName);

        Assert.False(hasDependency, $"Infrastructure assembly should not depend on Presentation assembly. Found dependency on {presentationAssemblyName}.");
    }
}
