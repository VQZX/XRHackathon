using ApiAiSDK.Model;
using Hackathon.Utility;
using UnityEngine;
using HackFufillment = Hackathon.Utility.AiApiJsonInterpreter.Fufillment;

namespace Hackathon
{
    public class TeacherController : MonoBehaviour
    {
        private TeacherAnimator animator;

        [SerializeField, Multiline(15)] 
        public string jsonTest;

        protected void Awake()
        {
            animator = new TeacherAnimator(gameObject);
            AiApiJsonInterpreter.Fufillment fufillment = AiApiJsonInterpreter.InterpretResponse(jsonTest);
            Debug.Log(fufillment);
            TTSManager.Initialize(gameObject.name, "TTSInitialized");
        }

        void TTSInitialized()
        {
            Debug.Log("TTS Initialized");
        }

        protected virtual void OnEnable()
        {
            ApiAiModule.ResponseRecieved += OnResponseRecieved;
        }

        protected virtual void OnDisable()
        {
            ApiAiModule.ResponseRecieved -= OnResponseRecieved;
        }

        void OnResponseRecieved(string json)
        {
            HackFufillment fulfillment = AiApiJsonInterpreter.InterpretResponse(json);
            HandleResponse(fulfillment);

        }

        private void HandleResponse(HackFufillment fulfillment)
        {
            // Respond positively
            if (fulfillment.ResolvedQuery.ToLowerInvariant().Contains("ye"))
            {
                PositiveResponse();
            }
            else if (fulfillment.ResolvedQuery.Length < 5 && fulfillment.ResolvedQuery.Contains("n"))
            {
                UnderstaningResponse();
            }
            else
            {
                NegativeResponse();
            }
            Speak(fulfillment.Speech);
        }

        private void Speak(string speech)
        {
            TTSManager.Speak(speech, false);
        }

        public void PositiveResponse()
        {
            animator.Positive = true;
        }

        public void NegativeResponse()
        {
            animator.Negative = true;
        }

        public void UnderstaningResponse()
        {
            animator.Understanding = true;
        }
    }

    public class TeacherAnimator : AnimatorWrapper
    {
        class Params
        {
            public const string POSITIVE = "Positive";
            public const string NEGATIVE = "Negative";
            public const string UNDERSTANDING = "Understanding";
        }

        public bool Positive
        {
            set {animator.SetTrigger(Params.POSITIVE);}
        }
        
        public bool Negative
        {
            set {animator.SetTrigger(Params.NEGATIVE);}
        }
        
        public bool Understanding
        {
            set {animator.SetTrigger(Params.UNDERSTANDING);}
        }
        
        public TeacherAnimator(Animator _animator) : base(_animator)
        {
        }

        public TeacherAnimator(GameObject gameObject) : base(gameObject)
        {
        }
    }

    public class AnimatorWrapper
    {
        protected Animator animator;

        public AnimatorWrapper(Animator _animator)
        {
            animator = _animator;
        }

        public AnimatorWrapper(GameObject gameObject)
        {
            animator = gameObject.GetComponent<Animator>();
        }
    }
}