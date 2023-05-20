namespace MadWorld.Unittests;

public class AutoDomainInlineDataAttribute : InlineAutoDataAttribute
{
    public AutoDomainInlineDataAttribute(params object[] objects) : base(new AutoDomainDataAttribute(), objects) { }
}