using System.Net.NetworkInformation;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazingTrails.Client.Features.ManageTrails.Shared;

public class FormStateTracker : ComponentBase
{
    [Inject] public AppState.AppState AppState { get; set; } = default!;
    [CascadingParameter] private EditContext CascadedEditContext { get; set; } = default!;

    protected override void OnInitialized()
    {
        if (CascadedEditContext is null)
        {
            throw new InvalidOperationException(
                $"{nameof(FormStateTracker)} requires a cascading parameter of type {nameof(EditContext)}");
        }

        CascadedEditContext.OnFieldChanged += CascadedEditContext_OnFieldChanged;
    }

    private void CascadedEditContext_OnFieldChanged(object? sender, FieldChangedEventArgs e)
    {
        if (sender is null)
        {
            return;
        }
        var trail = (TrailDto)e.FieldIdentifier.Model;
        if (trail.Id == 0)
        {
            AppState.NewTrailState.Trail = trail;
        }
    }
}