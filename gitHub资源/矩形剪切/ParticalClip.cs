using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proj_xqzd;

public class ParticalClip : MonoBehaviour
{
    public bool isClip = true;
    private RectTransform rectTrans; // Mask transform
    private List<Material> materialList = new List<Material>();
    private Transform canvas;
    private float halfWidth;
    private float halfHeight;
    private float canvasScale;

    void Awake()
    {
        var mask = this.transform.GetComponentInParent<UnityEngine.UI.Mask>();
        if (mask != null)
        {
            this.rectTrans = mask.gameObject.GetComponent<RectTransform>();
            //Debug.LogError(rectTrans.name);
        }

    }
    void Start()
    {
        if (this.rectTrans == null)
        {
            return;
        }

        this.canvas = this.transform.GetComponentInParent<Canvas>().transform;

        var renders = this.transform.GetComponentsInChildren<ParticleSystemRenderer>();
        for (int i = 0, j = renders.Length; i < j; i++)
        {
            var render = renders[i];
            var mat = render.material;
            this.materialList.Add(mat);
            //Debug.LogError(render.name);
        }

        Vector3[] corners = new Vector3[4];
        rectTrans.GetWorldCorners(corners);
        Vector2 topLeft = corners[0];
        Vector2 bottomRight = corners[2];

        Vector4 area = new Vector4(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);

        for (int i = 0, len = this.materialList.Count; i < len; i++)
        {
            this.materialList[i].SetInt("_IsClip", isClip ? 1 : 0);
            this.materialList[i].SetVector("_Area", area);
        }
    }

}
