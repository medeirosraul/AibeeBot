using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;

namespace Beehouse.Aibee.Dialogs.Luis
{
    public partial class AibeeLuis
    {
        [LuisIntent("About_Me")]
        public async Task About_Me(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Desculpe, eu não entendi.");
        }
    }
}