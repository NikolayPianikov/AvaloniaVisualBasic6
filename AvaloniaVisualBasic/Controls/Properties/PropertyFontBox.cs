using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Interactivity;
using AvaloniaVisualBasic.IDE;
using AvaloniaVisualBasic.Runtime.BuiltinTypes;
using AvaloniaVisualBasic.Utils;
using Classic.CommonControls.Dialogs;

namespace AvaloniaVisualBasic.Controls;

public class PropertyFontBox : TemplatedControl
{
    public static readonly StyledProperty<VBFont> FontProperty = AvaloniaProperty.Register<PropertyFontBox, VBFont>("Font", defaultBindingMode: BindingMode.TwoWay, defaultValue: VBFont.Default);
    public static readonly DirectProperty<PropertyFontBox, string?> FontNameProperty = AvaloniaProperty.RegisterDirect<PropertyFontBox, string?>("FontName", o => o.FontName);
    public static readonly StyledProperty<WindowManager> WindowManagerProperty = AvaloniaProperty.Register<PropertyFontBox, WindowManager>("WindowManager");

    public string? FontName
    {
        get => Font.FontFamily.Name;
    }

    public VBFont Font
    {
        get => GetValue(FontProperty);
        set => SetValue(FontProperty, value);
    }

    public WindowManager WindowManager
    {
        get => GetValue(WindowManagerProperty);
        set => SetValue(WindowManagerProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        var openFontWindowButton = e.NameScope.Get<Button>("PART_OpenFontWindowButton");
        openFontWindowButton.Click += OnButtonClick;
    }

    private void OnButtonClick(object? sender, RoutedEventArgs e)
    {
        async Task OpenFontWindow()
        {
            var result = await WindowManager.ShowFontDialog(new FontDialogResult(Font.FontFamily, Font.Style, Font.Weight, Font.Size));
            if (result != null)
            {
                SetCurrentValue(FontProperty, new VBFont(result.Family, (int)result.Size, result.Weight, result.Style));
            }
        }

        OpenFontWindow().ListenErrors();
    }

    static PropertyFontBox()
    {
        FontProperty.Changed.AddClassHandler<PropertyFontBox>((box, e) =>
        {
            box.RaisePropertyChanged(FontNameProperty, null, box.FontName);
        });
    }
}