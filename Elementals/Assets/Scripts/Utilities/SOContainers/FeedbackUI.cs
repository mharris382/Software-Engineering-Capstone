using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Utilities
{
    public class FeedbackUI : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        private const string FEEDBACK_DATA_PATH = "Scenes/Levels/Feedback/USERNAME.txt";


        public EventSystem eventSystem;
        public UnityEngine.UI.Button playFinishButtonPrefab;
        public UnityEvent<string> setPlayFinishButtonLabel;
        
        
        public UnityEngine.UI.Button feedbackButton;
        public Button cancelFeedbackButton;
        public UnityEvent<string> setFeedbackButtonLabel;

        public UnityEvent<bool> showFeedbackUI;

        
        
        [Header("Events")]
        public EventContainer StartGameEvent;
        public EventContainer PauseGameEvent;
        public EventContainer ResumeGameEvent;
        public EventContainer EndGameEvent;

        
        public Transform PlayerTransform { get; set; }

        public enum State
        {
            Waiting,
            Playing,
            Feedback
        }
        
        
        [System.Serializable]
        private class FeedbackInfo
        {
            public DateTime actualTime;
            public float gameTime;
            public Vector3 position;
            public string message;
        }
        
        [Serializable]
        private class SessionInfo
        {
            private DateTime SessionStart;
            private DateTime SessionEnd;
            public float totalPlayTime;
        }

        private bool _createdSelectionList = false;
        private List<string> _users = new List<string>();
        
   

        private State _state = State.Waiting;
        public State state => _state;


        private const string FINISH_LABEL = "End Session";
        private const string START_LABEL = "Start Session";
        
        private bool _isFeedbackUIVisible;

        #region [Feedback Logic]
        //section is dedicated to reading/writing all feedback information for the session and the individual feedback entries
        //_________________________________________________________________________________
        // DOES:
        //     - read data from gameobjects and/or components, 
        //     - keeps track of all feedback information
        //     - writes to a feedback output file
        //_________________________________________________________________________________
        // DOES NOT:
        //     - access the _state
        
        private SessionInfo _currentSession;
        private FeedbackInfo _currentFeedback;
        
        private List<FeedbackInfo> _submittedFeedback;//TODO: do something with submitted feedback
        public void UpdateFeedbackInfo(string message)
        {
            _currentFeedback.message = message;
        }
        private void InitializeNewFeedbackSession()
        {
            IEnumerator TrackPlayTime(Action<float> setFeedbackTimestamp, Action<float> setSessionTimestamp)
            {
                float playTime = 0;
                bool wasPlaying = true;
                while (_state != State.Waiting)
                {
                    if (_state == State.Playing)
                    {
                        wasPlaying = true;
                        playTime += Time.deltaTime;
                        continue;
                    }
                    else if (wasPlaying)
                    {
                        wasPlaying = false;
                        setFeedbackTimestamp(playTime);
                    }
                    yield return null;
                }
                setSessionTimestamp(playTime);
            }
            
            _state = State.Playing;
            _submittedFeedback = new List<FeedbackInfo>();
            _currentFeedback = new FeedbackInfo();
            _currentSession = new SessionInfo();
            StartCoroutine(TrackPlayTime(
                t => _currentFeedback.gameTime = t,
                t => _currentSession.totalPlayTime = t));


        }

        private void SaveFeedback()
        {
            var absoluteFeedbackPath = $"{Application.dataPath}/{FEEDBACK_DATA_PATH}";
            
            if (!File.Exists(absoluteFeedbackPath))
                throw new FileNotFoundException($"File Not Found at: <b>{absoluteFeedbackPath}</b>");
            
            //TODO: log current feedback to external file
            
            _submittedFeedback.Add(_currentFeedback);
            _currentFeedback =  new FeedbackInfo();
        }
        

        #endregion
        

     
        void SetFeedbackUIVisible(bool visible)
        {
            if (visible != _isFeedbackUIVisible)
            {
                _isFeedbackUIVisible = visible;
                showFeedbackUI.Invoke(_isFeedbackUIVisible = visible);
                
                if (visible && eventSystem != null) 
                    eventSystem.SetSelectedGameObject(feedbackButton.gameObject);
            }
        }

        #region [UI Controller]

        void UpdateUI()
        {
            SetFeedbackUIVisible( state == State.Feedback);
            
            playFinishButtonPrefab.enabled = _state != State.Feedback;
            feedbackButton.enabled = _state != State.Waiting;
            
            setPlayFinishButtonLabel.Invoke(state == State.Playing ? FINISH_LABEL : START_LABEL);
            setFeedbackButtonLabel.Invoke(state == State.Feedback ? "Confirm Feedback" : "Add Feedback");

        }

        private void Start()
        {
            if(this.eventSystem == null) this.eventSystem = EventSystem.current;
            
            _isFeedbackUIVisible = false;
           
            cancelFeedbackButton.onClick.AddListener(CancelFeedback);
            feedbackButton.onClick.AddListener(OnFeedbackButtonPressed);
            playFinishButtonPrefab.onClick.AddListener(OnPlayFinishPressed);


            void OnFeedbackButtonPressed() => GetFButtonAction(_state).Invoke();

            Action GetFButtonAction(State s) => s switch
            {
                State.Feedback => ConfirmFeedbackAndResumeGame,
                State.Playing => StartFeedback,
                State.Waiting => BeginGameSession,
                _ => throw new ArgumentException()
            };
            

            void OnPlayFinishPressed() => GetPFbuttonAction(_state).Invoke();

            Action GetPFbuttonAction(State s) => s switch
            {
                State.Feedback => CancelFeedback,
                State.Playing => EndGameSession,
                State.Waiting => BeginGameSession,
                _ => throw new ArgumentException()
            };
        }
        

        #endregion

        #region [State Changes]

        private void ConfirmFeedbackAndResumeGame()
        {
            SaveFeedback();
            ResumeGame();
            UpdateUI();
        }


        private void CancelFeedback()
        {
            ResumeGame();
            UpdateUI();
        }


        private void ResumeGame()
        {
            ResumeGameEvent.Invoke();
            _state = State.Playing;
            
            UpdateUI();
        }


        private void StartFeedback()
        {
            PauseGameEvent.Invoke();
            _state = State.Feedback;
         
            UpdateUI();
        }

        private void EndGameSession()
        {
            //TODO: log session info to external file
            
            //should be the final state
            _state = State.Waiting;
            UpdateUI();
        }


        private void BeginGameSession()
        {
            InitializeNewFeedbackSession();
            UpdateUI();
        }

        #endregion

        class NotInFeedbackStateException : Exception {}
    }
}