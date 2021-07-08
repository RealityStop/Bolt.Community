﻿using Unity.VisualScripting;

namespace Bolt.Addons.Community.Generation
{
    [UnitGenerator(typeof(ExclusiveOr))]
    public sealed class ExclusiveOrGenerator : UnitGenerator<ExclusiveOr>
    {
        public ExclusiveOrGenerator(ExclusiveOr unit) : base(unit)
        {
        }

        public override string GenerateValue(ValueInput input)
        {
            if (input == Unit.a)
            {
                if (Unit.a.hasAnyConnection)
                {
                    return (Unit.a.connection.source.unit as Unit).GenerateValue(Unit.a.connection.source);
                }
            }

            if (input == Unit.b)
            {
                if (Unit.b.hasAnyConnection)
                {
                    return (Unit.b.connection.source.unit as Unit).GenerateValue(Unit.b.connection.source);
                }
            }

            return base.GenerateValue(input);
        }

        public override string GenerateValue(ValueOutput output)
        {
            if (output == Unit.result)
            {
                return "(" + GenerateValue(Unit.a) + " ^ " + GenerateValue(Unit.b) + ")";
            }

            return base.GenerateValue(output);
        }
    }
}