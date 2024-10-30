[System.Serializable]

public class EnemyHp
{
    public float _hp;
    public float _maxHp;

    //constructor
    public EnemyHp(float initialHp, float maxHp)
    {
        _hp = initialHp;
        _maxHp = maxHp;
    }
}
