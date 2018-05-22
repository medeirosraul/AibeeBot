using System.Collections.Generic;
using Microsoft.Bot.Builder.Luis.Models;

namespace Beehouse.Aibee.Context
{
    public class Conversation
    {
        public string Id { get; set; }
        public List<IntentRecommendation> Contexts { get; set; }


        public void ResetContexts()
        {
            Contexts = new List<IntentRecommendation>();
        }
    }
}