using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slideshow : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform contentPanel;
    public RectTransform sampleListItem;

    public HorizontalLayoutGroup HLG;

    private bool isSnapped;

    public float snapForce;
    private float snapSpeed;
    private float adjustment;

    // For slide instantiation
    [Header("Slide Loading & Generation")]
    private List<Sprite> slides;           // List of sprites to display
    public GameObject slidePrefab;         // The Slide prefab with an Image component
    public ImportSlides importSlides;      // ImportSlides script

    // Start is called before the first frame update
    void Start()
    {
        isSnapped = false;
        Debug.Log("Sample list item width: " + sampleListItem.rect.width);
        adjustment = sampleListItem.rect.width / 2;

        // Get slides from ImportSlides and then instantiate them
        StartCoroutine(GenerateSlides());
    }

    // Update is called once per frame
    void Update()
    {
        int currentItem = Mathf.RoundToInt((0 - contentPanel.localPosition.x / (sampleListItem.rect.width + HLG.spacing)));

        // Snap to slide
        if (scrollRect.velocity.magnitude < 200 && !isSnapped) {
            Debug.Log("Snapping to slide " + currentItem);
            Debug.Log("Moving from " + contentPanel.localPosition.x + " to " + (0 - (currentItem * (sampleListItem.rect.width + HLG.spacing) + adjustment)));

            scrollRect.velocity = Vector2.zero;
            snapSpeed += snapForce * Time.deltaTime;

            contentPanel.localPosition = new Vector3(
                Mathf.MoveTowards(contentPanel.localPosition.x, 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing) + adjustment), snapSpeed), 
            contentPanel.localPosition.y, 
            contentPanel.localPosition.z);

            if (contentPanel.localPosition.x == 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing) + adjustment)) {
                isSnapped = true;
            }
        }
        if (scrollRect.velocity.magnitude > 200) {
            isSnapped = false;
            snapSpeed = 0;
        }
    }

    IEnumerator GenerateSlides() {
        yield return new WaitForSeconds(0.2f);
        slides = importSlides.GetSlides();
        // Debug.Log("Slides: " + slides);
        
        foreach (var sprite in slides)
        {
            // Instantiate the Slide prefab
            GameObject slide = Instantiate(slidePrefab, scrollRect.content.transform);

            // Find the child GameObject named "Image"
            Transform imageTransform = slide.transform.Find("Image");

            if (imageTransform != null)
            {
                Image imageComponent = imageTransform.GetComponent<Image>();
                if (imageComponent != null)
                {
                    // Set the sprite of the Image component
                    imageComponent.sprite = sprite;
                }
                else
                {
                    Debug.LogError("The Image child does not have an Image component.");
                }
            }
            else
            {
                Debug.LogError("There is no child GameObject named 'Image' in the prefab.");
            }
        }
    }
}
