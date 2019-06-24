using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

/*场景跳转脚本
 通过挂载到canvas上并修改button的onclick使用
 所以的场景跳转都可以调用这个函数
 不知道需不需要使用异步场景跳转，我现在还不太了解
     */
public class LevelChange : MonoBehaviour
{
    private int currLevel=0;//当前选择关卡
    private GameObject[] levelModel;//关卡模型
    public int levelCount = 3;//关卡总数
    public GameObject levelDetail;//当前关卡细节
    public float xPos=15f;//关卡按钮点击后移动的距离
    public float zRota = -10f;//关卡按钮点击后旋转的角度
    //当前关卡细节
    public Text levelText;
    public Text nameText;
    public Text storyText;
    public Text pieceText;
    public Text achivementText;
    public Text timeText;
    public GameObject levelImg;


    public List<SceneConfigData> sceneConfigDatas;//关卡配置信息
    public List<SceneSaveData>   sceneSaveDatas;//存档中每关数据列表

    // Start is called before the first frame update
    void Start()
    {
        currLevel = 0;
        //levelDetail.SetActive(false);
        SetActive(levelDetail, false);
        //获得所有关卡模型并隐藏
        levelModel = GameObject.FindGameObjectsWithTag("LevelModel").OrderBy(g => g.transform.GetSiblingIndex()).ToArray();
        foreach(var i in levelModel)
        {
            print(i);
            //i.SetActive(false);
            SetActive(i, false);
        }


        sceneConfigDatas = Game.globalData.ReadSceneConfigDataFile();//获取随机关卡信息，之后要修改从数据库中获取

        
        //Game.Save();//存档
        
        //Game.Load(1);//读档


        //测试用的生成的数据，之后要修改从数据库中获取
        //for (int i=0; i<3; i++)
        //{
        //    Game.globalData.AddNewSceneSaveData(i, 10, i, 0, 0, 20-i, "B");
        //}


        sceneSaveDatas = Game.globalData.sceneSaveDatas;//获取存档中每一关的数据


        foreach (SceneConfigData data in sceneConfigDatas)
        {
            Debug.Log("name: " + data.sceneName);
            Debug.Log("image :" + data.sceneImage);
        }

    }

    /*场景跳转函数
     参数为需要跳转的场景名称
         */

    public void LoadNextLevel(string nextLevel)
    {
        print("nextLevel");
        SceneManager.LoadScene(nextLevel);
    }

    public void LoadNewGame(string nextLevel)
    {
        print("nextLevel");
        Game.NewGame();
        SceneManager.LoadScene(nextLevel);
    }

    public void LoadGame(string nextLevel)
    {
        print("load game");
        Game.Load();
        SceneManager.LoadScene(nextLevel);
    }

    public void EnterLevel()
    {
        print("enterLevel");
        string levelName = "Level_" + currLevel.ToString();
        SceneManager.LoadScene(levelName);
    }

    public void SelectLevel(int levelNum)
    {
        /*先进行判断是否已选中该关卡按钮并且是可进入的关卡*/
        if (currLevel != levelNum && levelNum - 1 <= Game.globalData.passedSceneNums)
        {
            /*先还原上一个关卡按钮*/
            if (currLevel > 0)
            {
                //根据关卡号获得上一个关卡按钮GO
                string lastName = "Level" + currLevel.ToString();
                GameObject lastButton = GameObject.Find(lastName);
                print(lastButton.name);
                //把上一个关卡按钮位置还原
                lastButton.GetComponent<Transform>().position -= new Vector3(xPos, 0f, 0f);
                lastButton.GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f);
                //颜色还原
                lastButton.GetComponent<Image>().color = new Color(0, 0, 0, 1);
                //上一关的关卡细节设为不显示
                //levelDetail.SetActive(false);
                SetActive(levelDetail, false);
                //获得上一关模型并设置为不显示
                if (currLevel <= levelModel.Length)
                    //levelModel[currLevel - 1].SetActive(false);
                    SetActive(levelModel[currLevel - 1], false);
            }

            /*调整当前选中的关卡按钮*/
            //根据关卡号获得当前点击关卡按钮GO
            currLevel = levelNum;
            string levelName = "Level" + currLevel.ToString();
            print(levelName);
            //Button levelButton = <Button>GameObject.Find(levelName);
            GameObject levelButton = GameObject.Find(levelName);
            print(levelButton.name);
            //当前点击关卡按钮弹出
            /*Vector3 target = levelButton.GetComponent<Transform>().position + new Vector3(15, 0f, 0f);
            levelButton.GetComponent<Transform>().position = Vector3.Lerp(levelButton.GetComponent<Transform>().position, target,0.5f);*/
            levelButton.GetComponent<Transform>().position += new Vector3(xPos, 0f, 0f);
            levelButton.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, zRota);
            //当前点击按钮颜色改变
            levelButton.GetComponent<Image>().color = new Color(0.4824f, 0.5412f, 0.8588f, 1f);

            //显示关卡模型
            if (currLevel <= levelModel.Length)
                //levelModel[currLevel - 1].SetActive(true);
                SetActive(levelModel[currLevel - 1], true);

            //显示关卡细节
            //levelDetail.SetActive(true);
            SetActive(levelDetail, true);
            levelText.text = "stage " + currLevel.ToString();//关卡ID
            nameText.text = sceneConfigDatas[currLevel-1].sceneName;//关卡名字
            storyText.text = sceneConfigDatas[currLevel - 1].sceneDescription;//关卡故事背景
            //levelImg = sceneConfigDatas[currLevel - 1].sceneImage;//关卡图片
            //读取图片
            //print("reading"+ currLevel+" img from "+ sceneConfigDatas[currLevel - 1].sceneImage);

            print(sceneConfigDatas[currLevel - 1].sceneImage);
            string path = "E:\\unity\\project\\DeepHack_source\\DeepHack_source\\Assets\\UI\\关卡选择页面\\其他元素\\";
            Texture2D texture2d = new Texture2D(1, 1);
            texture2d.LoadImage(ReadPNG(path+sceneConfigDatas[currLevel - 1].sceneImage));
            levelImg.GetComponent<Image>().overrideSprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), new Vector2(0, 0));

            Debug.Log("sumOfPieces:        " + (currLevel - 1).ToString());
            int sumOfPieces = sceneConfigDatas[currLevel - 1].numOfCoins + sceneConfigDatas[currLevel - 1].numOfHidedCoins;//关卡碎片总数

            Debug.Log("sceneSaveDatas:        " + (currLevel - 1).ToString());
            Debug.Log("num of sceneSaveData: " + sceneSaveDatas.Count);
            if(levelNum - 1 == Game.globalData.passedSceneNums)
            {
                int collectPieces = 0;//需要收集的信息碎片总数
                pieceText.text = collectPieces.ToString() + "/" + sumOfPieces.ToString();//显示关卡收集物情况
                achivementText.text = "no";
                timeText.text = "no";//通关所用时间
            }
            else
            {
                int collectPieces = sceneSaveDatas[currLevel - 1].numOfCollectedCoins + sceneSaveDatas[currLevel - 1].numOfCollectedHidedCoins;//收集的关卡总数
                pieceText.text = collectPieces.ToString() + "/" + sumOfPieces.ToString();//显示关卡收集物情况
                achivementText.text = sceneSaveDatas[currLevel - 1].score;//关卡评级
                timeText.text = sceneSaveDatas[currLevel - 1].usedTime.ToString();//通关所用时间
            }

        }



    }

    static public void SetActive(GameObject go, bool state)
    {
        if (go == null)
        {
            return;
        }

        if (go.activeSelf != state)
        {
            go.SetActive(state);
        }
    }

    /*
    public void UpdateInfomation(string infomation)
    {
        Texture2D texture2d = new Texture2D(1, 1);
        texture2d.LoadImage(ReadPNG(infomation));
        gameObject.GetComponent<Image>().sprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), new Vector2(0, 0));
    }*/
    /// <summary>  
    /// 根据图片路径返回图片的字节流byte[]  
    /// </summary>  
    /// <param name="imagePath"></param>  
    /// <returns>返回的字节流</returns>  
    public static byte[] ReadPNG(string path)
    {
        print("reading img from" + path);
        FileStream fileStream = new FileStream(path, FileMode.Open, System.IO.FileAccess.Read);

        fileStream.Seek(0, SeekOrigin.Begin);

        byte[] binary = new byte[fileStream.Length]; //创建文件长度的buffer   
        fileStream.Read(binary, 0, (int)fileStream.Length);

        fileStream.Close();

        fileStream.Dispose();

        fileStream = null;

        return binary;
    }


}
