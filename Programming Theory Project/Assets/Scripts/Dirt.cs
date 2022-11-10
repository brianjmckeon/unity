using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : Ground
{
    protected override int productionRate => 3;

    protected override int productionLimit => 3;

    protected override int dormantPeriod => 5;
}
