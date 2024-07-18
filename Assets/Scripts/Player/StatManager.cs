using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stage;
    [SerializeField] private TextMeshProUGUI stat;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateStat();
        UpdateStage();
    }
    private void UpdateStat()
    {
        stat.text =
            "Weapon : <color=\"red\">" + DataManager.Instance.Weapon + "</color>\n" +
            "Special Weapon : <color=\"red\">" + (DataManager.Instance.SpecialWeapon == null ? "None" : DataManager.Instance.SpecialWeapon) + "</color>\n" +
            "Atk Damage : <color=\"red\">" + DataManager.Instance.firstDamage + " (+" + (DataManager.Instance.additionalDamage).ToString("0.0") + ")</color>\n" +
            "Atk Speed : <color=\"red\">" + DataManager.Instance.firstAttackSpeed + " (-" + (DataManager.Instance.additionalAttackSpeed).ToString("0.00") + ")</color>\n" +
            "Max HP : <color=\"green\">" + DataManager.Instance.firstMaxHealth + " (+" + (DataManager.Instance.additionalMaxHealth).ToString("0") + ")</color>\n" +
            "Speed : <color=\"green\">" + DataManager.Instance.firstSpeed + " (+" + (DataManager.Instance.additionalSpeed).ToString("0.0") + ")</color>\n" +
            "DashCount : <color=\"green\">" + DataManager.Instance.firstDashCount + " (+" + (DataManager.Instance.additionalDashCount).ToString("0") + ")</color>\n";
    }
    private void UpdateStage()
    {
        stage.text = "[STAGE - " + DataManager.Instance.StageLevel + "]";
    }
}
