using System.Diagnostics;
using System.Threading.Tasks;
using Beehouse.Aibee.Context;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;

namespace Beehouse.Aibee.Dialogs.Luis
{
    public partial class AibeeLuis
    {
        [LuisIntent("Greeting")]
        [LuisIntent("About")]
        [LuisIntent("How")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(ConversationHandler.Think(context, result));
        }

    }
}