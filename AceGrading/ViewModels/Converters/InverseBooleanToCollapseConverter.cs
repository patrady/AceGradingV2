using System.Windows;

namespace AceGrading
{
    public sealed class InverseBooleanToCollapseConverter : BooleanConverter<Visibility>
    {
        public InverseBooleanToCollapseConverter() :
            base(Visibility.Collapsed, Visibility.Visible)
        { }
    }
}
