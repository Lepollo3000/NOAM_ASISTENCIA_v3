using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Linq.Expressions;

namespace NOAM_ASISTENCIA_v3.Client.Components.Shared;

public partial class AppTextInput
{
    [Parameter]
    public string Value { get; set; } = null!;
    [Parameter]
    public string Label { get; set; } = null!;
    [Parameter]
    public string Description { get; set; } = null!;
    [Parameter]
    public Expression<Func<string>> For { get; set; } = null!;
    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    private readonly InputType inputType = InputType.Text;

    private async Task UpdateValue()
    {
        await ValueChanged.InvokeAsync(Value);
    }
}
