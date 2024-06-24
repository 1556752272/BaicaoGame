using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class CharacterAnimation : MonoBehaviour
{
    private Color mineColor; // ����Ϊ��ɫ
    //private Animator animator; // ��ɫ��Animator���
    //public float animatorTimer = 0.5f;
    private float animatorCount;
    public MeshRenderer meshRenderer;
    public SkeletonAnimation spineAnimation; // Spine��SkeletonAnimation���
    public float speed = 5f; // ��ɫ�ƶ��ٶ�
    public bool face_right = true;
    private float Hurttimer;
    public bool living = true;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        mineColor= meshRenderer.material.color;
        
        
        //MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        //mpb.SetColor("_Color", newColor);
        //_MeshRenderer.SetPropertyBlock(mpb);
        //animator = GetComponent<Animator>(); // ��ȡAnimator���

        //if (meshRenderer != null && meshRenderer.materials.Length > 0)
        //{
        //    // Debug.Log(meshRenderer.materials[0].color);
        //    // ����һ�����ʵ���ɫ����Ϊ��ɫ  
        //    //meshRenderer.material.color = newColor;

        //    meshRenderer.materials[0].SetColor("_Color", Color.red);

        //    //Debug.Log(meshRenderer.materials[0].color);

        //    // ʹ��StartCoroutine��ʼһ��Э������0.3���ָ���ɫ  
        //    // StartCoroutine(RestoreOriginalColor(0.3f));
        //}
        // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //Get the Renderer component from the new cube
        //var cubeRenderer = cube.GetComponent<Renderer>();

        //Call SetColor using the shader property name "_Color" and setting the color to red
        // cubeRenderer.material.SetColor("_Color", Color.red);
        //}
        //// ����Mecanim����
        //SkinnedMeshRenderer skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        //if (skinnedMeshRenderer != null)
        //{
        //    foreach (Material mat in skinnedMeshRenderer.materials)
        //    {
        //        mat.SetColor("_Color", newColor);
        //    }
        //}
    }

    
    void Update()
    {
        if (living)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            if (moveX > 0)
            {
                //spineAnimation.initialFlipX = false;
                this.transform.localScale = new Vector3(0.72f, 0.72f, 0);
                spineAnimation.AnimationName = "walk";
                face_right = true;
            }
            else if (moveX < 0)
            {
                //spineAnimation.initialFlipX = true;
                this.transform.localScale = new Vector3(-0.72f, 0.72f, 0);
                spineAnimation.AnimationName = "walk";
                face_right = false;
            }
            else if (face_right && moveY != 0)
            {
                this.transform.localScale = new Vector3(0.72f, 0.72f, 0);
                spineAnimation.AnimationName = "walk";
            }
            else if (!face_right && moveY != 0)
            {
                this.transform.localScale = new Vector3(-0.72f, 0.72f, 0);
                spineAnimation.AnimationName = "walk";
            }
            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            //if (moveX == 0 && moveY == 0)
            {
                //spineAnimation.AnimationName = "died";
                spineAnimation.AnimationName = "idle";
            }
            if (Hurttimer > 0)
            {
                Hurttimer -= Time.deltaTime;
                if (Hurttimer <= 0)
                {
                    meshRenderer.material.color = mineColor;
                }
            }
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        meshRenderer.material.color = Color.red;
    //        Hurttimer = 0.3f;
           
    //    }
    //}
    public void Die()
    {
        spineAnimation.AnimationName = "died";
        living = false;
        spineAnimation.loop = false; // ����������ѭ��
    }
}
