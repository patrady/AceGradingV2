namespace AceGrading
{
    public sealed class InverseBoolean : BooleanConverter<bool>
    {
        public InverseBoolean() : base(false, true) { }
    }
}
