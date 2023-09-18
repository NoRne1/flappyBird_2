using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipelineManager : MonoBehaviour
{
    public float deltaTime;
    public float randomGapMin;
    public float randomGapMax;
    public GameObject pipelineTemplate;

    public int pipelineCount;
    private List<GameObject> pipelines = new List<GameObject>();
    private int pipelineIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        InitPipelines();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitPipelines()
    {
        for (int i = pipelines.Count; i < pipelineCount; i = i + 1)
        {
            pipelines.Add(Instantiate(pipelineTemplate, transform));
        }
        pipelines.ForEach(pipeline => pipeline.SetActive(false));
    }

    Coroutine coroutine = null;

    public void StartGenerate()
    {
        coroutine = StartCoroutine(GeneratePipelines());
    }

    public void StopGenerate()
    {
        pipelines.ForEach(pipeline =>
        {
            pipeline.transform.position = transform.position;
            pipeline.SetActive(false);
        });
        StopCoroutine(coroutine);
    }

    IEnumerator GeneratePipelines()
    {
        while (true)
        {
            PlaceNewPipeline();
            yield return new WaitForSeconds(deltaTime);
        }
    }

    private void PlaceNewPipeline()
    {
        GameObject temp = pipelines[pipelineIndex];
        float randomGap = Random.Range(randomGapMin, randomGapMax);
        temp.transform.position = new Vector3(transform.position.x,
            transform.position.y + randomGap, transform.position.z);
        temp.SetActive(true);
        pipelineIndex = (pipelineIndex + 1) % pipelines.Count;
    }
}
