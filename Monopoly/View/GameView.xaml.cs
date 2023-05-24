using System.Windows.Controls;

namespace Monopoly.View;

/// <summary>
///     Interaction logic for testwindow.xaml
/// </summary>
public partial class GameView : UserControl
{
    // todo: одинаковые элементы типа Ellipse можно было бы выделить в отдельный UserControl
    // а тут использовать какие-нибудь контролы коллекций
    public GameView()
    {
        InitializeComponent();
    }
}