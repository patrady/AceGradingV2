using System.Windows;

namespace AceGrading
{
    public sealed class BooleanToCollapseConverter : BooleanConverter<Visibility>
    {
        public BooleanToCollapseConverter() :
            base(Visibility.Visible, Visibility.Collapsed)
        { }
    }
}
