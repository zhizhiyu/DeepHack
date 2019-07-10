using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//功能说明：当第一次玩家出现在地形第二层时，开始出现下降扩张的圆盘，之后每间隔一定时间，出现一次圆盘。圆盘出现时，取消探照灯功能
public class CylinderController : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public GameObject player;

    /// <summary>
    /// 关卡控制器
    /// </summary>
    public GameObject sceneControllerObject;

    public float finalSize = 12;//扩大后的最终尺寸
    public float speed =1f;//下降速度
    public float scaleSpeed=3.3f;//扩张速度
    public float lowest = 0f;//能下降到的最低点
    public float time = 10;//圆盘出现的间隔时间
    public float secondLevel = 2.6f;//地形第二层的高度
    public GameObject spotLight;//探照灯灯光
    public GameObject lightModel;//探照灯模型

    private Vector3 initPosition;//初始位置
    private Vector3 initScale;//初始尺寸
    private bool first;//是否是第一次下降
    private bool afterFirst;//是否是第二次下降

    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        initScale = transform.localScale;
        first = true;
        afterFirst = true;
        StartCoroutine("Period");//开始协程
    }

 
    //玩家触发死亡
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            sceneControllerObject.GetComponent<SceneController>().Attack();

        }
    }

    private IEnumerator Period()
    {
        while(true)//因为是协程，所以能每帧只执行一次
        {
            //玩家第一次到达第二层，圆盘第一次下降
            if (player.transform.position.y > secondLevel && first)
            {
                spotLight.SetActive(false);//圆盘出现时取消探照灯功能
                lightModel.GetComponent<RotateAround>().stop = true;
                ScaleAndDown();
                
                if (transform.position.y < lowest)
                {
                    
                    first = false;
                    ResetPosition();
                    spotLight.SetActive(true);
                    lightModel.GetComponent<RotateAround>().stop = false;
                }
            }
            else if (!first)//如果不是第一次下降
            {
                if (afterFirst)//如果是第二次下降
                {
                    //等待一定时间
                    yield return new WaitForSeconds(time);
                    afterFirst = false;
                }

                spotLight.SetActive(false);
                lightModel.GetComponent<RotateAround>().stop = true;
                ScaleAndDown();
                
                if (transform.position.y < lowest)
                { 
                    spotLight.SetActive(true);
                    lightModel.GetComponent<RotateAround>().stop = false;
                    ResetPosition();
                    //等待一定时间
                    yield return new WaitForSeconds(time);
                }
            }
            yield return null;
        }

        
    }

    //圆盘扩张并下降
    private void ScaleAndDown()
    {
        if (transform.localScale.x < finalSize && transform.localScale.z < finalSize)
        {
            transform.localScale = new Vector3(transform.localScale.x + scaleSpeed * Time.deltaTime,
                transform.localScale.y, transform.localScale.z + scaleSpeed * Time.deltaTime);

        }
        transform.Translate(0, -speed * Time.deltaTime, 0);
    }

    //重置圆盘位置为初始位置
    private void ResetPosition()
    {
        transform.position = initPosition;
        transform.localScale = initScale;
    }

}

