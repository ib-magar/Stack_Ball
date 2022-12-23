using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[System.Serializable]
public class levelData
{
    public levelData(levelData l)
    {
        totalStacks = l.totalStacks;
        points = new int[l.points.Length];
        for(int i=0;i<l.points.Length;i++)
        {
            points[i] = l.points[i];
        }
        objType = l.objType;
    }
    public int totalStacks;
    public int[] points;
    public objectType objType;
}
public enum objectType { circle,flower,hexagon,spikes,squares};
public class LevelManager : MonoBehaviour
{
    public static int currentLevel;


    public levelData[] LevelData;
    public levelData currentLevelData;

    public GameObject[] stackPrefabs;
    public GameObject stacks;
    public GameObject winPrefab;

    public float stackDistance;
    public float stackRotation;

    public Vector2 rotationLimit;

    Coroutine rotationCoroutine;
    IEnumerator rotateStacks()
    {
        /*float[] rotations = new float[stacks.transform.childCount-1];   
        for(int i=0;i<stacks.transform.childCount-1;i++)
        {
            rotations[i] = Random.Range(rotationLimit.x, rotationLimit.y);
        }
        while(true)
        {
            for(int i=0;i<rotations.Length;i++)
            {
                stacks.transform.GetChild(i).Rotate(Vector3.up * rotations[i]*Time.deltaTime);
            }
                yield return null;
        }*/
        while (true)
        {
            for (int i = 0; i < stacks.transform.childCount - 1; i++)
            {
                stacks.transform.GetChild(i).Rotate(Vector3.up * Random.Range(rotationLimit.x, rotationLimit.y)*Time.deltaTime);
            }
            yield return null;
        }
    }
    private void Awake()
    {
        stacks = GameObject.Find("Stacks");
    }
    private void Start()
    {
        currentLevel = -1;
        nextLevel();  
    }
    public void Restart()
    {
        LoadLevel();
    }
    public  void nextLevel()
    { 
        if (currentLevel + 1 < LevelData.Length)
        {
            currentLevel++;
            LoadLevel();
        }
        else
        {
            print("Game completed");
        }
    }
    public void LoadLevel()
    { 
        currentLevelData = new levelData(LevelData[currentLevel]);
        createLevel();
        GameObject.FindObjectOfType<ballHandler>().currentScore = 0;
        GameObject.FindObjectOfType<UImanager>().updateScore();
        GameObject.FindObjectOfType<cameraManager>().resetPosition();
    }
    public void createLevel()
    {
        if(rotationCoroutine!=null)                 //stopping the coroutine
        StopCoroutine(rotationCoroutine);

         for(int i=0;i<stacks.transform.childCount;i++)         //destroying the objects remaining
         {
            Destroy(stacks.transform.GetChild(i).gameObject);
         }

        /*if(currentLevelData.points.Length==0)
        {
            GameObject stackparent = new GameObject("stackParent");
            stackparent.transform.parent = stacks.transform;
            for (int i=0;i<currentLevelData.totalStacks;i++)
            {
                GameObject stack = Instantiate(stackPrefab);
                stack.transform.position = Vector3.zero + Vector3.down * i * stackDistance;
                stack.transform.eulerAngles = Vector3.zero + Vector3.up * i * stackRotation;
                stack.transform.parent = stackparent.transform;
            }
        }
        else*/
        
        {
            string name="";
            switch(currentLevelData.objType)
            {
                case objectType.circle: name = "circle "; break;
                case objectType.flower: name = "flower "; break;
                case objectType.hexagon: name = "hex "; break;
                case objectType.spikes:name = "spikes "; break;
                case objectType.squares:name = "square "; break;
                 
            };

            int x = 0,j;
            Vector3 rot = new Vector3(0, Random.Range(0, 360), 0);
            for(int i=0;i<currentLevelData.points.Length;i++)
            {
                GameObject stackparent = new GameObject("stackParent" + i);
                stackparent.transform.parent = stacks.transform;
                int k;
                for(j=x,k=0;j<currentLevelData.points[i];j++,k++)
                {
                    GameObject g =(GameObject) Resources.Load(name + Random.Range(0, 3));
                    GameObject stack = Instantiate(g,Vector3.zero,g.transform.rotation);
                    stack.transform.position = Vector3.zero + Vector3.down * j * stackDistance;
                    stack.transform.eulerAngles = rot + Vector3.up * k * stackRotation;
                    stack.transform.parent = stackparent.transform;
                }
                x = j;
                rot = new Vector3(0, Random.Range(0, 360), 0);
            }
        }

        //GameObject winObj = Instantiate(winPrefab, GameObject.Find("Stacks").transform.GetChild(GameObject.Find("Stacks").transform.childCount - 1).transform.position + Vector3.down *2* stackDistance, winPrefab.transform.rotation);
        GameObject winObj = Instantiate(winPrefab, Vector3.down*currentLevelData.totalStacks*stackDistance,winPrefab.transform.rotation);
        winObj.transform.parent = stacks.transform;
        GameObject.FindObjectOfType<ballHandler>().transform.position = new Vector3(0, 1.5f, -1.25f);
        GameObject.FindObjectOfType<ballHandler>().win = false;
        GameObject.FindObjectOfType<ballHandler>().stopBall(1);

        rotationCoroutine= StartCoroutine(rotateStacks());
    }

}
