using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Ground
{
    protected override int productionRate => 1;

    protected override int productionLimit => 5;

    protected override int dormantPeriod => 8;
}
