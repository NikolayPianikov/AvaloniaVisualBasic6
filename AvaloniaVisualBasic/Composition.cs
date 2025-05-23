using Pure.DI;
using System.Diagnostics;
using AvaloniaVisualBasic.IDE;
using AvaloniaVisualBasic.Projects;
using AvaloniaVisualBasic.Tools;
using AvaloniaVisualBasic.VisualDesigner;
using static Pure.DI.Lifetime;
using static Pure.DI.RootKinds;
using MdiWindowManager = AvaloniaVisualBasic.IDE.MdiWindowManager;

namespace AvaloniaVisualBasic;

public partial class Composition
{
    [Conditional("DI")]
    static void Setup() =>
        DI.Setup()
            .DefaultLifetime(Singleton)
            .Bind().To<ToolBoxToolViewModel>()
            .Bind().To<PropertiesToolViewModel>()
            .Bind().To<ProjectToolViewModel>()
            .Bind().To<FormLayoutToolViewModel>()
            .Bind().To<ImmediateToolViewModel>()
            .Bind().To<LocalsToolViewModel>()
            .Bind().To<WatchesToolViewModel>()
            .Bind().To<ColorPaletteToolViewModel>()
            .Bind().To<MdiWindowManager>()
            .Bind().To<WindowManager>()
            .Bind().To<ProjectManager>()
            .Bind().To<EditorService>()
            .Bind().To<MainViewViewModel.DockFactory>()
            .Bind().To<EventBus>()
            .Bind().To<ProjectRunnerService>()
            .Bind().To<ProjectService>()
            .Bind().To<FocusedProjectUtil>()
            .Root<MainViewViewModel>(nameof(Root), kind: Virtual);
}