using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImportSlides : MonoBehaviour
{
    private string folderPath;
    private List<Sprite> slides = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        string folderName = "SuperStageSlides";
        folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), folderName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Load images from folder
        LoadImagesFromFolder();
    }

    void LoadImagesFromFolder()
    {
        // Get image files from folder
        DirectoryInfo di = new DirectoryInfo(folderPath);
        FileInfo[] allFiles = di.GetFiles("*.*");
        int currLength = allFiles[0].FullName.Length;

        // Sort allFiles in numerical order, e.g. Slide 1 < Slide 2 < ... < Slide 10 < ...
        FileInfo[] imageFiles = new FileInfo[allFiles.Length];
        int index = 0;
        while (index < imageFiles.Length)
        {
            for (int i = 0; i < allFiles.Length; ++i)
            {
                if (allFiles[i].FullName.Length == currLength)
                {
                    imageFiles[index] = allFiles[i];
                    ++index;
                }
            }
            ++currLength;
        }

        // Load each image
        foreach(FileInfo image in imageFiles)
        {
            string fullPath = image.FullName;
            StartCoroutine(LoadImage(fullPath));
        }

    }

    IEnumerator LoadImage(string imagePath)
    {
        WWW www = new WWW("file:///" + imagePath);
        yield return www;

        Texture2D texture = new Texture2D(2, 2);
        www.LoadImageIntoTexture(texture);

        Sprite slide = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        slides.Add(slide);
    }

    public List<Sprite> GetSlides()
    {
        return slides;
    }
}
