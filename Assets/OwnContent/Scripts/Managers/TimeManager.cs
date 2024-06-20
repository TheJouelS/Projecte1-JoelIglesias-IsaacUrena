using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] float CustomTimeDilation = 1f;
    [SerializeField] float totalTimeOfGame = 0f;
    private bool stopCounting = false;

    public static TimeManager instance;

    private void Awake()
    {
        if (TimeManager.instance == null)
            TimeManager.instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        CustomTimeDilation = 1f;
        totalTimeOfGame = 0f;
        stopCounting = false;
    }

    private void Update()
    {
        if (!stopCounting)
            totalTimeOfGame += Time.deltaTime;
    }

    public float GetTotalTime()
    {
        return totalTimeOfGame/60f;
    }

    public void StopCountingTotalTime()
    {
        stopCounting = true;
    }
}
