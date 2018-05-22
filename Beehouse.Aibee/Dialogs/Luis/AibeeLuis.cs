using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Beehouse.Aibee.Dialogs.Luis
{
    [Serializable]
    [LuisModel("69e97c00-b277-4da7-90a4-9a97fa25a2f7", "322c47f38ca4456cb2f7e172da680e1e")]
    public partial class AibeeLuis:LuisDialog<object>
    {
        

        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Desculpe, eu não entendi.");
        }
        
    }
}