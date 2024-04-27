using MudBlazor;

namespace NOAM_ASISTENCIA_v3.Client.Helpers.MudBlazor;

public class PasswordInputConfigurations
{
    private bool showIcon = false;
    private static readonly string openedEyeIcon = "fa fa-eye";
    private static readonly string closedEyeIcon = "fa fa-eye-slash";

    public bool Inmediate { get; } = TextInputDefaults.Inmediate;
    public Color Color { get; } = TextInputDefaults.Color;
    public Variant Variant { get; } = TextInputDefaults.Variant;
    public Adornment Adornment { get; } = Adornment.End;
    public InputType InputType { get; set; } = InputType.Password;
    public string Icon { get; private set; } = openedEyeIcon;

    public void FieldIconPressed()
    {
        if (showIcon)
        {
            showIcon = false;
            Icon = closedEyeIcon;
            InputType = InputType.Password;
        }
        else
        {
            showIcon = true;
            Icon = openedEyeIcon;
            InputType = InputType.Text;
        }
    }
}
