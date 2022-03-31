using UnityEngine;

public class UIFeedbackLink : MonoBehaviour
{
    [SerializeField]
    private string feedbackURL = "https://forms.gle/Hs2vQjHBqDtGZDz9A";
    public void OpenFeedbackLink()
    {
        Application.OpenURL(feedbackURL);
    }
}