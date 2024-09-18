using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;

    public bool attackable = false;
    public bool attacktarget = false;
    public bool attackselectable = false;

    public bool healPosition = false;

    public List<Tile> adjacencyList = new List<Tile>();

    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;

    public Material material;
    public GameObject tile;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (current)
        {
            ParticleSystem par = GetComponentInChildren<ParticleSystem>(true);
            var emission = par.emission;
            emission.rateOverTime = 20;
            material = FindMaterialInChildrenExcludingSelf();
            material.EnableKeyword("_EMISSION");
            SetEmission(material, Color.yellow, 0.1f);
            par.gameObject.SetActive(true);
        }
        else if (target)
        {
            ParticleSystem par = GetComponentInChildren<ParticleSystem>(true);
            var emission = par.emission;
            emission.rateOverTime = 20;
            material = FindMaterialInChildrenExcludingSelf();
            material.EnableKeyword("_EMISSION");
            SetEmission(material, Color.cyan, 0.1f);
            par.gameObject.SetActive(true);
        }
        else if (selectable)
        {
            ParticleSystem par = GetComponentInChildren<ParticleSystem>(true);
            var emission = par.emission;
            emission.rateOverTime = 50;
            material = FindMaterialInChildrenExcludingSelf();
            material.EnableKeyword("_EMISSION");
            SetEmission(material, new Color(0f,0.05f,0f), 1f);
            par.gameObject.SetActive(true);
            if (healPosition)
            {
                emission.rateOverTime = 20;
                material = FindMaterialInChildrenExcludingSelf();
                material.EnableKeyword("_EMISSION");
                SetEmission(material, new Color(1f, 0f, 1f), 1f);
                par.gameObject.SetActive(true);
            }
        }
        else if (attackable)
        {
            ParticleSystem par = GetComponentInChildren<ParticleSystem>(true);
            var emission = par.emission;
            emission.rateOverTime = 20;
            material = FindMaterialInChildrenExcludingSelf();
            material.EnableKeyword("_EMISSION");
            SetEmission(material, Color.red, 1f);
            par.gameObject.SetActive(true);
        }
        else if (attacktarget)
        {
            ParticleSystem par = GetComponentInChildren<ParticleSystem>(true);
            var emission = par.emission;
            emission.rateOverTime = 20;
            material = FindMaterialInChildrenExcludingSelf();
            material.EnableKeyword("_EMISSION");
            SetEmission(material, Color.magenta, 1f);
            par.gameObject.SetActive(true);
        }
        else if (attackselectable)
        {
            ParticleSystem par = GetComponentInChildren<ParticleSystem>(true);
            var emission = par.emission;
            emission.rateOverTime = 50;
            material = FindMaterialInChildrenExcludingSelf();
            material.EnableKeyword("_EMISSION");
            SetEmission(material, new Color(0f, 0f, 0.05f), 1f);
            par.gameObject.SetActive(true);
        }
        else
        {
            ParticleSystem par = GetComponentInChildren<ParticleSystem>(true);
            par.gameObject.SetActive(false);
            material = FindMaterialInChildrenExcludingSelf();
            material.DisableKeyword("_EMISSION");
            SetEmission(material, new Color(1f, 1f, 1f), 0f);
        }

        if (TurnManager.Instance.EnemyTurn)
        {
            Reset();
        }
    }

    Material FindMaterialInChildrenExcludingSelf()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>(true);
        foreach (Renderer renderer in renderers)
        {
            if (renderer.gameObject != this.gameObject && renderer.gameObject != tile.gameObject)
            {
                return renderer.material;
            }
        }
        return null;
    }
    void SetEmission(Material mat,Color color, float intensity)
    {
        Color finalColor = color * Mathf.LinearToGammaSpace(intensity);
        mat.SetColor("_EmissionColor", finalColor);
    }
    public void Reset()
    {
        adjacencyList.Clear();
        current = false;
        target = false;
        selectable = false;

        attackable = false;
        attackselectable = false;
        attacktarget = false;

        healPosition = false;
        visited = false;
        parent = null;
        distance = 0;
    }
    public void Findneighbors(float jumpheight)
    {
        Reset();
        print("Find");
        CheckTile(Vector3.forward, jumpheight);
        CheckTile(Vector3.right, jumpheight);
        CheckTile(-Vector3.forward, jumpheight);
        CheckTile(-Vector3.right, jumpheight);
    }

    public void CheckTile(Vector3 direction, float jumpHeight)
    {
        print("Check");
        Vector3 halfExtents = new Vector3(0.25f, (1 + jumpHeight) / 2, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach (Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if (tile != null)
            {
                if (tile.walkable)
                {
                    RaycastHit hit;


                    adjacencyList.Add(tile);

                }
                
            }
        }
    }
    
}
