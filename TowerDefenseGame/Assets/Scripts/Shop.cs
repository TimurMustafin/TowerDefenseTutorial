
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standartTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;


    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }


    public void SelectStarndartTurret()
    {
        Debug.Log("Standart Turret purchase");
        buildManager.SelectTurretToBuild(standartTurret);
    }

    public void SelectMissileLauncher()
    {
        Debug.Log("Missile Launcher purchase");
        buildManager.SelectTurretToBuild(missileLauncher);
    }

    public void SelectLaserBeamer()
    {
        Debug.Log("Laser Beamer purchase");
        buildManager.SelectTurretToBuild(laserBeamer);
    }
}
