using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    private Node target;

    [SerializeField] private TMP_Text upgradeAmount;
    [SerializeField] private TMP_Text sellAmount;

    [SerializeField] private Button upgradeButton;

    public void SetTarget(Node targetNode)
    {
        target = targetNode;

        transform.position = target.GetBuildPosition();


        if (!target.isUpgraded)
        {
            upgradeAmount.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeAmount.text = "MAX UPGRADED";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();

        ui.SetActive(true);
    }

    public void Hide() => ui.SetActive(false);

    //Funcao para chamar o upgrade da turret
    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    //Funcao para chamar a venda a turret
    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}