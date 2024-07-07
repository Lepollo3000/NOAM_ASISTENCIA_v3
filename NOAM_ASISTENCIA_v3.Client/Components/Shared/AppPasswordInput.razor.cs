using Microsoft.AspNetCore.Components;
using MudBlazor;
using NOAM_ASISTENCIA_v3.Shared.Features.Accounts;
using System.Linq.Expressions;

namespace NOAM_ASISTENCIA_v3.Client.Components.Shared;

public partial class AppPasswordInput
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

    private InputType inputType = InputType.Password;
    private string passwordIcon = "fa fa-eye-slash";
    private bool showPasswordIcon;

    private void FieldIconPressed()
    {
        if (showPasswordIcon)
        {
            showPasswordIcon = false;
            passwordIcon = "fa fa-eye-slash";
            inputType = InputType.Password;
        }
        else
        {
            showPasswordIcon = true;
            passwordIcon = "fa fa-eye";
            inputType = InputType.Text;
        }
    }

    private async Task UpdateValue()
    {
        await ValueChanged.InvokeAsync(Value);
    }
}
