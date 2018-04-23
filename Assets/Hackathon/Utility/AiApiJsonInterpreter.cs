using System;
using System.Collections.Generic;
using SimpleJSON;

namespace Hackathon.Utility
{
    public static class AiApiJsonInterpreter
    {

        
      public struct Fufillment
      {
        public string Speech;
        public Message Message;
        public string ResolvedQuery;


        public Fufillment(string speech, Message messages, string query)
        {
          Speech = speech;
          Message = messages;
          ResolvedQuery = query;
        }

        public override string ToString()
        {
          return string.Format("Speech: {0}\n{1}", Speech, Message);
        }
      }
  
      public struct Message
      {
        public string MessageType;
        public string Speech;

        public Message(string messageType, string speech)
        {
          MessageType = messageType;
          Speech = speech;
        }

        public override string ToString()
        {
          return string.Format("Type: {0}\nSpeech: {1}", MessageType, Speech);
        }
      }

      public const string RESULT = "result";

      public const string FUFILLMENT = "fulfillment";

      public const string SPEECH = "speech";

      public const string MESSAGES = "messages";

      public static Fufillment InterpretResponse(string json)
      {
        JSONNode node = JSONNode.Parse(json);
        var result = node[RESULT];
        var fufillmentNode = result[FUFILLMENT];

        JSONArray messages = fufillmentNode[MESSAGES] as JSONArray;
        List<Message> list = new List<Message>();
        var speechMessage = messages[0];
        Message message = new Message(speechMessage["type"].ToString(), 
          speechMessage["textToSpeech"]);
        string query = result["resolvedQuery"];
        var fufillment = new Fufillment(fufillmentNode["speech"].ToString(), message, query);

        return fufillment;
      }
    }
}

        /*{
  "id": "8d68ffe1-ffaa-46fa-a8b9-ffdf235868eb",
  "timestamp": "2018-04-21T14:18:27.681Z",
  "lang": "en",
  "result": {
    "source": "agent",
    "resolvedQuery": "yes",
    "action": "arithmetic-equation-basic-yes.DefaultWelcomeIntent-yes-no-yes-yesequalitytest-yes-equation-recap-yes",
    "actionIncomplete": false,
    "parameters": {},
    "contexts": [
      {
        "name": "defaultwelcomeintent-yes-no-yes-yesequalitytest-yes-equation-recap-yes-followup",
        "parameters": {},
        "lifespan": 2
      },
      {
        "name": "defaultwelcomeintent-yes-no-yes-yesequalitytest-yes-equation-recap-followup",
        "parameters": {},
        "lifespan": 1
      }
    ],
    "metadata": {
      "intentId": "4a3890a8-1c8b-4414-8cbb-0a0b174594a3",
      "webhookUsed": "false",
      "webhookForSlotFillingUsed": "false",
      "intentName": "Default Welcome Intent - yes - no - yes - yes(equality test) - yes - equation-recap - yes"
    },
    "fulfillment": {
      "speech": "To remind ourselves. 3 multiplied by 4 and 6 added to 6 are both equal. Let's try something cool with this new found knowledge. Let us write it out. 3 * 4 = 6 + 6. Now let the magic begin. We will substitute 4 with the variable 'k'. Are you still with me so far?",
      "messages": [
        {
          "type": "simple_response",
          "platform": "google",
          "textToSpeech": "To remind ourselves.  \n\n3 multiplied by 4 and 6 added to 6 are both equal. Let's try something cool with this new found knowledge.\n\n Let us write it out.  3 * 4 = 6 + 6. \n\nNow let the magic begin. \n\nWe will substitute 4 with the variable  'k'. \n\nAre you still with me so far?"
        },
        {
          "type": 0,
          "speech": "To remind ourselves.  \n\n3 multiplied by 4 and 6 added to 6 are both equal. Let's try something cool with this new found knowledge.\n\n Let us write it out.  3 * 4 = 6 + 6. \n\nNow let the magic begin. \n\nWe will substitute 4 with the variable  'k'. \n\nAre you still with me so far?"
        }
      ]
    },
    "score": 0.75
  },
  "status": {
    "code": 200,
    "errorType": "success",
    "webhookTimedOut": false
  },
  "sessionId": "099f8ba8-be80-435e-91bc-b90f482f9a69"
}*/