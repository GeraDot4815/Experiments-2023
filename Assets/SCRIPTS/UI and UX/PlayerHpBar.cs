using UnityEngine;
using UnityEngine.UI;
public class PlayerHpBar : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    private Player player;
    private void Start()
    {
        player = Player.Instance;
        player.onChangeHP += BarUpdate;
    }
    private void BarUpdate(float factHP, float maxHP)//Эту хрень я изобрел еще в 2021
    {
        float fill = 1000 / maxHP * factHP;
        hpBar.fillAmount = fill / 1000;
    }
}
