namespace MadWorld.Unittests;

public sealed class AutoDomainInlineDataAttribute : InlineAutoDataAttribute
{
    public AutoDomainInlineDataAttribute(params object[] objects) : base(new AutoDomainDataAttribute(), objects) { }
}