using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace MadWorld.Frontend.UI.Admin.Pages.Accounts;

[UsedImplicitly]
public partial class AccountDetails
{
    [Parameter]
    public string Id { get; set; } = string.Empty;
}