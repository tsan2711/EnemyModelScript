
public interface ITriggerCheckable
{
    bool IsAggroed { get; set; }
    bool IsInAttackRange { get; set; }

    void SetAggroed(bool isAggroed);
    void SetInAttackRange(bool isInAttackRange);
}
