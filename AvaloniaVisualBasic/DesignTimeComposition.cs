using System;

namespace AvaloniaVisualBasic;

public class DesignTimeComposition: Composition
{
    private readonly Lazy<MainViewViewModel> designTimeRoot = new();

    public override MainViewViewModel Root => designTimeRoot.Value;
}