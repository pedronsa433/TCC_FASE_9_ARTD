using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private TurretBlueprint standardTurret;
    [SerializeField] private TurretBlueprint missileTurret;
    [SerializeField] private TurretBlueprint laserTurret;
    
    private BuildManager buildManager;

    private void Start() => buildManager = BuildManager.instance;
    
    public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret Selected");
        buildManager.SelectTurretToBuild(standardTurret);
    }
    
    public void SelectMissileTurret()
    {
        Debug.Log("Missile Turret Selected");
        buildManager.SelectTurretToBuild(missileTurret);
    }
    
    public void SelectLaserTurret()
    {
        Debug.Log("Laser Turret Selected");
        buildManager.SelectTurretToBuild(laserTurret);
    }
}
