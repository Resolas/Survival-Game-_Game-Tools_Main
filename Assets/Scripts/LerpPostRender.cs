using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LerpPostRender : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        myPostRender = GetComponent<PostProcessVolume>(); 
    }

    private PostProcessVolume myPostRender;
    [SerializeField]
    private float lerpTo = 0f;
    public float time = 5;
    private void FixedUpdate()
    {

        myPostRender.weight = Mathf.Lerp(myPostRender.weight,lerpTo,Time.deltaTime);

    }

}
