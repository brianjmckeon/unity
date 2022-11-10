using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravel : Ground
{
    protected override int productionRate => 2;

    protected override int productionLimit => 3;

    protected override int dormantPeriod => 10;
}
