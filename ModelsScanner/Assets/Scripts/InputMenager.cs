using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class InputMenager : MonoBehaviour
{
    public int index;
    public GameObject objMain;
    public MeshRenderer mRenderer;
    GameObject[] resourcesObj;
    public GameObject canvas;
    public Text textObjName;
    List<Model> models = new List<Model>();

    struct Model
    {
        public Model(string n,Mesh x, Material[] y)
        {
            name = n;
            mesh = x;
            materials = y;
        }

        public string name;
        public Mesh mesh;
        public Material[] materials;
    }


    // Start is called before the first frame update
    void Start()
    {
        resourcesObj = Resources.LoadAll<GameObject>("Input");

        foreach(GameObject obj in resourcesObj)
        {
            Mesh mesh = obj.GetComponent<MeshFilter>().sharedMesh;
            Material[] materials = obj.GetComponent<Renderer>().sharedMaterials;
            Model m = new Model(obj.name, mesh, materials);

            models.Add(m);
        }

        if (models.Count > 0)
        {
            int counter = 0;
            foreach (Model m in models)
            {
                Debug.Log(counter + "->" + models[counter].name);
                counter++;
            }

            objMain.GetComponent<MeshFilter>().sharedMesh = models[index].mesh;
            mRenderer.materials = models[index].materials;
            textObjName.text = models[index].name;

        }
        else
            Debug.Log("Input file is empty");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void NextModel()
    {
        if (index == models.Count-1)
        {
            index = 0;
        }
        else
            index++;

        objMain.GetComponent<MeshFilter>().sharedMesh = models[index].mesh;
        mRenderer.materials = models[index].materials;
        textObjName.text = models[index].name;
    }

    public void PrevModel()
    {
        if (index == 0)
        {
            index = models.Count-1;
        }
        else
            index--;

        objMain.GetComponent<MeshFilter>().sharedMesh = models[index].mesh;
        mRenderer.materials = models[index].materials;
        textObjName.text = models[index].name;
    }

    public void ScreenShot()
    {
        StartCoroutine(screenShotCourutine());
    }

    private IEnumerator screenShotCourutine()
    {
        canvas.SetActive(false);
        yield return new WaitForEndOfFrame();
        
        if (!Directory.Exists(Application.dataPath + "/Output/"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Output/");
        }

        ScreenCapture.CaptureScreenshot(Application.dataPath + "/Output/" + System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".png");
        //  UnityEditor.AssetDatabase.Refresh();
        canvas.SetActive(true);
    }
}
