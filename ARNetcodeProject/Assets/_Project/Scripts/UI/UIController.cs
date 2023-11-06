using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace sks {
    public class UIController : MonoBehaviour {
        public GameObject messageBox;
        public Button closeBtn;
        [SerializeField] TextMeshProUGUI heading;
        [SerializeField] TextMeshProUGUI metaData;
        // Start is called before the first frame update
        void Start() {
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
        void OnHeadingUpdate(string headingText) {
            if (heading != null) {
                heading.text = headingText;
            }

        }
        //whenever the user clicks on the data box, and csv is processed with the help of DataHandler.cs, this method is called
        void OnMetaDataUpdate(string message) {
            if (metaData != null) {
                metaData.text = message;
            }
        }

        public void SetDataMessage(DataMesage dataMessage) {
            OnHeadingUpdate(dataMessage.heading);
            OnMetaDataUpdate(dataMessage.metaData);
        }
        public void ReSetDataMessage() {
            OnHeadingUpdate("");
            OnMetaDataUpdate("");
        }
        void OnDestroy() {
            closeBtn.onClick.RemoveAllListeners();
        }
    }
}