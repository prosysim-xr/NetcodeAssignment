using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject messageBox;
    public Button closeBtn;
    [SerializeField] TextMeshProUGUI heading;
    [SerializeField] TextMeshProUGUI metaData;
    // Start is called before the first frame update
    void Start()
    {
        closeBtn.onClick.AddListener(OnCloseMessageBox);

    }

    public void OnCloseMessageBox() {
        messageBox.SetActive(false);
    }

    //Whenever the user clicks on the data box, this method is called
    public void OnOpenMessageBox() {         
        messageBox.SetActive(true);
    }

    //whenever the user clicks on the data box, this method is called
    public void OnHeadingUpdate(string userName) {
        if (heading != null) {
            heading.text = userName + " clicked Data-Box Metadata is:";
        }
        
    }
    //whenever the user clicks on the data box, and csv is processed with the help of DataHandler.cs, this method is called
    public void OnMetaDataUpdate(string message) {
        if (metaData != null) {
            heading.text = message;
        }
    }

    void OnDestroy() {
        closeBtn.onClick.RemoveAllListeners();
    }
}
