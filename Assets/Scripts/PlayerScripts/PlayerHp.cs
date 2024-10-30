[System.Serializable]
public class PlayerHp 
{
    public float _hp;
    public float _maxHp;

    //constructor
    public PlayerHp(float initialHp, float maxHp)
    {
        _hp = initialHp;
        _maxHp = maxHp;
    }
    
}
