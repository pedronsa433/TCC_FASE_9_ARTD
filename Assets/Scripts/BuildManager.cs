using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    
    public GameObject buildEffect;
    public GameObject sellEffect;
    
    public TurretBlueprint turretToBuild;

    [SerializeField] private NodeUI nodeUI;
    [SerializeField] private Node selectedNode;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;

        turretToBuild = null;
    }
    
    public bool CanBuild => turretToBuild != null;
    public bool HasMoney => PlayerStats.Money >= turretToBuild.cost;

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }
    
    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }
    
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
    
    public TurretBlueprint GetTurretToBuild() => turretToBuild;
}