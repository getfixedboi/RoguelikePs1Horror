using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerStatictics : MonoBehaviour
{
    [Range(1,200)] public int maxHP;
    #region items parameters
    public static int currentHp;
    public  int baseDamage;
    public static int bonusDamage;
    public  int baseAmmo;
    public static int bonusAmmo;
    public float baseSpeed;
    public static float speedBonus;
    #endregion
    public Weapons currentWeapon;
    public Text hpText;

    #region items
    
    #endregion
    public void Awake()
    {
        currentHp = maxHP;
        hpText.text = $"HP: {currentHp} ";
    }
    public void SetDamage()
    {

    }
    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        hpText.text = $"HP: {currentHp} ";
        if (currentHp <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
