using System.Windows;

namespace AceGrading
{
    public sealed class InverseBooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        public InverseBooleanToVisibilityConverter() : base(Visibility.Hidden, Visibility.Visible)
        { }
    }
}
