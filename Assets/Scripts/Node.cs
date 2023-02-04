using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [SerializeField] private Color Positive;
    [SerializeField] private Color Negative;
    [SerializeField] private Vector3 spawnOffset;
    
    [Header("Optional")] 
    [SerializeField] private GameObject turret;
    public TurretBlueprint turretBlueprint;
    public bool isUpgraded = false;
    
    private BuildManager buildManager;
    private Color startColor;
    private Renderer rend;
    private Outline outline;

    private void Start()
    {
        buildManager = BuildManager.instance;
        outline = GetComponent<Outline>();
    }

    public Vector3 GetBuildPosition() => transform.position + spawnOffset;

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (!buildManager.CanBuild)
            return;
    }
    
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }
        
        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }
    
    public void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
            return;

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        
        turret = _turret;
        turretBlueprint = blueprint;

        Debug.Log("Turret build - " + blueprint.prefab.name);
        buildManager.turretToBuild = null;
        Destroy(effect, 2f);
    }
    
    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
            return;

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        Destroy(turret);
        
        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        
        turret = _turret;

        isUpgraded = true;
        
        Debug.Log("Turret upgraded - " + turretBlueprint.upgradedPrefab.name);

        Destroy(effect, 2f);
    }
    
    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();
        
        Destroy(turret);
        turretBlueprint = null;
        
        GameObject effect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 2f);
    }

    private void Update()
    {
        if (buildManager.turretToBuild != null)
        {
            outline.enabled = true;
            
            if (turret == null)
            {
                outline.OutlineColor = Positive;
                outline.OutlineWidth = 3;
            }
            else
            {
                outline.OutlineColor = Negative;
                outline.OutlineWidth = 4;
            }
        }
        else
        {
            outline.enabled = false;
        }
    }
}