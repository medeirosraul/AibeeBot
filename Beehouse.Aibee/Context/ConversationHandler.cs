using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Resources;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;

namespace Beehouse.Aibee.Context
{
    public static class ConversationHandler
    {
        public static List<Conversation> Conversations { get; set; }
        public static XmlDocument FullBook { get; set; }
        public static ContextBook ContextBook { get; set; }

        static ConversationHandler()
        {
            // Init conversations
            if(Conversations is null) Conversations = new List<Conversation>();

            Debug.WriteLine(Properties.Resources.aibee_pt_br.ToString());

            // Copy contexts
            //var stringReader = new StringReader(Properties.Resources.aibee_pt_br);
            ContextBook =
                (ContextBook) (new XmlSerializer(typeof(ContextBook)).Deserialize(new StringReader(Properties.Resources.aibee_pt_br)));
        }

        public static List<IntentRecommendation> GetContexts(string conversationId)
        {
            // Get conversation
            var conversation = Conversations.FirstOrDefault(p => p.Id == conversationId);

            // If null
            if (conversation is null)
            {
                conversation = new Conversation
                {
                    Id = conversationId,
                    Contexts = new List<IntentRecommendation>()
                };
                Conversations.Add(conversation);
                return conversation.Contexts;
            }

            var contexts = conversation.Contexts;

            Debug.WriteLine("Active contexts -> ");
            contexts.ForEach(p => Debug.Write(p.Intent + " "));

            return contexts;
        }

        public static void AddContext(string conversationId, IntentRecommendation intent)
        {
            Debug.WriteLine("Adding intent context");
            var sameContext = GetContexts(conversationId).FirstOrDefault(p => p.Intent == intent.Intent);
            if (sameContext != null) return;
            Conversations.FirstOrDefault(p => p.Id == conversationId)?.Contexts.Add(intent);
        }

        public static void ResetContexts(string conversationId)
        {
            Debug.WriteLine("Context requires reset");
            Conversations.FirstOrDefault(p => p.Id == conversationId)?.ResetContexts();
        }

        public static string Think(IDialogContext context, LuisResult result)
        {
            Debug.WriteLine("Thinking about intent: " + result.TopScoringIntent.Intent);

            // Verify if needs reset
            var intent = GetContextInfo(result.TopScoringIntent.Intent);
            if (intent.RequireReset) ResetContexts(context.Activity.Conversation.Id);

            // Add context in memory
            AddContext(context.Activity.Conversation.Id, result.TopScoringIntent);

            // Get memorized contexts
            var contexts = ConversationHandler.GetContexts(context.Activity.Conversation.Id);

            // Retrieve informations from ContextBook about all contexts
            var thinkContext = GetAllContextInfo(contexts);

            List<EntityRecommendation> entities = result.Entities.ToList();

            var message = GetMessage(thinkContext, entities);

            if (message != null) return message;

            return "Desculpe, não consegui entender você.";
        }

        public static Context GetContextInfo(string name)
        {
            var context = ContextBook.Contexts.FirstOrDefault(p => p.Name.Contains("[" + name + "]"));
            return context;
        }

        public static Context GetAllContextInfo(List<IntentRecommendation> intentions)
        {
            var contextQuery = ContextBook.Contexts.Where(p => p.Name.Contains("[" + intentions.First().Intent + "]"));
            if (intentions.Count > 1)
            {
                for (var i = 1; i <= intentions.Count; i++)
                {
                    contextQuery = contextQuery.Where(p => p.Name.Contains("[" + intentions.ElementAt(i).Intent + "]"));
                }
            }
            
            return contextQuery.FirstOrDefault();
        }

        public static string GetMessage(Context context, List<EntityRecommendation> entities)
        {
            if (entities is null || !entities.Any())
            {
                return context.Messages.FirstOrDefault()?.Message;
            }

            var query = context.Messages.Where(p => p.Entities.Contains("[" + entities.First().Type + "]"));
            if (entities.Count > 1)
            {
                for (var i = 1; i <= entities.Count - 1; i++)
                {
                    var index = i;
                    query = query.Where(p => p.Entities.Contains("[" + entities[index].Type + "]"));
                }
            }

            return query.FirstOrDefault()?.Message;
        }
        #region Helpers

        #endregion
    }
}