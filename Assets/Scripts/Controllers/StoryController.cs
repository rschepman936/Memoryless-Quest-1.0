using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI storyText1;  //object for the text
    [SerializeField]
    private TextMeshProUGUI storyText2;
    [SerializeField]
    private TextMeshProUGUI storyText3;
    [SerializeField]
    private Image storyImage;           //object(image) for the background
    [SerializeField]
    private GameObject panel;           //panel for cutscene
    [SerializeField]
    private Sprite[] backgroundImages;

    public static bool isStory = false;

    void Start(){
        // get the havingCount value from MemoryController
        int currentCount = MemoryController.Instance.havingCount;

        // other stories per floors
        string message1 = "";
        string message2 = "";
        string message3 = "";
        Sprite bg = null;
        switch (currentCount){
            case 0:
                message1 = "\'You\'ll soon wake up\'";
                message2 = "You\'ve stepped onto unfamiliar ground without knowing why. Your head is throbbing";
                message3 = "\"Why am I here...? That voice just now, what was it?\"";
                bg = backgroundImages.Length > 0 ? backgroundImages[0] : null;
                isStory = true;
                break;
            case 1:
                message1 = "\'Misfortune visits you all of a sudden\'";
                message2 = "Your unconscious mind longs intensely for a 'Memory Fragment.' It will serve as a positive hint for you";
                message3 = "\"It feels like my consciousness is gradually coming back.\"";
                bg = backgroundImages.Length > 1 ? backgroundImages[1] : null;
                isStory = true;
                break;
            case 2:
                message1 = "\'I must return\'";
                message2 = "You are certain you ended up here due to an unforeseen accident. There isn\'t much farther to go before you can return to reality";
                message3 = "\"Oh… sorry, it was my fault.\"";
                bg = backgroundImages.Length > 2 ? backgroundImages[2] : null;
                isStory = true;
                break;
            case 3:
                message1 = "\'Are you ready to accept the heavy reality and pay the price you deserve?\'";
                message2 = "As the perpetrator of a traffic accident, you have entirely taken another’s life. If you want to accept reality, then run; if not, you can only remain here";
                message3 = "\"Let’s deal with all\"";
                bg = backgroundImages.Length > 3 ? backgroundImages[3] : null;
                isStory = true;
                break;
            case 4:
                message1 = "\'I\'ve been waiting for you\'";
                message2 = "At the end of your blurred memories, what kind of welcome will the cruel reality give you?";
                message3 = "";
                bg = backgroundImages.Length > 4 ? backgroundImages[4] : null;
                isStory = true;
                break;
            default:
                message1 = "\'Finally Facing Reality\'";
                message2 = "Having barely regained your memories and escaped the dungeon, you find yourself standing between the victim\'s grieving family, who look at you with concern, and your own family, who sigh in relief. Now, it\'s time to accept it.";
                message3 = "";
                isStory = true;
                break;
        }

        // text and image appear
        ShowCutscene(message1, message2, message3, bg);
    }

    public void ShowCutscene(string message1, string message2, string message3, Sprite bg = null){
        panel.SetActive(true);
        storyText1.text = message1;
        storyText2.text = message2;
        storyText3.text = message3;
        if (storyImage != null && bg != null){
            storyImage.sprite = bg;
        }
    }

    public void HideCutscene()
    {
        panel.SetActive(false);
    }

    void Update(){
        if (panel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            isStory = false;
            HideCutscene();
        }
    }
}
